using ShriVivah.Models;
using ShriVivah.Models.ContextModel;
using ShriVivah.Models.DataModels;
using ShriVivah.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShriVivah.Models.Filters;

namespace ShriVivah.Controllers
{
    public class CastController : Controller
    {
        public CastContextModel objCast { get; set; }
        
        public int PageSize { get { return 5; } }
        public int CurentPageIndex { get; set; }

        public CastController()
        {
            objCast = new CastContextModel();
        }
        // GET: Cast
        [MyAuthorizeAttribute(IsAdmin = true)]
        [CustomViewAttribute]
        public ActionResult Index()
        {
            this.LoadIsAdmin();
            ReligionModel objReligion = new ReligionModel();
            var lst = objReligion.GetReligions();
            List<SelectListItem> lstData = (from tbl in lst
                                            select new SelectListItem { Text = tbl.ReligionName, Value = tbl.ReligionId.ToString() }).ToList();

            lstData.Insert(0, new SelectListItem() { Text = SettingsManager.Instance.Branding == "SINDHI" ? "---Select Religion---" : "---धर्म निवडा---", Value = "0" });
            ViewBag.ReligionId = lstData;

            
            var countries = objCast.GetCasts();
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.CastId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            return View("Index", filter);
        }

        public ActionResult GetCasts(int ReligionId)
        {
            var lst = objCast.GetCasts().Where(p=>p.ReligionId==ReligionId);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CastFirst()
        {
            IQueryable<CastModel> users = (IQueryable<CastModel>)Session["users"];
            int pageindex = 0;
            var filter = users.OrderBy(p => p.CastId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = users;
            Session["pageindex"] = 0;
            CastDetails obj = new CastDetails()
            {
                Status = true,
                CastList = filter
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        

        public ActionResult Search(string prefix)
        {
            var countries = objCast.GetCasts().Where(p => p.CastName.ToUpper() == prefix.ToUpper());
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.CastId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            if (filter.Count() > 0)
            {
                CastDetails obj = new CastDetails()
                {
                    Status = true,
                    CastList = filter
                };
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            else
            {
                CastDetails obj = new CastDetails()
                {
                    Status = false,
                    ErrorMessage = SettingsManager.Instance.Branding == "SINDHI" ? Resources.SPMOResources.InvalidUserNamePassword : "आणखी माहिती उपलब्ध नाही."
                };
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Reset()
        {
            var countries = objCast.GetCasts();
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.CastId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            return Json(filter, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CastNext()
        {
            IQueryable<CastModel> users = (IQueryable<CastModel>)Session["users"];
            if (users != null)
            {
                int pageindex = Convert.ToInt32(Session["pageindex"]);
                pageindex++;
                var filter = users.OrderBy(p => p.CastId).Skip(pageindex * PageSize).Take(PageSize);
                if (filter.Count() > 0)
                {
                    Session["pageindex"] = pageindex;
                    CastDetails obj = new CastDetails()
                    {
                        Status = true,
                        ErrorMessage = "",
                        CastList = filter
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    CastDetails obj = new CastDetails()
                    {
                        Status = false,
                        ErrorMessage = SettingsManager.Instance.Branding == "SINDHI" ? Resources.SPMOResources.NoMoreInformationAvail : "आणखी माहिती उपलब्ध नाही"
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return CastFirst();
            }
        }

        public ActionResult CastPrev()
        {
            IQueryable<CastModel> users = (IQueryable<CastModel>)Session["users"];

            if (users != null)
            {
                int pageindex = Convert.ToInt32(Session["pageindex"]);
                if (pageindex > 0)
                {
                    pageindex--;
                    var filter = users.OrderBy(p => p.CastId).Skip(pageindex * PageSize).Take(PageSize);
                    Session["pageindex"] = pageindex;
                    CastDetails obj = new CastDetails()
                    {
                        Status = true,
                        ErrorMessage = "",
                        CastList = filter,
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    CastDetails obj = new CastDetails()
                    {
                        Status = false,
                        ErrorMessage = SettingsManager.Instance.Branding == "SINDHI" ? Resources.SPMOResources.FirstPage : "तुम्ही पहिल्याच पानावर आहात",
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return CastFirst();
            }
        }

        public ActionResult CastLast()
        {
            var users = objCast.GetCasts();
            CastDetails obj = new CastDetails();
            int pageindex = Convert.ToInt32(Session["pageindex"]);
            pageindex++;
            obj.Status = true;
            if (users != null)
            {
                Session["pageindex"] = pageindex;
                if ((users.Count() % PageSize) == 0)
                {
                    obj.CastList = users.OrderBy(p => p.CastId).Skip(users.Count() - 2).Take(PageSize);
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    int rem = users.Count() % PageSize;
                    obj.CastList = users.OrderBy(p => p.CastId).Skip(users.Count() - rem).Take(PageSize);
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return CastFirst();
            }
        }

        public ActionResult AddCast(tblCast model)
        {
            var countries = objCast.GetCasts();
            var test = countries.Where(p => p.CastName.ToUpper() == model.CastName.ToUpper() && p.ReligionId == model.ReligionId).FirstOrDefault();
            CastDetails obj = new CastDetails();
            if (test != null)
            {
                obj.Status = false;
                obj.ErrorMessage = SettingsManager.Instance.Branding == "SINDHI" ? Resources.SPMOResources.AlreadyExist : "हि माहिती आधीपासून उपलब्ध आहे.";
            }
            else
            {
                obj.Status = true;
                obj.ErrorMessage = SettingsManager.Instance.Branding == "SINDHI" ? Resources.SPMOResources.InformationSave : "माहिती सेव केली आहे.";
                objCast.Save(model);
            }
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.CastId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            obj.CastList = filter;
            return Json(obj, JsonRequestBehavior.AllowGet);
        }



        public ActionResult Update(tblCast model)
        {
            var countries = objCast.GetCasts();
            var test = countries.Where(p => p.CastName.ToUpper() == model.CastName.ToUpper() && p.ReligionId == model.ReligionId).FirstOrDefault();
            CastDetails obj = new CastDetails();
            if (test != null)
            {
                obj.Status = false;
                obj.ErrorMessage = SettingsManager.Instance.Branding == "SINDHI" ? Resources.SPMOResources.AlreadyExist : "हि माहिती आधीपासून उपलब्ध आहे.";
            }
            else
            {
                obj.Status = true;
                obj.ErrorMessage = SettingsManager.Instance.Branding == "SINDHI" ? Resources.SPMOResources.InformationSave : "माहिती सेव केली आहे.";
                objCast.Update(model);
            }
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.CastId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            obj.CastList = filter;
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int BGId)
        {
            CastModel tbl = objCast.GetCasts().Where(p => p.CastId == BGId).FirstOrDefault();
            return Json(tbl, JsonRequestBehavior.AllowGet);
        }
    }
}