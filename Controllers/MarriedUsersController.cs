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
    public class MarriedUsersController : Controller
    {
        UserContextModel objUser = new UserContextModel();
        public int PageSize { get { return 10; } }
        public int CurentPageIndex { get; set; }

        [MyAuthorizeAttribute(IsAdmin = true, IsAgent = true)]
        [CustomView]
        public ActionResult Index()
        {
            this.LoadIsAdmin();
            //ViewBag.SMSAPI= ConfigurationManager.ConnectionStrings["MSG91"] != null ? ConfigurationManager.ConnectionStrings["MSG91"].ConnectionString : "";
            var countries = objUser.Select_STP_GetUserDetails().Where(p => p.ismarried == 1 && p.UserType.ToUpper() == "USER");
            
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.UserId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            return View("Index", filter);
        }

        public ActionResult MarriedUserList()
        {
            var countries = objUser.Select_STP_GetUserDetails().Where(p => p.ismarried == 1 && p.UserType.ToUpper() == "USER");
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.UserId).Skip(pageindex * PageSize).Take(PageSize);
            foreach (var item in filter)
            {
                if (string.IsNullOrEmpty(item.Img1))
                {
                    if (item.Gender=="M")
                    {
                        item.Img1 = SettingsManager.Instance.SourcePathM;
                    }
                    else
                    {
                        item.Img1 = SettingsManager.Instance.SourcePathF;
                    }
                }
                else
                {
                    item.Img1 = SettingsManager.Instance.SourcePath + item.Img1;
                }
            }

            return Json(filter,JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(string prefix)
        {
            var countries = objUser.Select_STP_GetUserDetails().Where(p => p.ismarried == 1 && p.UserType.ToUpper() == "USER" && (p.FirstName.ToUpper() == prefix.ToUpper() || p.MName.ToUpper() == prefix.ToUpper() || p.LName.ToUpper() == prefix.ToUpper()));
            
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
                    ErrorMessage = SettingsManager.Instance.Branding == "SINDHI" ? Resources.SPMOResources.NoMoreInformationAvail : "आणखी माहिती उपलब्ध नाही"
                };
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult Reset()
        {
            var countries = objUser.Select_STP_GetUserDetails().Where(p => p.ismarried == 1 && p.UserType.ToUpper() == "USER");
            
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.UserId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            return Json(filter, JsonRequestBehavior.AllowGet);
        }


        public ActionResult MarriedUsersFirst()
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

        public ActionResult MarriedUsersNext()
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
                        ErrorMessage = SettingsManager.Instance.Branding == "SINDHI" ? Resources.SPMOResources.NoMoreInformationAvail : "आणखी माहिती उपलब्ध नाही"
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return MarriedUsersFirst();
            }
        }

        public ActionResult MarriedUsersPrev()
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
                        ErrorMessage = SettingsManager.Instance.Branding == "SINDHI" ? Resources.SPMOResources.FirstPage : "तुम्ही पहिल्याच पानावर आहात.",
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return MarriedUsersFirst();
            }
        }

        public ActionResult MarriedUsersLast()
        {
            var users = objUser.Select_STP_GetUserDetails().Where(p => p.ismarried == 1 && p.UserType.ToUpper() == "USER");
            
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
                return MarriedUsersFirst();
            }
        }
    }

}