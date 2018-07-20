using ShriVivah.Models;
using ShriVivah.Models.ContextModel;
using ShriVivah.Models.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShriVivah.Controllers
{
    public class VendorTypeController : Controller
    {
        public VendorTypeContextModel objOras { get; set; }
        
        public int PageSize { get { return 5; } }
        public int CurentPageIndex { get; set; }

        public VendorTypeController()
        {
            objOras = new VendorTypeContextModel();
        }
        // GET: VendorType
        [MyAuthorizeAttribute(IsAdmin=true)]
        [CustomView]
        public ActionResult Index()
        {
            this.LoadIsAdmin();
            var countries = objOras.GetVendorTypes();
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.VendorTypeId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            return View("Index", filter);
        }

        public ActionResult UploadBrand()
        {
            Error error = new Error() { Status = false };
            //bool result = false;
            if (Request.Files.Count > 0)
            {
                string base64string = string.Empty;
                HttpPostedFileBase file = Request.Files[0];
                base64string = "Content/VendorImages/Temp/" + string.Format("{0:dd_MM_yyyy_hh_mm_ss}", DateTime.Now) + "_B.jpg";
                file.SaveAs(Server.MapPath("~/" + base64string));
                //SessionManager.GetInstance.ImagePath = "~/" + base64string;
                error.FilePath = base64string;
                error.Status = true;
            }
            return Json(error);
        }

        public ActionResult VendorTypeFirst()
        {
            IQueryable<VendorTypeModel> users = (IQueryable<VendorTypeModel>)Session["users"];
            int pageindex = 0;
            var filter = users.OrderBy(p => p.VendorTypeId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = users;
            Session["pageindex"] = 0;
            VendorTypeDetails obj = new VendorTypeDetails()
            {
                Status = true,
                VendorTypeList = filter
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(string prefix)
        {
            var countries = objOras.GetVendorTypes().Where(p => p.VendorType.ToUpper() == prefix.ToUpper());
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.VendorTypeId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            if (filter.Count() > 0)
            {
                VendorTypeDetails obj = new VendorTypeDetails()
                {
                    Status = true,
                    VendorTypeList = filter
                };
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            else
            {
                VendorTypeDetails obj = new VendorTypeDetails()
                {
                    Status = false,
                    ErrorMessage = SettingsManager.Instance.Branding == "SPMO" ? Resources.SPMOResources.NoMoreInformationAvail : "आणखी माहिती उपलब्ध नाही."
                };
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Reset()
        {
            var countries = objOras.GetVendorTypes();
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.VendorTypeId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            return Json(filter, JsonRequestBehavior.AllowGet);
        }

        public ActionResult VendorTypeNext()
        {
            IQueryable<VendorTypeModel> users = (IQueryable<VendorTypeModel>)Session["users"];
            if (users != null)
            {
                int pageindex = Convert.ToInt32(Session["pageindex"]);
                pageindex++;
                var filter = users.OrderBy(p => p.VendorTypeId).Skip(pageindex * PageSize).Take(PageSize);
                if (filter.Count() > 0)
                {
                    Session["pageindex"] = pageindex;
                    VendorTypeDetails obj = new VendorTypeDetails()
                    {
                        Status = true,
                        ErrorMessage = "",
                        VendorTypeList = filter
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                    //return Json(filter, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    VendorTypeDetails obj = new VendorTypeDetails()
                    {
                        Status = false,
                        ErrorMessage = SettingsManager.Instance.Branding == "SPMO" ? Resources.SPMOResources.NoMoreInformationAvail : "आणखी माहिती उपलब्ध नाही"
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return VendorTypeFirst();
            }
        }

        public ActionResult VendorTypePrev()
        {
            IQueryable<VendorTypeModel> users = (IQueryable<VendorTypeModel>)Session["users"];

            if (users != null)
            {
                int pageindex = Convert.ToInt32(Session["pageindex"]);
                if (pageindex > 0)
                {
                    pageindex--;
                    var filter = users.OrderBy(p => p.VendorTypeId).Skip(pageindex * PageSize).Take(PageSize);
                    Session["pageindex"] = pageindex;
                    VendorTypeDetails obj = new VendorTypeDetails()
                    {
                        Status = true,
                        ErrorMessage = "",
                        VendorTypeList = filter,
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    VendorTypeDetails obj = new VendorTypeDetails()
                    {
                        Status = false,
                        ErrorMessage = SettingsManager.Instance.Branding == "SPMO" ? Resources.SPMOResources.FirstPage : "तुम्ही पहिल्याच पानावर आहात.",
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return VendorTypeFirst();
            }
        }

        public ActionResult VendorTypeLast()
        {
            var users = objOras.GetVendorTypes();
            VendorTypeDetails obj = new VendorTypeDetails();
            int pageindex = Convert.ToInt32(Session["pageindex"]);
            pageindex++;
            obj.Status = true;
            if (users != null)
            {
                Session["pageindex"] = pageindex;
                if ((users.Count() % PageSize) == 0)
                {
                    obj.VendorTypeList = users.OrderBy(p => p.VendorTypeId).Skip(users.Count() - 2).Take(PageSize);
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    int rem = users.Count() % PageSize;
                    obj.VendorTypeList = users.OrderBy(p => p.VendorTypeId).Skip(users.Count() - rem).Take(PageSize);
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return VendorTypeFirst();
            }
        }
        [HttpPost]
        public ActionResult AddVendorType(VendorTypeModel model)
        {
            var countries = objOras.GetVendorTypes();
            var test = countries.Where(p => p.VendorType.ToUpper() == model.VendorType.ToUpper()).FirstOrDefault();
            VendorTypeDetails obj = new VendorTypeDetails();
            obj.Status = true;
            obj.ErrorMessage = SettingsManager.Instance.Branding == "SPMO" ? Resources.SPMOResources.InformationSave : "माहिती सेव केली आहे.";
            objOras.Save(model);
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.VendorTypeId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            obj.VendorTypeList = filter;
            return Json(obj, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult Update(VendorTypeModel model)
        {
            var countries = objOras.GetVendorTypes();
            var test = countries.Where(p => p.VendorType.ToUpper() == model.VendorType.ToUpper()).FirstOrDefault();
            VendorTypeDetails obj = new VendorTypeDetails();
            //model.TypeImagesPath = SessionManager.GetInstance.ImagePath;
            obj.Status = true;
            obj.ErrorMessage = SettingsManager.Instance.Branding == "SPMO" ? Resources.SPMOResources.UpdateSuccess : "माहितीमध्ये बदल करण्यात आला आहे.";
            objOras.Update(model);
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.VendorTypeId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            obj.VendorTypeList = filter;
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int VendorTypeId)
        {
            VendorTypeModel tbl = objOras.GetVendorTypes().Where(p => p.VendorTypeId == VendorTypeId).FirstOrDefault();
            return Json(tbl, JsonRequestBehavior.AllowGet);
        }
    }
}