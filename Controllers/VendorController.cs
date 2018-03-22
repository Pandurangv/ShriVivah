using ShriVivah.Models;
using ShriVivah.Models.ContextModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShriVivah.Controllers
{
    public class VendorController : Controller
    {
        public VendorContextModel objVendor { get; set; }

        public int PageSize { get { return 5; } }

        public int CurentPageIndex { get; set; }


        public int PageSizeSearch { get { return 10; } }

        public int CurentPageIndexSearch { get; set; }

        public VendorController()
        {
            objVendor = new VendorContextModel();
        }

        public ActionResult Search(int VendorTypeId)
        {
            var countries = objVendor.GetVendors().Where(p => p.IsActive == true);
            return View();
        }

        [MyAuthorizeAttribute(IsAdmin = true)]
        public ActionResult Index()
        {
            this.LoadIsAdmin();
            UserContextModel objUser = new UserContextModel();
            

            var countries = objVendor.GetVendors();
            VendorTypeContextModel objVendorType=new VendorTypeContextModel();
            List<SelectListItem> lst = (from tbl in objVendorType.GetVendorTypes()
                                        select new SelectListItem { Value = tbl.VendorTypeId.ToString(), Text = tbl.VendorType }).ToList();
            lst.Insert(0, new SelectListItem() { Value = "0", Text = "----व्यवसायाचा प्रकार----" });
            ViewBag.VendorTypeId = lst;

            lst = (from tbl in objUser.GetAgentDetails()
                   select new SelectListItem { 
                        Value=tbl.UserId.ToString(),
                        Text=tbl.FirstName + " " + tbl.LName
                   }).ToList();

            lst.Insert(0, new SelectListItem() { Value = "0", Text = "----माहिती देणारा ----" });
            ViewBag.AgentId = lst;

            int pageindex = 0;
            var filter = countries.OrderBy(p => p.VendorId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            return View("Index", filter);
        }

        [HttpGet]
        public ActionResult GetAllVendorList()
        {
            var countries = objVendor.GetVendors();
            return Json(new ResponseModel() { Status = true, DataResponse = countries }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RegisterVendor()
        {
            UserContextModel objUser = new UserContextModel();
            
            var countries = objVendor.GetVendors();
            VendorTypeContextModel objVendorType = new VendorTypeContextModel();
            List<SelectListItem> lst = (from tbl in objVendorType.GetVendorTypes()
                                        select new SelectListItem { Value = tbl.VendorTypeId.ToString(), Text = tbl.VendorType }).ToList();
            lst.Insert(0, new SelectListItem() { Value = "0", Text = "----व्यवसायाचा प्रकार ----" });
            ViewBag.VendorTypeId = lst;

            lst = (from tbl in objUser.GetAgentDetails()
                   select new SelectListItem
                   {
                       Value = tbl.UserId.ToString(),
                       Text = tbl.FirstName + " " + tbl.LName
                   }).ToList();

            lst.Insert(0, new SelectListItem() { Value = "0", Text = "----माहिती देणारा ----" });
            ViewBag.AgentId = lst;
            return View("RegisterVendor");

        }

        

        public ActionResult SaveVendor(VendorModel model)
        {
            model.IsActive = false;
            model.RegistrationDate = null;
            model.ExpiryDate = null;
            objVendor.Save(model);
            return Json(new ResponseModel() { Status=true},JsonRequestBehavior.AllowGet);
        }

        public ActionResult VendorFirst()
        {
            IQueryable<VendorModel> users = (IQueryable<VendorModel>)Session["users"];
            int pageindex = 0;
            var filter = users.OrderBy(p => p.VendorId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = users;
            Session["pageindex"] = 0;
            VendorDetails obj = new VendorDetails()
            {
                Status = true,
                VendorList = filter
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UploadLogo()
        {
            Error error = new Error() { Status=false};
            //bool result = false;
            if (Request.Files.Count > 0)
            {
                string base64string = string.Empty;
                HttpPostedFileBase file = Request.Files[0];
                base64string = "Content/VendorImages/Temp/" + string.Format("{0:dd_MM_yyyy_hh_mm_ss}",DateTime.Now) + "_L.jpg";
                file.SaveAs(Server.MapPath("~/" + base64string));
                //model.Image1=imgArr;
                error.FilePath = base64string;
                //objUser.UploadImage(base64string, "P");
                error.Status = true;
            }
            return Json(error);
        }

        [HttpPost]
        public ActionResult UploadProfile()
        {
            Error error = new Error() { Status = false };
            //bool result = false;
            if (Request.Files.Count > 0)
            {
                string base64string = string.Empty;
                HttpPostedFileBase file = Request.Files[0];
                base64string = "Content/VendorImages/Temp/" + string.Format("{0:dd_MM_yyyy_hh_mm_ss}", DateTime.Now) + "_D.jpg";
                file.SaveAs(Server.MapPath("~/" + base64string));
                //model.Image1=imgArr;
                error.FilePath = base64string;
                //objUser.UploadImage(base64string, "P");
                error.Status = true;
            }
            return Json(error);
        }

        [HttpPost]
        public ActionResult UploadProduct()
        {
            Error error = new Error() { Status = false };
            //bool result = false;
            if (Request.Files.Count > 0)
            {
                string base64string = string.Empty;
                HttpPostedFileBase file = Request.Files[0];
                base64string = "Content/VendorImages/Temp/" + string.Format("{0:dd_MM_yyyy_hh_mm_ss}", DateTime.Now) + "_P.jpg";
                file.SaveAs(Server.MapPath("~/" + base64string));
                //model.Image1=imgArr;
                error.FilePath = base64string;
                //objUser.UploadImage(base64string, "P");
                error.Status = true;
            }
            return Json(error);
        }

        public ActionResult SearchVendor(string prefix)
        {
            var countries = objVendor.GetVendors().Where(p => p.VendorName.ToUpper() == prefix.ToUpper());
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.VendorId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            if (filter.Count() > 0)
            {
                VendorDetails obj = new VendorDetails()
                {
                    Status = true,
                    VendorList = filter
                };
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            else
            {
                VendorDetails obj = new VendorDetails()
                {
                    Status = false,
                    ErrorMessage = "आणखी माहिती उपलब्ध नाही."
                };
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Reset()
        {
            var countries = objVendor.GetVendors();
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.VendorId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            return Json(filter, JsonRequestBehavior.AllowGet);
        }

        public ActionResult VendorNext()
        {
            IQueryable<VendorModel> users = (IQueryable<VendorModel>)Session["users"];
            if (users != null)
            {
                int pageindex = Convert.ToInt32(Session["pageindex"]);
                pageindex++;
                var filter = users.OrderBy(p => p.VendorId).Skip(pageindex * PageSize).Take(PageSize);
                if (filter.Count() > 0)
                {
                    Session["pageindex"] = pageindex;
                    VendorDetails obj = new VendorDetails()
                    {
                        Status = true,
                        ErrorMessage = "",
                        VendorList = filter
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                    //return Json(filter, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    VendorDetails obj = new VendorDetails()
                    {
                        Status = false,
                        ErrorMessage = "आणखी माहिती उपलब्ध नाही."
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return VendorFirst();
            }
        }

        public ActionResult VendorPrev()
        {
            IQueryable<VendorModel> users = (IQueryable<VendorModel>)Session["users"];

            if (users != null)
            {
                int pageindex = Convert.ToInt32(Session["pageindex"]);
                if (pageindex > 0)
                {
                    pageindex--;
                    var filter = users.OrderBy(p => p.VendorId).Skip(pageindex * PageSize).Take(PageSize);
                    Session["pageindex"] = pageindex;
                    VendorDetails obj = new VendorDetails()
                    {
                        Status = true,
                        ErrorMessage = "",
                        VendorList = filter,
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    VendorDetails obj = new VendorDetails()
                    {
                        Status = false,
                        ErrorMessage = "तुम्ही पहिल्याच पानावर आहात",
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return VendorFirst();
            }
        }

        public ActionResult VendorLast()
        {
            var users = objVendor.GetVendors();
            VendorDetails obj = new VendorDetails();
            int pageindex = Convert.ToInt32(Session["pageindex"]);
            pageindex++;
            obj.Status = true;
            if (users != null)
            {
                Session["pageindex"] = pageindex;
                if ((users.Count() % PageSize) == 0)
                {
                    obj.VendorList = users.OrderBy(p => p.VendorId).Skip(users.Count() - PageSize).Take(PageSize);
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    int rem = users.Count() % PageSize;
                    obj.VendorList = users.OrderBy(p => p.VendorId).Skip(users.Count() - rem).Take(PageSize);
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return VendorFirst();
            }
        }

        public ActionResult Vendors()
        {
            this.loadViewBag();
            Session["pageindexsearch"] = 0;
            var users = objVendor.GetVendors().Skip(0 * PageSize).Take(PageSize);
            return View(users);
        }

        public ActionResult VendorsFilter(string prefix)
        {
            var userdetails = objVendor.GetVendors();
            var filter = userdetails.OrderBy(p => p.VendorId).Skip(0 * PageSize).Take(PageSize).Where(p=>p.VendorName.Contains(prefix) || p.VendorType.Contains(prefix) || p.Address.Contains(prefix) || p.BusinessDescription.Contains(prefix) || p.City.Contains(prefix) || p.OwnerName.Contains(prefix));
            Session["pageindexsearch"] = 0;
            Session["resultsearch"] = userdetails;
            return Json(filter,JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadMoreVendors()
        {
            IQueryable<VendorModel> result = null;
            int page = 0;
            if (Session["resultsearch"]!=null)
            {
                result = (IQueryable<VendorModel>)Session["resultsearch"];
            }
            else
            {
                result = objVendor.GetVendors();
            }
            page = Convert.ToInt32(Session["pageindexsearch"]);
            page++;
            Session["pageindexsearch"] = page;
            Session["resultsearch"] = result;
            var filter = result.OrderBy(p => p.VendorId).Skip(page * PageSize).Take(PageSize);
            return Json(filter, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddVendor(VendorModel model)
        {
            var countries = objVendor.GetVendors();
            VendorDetails obj = new VendorDetails();
            obj.Status = true;
            try
            {
                obj.ErrorMessage = "माहिती सेव केली आहे.";
                model.IsActive = true;
                if (model.RegistrationDate==null)
                {
                    model.RegistrationDate = DateTime.Now.Date;
                }
                if (model.ExpiryDate==null)
                {
                    model.ExpiryDate = DateTime.Now.Date.AddYears(1);   
                }
                objVendor.Save(model);
                int pageindex = 0;
                var filter = countries.OrderBy(p => p.VendorId).Skip(pageindex * PageSize).Take(PageSize);
                Session["users"] = countries;
                Session["pageindex"] = 0;
                obj.VendorList = filter;
            }
            catch (Exception)
            {
                obj.Status = false;
                obj.ErrorMessage = "माहिती सेव करू शकत नाही.";
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult Update(VendorModel model)
        {
            var countries = objVendor.GetVendors();
            //var test = countries.Where(p => p.Vendor.ToUpper() == model.Vendor.ToUpper()).FirstOrDefault();
            VendorDetails obj = new VendorDetails();
            obj.Status = true;
            obj.ErrorMessage = "माहिती मध्ये बदल केला आहे.";
            objVendor.Update(model);
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.VendorId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            obj.VendorList = filter;
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int VendorId)
        {
            VendorModel tbl = objVendor.GetVendors().Where(p => p.VendorId == VendorId).FirstOrDefault();
            return Json(tbl, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ActiveDeactiveVendor(int VendorId,bool IsActive)
        {
            bool success= objVendor.ActiveDeactiveVendor(VendorId,IsActive);
            return Json(success, JsonRequestBehavior.AllowGet);
        }
    }
}