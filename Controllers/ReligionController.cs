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
    public class ReligionController : Controller
    {
        public ReligionModel objReligion { get; set; }
        
        public int PageSize { get { return 5; } }
        public int CurentPageIndex { get; set; }

        public ReligionController()
        { 
             objReligion = new ReligionModel();
        }

        [MyAuthorizeAttribute(IsAdmin = true)]
        [CustomViewAttribute]
        public ActionResult Index()
        {
            this.LoadIsAdmin();
            var countries = objReligion.GetReligions();
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.ReligionId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            return View("Index", filter);
        }

        public ActionResult ReligionFirst()
        {
            IQueryable<tblReligion> users = (IQueryable<tblReligion>)Session["users"];
            int pageindex = 0;
            var filter = users.OrderBy(p => p.ReligionId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = users;
            Session["pageindex"] = 0;
            ReligionDetails obj = new ReligionDetails()
            {
                Status = true,
                ReligionList = filter
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(string prefix)
        {
            var countries = objReligion.GetReligions().Where(p => p.ReligionName.ToUpper() == prefix.ToUpper());
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.ReligionId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            if (filter.Count() > 0)
            {
                ReligionDetails obj = new ReligionDetails()
                {
                    Status = true,
                    ReligionList = filter
                };
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            else
            {
                ReligionDetails obj = new ReligionDetails()
                {
                    Status = false,
                    ErrorMessage = SettingsManager.Instance.Branding == "SINDHI" ? Resources.SPMOResources.NoRecords : "No Records to display"
                };
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Reset()
        {
            var countries = objReligion.GetReligions();
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.ReligionId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            return Json(filter, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReligionNext()
        {
            IQueryable<tblReligion> users = (IQueryable<tblReligion>)Session["users"];
            if (users != null)
            {
                int pageindex = Convert.ToInt32(Session["pageindex"]);
                pageindex++;
                var filter = users.OrderBy(p => p.ReligionId).Skip(pageindex * PageSize).Take(PageSize);
                if (filter.Count() > 0)
                {
                    Session["pageindex"] = pageindex;
                    ReligionDetails obj = new ReligionDetails()
                    {
                        Status = true,
                        ErrorMessage = "",
                        ReligionList = filter
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                    //return Json(filter, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    ReligionDetails obj = new ReligionDetails()
                    {
                        Status = false,
                        ErrorMessage = SettingsManager.Instance.Branding == "SINDHI" ? Resources.SPMOResources.NoMoreInformationAvail : "आणखी माहिती उपलब्ध नाही"
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return ReligionFirst();
            }
        }

        public ActionResult ReligionPrev()
        {
            IQueryable<tblReligion> users = (IQueryable<tblReligion>)Session["users"];

            if (users != null)
            {
                int pageindex = Convert.ToInt32(Session["pageindex"]);
                if (pageindex > 0)
                {
                    pageindex--;
                    var filter = users.OrderBy(p => p.ReligionId).Skip(pageindex * PageSize).Take(PageSize);
                    Session["pageindex"] = pageindex;
                    ReligionDetails obj = new ReligionDetails()
                    {
                        Status = true,
                        ErrorMessage = "",
                        ReligionList = filter,
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    ReligionDetails obj = new ReligionDetails()
                    {
                        Status = false,
                        ErrorMessage = SettingsManager.Instance.Branding == "SINDHI" ? Resources.SPMOResources.FirstPage : "You are already on first page",
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return ReligionFirst();
            }
        }

        public ActionResult ReligionLast()
        {
            var users = objReligion.GetReligions();
            ReligionDetails obj = new ReligionDetails();
            int pageindex = Convert.ToInt32(Session["pageindex"]);
            pageindex++;
            obj.Status = true;
            if (users != null)
            {
                Session["pageindex"] = pageindex;
                if ((users.Count() % PageSize) == 0)
                {
                    obj.ReligionList = users.OrderBy(p => p.ReligionId).Skip(users.Count() - 2).Take(PageSize);
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    int rem = users.Count() % PageSize;
                    obj.ReligionList = users.OrderBy(p => p.ReligionId).Skip(users.Count() - rem).Take(PageSize);
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return ReligionFirst();
            }
        }

        public ActionResult AddReligion(tblReligion model)
        {
            var countries = objReligion.GetReligions();
            var test = countries.Where(p => p.ReligionName.ToUpper() == model.ReligionName.ToUpper()).FirstOrDefault();
            ReligionDetails obj = new ReligionDetails();
            if (test != null)
            {
                obj.Status = false;
                obj.ErrorMessage = SettingsManager.Instance.Branding == "SINDHI" ? Resources.SPMOResources.AlreadyExist : "Record already exist.";
            }
            else
            {
                obj.Status = true;
                obj.ErrorMessage = SettingsManager.Instance.Branding == "SINDHI" ? Resources.SPMOResources.InformationSave : "Record saved successfully.";
                objReligion.Save(model);
            }
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.ReligionId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            obj.ReligionList = filter;
            return Json(obj, JsonRequestBehavior.AllowGet);
        }



        public ActionResult Update(tblReligion model)
        {
            var countries = objReligion.GetReligions();
            var test = countries.Where(p => p.ReligionName.ToUpper() == model.ReligionName.ToUpper()).FirstOrDefault();
            ReligionDetails obj = new ReligionDetails();
            if (test != null)
            {
                obj.Status = false;
                obj.ErrorMessage = SettingsManager.Instance.Branding == "SINDHI" ? Resources.SPMOResources.AlreadyExist : "Record already exist.";
            }
            else
            {
                obj.Status = true;
                obj.ErrorMessage = SettingsManager.Instance.Branding == "SINDHI" ? Resources.SPMOResources.UpdateSuccess : "Record updated successfully.";
                objReligion.Update(model);
            }
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.ReligionId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            obj.ReligionList = filter;
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int BGId)
        {
            tblReligion tbl = objReligion.GetReligions().Where(p => p.ReligionId == BGId).FirstOrDefault();
            return Json(tbl, JsonRequestBehavior.AllowGet);
        }
    }
}