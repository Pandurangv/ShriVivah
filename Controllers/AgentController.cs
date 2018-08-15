using ShriVivah.Models;
using ShriVivah.Models.ContextModel;
using ShriVivah.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShriVivah.Models.Filters;

namespace ShriVivah.Controllers
{
    public class AgentController : Controller
    {
        UserContextModel objUser = null;
        public int PageSize { get { return 5; } }
        public int CurentPageIndex { get; set; }
        public AgentController()
        { 
            objUser= new UserContextModel();
        }

        // GET: Agent
        [MyAuthorizeAttribute(IsAdmin = true)]
        [CustomViewAttribute]
        public ActionResult Index()
        {
            this.LoadIsAdmin();
            var countries = objUser.GetAgentDetails();//.Where(p =>p.UserType.ToUpper()=="AGENT");
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.UserId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
          //  string ViewName = SettingsManager.Instance.Branding == "SINDHI" ? "IndexSPMO" : "Index";
            return View("Index", filter);
        }

        [CustomViewAttribute]
        public ActionResult Panchayat()
        {
            this.loadViewBag();
            return View("Panchayat");
        }

        public ActionResult ActivateDeactivate(int UserId, bool IsActive)
        {
            objUser.SetActive(UserId, IsActive,true);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getpanchayatlist()
        {
            var countries = objUser.GetAgentDetails().Where(p =>p.IsActive==true);
            UserDetails obj = new UserDetails()
            {
                Status = true,
                UserList = countries
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AgentFirst()
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

        
        public ActionResult Search(string prefix)
        {
            var countries = objUser.GetAgentDetails().Where(p => p.FirstName.Contains(prefix) || p.LName.Contains(prefix) || p.Address.Contains(prefix) || p.PanchayatCode.Contains(prefix) || p.MobileNo.Contains(prefix));
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
            var countries = objUser.GetAgentDetails();//.Where(p => p.UserType.ToUpper() == "AGENT").ToList();
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.UserId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            return Json(filter, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AgentNext()
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
                return AgentFirst();
            }
        }

        public ActionResult AgentPrev()
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
                        ErrorMessage = SettingsManager.Instance.Branding == "SINDHI" ? Resources.SPMOResources.FirstPage : "तुम्ही पहिल्याच पानावर आहात",
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return AgentFirst();
            }
        }

        public ActionResult AgentLast()
        {
            var users = objUser.GetAgentDetails();//.Where(p => p.UserType.ToUpper() == "AGENT"); ;
            UserDetails obj = new UserDetails();
            int pageindex = Convert.ToInt32(Session["pageindex"]);
            pageindex++;
            obj.Status = true;
            if (users != null)
            {
                Session["pageindex"] = pageindex;
                if ((users.Count() % PageSize) == 0)
                {
                    obj.UserList = users.OrderBy(p => p.UserId).Skip(users.Count() - PageSize).Take(PageSize);
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
                return AgentFirst();
            }
        }
        [HttpPost]
        public ActionResult AddAgent(RegisterViewModel model)
        {
            var countries = objUser.Select_STP_GetUserDetails();
            UserDetails obj = new UserDetails();
            obj.Status = true;
            try
            {
                var checkexist=countries.Where(p => p.MobileNo.ToUpper()==model.MobileNo.ToUpper());
                obj.ErrorMessage = SettingsManager.Instance.Branding == "SINDHI" ? Resources.SPMOResources.AlreadyExist : "माहिती सेव केली आहे.";
                if (checkexist.Count()==0)
                {
                    model.IsActive = true;
                    model.UserType = "Agent";
                    obj.ErrorMessage = SettingsManager.Instance.Branding == "SINDHI" ? Resources.SPMOResources.InformationSave : "माहिती सेव केली आहे.";
                    objUser.SaveAgent(model);
                }
                else
                {
                    obj.Status = false;
                    obj.ErrorMessage = "Another user registerd with same mobile number.";
                }
                int pageindex = 0;
                var filter = objUser.GetAgentDetails().OrderBy(p => p.UserId).Skip(pageindex * PageSize).Take(PageSize);
                Session["users"] = countries;
                Session["pageindex"] = 0;
                obj.UserList = filter;
            }
            catch (Exception)
            {
                obj.Status = false;
                obj.ErrorMessage = SettingsManager.Instance.Branding == "SINDHI" ? Resources.SPMOResources.FailedToSave : "माहिती सेव करू शकत नाही";
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult Update(RegisterViewModel model)
        {
            var countries = objUser.GetAgentDetails();//.Where(p => p.UserType.ToUpper() == "AGENT");
            //var test = countries.Where(p => p.Agent.ToUpper() == model.Agent.ToUpper()).FirstOrDefault();
            UserDetails obj = new UserDetails();
            obj.Status = true;
            obj.ErrorMessage = SettingsManager.Instance.Branding == "SINDHI" ? Resources.SPMOResources.InformationSave : "माहिती सेव केली आहे.";
            objUser.UpdateAgent(model);
            int pageindex = 0;
            var filter = objUser.GetAgentDetails().OrderBy(p => p.UserId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            obj.UserList = filter;
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int AgentId)
        {
            STP_GetUserDetail tbl = objUser.GetAgentDetails().Where(p =>p.UserType.ToUpper()=="AGENT" && p.UserId == AgentId).FirstOrDefault();
            return Json(tbl, JsonRequestBehavior.AllowGet);
        }
    }
}