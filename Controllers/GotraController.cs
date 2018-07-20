using ShriVivah.Models;
using ShriVivah.Models.ContextModel;
using ShriVivah.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShriVivah.Controllers
{
    public class GotraController : Controller
    {
        public GotraModel objGotra { get; set; }
        
        public int PageSize { get { return 5; } }
        public int CurentPageIndex { get; set; }

        public GotraController()
        {
            objGotra = new GotraModel();
        }
        // GET: Gan
        [MyAuthorizeAttribute(IsAdmin = true)]
        public ActionResult Index()
        {
            this.LoadIsAdmin();
            var countries = objGotra.GetGotras();
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.GotraId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            return View("Index", filter);
        }

        public ActionResult GotraFirst()
        {
            IQueryable<tblGotra> users = (IQueryable<tblGotra>)Session["users"];
            int pageindex = 0;
            var filter = users.OrderBy(p => p.GotraId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = users;
            Session["pageindex"] = 0;
            GotraDetails obj = new GotraDetails()
            {
                Status = true,
                GotraList = filter
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(string prefix)
        {
            var countries = objGotra.GetGotras().Where(p => p.GotraName.ToUpper() == prefix.ToUpper());
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.GotraId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            if (filter.Count() > 0)
            {
                GotraDetails obj = new GotraDetails()
                {
                    Status = true,
                    GotraList = filter
                };
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            else
            {
                GotraDetails obj = new GotraDetails()
                {
                    Status = false,
                    ErrorMessage = SettingsManager.Instance.Branding == "SPMO" ? Resources.SPMOResources.NoRecords : "No Records to display"
                };
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Reset()
        {
            var countries = objGotra.GetGotras();
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.GotraId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            return Json(filter, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GotraNext()
        {
            IQueryable<tblGotra> users = (IQueryable<tblGotra>)Session["users"];
            if (users != null)
            {
                int pageindex = Convert.ToInt32(Session["pageindex"]);
                pageindex++;
                var filter = users.OrderBy(p => p.GotraId).Skip(pageindex * PageSize).Take(PageSize);
                if (filter.Count() > 0)
                {
                    Session["pageindex"] = pageindex;
                    GotraDetails obj = new GotraDetails()
                    {
                        Status = true,
                        ErrorMessage = "",
                        GotraList = filter
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                    //return Json(filter, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    GotraDetails obj = new GotraDetails()
                    {
                        Status = false,
                        ErrorMessage = SettingsManager.Instance.Branding == "SPMO" ? Resources.SPMOResources.NoMoreInformationAvail : "आणखी माहिती उपलब्ध नाही"
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return GotraFirst();
            }
        }

        public ActionResult GotraPrev()
        {
            IQueryable<tblGotra> users = (IQueryable<tblGotra>)Session["users"];

            if (users != null)
            {
                int pageindex = Convert.ToInt32(Session["pageindex"]);
                if (pageindex > 0)
                {
                    pageindex--;
                    var filter = users.OrderBy(p => p.GotraId).Skip(pageindex * PageSize).Take(PageSize);
                    Session["pageindex"] = pageindex;
                    GotraDetails obj = new GotraDetails()
                    {
                        Status = true,
                        ErrorMessage = "",
                        GotraList = filter,
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    GotraDetails obj = new GotraDetails()
                    {
                        Status = false,
                        ErrorMessage = SettingsManager.Instance.Branding == "SPMO" ? Resources.SPMOResources.FirstPage : "You are already on first page",
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return GotraFirst();
            }
        }

        public ActionResult GotraLast()
        {
            var users = objGotra.GetGotras();
            GotraDetails obj = new GotraDetails();
            int pageindex = Convert.ToInt32(Session["pageindex"]);
            pageindex++;
            obj.Status = true;
            if (users != null)
            {
                Session["pageindex"] = pageindex;
                if ((users.Count() % PageSize) == 0)
                {
                    obj.GotraList = users.OrderBy(p => p.GotraId).Skip(users.Count() - 2).Take(PageSize);
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    int rem = users.Count() % PageSize;
                    obj.GotraList = users.OrderBy(p => p.GotraId).Skip(users.Count() - rem).Take(PageSize);
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return GotraFirst();
            }
        }

        public ActionResult AddGotra(tblGotra model)
        {
            var countries = objGotra.GetGotras();
            var test = countries.Where(p => p.GotraName.ToUpper() == model.GotraName.ToUpper()).FirstOrDefault();
            GotraDetails obj = new GotraDetails();
            if (test != null)
            {
                obj.Status = false;
                obj.ErrorMessage = SettingsManager.Instance.Branding == "SPMO" ? Resources.SPMOResources.AlreadyExist : "Record already exist.";
            }
            else
            {
                obj.Status = true;
                obj.ErrorMessage = SettingsManager.Instance.Branding == "SPMO" ? Resources.SPMOResources.InformationSave : "Record saved successfully.";
                objGotra.Save(model);
            }
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.GotraId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            obj.GotraList = filter;
            return Json(obj, JsonRequestBehavior.AllowGet);
        }



        public ActionResult Update(tblGotra model)
        {
            var countries = objGotra.GetGotras();
            var test = countries.Where(p => p.GotraName.ToUpper() == model.GotraName.ToUpper()).FirstOrDefault();
            GotraDetails obj = new GotraDetails();
            if (test != null)
            {
                obj.Status = false;
                obj.ErrorMessage = SettingsManager.Instance.Branding == "SPMO" ? Resources.SPMOResources.AlreadyExist : "Record already exist.";
            }
            else
            {
                obj.Status = true;
                obj.ErrorMessage = SettingsManager.Instance.Branding == "SPMO" ? Resources.SPMOResources.UpdateSuccess : "Record updated successfully.";
                objGotra.Update(model);
            }
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.GotraId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            obj.GotraList = filter;
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int BGId)
        {
            tblGotra tbl = objGotra.GetGotras().Where(p => p.GotraId == BGId).FirstOrDefault();
            return Json(tbl, JsonRequestBehavior.AllowGet);
        }
    }
}