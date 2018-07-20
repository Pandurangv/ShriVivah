using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShriVivah.Models;
using ShriVivah.Models.DataModels;
using ShriVivah.Models.ContextModel;
using ShriVivah.Models.Entities;
using System.Configuration;
using ShriVivah.Models.Filters;
namespace ShriVivah.Controllers
{
    public class PendingUsersController : Controller
    {
        // GET: PendingUsers
        UserContextModel objUser = new UserContextModel();
        public int PageSize { get { return 10; } }
        public int CurentPageIndex { get; set; }

        [MyAuthorizeAttribute(IsAdmin = true,IsAgent =true)]
        [CustomView]
        public ActionResult Index()
        {
            this.LoadIsAdmin();
            //ViewBag.SMSAPI= ConfigurationManager.ConnectionStrings["MSG91"] != null ? ConfigurationManager.ConnectionStrings["MSG91"].ConnectionString : "";
            var countries = objUser.Select_STP_GetUserDetails().Where(p=> p.IsActive==false && p.UserType.ToUpper()=="USER");
            if (SessionManager.GetInstance.ActiveUser.UserType=="Agent")
            {
                countries = countries.Where(p => p.PanchayatId == SessionManager.GetInstance.ActiveUser.UserId);
            }
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.UserId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            return View("Index", filter);
        }

        public ActionResult Search(string prefix)
        {
            var countries = objUser.Select_STP_GetUserDetails().Where(p => p.IsActive == false && p.UserType.ToUpper() == "USER" && (p.FirstName.ToUpper() == prefix.ToUpper() || p.MName.ToUpper() == prefix.ToUpper() || p.LName.ToUpper() == prefix.ToUpper()));
            if (SessionManager.GetInstance.ActiveUser.UserType == "Agent")
            {
                countries = countries.Where(p => p.PanchayatId == SessionManager.GetInstance.ActiveUser.UserId);
            }
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.UserId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            if (filter.Count() > 0)
            {
                UserDetails obj = new UserDetails()
                {
                    Status = true,
                    UserList = filter
                };
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            else
            {
                UserDetails obj = new UserDetails()
                {
                    Status = false,
                    ErrorMessage = SettingsManager.Instance.Branding == "SPMO" ? Resources.SPMOResources.NoMoreInformationAvail : "आणखी माहिती उपलब्ध नाही"
                };
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ActivateUser(int UserId)
        {
            objUser.SetActive(UserId, true);
            var countries = objUser.Select_STP_GetUserDetails().Where(p => p.IsActive == false && p.UserType.ToUpper() == "USER");
            if (SessionManager.GetInstance.ActiveUser.UserType == "Agent")
            {
                countries = countries.Where(p => p.PanchayatId == SessionManager.GetInstance.ActiveUser.UserId);
            }
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.UserId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            return Json(filter, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Reset()
        {
            var countries = objUser.Select_STP_GetUserDetails().Where(p => p.IsActive == false && p.UserType.ToUpper() == "USER");
            if (SessionManager.GetInstance.ActiveUser.UserType == "Agent")
            {
                countries = countries.Where(p => p.PanchayatId == SessionManager.GetInstance.ActiveUser.UserId);
            }
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.UserId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            return Json(filter, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SendMail(int? UserId)
        {
            var user= objUser.Select_STP_GetUserDetails().Where(p => p.UserId == UserId).FirstOrDefault();
            SettingsHelper.SendMail(user.MailId,user.FirstName,false,user.UserName,user.Password);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SendSMS(int? UserId)
        {
            string smsurl = ConfigurationManager.ConnectionStrings["MSG91"] != null ? ConfigurationManager.ConnectionStrings["MSG91"].ConnectionString : "";
            if (!string.IsNullOrEmpty(smsurl))
            {
                STP_GetUserDetail user = objUser.Select_STP_GetUserDetails().Where(p => p.UserId == UserId).FirstOrDefault();
                return Json(objUser.SendSMS(user));
            }
            else
            {
                return Json("URL Blank");
            }
        }

        public ActionResult PendingUsersFirst()
        {
            IQueryable<STP_GetUserDetail> users = (IQueryable<STP_GetUserDetail>)Session["users"];
            
            int pageindex = 0;
            var filter = users.OrderBy(p => p.UserId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = users;
            Session["pageindex"] = 0;
            UserDetails obj = new UserDetails()
            {
                Status = true,
                UserList = filter
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PendingUsersNext()
        {
            IQueryable<STP_GetUserDetail> users = (IQueryable<STP_GetUserDetail>)Session["users"];
            if (users != null)
            {
                int pageindex = Convert.ToInt32(Session["pageindex"]);
                pageindex++;
                var filter = users.OrderBy(p => p.UserId).Skip(pageindex * PageSize).Take(PageSize);
                if (filter.Count() > 0)
                {
                    Session["pageindex"] = pageindex;
                    UserDetails obj = new UserDetails()
                    {
                        Status = true,
                        ErrorMessage = "",
                        UserList = filter
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    UserDetails obj = new UserDetails()
                    {
                        Status = false,
                        ErrorMessage = SettingsManager.Instance.Branding == "SPMO" ? Resources.SPMOResources.NoMoreInformationAvail : "आणखी माहिती उपलब्ध नाही"
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return PendingUsersFirst();
            }
        }

        public ActionResult PendingUsersPrev()
        {
            IQueryable<STP_GetUserDetail> users = (IQueryable<STP_GetUserDetail>)Session["users"];

            if (users != null)
            {
                int pageindex = Convert.ToInt32(Session["pageindex"]);
                if (pageindex > 0)
                {
                    pageindex--;
                    var filter = users.OrderBy(p => p.UserId).Skip(pageindex * PageSize).Take(PageSize);
                    Session["pageindex"] = pageindex;
                    UserDetails obj = new UserDetails()
                    {
                        Status = true,
                        ErrorMessage = "",
                        UserList = filter,
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    UserDetails obj = new UserDetails()
                    {
                        Status = false,
                        ErrorMessage = SettingsManager.Instance.Branding == "SPMO" ? Resources.SPMOResources.FirstPage : "तुम्ही पहिल्याच पानावर आहात.",
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return PendingUsersFirst();
            }
        }

        public ActionResult PendingUsersLast()
        {
            var users = objUser.Select_STP_GetUserDetails().Where(p => p.IsActive == false && p.UserType.ToUpper() == "USER");

            if (SessionManager.GetInstance.ActiveUser.UserType == "Agent")
            {
                users = users.Where(p => p.PanchayatId == SessionManager.GetInstance.ActiveUser.UserId);
            }
            UserDetails obj = new UserDetails();
            int pageindex = Convert.ToInt32(Session["pageindex"]);
            pageindex++;
            obj.Status = true;
            if (users != null)
            {
                Session["pageindex"] = pageindex;
                if ((users.Count() % PageSize) == 0)
                {
                    obj.UserList = users.OrderBy(p => p.UserId).Skip(users.Count() - 2).Take(PageSize);
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    int rem = users.Count() % PageSize;
                    obj.UserList = users.OrderBy(p => p.UserId).Skip(users.Count() - rem).Take(PageSize);
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return PendingUsersFirst();
            }
        }
    }
}