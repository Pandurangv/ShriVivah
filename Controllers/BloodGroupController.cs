using ShriVivah.Models;
using ShriVivah.Models.ContextModel;
using ShriVivah.Models.DataModels;
using ShriVivah.Models.Entities;
using ShriVivah.Models.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ShriVivah.Controllers
{
    public class BloodGroupController : Controller
    {
        public BloodGroupController()
        {
            BloodGroup = new BloodGroupModel();
        }

        BloodGroupModel BloodGroup = null;
        public int PageSize { get { return 5; } }
        public int CurentPageIndex { get; set; }
        // GET: BloodGroup

        [MyAuthorizeAttribute(IsAdmin=true)]
        [CustomView]
        public ActionResult Index()
        {
            this.LoadIsAdmin();
            var countries = BloodGroup.GetBloodGroups();
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.BloodGroupId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            return View("Index", filter);
        }

        public ActionResult BGFirst()
        {
            IQueryable<tblBloodGroup> users = (IQueryable<tblBloodGroup>)Session["users"];
            int pageindex = 0;
            var filter = users.OrderBy(p => p.BloodGroupId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = users;
            Session["pageindex"] = 0;
            BGDetails obj = new BGDetails()
            {
                Status = true,
                BGList = filter
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(string prefix)
        {
            var countries = BloodGroup.GetBloodGroups().Where(p => p.BloodGroupName.ToUpper() == prefix.ToUpper());
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.BloodGroupId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            if (filter.Count() > 0)
            {
                BGDetails obj = new BGDetails()
                {
                    Status = true,
                    BGList = filter
                };
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            else
            {
                BGDetails obj = new BGDetails()
                {
                    Status = false,
                    ErrorMessage = SettingsManager.Instance.Branding == "SPMO" ? Resources.SPMOResources.InformationSave : "माहिती सेव केली आहे."
                };
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Reset()
        {
            var countries = BloodGroup.GetBloodGroups();
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.BloodGroupId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            return Json(filter, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BGNext()
        {
            IQueryable<tblBloodGroup> users = (IQueryable<tblBloodGroup>)Session["users"];
            if (users != null)
            {
                int pageindex = Convert.ToInt32(Session["pageindex"]);
                pageindex++;
                var filter = users.OrderBy(p => p.BloodGroupId).Skip(pageindex * PageSize).Take(PageSize);
                if (filter.Count() > 0)
                {
                    Session["pageindex"] = pageindex;
                    BGDetails obj = new BGDetails()
                    {
                        Status = true,
                        ErrorMessage = "",
                        BGList = filter
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                    //return Json(filter, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    BGDetails obj = new BGDetails()
                    {
                        Status = false,
                        ErrorMessage = SettingsManager.Instance.Branding == "SPMO" ? Resources.SPMOResources.NoMoreInformationAvail : "आणखी माहिती उपलब्ध नाही"
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return BGFirst();
            }
        }

        public ActionResult BGPrev()
        {
            IQueryable<tblBloodGroup> users = (IQueryable<tblBloodGroup>)Session["users"];
            
            if (users != null)
            {
                int pageindex = Convert.ToInt32(Session["pageindex"]);
                if (pageindex > 0)
                {
                    pageindex--;
                    var filter = users.OrderBy(p => p.BloodGroupId).Skip(pageindex * PageSize).Take(PageSize);
                    Session["pageindex"] = pageindex;
                    BGDetails obj = new BGDetails()
                    {
                        Status = true,
                        ErrorMessage = "",
                        BGList = filter,
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    BGDetails obj = new BGDetails()
                    {
                        Status = false,
                        ErrorMessage = SettingsManager.Instance.Branding == "SPMO" ? Resources.SPMOResources.InvalidUserNamePassword : "तुम्ही पहिल्याच पानावर आहात.",
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return BGFirst();
            }
        }

        public ActionResult BGLast()
        {
            var users = BloodGroup.GetBloodGroups();
            BGDetails obj = new BGDetails();
            int pageindex = Convert.ToInt32(Session["pageindex"]);
            pageindex++;
            obj.Status = true;
            if (users != null)
            {
                Session["pageindex"] = pageindex;
                if ((users.Count() % PageSize) == 0)
                {
                    obj.BGList=users.OrderBy(p => p.BloodGroupId).Skip(users.Count() - 2).Take(PageSize);
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    int rem = users.Count() % PageSize;
                    obj.BGList = users.OrderBy(p => p.BloodGroupId).Skip(users.Count() - rem).Take(PageSize);
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return BGFirst();
            }
        }

        public ActionResult AddBG(tblBloodGroup model)
        {
            var countries = BloodGroup.GetBloodGroups();
            var test = countries.Where(p => p.BloodGroupName.ToUpper() == model.BloodGroupName.ToUpper()).FirstOrDefault();
            BGDetails obj = new BGDetails();
            if (test != null)
            {
                obj.Status = false;
                obj.ErrorMessage = SettingsManager.Instance.Branding == "SPMO" ? Resources.SPMOResources.AlreadyExist : "हि माहिती आधीपासून उपलब्ध आहे.";
            }
            else
            {
                obj.Status = true;
                obj.ErrorMessage = SettingsManager.Instance.Branding == "SPMO" ? Resources.SPMOResources.InformationSave : "माहिती सेव केली आहे.";
                BloodGroup.Save(model);
            }
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.BloodGroupId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            obj.BGList = filter;
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        

        public ActionResult Update(tblBloodGroup model)
        {
            var countries = BloodGroup.GetBloodGroups();
            var test = countries.Where(p => p.BloodGroupName.ToUpper() == model.BloodGroupName.ToUpper()).FirstOrDefault();
            BGDetails obj = new BGDetails();
            if (test!=null)
            {
                obj.Status = false;
                obj.ErrorMessage = SettingsManager.Instance.Branding == "SPMO" ? Resources.SPMOResources.AlreadyExist : "हि माहिती आधीपासून उपलब्ध आहे.";
            }
            else
            {
                obj.Status = true;
                obj.ErrorMessage = SettingsManager.Instance.Branding == "SPMO" ? Resources.SPMOResources.InformationSave : "माहिती सेव केली आहे.";
                BloodGroup.Update(model);
            }
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.BloodGroupId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            obj.BGList = filter;
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int BGId)
        {
            tblBloodGroup tbl = BloodGroup.GetBloodGroups().Where(p => p.BloodGroupId == BGId).FirstOrDefault();
            return Json(tbl, JsonRequestBehavior.AllowGet);
        }
    }
}