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
    public class StateController : Controller
    {
        public StateModel objState { get; set; }
        
        public int PageSize { get { return 5; } }
        public int CurentPageIndex { get; set; }

        public StateController()
        {
            objState = new StateModel();
        }

        // GET: State
        [MyAuthorizeAttribute(IsAdmin=true)]
        [CustomViewAttribute]
        public ActionResult Index()
        {
            this.LoadIsAdmin();
            CountryModel objCountry = new CountryModel();
            var lst = objCountry.GetCountrys();
            List<SelectListItem> lstData=(from tbl in lst
                                         select new SelectListItem
                                         {Text=tbl.CountryName,Value=tbl.CountryId.ToString()}).ToList();

            lstData.Insert(0, new SelectListItem() { Text = "---Select---", Value = "0" });
            ViewBag.CountryId = lstData;
            var countries = objState.GetStates();
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.StateId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            return View("Index", filter);
        }

        public ActionResult StateFirst()
        {
            IQueryable<StateContextModel> users = (IQueryable<StateContextModel>)Session["users"];
            int pageindex = 0;
            var filter = users.OrderBy(p => p.StateId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = users;
            Session["pageindex"] = 0;
            StateDetails obj = new StateDetails()
            {
                Status = true,
                StateList = filter
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetStatesByCountry(int CountryId)
        {
            var countries = objState.GetStates().Where(p => p.CountryId==CountryId);
            return Json(countries, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(string prefix)
        {
            var countries = objState.GetStates().Where(p => p.StateName.ToUpper() == prefix.ToUpper());
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.StateId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            if (filter.Count() > 0)
            {
                StateDetails obj = new StateDetails()
                {
                    Status = true,
                    StateList = filter
                };
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            else
            {
                StateDetails obj = new StateDetails()
                {
                    Status = false,
                    ErrorMessage = SettingsManager.Instance.Branding == "SINDHI" ? Resources.SPMOResources.NoMoreInformationAvail : "आणखी माहिती उपलब्ध नाही"
                };
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Reset()
        {
            var countries = objState.GetStates();
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.StateId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            return Json(filter, JsonRequestBehavior.AllowGet);
        }

        public ActionResult StateNext()
        {
            IQueryable<StateContextModel> users = (IQueryable<StateContextModel>)Session["users"];
            if (users != null)
            {
                int pageindex = Convert.ToInt32(Session["pageindex"]);
                pageindex++;
                var filter = users.OrderBy(p => p.StateId).Skip(pageindex * PageSize).Take(PageSize);
                if (filter.Count() > 0)
                {
                    Session["pageindex"] = pageindex;
                    StateDetails obj = new StateDetails()
                    {
                        Status = true,
                        ErrorMessage = "",
                        StateList = filter
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    StateDetails obj = new StateDetails()
                    {
                        Status = false,
                        ErrorMessage = SettingsManager.Instance.Branding == "SINDHI" ? Resources.SPMOResources.NoMoreInformationAvail : "आणखी माहिती उपलब्ध नाही"
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return StateFirst();
            }
        }

        public ActionResult StatePrev()
        {
            IQueryable<StateContextModel> users = (IQueryable<StateContextModel>)Session["users"];

            if (users != null)
            {
                int pageindex = Convert.ToInt32(Session["pageindex"]);
                if (pageindex > 0)
                {
                    pageindex--;
                    var filter = users.OrderBy(p => p.StateId).Skip(pageindex * PageSize).Take(PageSize);
                    Session["pageindex"] = pageindex;
                    StateDetails obj = new StateDetails()
                    {
                        Status = true,
                        ErrorMessage = "",
                        StateList = filter,
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    StateDetails obj = new StateDetails()
                    {
                        Status = false,
                        ErrorMessage = SettingsManager.Instance.Branding == "SINDHI" ? Resources.SPMOResources.FirstPage : "तुम्ही पहिल्याच पानावर आहात",
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return StateFirst();
            }
        }

        public ActionResult StateLast()
        {
            var users = objState.GetStates();
            StateDetails obj = new StateDetails();
            int pageindex = Convert.ToInt32(Session["pageindex"]);
            pageindex++;
            obj.Status = true;
            if (users != null)
            {
                Session["pageindex"] = pageindex;
                if ((users.Count() % PageSize) == 0)
                {
                    obj.StateList = users.OrderBy(p => p.StateId).Skip(users.Count() - 2).Take(PageSize);
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    int rem = users.Count() % PageSize;
                    obj.StateList = users.OrderBy(p => p.StateId).Skip(users.Count() - rem).Take(PageSize);
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return StateFirst();
            }
        }

        public ActionResult AddState(tblState model)
        {
            var countries = objState.GetStates();
            var test = countries.Where(p => p.StateName.ToUpper() == model.StateName.ToUpper() && p.CountryId==model.CountryId).FirstOrDefault();
            StateDetails obj = new StateDetails();
            if (test != null)
            {
                obj.Status = false;
                obj.ErrorMessage = SettingsManager.Instance.Branding == "SINDHI" ? Resources.SPMOResources.AlreadyExist : "हि माहिती आधीपासून उपलब्ध आहे.";
            }
            else
            {
                obj.Status = true;
                obj.ErrorMessage = SettingsManager.Instance.Branding == "SINDHI" ? Resources.SPMOResources.InformationSave : "माहिती सेव केली आहे.";
                objState.Save(model);
            }
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.StateId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            obj.StateList = filter;
            return Json(obj, JsonRequestBehavior.AllowGet);
        }



        public ActionResult Update(tblState model)
        {
            var countries = objState.GetStates();
            var test = countries.Where(p => p.StateName.ToUpper() == model.StateName.ToUpper() && p.CountryId == model.CountryId).FirstOrDefault();
            StateDetails obj = new StateDetails();
            if (test != null)
            {
                obj.Status = false;
                obj.ErrorMessage = SettingsManager.Instance.Branding == "SINDHI" ? Resources.SPMOResources.AlreadyExist : "हि माहिती आधीपासून उपलब्ध आहे.";
            }
            else
            {
                obj.Status = true;
                obj.ErrorMessage = SettingsManager.Instance.Branding == "SINDHI" ? Resources.SPMOResources.UserinformationSaveFailed : "माहितीमध्ये बदल करण्यात आला आहे.";
                objState.Update(model);
            }
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.StateId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            obj.StateList = filter;
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int BGId)
        {
            StateContextModel tbl = objState.GetStates().Where(p => p.StateId == BGId).FirstOrDefault();
            return Json(tbl, JsonRequestBehavior.AllowGet);
        }
    }
}