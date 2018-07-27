using ShriVivah.Models;
using ShriVivah.Models.ContextModel;
using ShriVivah.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using ShriVivah.Models.Filters;

namespace ShriVivah.Controllers
{
    public class UserProfileController : Controller
    {
        UserContextModel objUser = new UserContextModel();

        public int PageSize { get { return 10; } }
        public int CurentPageIndex { get; set; }

        public ActionResult MarriageDone(int UserId)
        {
            return Json(objUser.MarriageDone(UserId),JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetMarriedUserData()
        {
            var lst= objUser.Select_STP_GetUserDetails().Where(p => p.UserType.ToUpper() != "ADMIN" && p.UserType.ToUpper() != "AGENT" && p.ismarried == 1);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        [MyAuthorizeAttribute(IsAdmin=false)]
        public ActionResult Index()
        {
            this.LoadIsAdmin();
            ViewBag.IsAdmin = false;
            if (SessionManager.GetInstance.ActiveUser.UserType.ToUpper()=="ADMIN")
            {
                ViewBag.IsAdmin = true;
            }
            var user= objUser.Select_STP_GetUserDetails().Where(p => p.UserId == SessionManager.GetInstance.ActiveUser.UserId).FirstOrDefault();
            bool active = user.UserType.ToUpper()=="ADMIN"?true:(user.IsActive == null ? false : user.IsActive.Value);
            if (!active)
            {
                if (user.UserType.ToUpper() == "USER")
                {
                    if (user.DateOfBirth != null)
                    {
                        return RedirectToAction("UploadPhotos", "UserProfile");
                    }
                    else
                    {
                        return RedirectToAction("MyProfileDetails", "UserProfile");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Vendor");
                }
            }
            else
            {
                string gender = SessionManager.GetInstance.ActiveUser.Gender == "M" ? "F" : "M";
                ViewBag.SearchGender = gender;
                List<SelectListItem> lstData = new List<SelectListItem>();
                CastContextModel objC = new CastContextModel();
                lstData = (from tbl in objC.GetCasts()
                           select new SelectListItem { Text = tbl.CastName, Value = tbl.CastId.ToString() }).ToList();

                lstData.Insert(0, new SelectListItem() { Text = SettingsManager.Instance.Branding=="SPMO"? "---Select Cast---": "---जात निवडा---", Value = "0" });
                ViewBag.CasteId = lstData;

                OrasModel oras = new OrasModel();
                lstData = (from tbl in oras.GetOrass()
                           select new SelectListItem { Text = tbl.OrasName, Value = tbl.OrasId.ToString() }).ToList();

                lstData.Insert(0, new SelectListItem() { Text = SettingsManager.Instance.Branding == "SPMO" ? "---Select Rashi---" : "---राशी निवडा---", Value = "0" });

                ViewBag.OrasId = lstData;

                lstData = (from tbl in oras.GetHeights()
                           select new SelectListItem { Text = tbl.Height, Value = tbl.HeightId.ToString() }).ToList();

                lstData.Insert(0, new SelectListItem() { Text = SettingsManager.Instance.Branding == "SPMO" ? "---Select Height---": "---उंची---", Value = "0" });

                ViewBag.HeightId = lstData;
                ViewBag.EHeightId = lstData;

                BloodGroupModel bg = new BloodGroupModel();

                lstData = (from tbl in bg.GetEducations()
                           select new SelectListItem { Text = tbl.DegreeName, Value = tbl.QualificationId.ToString() }).ToList();

                lstData.Insert(0, new SelectListItem() { Text = SettingsManager.Instance.Branding == "SPMO" ? "---Select Education---" : "---शिक्षण---", Value = "0" });

                ViewBag.QualificationId = lstData;

                lstData = (from tbl in bg.GetBloodGroups()
                           select new SelectListItem { Text = tbl.BloodGroupName, Value = tbl.BloodGroupId.ToString() }).ToList();

                lstData.Insert(0, new SelectListItem() { Text = SettingsManager.Instance.Branding == "SPMO" ? "---Select Blood Group---" : "---शिक्षण---", Value = "0" });

                ViewBag.BloodGroupId = lstData;
                Session["pageindex"] = 0;
                string ViewName = SettingsManager.Instance.Branding == "SPMO" ? "UserRequestsSPMO" : "UserRequests";
                if (SessionManager.GetInstance.ActiveUser.UserType.ToUpper()=="ADMIN")
                {
                    var userdetails = objUser.Select_STP_GetUserDetails().Where(p => p.UserId != SessionManager.GetInstance.ActiveUser.UserId 
                                && p.UserType.ToUpper() != "ADMIN" && p.UserType.ToUpper() != "AGENT" && p.ismarried==0);
                    Session["users"] = userdetails;
                    var filter = userdetails.OrderBy(p => p.UserId).Skip(0 * PageSize).Take(PageSize);
                    
                    return View(ViewName, filter);
                }
                else if(SessionManager.GetInstance.ActiveUser.UserType.ToUpper() == "USER")
                {
                    var userdetails = objUser.GetUserDetailsForUser().Where(p => p.UserId != SessionManager.GetInstance.ActiveUser.UserId && p.Gender == gender && p.UserType.ToUpper() != "ADMIN" && p.ismarried == 0);
                    Session["users"] = userdetails;
                    var filter = userdetails.OrderBy(p => p.UserId).Skip(0 * PageSize).Take(PageSize);
                    return View(ViewName, filter);
                }
                else //if (SessionManager.GetInstance.ActiveUser.UserType.ToUpper() == "AGENT")
                {
                    var userdetails = objUser.GetUserDetailsForUser().Where(p => p.UserId != SessionManager.GetInstance.ActiveUser.UserId && p.Gender == gender && p.UserType.ToUpper() != "ADMIN" && p.ismarried == 0 && p.City.ToUpper()==SessionManager.GetInstance.ActiveUser.City.ToUpper());
                    Session["users"] = userdetails;
                    var filter = userdetails.OrderBy(p => p.UserId).Skip(0 * PageSize).Take(PageSize);
                    return View(ViewName, filter);
                }
            }
        }

        [MyAuthorizeAttribute(IsAdmin = false)]
        public ActionResult VerifyAndLogin(int UserId)
        {
            STP_GetUserDetail user = null;
            if (SettingsManager.Instance.Branding != "SPMO")
            {
                user = objUser.Select_STP_GetUserDetails().Where(p => p.UserId==UserId).FirstOrDefault();
            }
            else
            {
                user = objUser.Select_STP_GetUserDetails().Where(p => p.UserId==UserId).FirstOrDefault();
            }
            if (user != null)
            {
                SessionManager.GetInstance.AdminUserId = SessionManager.GetInstance.ActiveUser.UserId;
                if (Convert.ToBoolean(user.IsActive) && user.UserType.ToUpper() != "AGENT")
                {
                    SessionManager.GetInstance.ActiveUser = user;
                    var lst = new List<RegisterViewModel>();
                    lst.Add(new RegisterViewModel() { UserId = user.UserId, FirstName = user.FirstName, Gender = user.Gender, UserType = user.UserType });
                    return Json(new ResponseModel() { Status = true, ErrorMessage = "", DataResponse = lst.AsQueryable() }, JsonRequestBehavior.AllowGet);
                }
                else if (!Convert.ToBoolean(user.IsActive) && user.UserType.ToUpper() == "USER" && user.DateOfBirth == null)
                {
                    SessionManager.GetInstance.ActiveUser = user;
                    var lst = new List<RegisterViewModel>();
                    lst.Add(new RegisterViewModel() { UserId = user.UserId, FirstName = user.FirstName, Gender = user.Gender, UserType = "USER" });
                    return Json(new ResponseModel() { Status = true, ErrorMessage = "", DataResponse = lst.AsQueryable() }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    if (user.UserType.ToUpper() == "ADMIN")
                    {
                        SessionManager.GetInstance.ActiveUser = user;
                        var lst = new List<RegisterViewModel>();
                        lst.Add(new RegisterViewModel() { UserId = user.UserId, FirstName = user.FirstName, Gender = user.Gender, UserType = "ADMIN" });
                        return Json(new ResponseModel() { Status = true, ErrorMessage = "", DataResponse = lst.AsQueryable() }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        if (user.UserType.ToUpper() == "AGENT")
                        {
                            if (SettingsManager.Instance.Branding == "SPMO")
                            {
                                SessionManager.GetInstance.ActiveUser = user;
                                var lst = new List<RegisterViewModel>();
                                lst.Add(new RegisterViewModel() { UserId = user.UserId, FirstName = user.FirstName, Gender = user.Gender, UserType = "AGENT" });
                                return Json(new ResponseModel() { Status = true, ErrorMessage = "", DataResponse = lst.AsQueryable() }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new ResponseModel() { Status = false, ErrorMessage = SettingsManager.Instance.Branding == "SPMO" ? Resources.SPMOResources.AgentLogin : "प्रतिनिधी लॉगिन साठी अद्याप सुविधा नाहीत." }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json(new ResponseModel() { Status = false, ErrorMessage = SettingsManager.Instance.Branding == "SPMO" ? Resources.SPMOResources.AccountActivate : "आपले खाते 48 तासांच्या आत सक्रिय होईल ." }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
            }
            else
            {
                return Json(new ResponseModel() { Status = false, ErrorMessage = SettingsManager.Instance.Branding == "SPMO" ? Resources.SPMOResources.InvalidUserNamePassword : "अवैध लॉग-इन नाव किंवा पासवर्ड." }, JsonRequestBehavior.AllowGet);
            }
        }

        [MyAuthorizeAttribute(IsAdmin = false)]
        [CustomView]
        public ActionResult ChangePassword()
        {
            this.LoadIsAdmin();
            return View("ChangePassword", new RegisterViewModel());
        }

        public ActionResult UpdatePassword(string Old, string NewPassword)
        {
            ResponseModel response = new ResponseModel()
            {
                Status = false
            };
            STP_GetUserDetail user = SessionManager.GetInstance.ActiveUser;
            user.Password = NewPassword;
            objUser.ChangePassword(user);
            response.Status = true;
            SessionManager.GetInstance.ActiveUser = user;
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [CustomView]
        public ActionResult NewUsers()
        {
            this.LoadIsAdmin();
            List<SelectListItem> lstData = new List<SelectListItem>();
            CastContextModel objC = new CastContextModel();
            lstData = (from tbl in objC.GetCasts()
                       select new SelectListItem { Text = tbl.CastName, Value = tbl.CastId.ToString() }).ToList();

            lstData.Add(new SelectListItem() { Text = SettingsManager.Instance.Branding == "SPMO" ? "---Select Caste---" : "---जात निवडा---", Value = "0" });
            ViewBag.CasteId = lstData;

            OrasModel oras = new OrasModel();
            lstData = (from tbl in oras.GetOrass()
                       select new SelectListItem { Text = tbl.OrasName, Value = tbl.OrasId.ToString() }).ToList();

            lstData.Insert(0, new SelectListItem() { Text = SettingsManager.Instance.Branding == "SPMO" ? "---Select Rashi---" : "---राशी निवडा---", Value = "0" });

            ViewBag.OrasId = lstData;

            lstData = (from tbl in oras.GetHeights()
                       select new SelectListItem { Text = tbl.Height, Value = tbl.HeightId.ToString() }).ToList();

            lstData.Insert(0, new SelectListItem() { Text = SettingsManager.Instance.Branding == "SPMO" ? "---Select Height---" : "---उंची---", Value = "0" });

            ViewBag.HeightId = lstData;
            ViewBag.EHeightId = lstData;

            BloodGroupModel bg = new BloodGroupModel();

            lstData = (from tbl in bg.GetEducations()
                       select new SelectListItem { Text = tbl.DegreeName, Value = tbl.QualificationId.ToString() }).ToList();

            lstData.Insert(0, new SelectListItem() { Text = SettingsManager.Instance.Branding == "SPMO" ? "---Select Education---" : "---शिक्षण---", Value = "0" });

            ViewBag.QualificationId = lstData;
            //ViewBag.EQualificationId = lstData;


            string gender = SessionManager.GetInstance.ActiveUser.Gender == "M" ? "F" : "M";
            ViewBag.SearchGender = gender;
            if (SessionManager.GetInstance.ActiveUser.UserType.ToUpper() == "ADMIN")
            {
                var userdetails = objUser.Select_STP_GetUserDetails().Where(p => p.UserId != SessionManager.GetInstance.ActiveUser.UserId && p.UserType.ToUpper() != "ADMIN" && p.UserType.ToUpper() != "AGENT" && p.IsActive==false);
                Session["users"] = userdetails;
                Session["pageindex"] = 0;
                var filter = userdetails.OrderBy(p => p.UserId).Skip(0 * PageSize).Take(PageSize);
                return View("UserRequests", filter);
            }
            else
            {
                var userdetails = objUser.GetUserDetailsForUser().Where(p => p.UserId != SessionManager.GetInstance.ActiveUser.UserId && p.Gender == gender && p.UserType.ToUpper() != "ADMIN");
                return View("UserRequests", userdetails);
            }
        }

        [MyAuthorize]
        public ActionResult GetUserData()
        {
            var result = objUser.Select_STP_GetUserDetails().Where(p => p.UserType.ToUpper() != "ADMIN" && p.UserType.ToUpper() != "AGENT");// && p.ismarried == 0);
            if (SessionManager.GetInstance.ActiveUser!=null && SessionManager.GetInstance.ActiveUser.UserType.ToUpper() != "ADMIN")
            {
                string genderfilter = SessionManager.GetInstance.ActiveUser.Gender == "M" ? "F" : "M";
                result = objUser.Select_STP_GetUserDetails().Where(p => p.UserId != SessionManager.GetInstance.ActiveUser.UserId && p.UserType.ToUpper() != "ADMIN" && p.UserType.ToUpper() != "AGENT" && p.Gender==genderfilter && p.IsActive==true);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetUsers()
        {
            var result = objUser.Select_STP_GetUserDetails().Where(p => p.UserType.ToUpper() != "ADMIN" && p.UserType.ToUpper() != "AGENT" && p.ismarried == 0 && p.DateOfBirth!=null);
            if (SessionManager.GetInstance.ActiveUser != null && SessionManager.GetInstance.ActiveUser.UserType.ToUpper() != "ADMIN")
            {
                string genderfilter = SessionManager.GetInstance.ActiveUser.Gender == "M" ? "F" : "M";
                result = objUser.Select_STP_GetUserDetails().Where(p => p.UserId != SessionManager.GetInstance.ActiveUser.UserId && p.UserType.ToUpper() != "ADMIN" && p.UserType.ToUpper() != "AGENT" && p.ismarried == 0 && p.Gender == genderfilter);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GoLast()
        {
            //var users = objVendor.GetVendors();
            IQueryable<STP_GetUserDetail> users = (IQueryable<STP_GetUserDetail>)Session["users"];
            ResponseModel obj = new ResponseModel();
            int pageindex = Convert.ToInt32(Session["pageindex"]);
            pageindex++;
            obj.Status = true;
            if (users != null)
            {
                if ((users.Count() % PageSize) == 0)
                {
                    int pages = users.Count() / PageSize;
                    Session["pageindex"] = pages;
                    obj.DataResponse = users.OrderBy(p => p.UserId).Skip(users.Count() - PageSize).Take(PageSize);
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    int count = users.Count();
                    int rem = count % PageSize;
                    int pages = count / PageSize;
                    if (rem>0)
                    {
                        pages++;
                        Session["pageindex"] = pages;
                    }
                    obj.DataResponse = users.OrderBy(p => p.UserId).Skip(users.Count() - rem).Take(PageSize);
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return GoFirst();
            }
        }

        public ActionResult GoFirst()
        {
            IQueryable<STP_GetUserDetail> users = (IQueryable<STP_GetUserDetail>)Session["users"];
            if (users==null)
            {
                if (SessionManager.GetInstance.ActiveUser.UserType=="ADMIN")
                {
                    //string gender = SessionManager.GetInstance.ActiveUser.Gender == "M" ? "F" : "M";
                    var userdetails = objUser.Select_STP_GetUserDetails().Where(p => p.UserId != SessionManager.GetInstance.ActiveUser.UserId && p.UserType.ToUpper() != "ADMIN" && p.UserType.ToUpper() != "AGENT" && p.IsActive == false);
                    Session["users"] = userdetails;
                    var filter = userdetails.OrderBy(p => p.UserId).Skip(0 * PageSize).Take(PageSize);
                    return Json(filter, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string gender = SessionManager.GetInstance.ActiveUser.Gender == "M" ? "F" : "M";
                    var userdetails = objUser.GetUserDetailsForUser().Where(p => p.UserId != SessionManager.GetInstance.ActiveUser.UserId && p.Gender == gender && p.UserType.ToUpper() != "ADMIN");
                    Session["users"] = userdetails;
                    var filter = userdetails.OrderBy(p => p.UserId).Skip(0 * PageSize).Take(PageSize);
                    return Json(filter, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                int pageindex = 0;
                var filter = users.OrderBy(p => p.UserId).Skip(pageindex * PageSize).Take(PageSize);
                Session["users"] = users;
                Session["pageindex"] = 0;
                ResponseModel obj = new ResponseModel()
                {
                    Status = true,
                    DataResponse = filter
                };
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult GoBack()
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
                    ResponseModel obj = new ResponseModel()
                    {
                        Status = true,
                        ErrorMessage = "",
                        DataResponse = filter,
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    ResponseModel obj = new ResponseModel()
                    {
                        Status = false,
                        ErrorMessage = SettingsManager.Instance.Branding == "SPMO" ? Resources.SPMOResources.FirstPage : "तुम्ही पहिल्याच पानावर आहात",
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                var filter = users.OrderBy(p => p.UserId).Skip(0 * PageSize).Take(PageSize);
                Session["pageindex"] = 0;
                ResponseModel obj = new ResponseModel()
                {
                    Status = true,
                    ErrorMessage = "",
                    DataResponse = filter,
                };
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UserNext()
        {
            string gender = SessionManager.GetInstance.ActiveUser.Gender == "M" ? "F" : "M";
            IQueryable<STP_GetUserDetail> users = (IQueryable<STP_GetUserDetail>)Session["users"];
            if (users != null)
            {
                int pageindex = Convert.ToInt32(Session["pageindex"]);
                pageindex++;
                var filter = users.OrderBy(p => p.UserId).Skip(pageindex * PageSize).Take(PageSize);
                if (filter.Count() > 0)
                {
                    Session["pageindex"] = pageindex;
                    ResponseModel obj = new ResponseModel()
                    {
                        Status = true,
                        ErrorMessage = "",
                        DataResponse = filter
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    ResponseModel obj = new ResponseModel()
                    {
                        Status = false,
                        ErrorMessage = SettingsManager.Instance.Branding == "SPMO" ? Resources.SPMOResources.NoMoreInformationAvail : "आणखी माहिती उपलब्ध नाही"
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                Session["pageindex"] = 0;
                var userdetails = objUser.GetUserDetailsForUser().Where(p => p.UserId != SessionManager.GetInstance.ActiveUser.UserId && p.Gender == gender && p.UserType.ToUpper() != "ADMIN");
                Session["users"] = userdetails;
                var filter = userdetails.OrderBy(p => p.UserId).Skip(0 * PageSize).Take(PageSize);
                return View("UserRequests", filter);
            }
        }


        public ActionResult GetAllImages()
        {
            var result= objUser.GetAllImages();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [MyAuthorizeAttribute(IsAdmin = false)]
        public ActionResult UploadPhotos()
        {
            this.LoadIsAdmin();
            ViewBag.IsActive = SessionManager.GetInstance.ActiveUser.IsActive;
            ViewBag.IsAdmin = false;
            var user = new UserContextModel();
            var users = user.Select_STP_GetUserDetails();
            ViewBag.Gender = "M";
            ViewBag.TotalUsers = users.Count();
            var dt = DateTime.Now.Date.AddDays(-15);
            //target.ViewBag.NewRegisterd = users.Where(p => dt <= p.DateofReg.Value.Date).Count();
            ViewBag.UserId = SessionManager.GetInstance.ActiveUser.UserId;
            if (SessionManager.GetInstance.ActiveUser != null)
            {
                bool IsAdmin = SessionManager.GetInstance.ActiveUser.UserType.Equals("User") == true ? false : true;
                ViewBag.IsAdmin = IsAdmin;
                if (IsAdmin == false)
                {
                    ViewBag.Gender = SessionManager.GetInstance.ActiveUser.Gender;
                }
                ViewBag.VisitorCount = user.GetVisitors().Count();
            }
            string ViewName = SettingsManager.Instance.Branding == "SPMO" ? "UploadPhotosSPMO" : "UploadPhotos";
            return View(ViewName);
        }

        [MyAuthorizeAttribute(IsAdmin = true)]
        public ActionResult ApproveUser(int UserId, bool IsActive)
        {
            objUser.SetActive(UserId, IsActive);
            return Json(true,JsonRequestBehavior.AllowGet);
        }

        [MyAuthorizeAttribute(IsAdmin = true)]
        public ActionResult UserRequests()
        {
            this.LoadIsAdmin();
            List<SelectListItem>  lstData = new List<SelectListItem>();
            CastContextModel objC = new CastContextModel();
            lstData = (from tbl in objC.GetCasts()
                       select new SelectListItem { Text=tbl.CastName,Value=tbl.CastId.ToString()}).ToList();

            lstData.Add(new SelectListItem() { Text = SettingsManager.Instance.Branding == "SPMO" ? "---Select Caste---" : "---जात निवडा---", Value = "0" });
            ViewBag.CasteId = lstData;

            OrasModel oras = new OrasModel();
            lstData = (from tbl in oras.GetOrass()
                       select new SelectListItem { Text = tbl.OrasName, Value = tbl.OrasId.ToString() }).ToList();

            lstData.Insert(0, new SelectListItem() { Text = SettingsManager.Instance.Branding == "SPMO" ? "---Select Rashi---" : "---राशी निवडा---", Value = "0" });

            ViewBag.OrasId = lstData;

            lstData = (from tbl in oras.GetHeights()
                       select new SelectListItem { Text = tbl.Height, Value = tbl.HeightId.ToString() }).ToList();

            lstData.Insert(0, new SelectListItem() { Text = "---Select Height---", Value = "0" });

            ViewBag.HeightId = lstData;
            ViewBag.EHeightId = lstData;

            BloodGroupModel bg = new BloodGroupModel();

            lstData = (from tbl in bg.GetEducations()
                       select new SelectListItem { Text = tbl.DegreeName, Value = tbl.QualificationId.ToString() }).ToList();

            lstData.Insert(0, new SelectListItem() { Text = SettingsManager.Instance.Branding == "SPMO" ? "---Select Education---" : "---शिक्षण---", Value = "0" });

            ViewBag.QualificationId = lstData;
            //ViewBag.EQualificationId = lstData;

            string ViewName = SettingsManager.Instance.Branding == "SPMO" ? "UserRequestsSPMO" : "UserRequests";

            string gender = SessionManager.GetInstance.ActiveUser.Gender == "M" ? "F" : "M";
            ViewBag.SearchGender = gender;
            if (SessionManager.GetInstance.ActiveUser.UserType.ToUpper() == "ADMIN")
            {
                var userdetails = objUser.Select_STP_GetUserDetails().Where(p => p.UserId != SessionManager.GetInstance.ActiveUser.UserId 
                && p.UserType.ToUpper() != "ADMIN" && p.UserType.ToUpper() != "AGENT" && p.ismarried==0);
                return View(ViewName, userdetails);
            }
            else
            {
                var userdetails = objUser.GetUserDetailsForUser().Where(p => p.UserId != SessionManager.GetInstance.ActiveUser.UserId 
                && p.Gender == gender && p.UserType.ToUpper() != "ADMIN" && p.ismarried == 0);
                return View(ViewName, userdetails);
            }
            
        }

        [MyAuthorizeAttribute(IsAdmin = false)]
        [CustomView]
        public ActionResult Visitors()
        {
            this.LoadIsAdmin();
            var visitors =objUser.GetVisitors();
            var filter = visitors.OrderBy(p => p.VisitorId).Skip(0 * PageSize).Take(PageSize);
            Session["visitors"] = visitors;
            Session["visitorpageindex"] = 0;
            return View("Visitors",filter);
        }


        public ActionResult VisitorFirst()
        {
            IQueryable<VisitorModel> users = (IQueryable<VisitorModel>)Session["users"];
            int pageindex = 0;
            var filter = users.OrderBy(p => p.VisitorId).Skip(pageindex * PageSize).Take(PageSize);
            Session["visitors"] = users;
            Session["visitorpageindex"] = 0;
            VisitorResponse obj = new VisitorResponse()
            {
                Status = true,
                VisitorDetails = filter
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult VisitorLast()
        {
            var users = objUser.GetVisitors();
            VisitorResponse obj = new VisitorResponse();
            int pageindex = Convert.ToInt32(Session["visitorpageindex"]);
            pageindex++;
            obj.Status = true;
            if (users != null)
            {
                Session["visitorpageindex"] = pageindex;
                if ((users.Count() % PageSize) == 0)
                {
                    obj.VisitorDetails = users.OrderBy(p => p.VisitorId).Skip(users.Count() - PageSize).Take(PageSize);
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    int rem = users.Count() % PageSize;
                    obj.VisitorDetails = users.OrderBy(p => p.VisitorId).Skip(users.Count() - rem).Take(PageSize);
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return VisitorFirst();
            }
        }

        public ActionResult VisitorNext()
        {
            IQueryable<VisitorModel> users = (IQueryable<VisitorModel>)Session["visitors"];
            if (users != null)
            {
                int pageindex = Convert.ToInt32(Session["visitorpageindex"]);
                pageindex++;
                var filter = users.OrderBy(p => p.VisitorId).Skip(pageindex * PageSize).Take(PageSize);
                if (filter.Count() > 0)
                {
                    Session["visitorpageindex"] = pageindex;
                    VisitorResponse obj = new VisitorResponse()
                    {
                        Status = true,
                        ErrorMessage = "",
                        VisitorDetails = filter
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                    //return Json(filter, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    VisitorResponse obj = new VisitorResponse()
                    {
                        Status = false,
                        ErrorMessage = SettingsManager.Instance.Branding == "SPMO" ? Resources.SPMOResources.NoMoreInformationAvail : "आणखी माहिती उपलब्ध नाही"
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return VisitorFirst();
            }
        }

        public ActionResult QualificationPrev()
        {
            IQueryable<VisitorModel> users = (IQueryable<VisitorModel>)Session["visitors"];

            if (users != null)
            {
                int pageindex = Convert.ToInt32(Session["visitorpageindex"]);
                if (pageindex > 0)
                {
                    pageindex--;
                    var filter = users.OrderBy(p => p.VisitorId).Skip(pageindex * PageSize).Take(PageSize);
                    Session["visitorpageindex"] = pageindex;
                    VisitorResponse obj = new VisitorResponse()
                    {
                        Status = true,
                        ErrorMessage = "",
                        VisitorDetails = filter,
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    VisitorResponse obj = new VisitorResponse()
                    {
                        Status = false,
                        ErrorMessage = SettingsManager.Instance.Branding == "SPMO" ? Resources.SPMOResources.FirstPage : "You are already on first page",
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return VisitorFirst();
            }
        }

        [MyAuthorizeAttribute(IsAdmin = false)]
        public ActionResult ShowProfile(int ProfileId)
        {
            this.LoadIsAdmin();
            ViewBag.ActiveUserId = ProfileId;
            if (SessionManager.GetInstance.ActiveUser.UserType.ToUpper()!="ADMIN" && SessionManager.GetInstance.ActiveUser.UserId!=ProfileId)
            {
                objUser.InsertVisitorDetails(ProfileId);
            }
            var userinfo =objUser.Select_STP_GetUserDetails().Where(p => p.UserId == ProfileId).FirstOrDefault();
            if (!string.IsNullOrEmpty(userinfo.Img1))
            {
                userinfo.Img1 = userinfo.Img1 + "?lastmodified=" + DateTime.Now.Hour + "_" + DateTime.Now.Minute;
            }
            
            ViewBag.RelativeDetails= objUser.GetRelativeDetails(ProfileId);
            string ViewName = SettingsManager.Instance.Branding == "SPMO" ? "ShowProfileSPMO" : "ShowProfile";
            return View(ViewName, userinfo);
        }

        public ActionResult ApproveRequest(int RequestId)
        {
            objUser.UpdateRequest(RequestId);
            ResponseModel model = new ResponseModel() { 
                ErrorMessage="",
                Status=true,
            };
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [MyAuthorize(IsAdmin = true)]
        [CustomView]
        public ActionResult RegisterFromAdmin()
        {
            this.LoadIsAdmin();
            return View("RegisterFromAdmin");
        }

        [MyAuthorize(IsAdmin =true)]
        [HttpPost]
        public ActionResult RegisterUser(RegisterViewModel model)
        {
            tblUser user = null;
            if (SettingsManager.Instance.Branding == "SPMO")
            {
                user = objUser.GetUser().Where(p => p.MobileNo.ToUpper() == model.MobileNo.ToUpper()).FirstOrDefault();
                if (user == null)
                {
                    model.RoleName = "User";
                    int userid = objUser.Save(model);
                    if (userid > 0)
                    {
                        return Json(new ResponseModel() { Status = true, ErrorMessage = SettingsManager.Instance.Branding == "SPMO" ? Resources.SPMOResources.RegistrationPage : "उमेदवार माहिती यशस्वीपणे जतन केले आहे." }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new ResponseModel() { Status = false, ErrorMessage = SettingsManager.Instance.Branding == "SPMO" ? Resources.SPMOResources.RegistrationFailed : "उमेदवार नोंदणी करणे अशक्य." }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new ResponseModel() { Status = false, ErrorMessage = SettingsManager.Instance.Branding == "SPMO" ? Resources.SPMOResources.SameUserExist : "लॉग-इन नाव आधीपासून दुसर्या उमेदवार वापरले." }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                user = objUser.GetUser().Where(p => p.UserName.ToUpper() == model.UserName.ToUpper()).FirstOrDefault();
                if (user == null)
                {
                    model.RoleName = "User";
                    int userid = objUser.Save(model);
                    if (userid > 0)
                    {
                        return Json(new ResponseModel() { Status = true, ErrorMessage = SettingsManager.Instance.Branding == "SPMO" ? Resources.SPMOResources.RegistrationPage : "उमेदवार माहिती यशस्वीपणे जतन केले आहे." }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new ResponseModel() { Status = false, ErrorMessage = SettingsManager.Instance.Branding == "SPMO" ? Resources.SPMOResources.RegistrationFailed : "उमेदवार नोंदणी करणे अशक्य." }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new ResponseModel() { Status = false, ErrorMessage = SettingsManager.Instance.Branding == "SPMO" ? Resources.SPMOResources.SameUserExist : "लॉग-इन नाव आधीपासून दुसर्या उमेदवार वापरले." }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [MyAuthorize(IsAdmin =false)]
        public ActionResult UpdateUserData(RegisterViewModel model)
        {
            ResponseModel response = new ResponseModel() { Status = false };
            try
            {
                JavaScriptSerializer serialize = new JavaScriptSerializer();
                model = serialize.Deserialize<RegisterViewModel>(Request.Params["model"]);
                FamilyModel family = serialize.Deserialize<FamilyModel>(Request.Params["family"]);
                tblJobDetails job = serialize.Deserialize<tblJobDetails>(Request.Params["JobDetails"]);
                objUser.UpdateUserProfile(model);
                objUser.UpdateParentContact(family);
                objUser.UpdateJobLocation(job);
                
                response.Status = true;
                response.ErrorMessage = SettingsManager.Instance.Branding == "SPMO" ? Resources.SPMOResources.UserInformationSave : "उमेदवाराची माहिती जतन केली .";
            }
            catch (Exception)
            {
                response.Status = false;
                response.ErrorMessage = SettingsManager.Instance.Branding == "SPMO" ? Resources.SPMOResources.InvalidUserNamePassword : "उमेदवाराची माहिती जतन करू शकत नाही.";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [MyAuthorizeAttribute(IsAdmin = false)]
        public ActionResult EditProfile()
        {
            this.LoadIsAdmin();
            var user = objUser.Select_STP_GetUserDetails().Where(p => p.UserId == SessionManager.GetInstance.ActiveUser.UserId).FirstOrDefault();
            ViewBag.UserDetails = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            string ViewName = SettingsManager.Instance.Branding == "SPMO" ? "EditProfileSPMO" : "EditProfile";
            return View(ViewName);
        }

        [MyAuthorizeAttribute(IsAdmin = true)]
        public ActionResult EditUser(int UserId)
        {
            this.LoadIsAdmin();
            ReligionModel objReligion = new ReligionModel();
            var lst = objReligion.GetReligions();
            List<SelectListItem> lstData = (from tbl in lst
                                            select new SelectListItem { Text = tbl.ReligionName, Value = tbl.ReligionId.ToString() }).ToList();

            lstData.Insert(0, new SelectListItem() { Text = SettingsManager.Instance.Branding == "SPMO" ? "---Select Religion---" : "---धर्म निवडा---", Value = "0" });
            ViewBag.ReligionId = lstData;

            lstData = new List<SelectListItem>();
            lstData.Add(new SelectListItem() { Text = SettingsManager.Instance.Branding == "SPMO" ? "---Select Caste---" : "---जात निवडा---", Value = "0" });
            ViewBag.CasteId = lstData;

            OrasModel oras = new OrasModel();
            lstData = (from tbl in oras.GetOrass()
                       select new SelectListItem { Text = tbl.OrasName, Value = tbl.OrasId.ToString() }).ToList();

            lstData.Insert(0, new SelectListItem() { Text = SettingsManager.Instance.Branding == "SPMO" ? "---Select Rashi---" : "---राशी निवडा---", Value = "0" });

            ViewBag.OrasId = lstData;
            ViewBag.EOrasId = lstData;

            CountryModel objCountry = new CountryModel();
            lstData = (from tbl in objCountry.GetCountrys()
                       select new SelectListItem { Text = tbl.CountryName, Value = tbl.CountryId.ToString() }).ToList();

            lstData.Insert(0, new SelectListItem() { Text = SettingsManager.Instance.Branding == "SPMO" ? "---Select Country---" : "---देश निवडा---", Value = "0" });
            ViewBag.CountryId = lstData;

            StateModel objState = new StateModel();
            lstData = (from tbl in objState.GetStates()
                       select new SelectListItem { Text = tbl.StateName, Value = tbl.StateId.ToString() }).ToList();

            lstData.Insert(0, new SelectListItem() { Text = SettingsManager.Instance.Branding == "SPMO" ? "---Select State---" : "---राज्य निवडा---", Value = "0" });
            ViewBag.StateId = lstData;



            lstData = (from tbl in oras.GetHeights()
                       select new SelectListItem { Text = tbl.Height, Value = tbl.HeightId.ToString() }).ToList();

            lstData.Insert(0, new SelectListItem() { Text = SettingsManager.Instance.Branding == "SPMO" ? "---Select Height---" : "---उंची निवडा---", Value = "0" });

            ViewBag.HeightId = lstData;
            ViewBag.EHeightId = lstData;

            BloodGroupModel bg = new BloodGroupModel();

            lstData = (from tbl in bg.GetBloodGroups()
                       select new SelectListItem { Text = tbl.BloodGroupName, Value = tbl.BloodGroupId.ToString() }).ToList();

            lstData.Insert(0, new SelectListItem() { Text = SettingsManager.Instance.Branding == "SPMO" ? "---Select Blood Group---" : "---रक्त गट---", Value = "0" });

            ViewBag.BloodGroupId = lstData;


            lstData = (from tbl in bg.GetEducations()
                       select new SelectListItem { Text = tbl.DegreeName, Value = tbl.QualificationId.ToString() }).ToList();

            lstData.Insert(0, new SelectListItem() { Text = SettingsManager.Instance.Branding == "SPMO" ? "---Select Education---" : "---शिक्षण निवडा---", Value = "0" });

            ViewBag.QualificationId = lstData;
            ViewBag.EQualificationId = lstData;

            var user = objUser.Select_STP_GetUserDetails().Where(p => p.UserId == UserId).FirstOrDefault();
            ViewBag.UserDetails = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            string ViewName = SettingsManager.Instance.Branding == "SPMO" ? "EditUserSPMO" : "EditUser";
            return View(ViewName);
        }

        [MyAuthorizeAttribute(IsAdmin = false)]
        public ActionResult MyProfileDetails()
        {
            this.LoadIsAdmin();
            ReligionModel objReligion = new ReligionModel();
            var lst = objReligion.GetReligions();
            List<SelectListItem> lstData = (from tbl in lst
                                            select new SelectListItem { Text = tbl.ReligionName, Value = tbl.ReligionId.ToString() }).ToList();

            lstData.Insert(0, new SelectListItem() { Text = SettingsManager.Instance.Branding == "SPMO" ? "---Select Religion---" : "---धर्म निवडा---", Value = "0" });
            ViewBag.ReligionId = lstData;

            lstData = new List<SelectListItem>();
            lstData.Add(new SelectListItem() { Text = SettingsManager.Instance.Branding == "SPMO" ? "---Select Caste---" : "---जात निवडा---", Value = "0" });
            ViewBag.CasteId = lstData;

            OrasModel oras = new OrasModel();
            lstData = (from tbl in oras.GetOrass()
                       select new SelectListItem { Text = tbl.OrasName, Value = tbl.OrasId.ToString() }).ToList();

            lstData.Insert(0, new SelectListItem() { Text = SettingsManager.Instance.Branding == "SPMO" ? "---Select Rashi---" : "---राशी निवडा---", Value = "0" });

            ViewBag.OrasId = lstData;
            ViewBag.EOrasId = lstData;

            CountryModel objCountry = new CountryModel();
            lstData = (from tbl in objCountry.GetCountrys()
                       select new SelectListItem { Text = tbl.CountryName, Value = tbl.CountryId.ToString() }).ToList();

            lstData.Insert(0, new SelectListItem() { Text = SettingsManager.Instance.Branding == "SPMO" ? "---Select Country---" : "---देश निवडा---", Value = "0" });
            ViewBag.CountryId = lstData;

            StateModel objState = new StateModel();
            lstData = (from tbl in objState.GetStates()
                       select new SelectListItem { Text = tbl.StateName, Value = tbl.StateId.ToString() }).ToList();

            lstData.Insert(0, new SelectListItem() { Text = SettingsManager.Instance.Branding == "SPMO" ? "---Select State---" : "---राज्य निवडा---", Value = "0" });
            ViewBag.StateId = lstData;



            lstData = (from tbl in oras.GetHeights()
                       select new SelectListItem { Text = tbl.Height, Value = tbl.HeightId.ToString() }).ToList();

            lstData.Insert(0, new SelectListItem() { Text = SettingsManager.Instance.Branding == "SPMO" ? "---Select Height---" : "---उंची---", Value = "0" });

            ViewBag.HeightId = lstData;
            ViewBag.EHeightId = lstData;

            BloodGroupModel bg = new BloodGroupModel();

            lstData = (from tbl in bg.GetBloodGroups()
                       select new SelectListItem { Text = tbl.BloodGroupName, Value = tbl.BloodGroupId.ToString() }).ToList();

            lstData.Insert(0, new SelectListItem() { Text = SettingsManager.Instance.Branding == "SPMO" ? "---Blood Group---" : "---रक्त गट ---", Value = "0" });

            ViewBag.BloodGroupId = lstData;


            lstData = (from tbl in bg.GetEducations()
                       select new SelectListItem { Text = tbl.DegreeName, Value = tbl.QualificationId.ToString() }).ToList();

            lstData.Insert(0, new SelectListItem() { Text = SettingsManager.Instance.Branding == "SPMO" ? "---Select Education---" : "---शिक्षण---", Value = "0" });

            ViewBag.MinAge = SessionManager.GetInstance.ActiveUser.Gender == "M" ? 22 : 18;

            ViewBag.QualificationId = lstData;
            ViewBag.EQualificationId = lstData;
            string ViewName = SettingsManager.Instance.Branding == "SPMO" ? "IndexSPMO" : "Index";

            return View(ViewName);
        }

        

        [HttpPost]
        public ActionResult UpdateUserByAdmin()
        {
            ResponseModel response = new ResponseModel() { Status = false, ErrorMessage = SettingsManager.Instance.Branding == "SPMO" ? Resources.SPMOResources.UserinformationSaveFailed : "उमेदवाराची माहिती जतन करू शकत नाही." };
            try
            {
                JavaScriptSerializer serialize = new JavaScriptSerializer();
                RegisterViewModel model = serialize.Deserialize<RegisterViewModel>(Request.Params["model"]);
                
                FamilyModel family = serialize.Deserialize<FamilyModel>(Request.Params["family"]);
                tblJobDetails job = serialize.Deserialize<tblJobDetails>(Request.Params["JobDetails"]);
                List<RelativeModel> relatives = serialize.Deserialize<List<RelativeModel>>(Request.Params["relatives"]);

                int? userid= objUser.UpdateUserByAdmin(model);
                objUser.UpdateUserByAdmin(family,userid.Value);
                objUser.SaveUserByAdmin(job,model.UserId.Value);
                objUser.Update(relatives,model.UserId.Value);
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult UpdateUserProfile(RegisterViewModel model,FamilyModel family,List<RelativeModel> relatives)
        {
            ResponseModel response = new ResponseModel() { Status = false,
                ErrorMessage = SettingsManager.Instance.Branding == "SPMO" ? Resources.SPMOResources.UserinformationSaveFailed : "उमेदवाराची माहिती जतन करू शकत नाही." };
            try
            {
                JavaScriptSerializer serialize = new JavaScriptSerializer();
                model = serialize.Deserialize<RegisterViewModel>(Request.Params["model"]);
                family = serialize.Deserialize<FamilyModel>(Request.Params["family"]);
                tblExpectation tbl = new tblExpectation();
                relatives = serialize.Deserialize<List<RelativeModel>>(Request.Params["relatives"]);
                tblJobDetails job = serialize.Deserialize<tblJobDetails>(Request.Params["JobDetails"]);
                model.Income = job.Income;
                //List<string> colorlist = null;
                if (Request.Params["colorlist[]"] != null)
                {
                    try
                    {
                        tbl.Color = Convert.ToString(Request.Params["colorlist[]"]);
                    }
                    catch (Exception ex)
                    {

                    }
                }
                //List<string> HeightIdlist = null;
                if (Request.Params["HeightIdlist[]"] != null)
                {
                    try
                    {
                        //HeightIdlist = serialize.Deserialize<List<string>>
                        tbl.Height = Convert.ToString(Request.Params["HeightIdlist[]"]);
                    }
                    catch (Exception ex)
                    {

                    }
                }
                //List<string> OrasIdlist = null;
                if (Request.Params["OrasIdlist[]"] != null)
                {
                    try
                    {
                        //OrasIdlist = serialize.Deserialize<List<string>>(Request.Params["OrasIdlist[]"]);
                        tbl.OrasId = Convert.ToString(Request.Params["OrasIdlist[]"]);
                    }
                    catch (Exception ex)
                    {

                    }
                }
                //List<string> QualificationIdlist = null;
                if (Request.Params["QualificationIdlist[]"] != null)
                {
                    try
                    {
                        //QualificationIdlist = serialize.Deserialize<List<string>>(Request.Params["QualificationIdlist[]"]);
                        tbl.Qualification = Convert.ToString(Request.Params["QualificationIdlist[]"]);
                    }
                    catch (Exception ex)
                    {

                    }
                }

                objUser.Update(model);
                objUser.Update(family);
                objUser.Update(relatives);
                objUser.Save(tbl);
                objUser.Save(job);
                
                SessionManager.GetInstance.ActiveUser.Gender = model.Gender;
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Reset()
        {
            string gender = SessionManager.GetInstance.ActiveUser.Gender == "M" ? "F" : "M";
            if (SessionManager.GetInstance.ActiveUser.UserType.ToUpper() == "ADMIN")
            {
                var lst = objUser.Select_STP_GetUserDetails().Where(p => p.UserId != SessionManager.GetInstance.ActiveUser.UserId 
                && p.UserType.ToUpper() != "ADMIN" && p.UserType.ToUpper() != "AGENT" && p.ismarried == 0); //.Where(p => p.FirstName.Contains(prefix) || p.LastName.Contains(prefix) || p.Cast.Contains(prefix) || p.OrasName.Contains(prefix) || p.Qualification.Contains(prefix));
                ResponseModel response = new ResponseModel()
                {
                    DataResponse = lst,
                    Status = true
                };
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var lst = objUser.GetUserDetailsForUser().Where(p => (p.UserId != SessionManager.GetInstance.ActiveUser.UserId 
                && p.Gender == gender && p.UserType.ToUpper() != "ADMIN" && p.ismarried == 0));
                ResponseModel response = new ResponseModel()
                {
                    DataResponse = lst,
                    Status = true
                };
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }


        public Expression<Func<RegisterViewModel, bool>> IsMatchedExpression(string propname,object parameter,string operatortest)
        {
            var parameterExpression = Expression.Parameter(typeof(RegisterViewModel));
            var propertyOrField = Expression.PropertyOrField(parameterExpression, propname);
            BinaryExpression binaryExpression = null;
            if (operatortest==">=")
            {
                binaryExpression = Expression.GreaterThanOrEqual(propertyOrField, Expression.Constant(parameter));    
            }
            else if (operatortest == "<=")
            {
                binaryExpression = Expression.LessThanOrEqual(propertyOrField, Expression.Constant(parameter));
            }
            else if (operatortest == "=")
            {
                binaryExpression = Expression.Equal(propertyOrField, Expression.Constant(parameter));
            }
            return Expression.Lambda<Func<RegisterViewModel, bool>>(binaryExpression, parameterExpression);
        }

        public ActionResult GetUserDetailsByParam(string MinAge, string MaxAge, string CastId, string MaxHeightId, string MinHeightId, string QualificationId,string JobLocation,string Income)
        {
            string gender = SessionManager.GetInstance.ActiveUser.Gender == "M" ? "F" : "M";
            IQueryable<STP_GetUserDetail> lst = null;
            int? minAge = 0;
            int? maxAge = 0;
            int? castId = 0;
            int? minHeightId = 0;
            int? maxHeightId = 0;
            //string qualificationId = "";
            if (!string.IsNullOrEmpty(MinAge))
            {
                minAge = Convert.ToInt32(MinAge);
            }
            if (!string.IsNullOrEmpty(MaxAge))
            {
                maxAge = Convert.ToInt32(MaxAge);
            }
            if (!string.IsNullOrEmpty(CastId))
            {
                castId = Convert.ToInt32(CastId);
            }
            if (!string.IsNullOrEmpty(MinHeightId))
            {
                minHeightId = Convert.ToInt32(MinHeightId);
            }
            if (!string.IsNullOrEmpty(MaxHeightId))
            {
                maxHeightId = Convert.ToInt32(MaxHeightId);
            }
            //if (!string.IsNullOrEmpty(QualificationId))
            //{
            //    qualificationId = Convert.ToInt32(QualificationId);
            //}
            int[] arr = null;
            try
            {
                if (minHeightId > 0 && maxHeightId > 0)
                    {
                        OrasModel oras = new OrasModel();
                        var hids = (from tbl in oras.GetHeights()
                                    where tbl.HeightId <= maxHeightId
                                    && tbl.HeightId >= minHeightId
                                    select new
                                    {
                                        Id = tbl.HeightId
                                    }).ToList();
                        if (hids != null && hids.Count > 0)
                        {
                            arr = new int[hids.Count];
                            for (int i = 0; i < hids.Count; i++)
                            {
                                arr[i] = Convert.ToInt32(hids[i].Id);
                            }
                        }
                    }
            }
            catch (Exception ex)
            {
            }
            if (SessionManager.GetInstance.ActiveUser.UserType.ToUpper() == "ADMIN")
            {
                try
                {
                    //1
                    if (maxAge > 0 && minAge > 0 && castId > 0 && !string.IsNullOrEmpty(QualificationId) && arr!=null && arr.Length>0)
                    {
                        lst = objUser.Select_STP_GetUserDetails().Where(p => p.Age >= minAge && p.Age <= maxAge && p.CasteId == castId 
                        && p.Qualification.Trim().ToLower()==QualificationId.Trim().ToLower() && arr.Contains(p.HeightId.Value) && p.ismarried==0);
                    }
                        //2
                    else if (maxAge > 0 && minAge > 0 && castId > 0 && !string.IsNullOrEmpty(QualificationId) && arr == null)
                    {
                        lst = objUser.Select_STP_GetUserDetails().Where(p => p.Age >= minAge && p.ismarried == 0  && p.Age <= maxAge && p.CasteId == castId && p.Qualification.Trim().ToLower()==QualificationId.Trim().ToLower());
                    }
                        //3
                    else if (maxAge > 0 && minAge > 0 && castId > 0 && string.IsNullOrEmpty(QualificationId) && arr != null && arr.Length>0)
                    {
                        lst = objUser.Select_STP_GetUserDetails().Where(p => p.Age >= minAge && p.ismarried == 0  && p.Age <= maxAge && p.CasteId == castId && arr.Contains(p.HeightId.Value));
                    }
                        //4
                    else if (maxAge > 0 && minAge > 0 && castId == 0 && !string.IsNullOrEmpty(QualificationId) && arr != null && arr.Length > 0)
                    {
                        lst = objUser.Select_STP_GetUserDetails().Where(p => p.Age >= minAge && p.ismarried == 0  && p.Age <= maxAge && p.Qualification.Trim().ToLower()==QualificationId.Trim().ToLower() && arr.Contains(p.HeightId.Value));
                    }
                        //5
                    else if (maxAge > 0 && minAge == 0 && castId > 0 && !string.IsNullOrEmpty(QualificationId) && arr != null && arr.Length > 0)
                    {
                        lst = objUser.Select_STP_GetUserDetails().Where(p => p.CasteId == castId && p.ismarried == 0  && p.Age <= maxAge && p.Qualification.Trim().ToLower()==QualificationId.Trim().ToLower() && arr.Contains(p.HeightId.Value));
                    }
                        //6
                    else if (maxAge == 0 && minAge > 0 && castId > 0 && !string.IsNullOrEmpty(QualificationId) && arr != null && arr.Length > 0)
                    {
                        lst = objUser.Select_STP_GetUserDetails().Where(p => p.CasteId == castId && p.ismarried == 0  && p.Age >= minAge && p.Qualification.Trim().ToLower()==QualificationId.Trim().ToLower() && arr.Contains(p.HeightId.Value));
                    }
                        //7
                    else if (maxAge > 0 && minAge > 0 && castId > 0 && string.IsNullOrEmpty(QualificationId) && arr == null)
                    {
                        lst = objUser.Select_STP_GetUserDetails().Where(p => p.Age >= minAge && p.Age <= maxAge && p.CasteId == castId);
                    }
                    //8
                    else if (maxAge > 0 && minAge > 0 && castId == 0 && string.IsNullOrEmpty(QualificationId) && arr != null && arr.Length>0)
                    {
                        lst = objUser.Select_STP_GetUserDetails().Where(p => p.Age >= minAge && p.Age <= maxAge && arr.Contains(p.HeightId.Value) && p.ismarried == 0);
                    }
                    //9
                    else if (maxAge > 0 && minAge == 0 && castId == 0 && !string.IsNullOrEmpty(QualificationId) && arr != null && arr.Length > 0)
                    {
                        lst = objUser.Select_STP_GetUserDetails().Where(p => p.Qualification.Trim().ToLower()==QualificationId.Trim().ToLower() && p.ismarried == 0 && p.Age <= maxAge && arr.Contains(p.HeightId.Value));
                    }
                        //10
                    else if (maxAge == 0 && minAge == 0 && castId > 0 && !string.IsNullOrEmpty(QualificationId) && arr != null && arr.Length > 0)
                    {
                        lst = objUser.Select_STP_GetUserDetails().Where(p => p.Qualification.Trim().ToLower()==QualificationId.Trim().ToLower() && p.ismarried == 0  && p.CasteId == castId && arr.Contains(p.HeightId.Value));
                    }
                    //11
                    else if (maxAge > 0 && minAge > 0 && castId == 0 && string.IsNullOrEmpty(QualificationId) && arr == null)
                    {
                        lst = objUser.Select_STP_GetUserDetails().Where(p => p.Age >= minAge && p.Age <= maxAge && p.ismarried == 0);
                    }
                    else if (maxAge > 0 && minAge == 0 && castId == 0 && string.IsNullOrEmpty(QualificationId) && arr != null && arr.Length>0)
                    {
                        lst = objUser.Select_STP_GetUserDetails().Where(p => p.Age >= minAge && arr.Contains(p.HeightId.Value) && p.ismarried == 0);
                    }
                    else if (maxAge > 0 && minAge == 0 && castId == 0 && !string.IsNullOrEmpty(QualificationId) && arr == null)
                    {
                        lst = objUser.Select_STP_GetUserDetails().Where(p => p.Age <= maxAge && p.Qualification.Trim().ToLower()==QualificationId.Trim().ToLower() && p.ismarried == 0);
                    }
                    else if (maxAge == 0 && minAge == 0 && castId > 0 && !string.IsNullOrEmpty(QualificationId) && arr == null)
                    {
                        lst = objUser.Select_STP_GetUserDetails().Where(p => p.CasteId == castId && p.Qualification.Trim().ToLower()==QualificationId.Trim().ToLower() && p.ismarried == 0);
                    }
                    else if (maxAge >= 0 && minAge == 0 && castId == 0 && string.IsNullOrEmpty(QualificationId) && arr == null)
                    {
                        lst = objUser.Select_STP_GetUserDetails().Where(p => p.Age <= maxAge && p.ismarried == 0);
                    }
                    else if (maxAge == 0 && minAge > 0 && castId == 0 && string.IsNullOrEmpty(QualificationId) && arr == null)
                    {
                        lst = objUser.Select_STP_GetUserDetails().Where(p => p.Age >= minAge && p.ismarried == 0);
                    }
                    else if (maxAge == 0 && minAge == 0 && castId > 0 && string.IsNullOrEmpty(QualificationId) && arr == null)
                    {
                        lst = objUser.Select_STP_GetUserDetails().Where(p => p.CasteId == castId && p.ismarried == 0);
                    }
                    else if (maxAge == 0 && minAge == 0 && castId == 0 && !string.IsNullOrEmpty(QualificationId) && arr == null)
                    {
                        lst = objUser.Select_STP_GetUserDetails().Where(p => p.Qualification.Trim().ToLower()==QualificationId.Trim().ToLower() && p.ismarried == 0);
                    }
                    else if (maxAge == 0 && minAge == 0 && castId == 0 && !string.IsNullOrEmpty(QualificationId) && arr == null)
                    {
                        lst = objUser.Select_STP_GetUserDetails().Where(p => p.Qualification.Trim().ToLower()==QualificationId.Trim().ToLower() && p.ismarried == 0);
                    }
                    else if (maxAge == 0 && minAge == 0 && castId == 0 && !string.IsNullOrEmpty(QualificationId) && arr != null && arr.Length>0)
                    {
                        lst = objUser.Select_STP_GetUserDetails().Where(p => arr.Contains(p.HeightId.Value) && p.ismarried == 0);
                    }
                }
                catch (Exception ex)
                {
                }
                ResponseModel response = new ResponseModel()
                {
                    DataResponse = lst,
                    Status = true
                };
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            else
            {
                //var lst = objUser.GetUserDetailsForUser().Where();
                try
                {
                    // 1
                    if (maxAge > 0 && minAge > 0 && castId > 0 && !string.IsNullOrEmpty(QualificationId) && arr != null && arr.Length > 0 && !string.IsNullOrEmpty(Income) && !string.IsNullOrEmpty(JobLocation))
                    {
                        lst = objUser.GetUserDetailsForUser().Where(p => (p.UserId != SessionManager.GetInstance.ActiveUser.UserId && p.ismarried == 0 && p.Gender == gender && p.UserType.ToUpper() != "ADMIN") && p.Age >= minAge && p.Age <= maxAge && p.CasteId == castId && p.Qualification.Trim().ToLower()==QualificationId.Trim().ToLower() && arr.Contains(p.HeightId.Value) && p.Income.Contains(Income) && p.JobLocation.Contains(JobLocation));
                    }
                    if (maxAge == 0 && minAge > 0 && castId > 0 && !string.IsNullOrEmpty(QualificationId) && arr != null && arr.Length > 0 && !string.IsNullOrEmpty(Income) && !string.IsNullOrEmpty(JobLocation))
                    {
                        lst = objUser.GetUserDetailsForUser().Where(p => (p.UserId != SessionManager.GetInstance.ActiveUser.UserId && p.ismarried == 0 && p.Gender == gender && p.UserType.ToUpper() != "ADMIN") && p.Age >= minAge && p.CasteId == castId && p.Qualification.Trim().ToLower()==QualificationId.Trim().ToLower() && arr.Contains(p.HeightId.Value) && p.Income.Contains(Income) && p.JobLocation.Contains(JobLocation));
                    }
                    if (maxAge > 0 && minAge > 0 && castId == 0 && !string.IsNullOrEmpty(QualificationId) && arr != null && arr.Length > 0 && !string.IsNullOrEmpty(Income) && !string.IsNullOrEmpty(JobLocation))
                    {
                        lst = objUser.GetUserDetailsForUser().Where(p => (p.UserId != SessionManager.GetInstance.ActiveUser.UserId && p.ismarried == 0 && p.Gender == gender && p.UserType.ToUpper() != "ADMIN") && p.Age >= minAge && p.Age <= maxAge && p.Qualification.Trim().ToLower()==QualificationId.Trim().ToLower() && arr.Contains(p.HeightId.Value) && p.JobLocation.Contains(JobLocation) && p.Income.Contains(Income));
                    }
                    if (maxAge > 0 && minAge > 0 && castId > 0 && string.IsNullOrEmpty(QualificationId) && arr != null && arr.Length > 0 && !string.IsNullOrEmpty(Income) && !string.IsNullOrEmpty(JobLocation))
                    {
                        lst = objUser.GetUserDetailsForUser().Where(p => (p.UserId != SessionManager.GetInstance.ActiveUser.UserId && p.ismarried == 0 && p.Gender == gender && p.UserType.ToUpper() != "ADMIN") && p.Age >= minAge && p.Age <= maxAge  && p.CasteId == castId && arr.Contains(p.HeightId.Value) && p.JobLocation.Contains(JobLocation) && p.Income.Contains(Income));
                    }
                    if (maxAge > 0 && minAge > 0 && castId > 0 && !string.IsNullOrEmpty(QualificationId) && arr == null && arr.Length == 0 && !string.IsNullOrEmpty(Income) && !string.IsNullOrEmpty(JobLocation))
                    {
                        lst = objUser.GetUserDetailsForUser().Where(p => (p.UserId != SessionManager.GetInstance.ActiveUser.UserId && p.Gender == gender && p.ismarried == 0 && p.UserType.ToUpper() != "ADMIN") && p.Age >= minAge && p.Age <= maxAge && p.CasteId == castId && p.Qualification.Trim().ToLower()==QualificationId.Trim().ToLower() && p.JobLocation.Contains(JobLocation) && p.Income.Contains(Income));
                    }
                    if (maxAge > 0 && minAge > 0 && castId > 0 && !string.IsNullOrEmpty(QualificationId) && arr != null && arr.Length > 0 && !string.IsNullOrEmpty(Income) && string.IsNullOrEmpty(JobLocation))
                    {
                        lst = objUser.GetUserDetailsForUser().Where(p => (p.UserId != SessionManager.GetInstance.ActiveUser.UserId && p.Gender == gender && p.ismarried == 0 && p.UserType.ToUpper() != "ADMIN") && p.Age >= minAge && p.Age <= maxAge && p.CasteId == castId && p.Qualification.Trim().ToLower()==QualificationId.Trim().ToLower() && p.Income.Contains(Income) && arr.Contains(p.HeightId.Value));
                    }
                    if (maxAge > 0 && minAge > 0 && castId > 0 && !string.IsNullOrEmpty(QualificationId) && arr != null && arr.Length > 0 && string.IsNullOrEmpty(Income) && !string.IsNullOrEmpty(JobLocation))
                    {
                        lst = objUser.GetUserDetailsForUser().Where(p => (p.UserId != SessionManager.GetInstance.ActiveUser.UserId && p.Gender == gender && p.ismarried == 0 && p.UserType.ToUpper() != "ADMIN") && p.Age >= minAge && p.Age <= maxAge && p.CasteId == castId && p.Qualification.Trim().ToLower()==QualificationId.Trim().ToLower() && p.JobLocation.Contains(JobLocation) && arr.Contains(p.HeightId.Value));
                    }
                    // 2
                    else if (maxAge > 0 && minAge > 0 && castId > 0 && !string.IsNullOrEmpty(QualificationId) && arr != null && arr.Length > 0 && string.IsNullOrEmpty(Income) && string.IsNullOrEmpty(JobLocation))
                    {
                        lst = objUser.GetUserDetailsForUser().Where(p => (p.UserId != SessionManager.GetInstance.ActiveUser.UserId && p.Gender == gender && p.ismarried == 0 && p.UserType.ToUpper() != "ADMIN") && p.Age >= minAge && p.Age <= maxAge && p.CasteId == castId && p.Qualification.Trim().ToLower()==QualificationId.Trim().ToLower() && arr.Contains(p.HeightId.Value));
                    }
                    //2
                    else if (maxAge > 0 && minAge > 0 && castId > 0 && !string.IsNullOrEmpty(QualificationId) && arr == null && string.IsNullOrEmpty(Income) && string.IsNullOrEmpty(JobLocation))
                    {
                        lst = objUser.GetUserDetailsForUser().Where(p => (p.UserId != SessionManager.GetInstance.ActiveUser.UserId && p.Gender == gender && p.ismarried == 0 && p.UserType.ToUpper() != "ADMIN") && p.Age >= minAge && p.Age <= maxAge && p.CasteId == castId && p.Qualification.Trim().ToLower()==QualificationId.Trim().ToLower());
                    }
                    //3
                    else if (maxAge > 0 && minAge > 0 && castId > 0 && string.IsNullOrEmpty(QualificationId) && arr != null && arr.Length > 0 && string.IsNullOrEmpty(Income) && string.IsNullOrEmpty(JobLocation))
                    {
                        lst = objUser.GetUserDetailsForUser().Where(p => (p.UserId != SessionManager.GetInstance.ActiveUser.UserId && p.Gender == gender && p.ismarried == 0 && p.UserType.ToUpper() != "ADMIN") && p.Age >= minAge && p.Age <= maxAge && p.CasteId == castId && arr.Contains(p.HeightId.Value));
                    }
                    //4
                    else if (maxAge > 0 && minAge > 0 && castId == 0 && !string.IsNullOrEmpty(QualificationId) && arr != null && arr.Length > 0 && string.IsNullOrEmpty(Income) && string.IsNullOrEmpty(JobLocation))
                    {
                        lst = objUser.GetUserDetailsForUser().Where(p => (p.UserId != SessionManager.GetInstance.ActiveUser.UserId && p.Gender == gender && p.ismarried == 0 && p.UserType.ToUpper() != "ADMIN") && p.Age >= minAge && p.Age <= maxAge && p.Qualification.Trim().ToLower()==QualificationId.Trim().ToLower() && arr.Contains(p.HeightId.Value));
                    }
                    //5
                    else if (maxAge > 0 && minAge == 0 && castId > 0 && !string.IsNullOrEmpty(QualificationId) && arr != null && arr.Length > 0 && string.IsNullOrEmpty(Income) && string.IsNullOrEmpty(JobLocation))
                    {
                        lst = objUser.GetUserDetailsForUser().Where(p => (p.UserId != SessionManager.GetInstance.ActiveUser.UserId && p.Gender == gender && p.ismarried == 0 && p.UserType.ToUpper() != "ADMIN") && p.CasteId == castId && p.Age <= maxAge && p.Qualification.Trim().ToLower()==QualificationId.Trim().ToLower() && arr.Contains(p.HeightId.Value));
                    }
                    //6
                    else if (maxAge == 0 && minAge > 0 && castId > 0 && !string.IsNullOrEmpty(QualificationId) && arr != null && arr.Length > 0 && string.IsNullOrEmpty(Income) && string.IsNullOrEmpty(JobLocation))
                    {
                        lst = objUser.GetUserDetailsForUser().Where(p => (p.UserId != SessionManager.GetInstance.ActiveUser.UserId && p.Gender == gender && p.ismarried == 0 && p.UserType.ToUpper() != "ADMIN") && p.CasteId == castId && p.Age >= minAge && p.Qualification.Trim().ToLower()==QualificationId.Trim().ToLower() && arr.Contains(p.HeightId.Value));
                    }
                    //7
                    else if (maxAge > 0 && minAge > 0 && castId > 0 && string.IsNullOrEmpty(QualificationId) && arr == null && string.IsNullOrEmpty(Income) && string.IsNullOrEmpty(JobLocation))
                    {
                        lst = objUser.GetUserDetailsForUser().Where(p => (p.UserId != SessionManager.GetInstance.ActiveUser.UserId && p.Gender == gender && p.ismarried == 0 && p.UserType.ToUpper() != "ADMIN") && p.Age >= minAge && p.Age <= maxAge && p.CasteId == castId);
                    }
                    //8
                    else if (maxAge > 0 && minAge > 0 && castId == 0 && string.IsNullOrEmpty(QualificationId) && arr != null && arr.Length > 0 && string.IsNullOrEmpty(Income) && string.IsNullOrEmpty(JobLocation))
                    {
                        lst = objUser.GetUserDetailsForUser().Where(p => (p.UserId != SessionManager.GetInstance.ActiveUser.UserId && p.Gender == gender && p.ismarried == 0 && p.UserType.ToUpper() != "ADMIN") && p.Age >= minAge && p.Age <= maxAge && arr.Contains(p.HeightId.Value));
                    }
                    //9
                    else if (maxAge > 0 && minAge == 0 && castId == 0 && !string.IsNullOrEmpty(QualificationId) && arr != null && arr.Length > 0 && string.IsNullOrEmpty(Income) && string.IsNullOrEmpty(JobLocation))
                    {
                        lst = objUser.GetUserDetailsForUser().Where(p => (p.UserId != SessionManager.GetInstance.ActiveUser.UserId && p.Gender == gender && p.ismarried == 0 && p.UserType.ToUpper() != "ADMIN") && p.Qualification.Trim().ToLower()==QualificationId.Trim().ToLower() && p.Age <= maxAge && arr.Contains(p.HeightId.Value));
                    }
                    //10
                    else if (maxAge == 0 && minAge == 0 && castId > 0 && !string.IsNullOrEmpty(QualificationId) && arr != null && arr.Length > 0 && string.IsNullOrEmpty(Income) && string.IsNullOrEmpty(JobLocation))
                    {
                        lst = objUser.GetUserDetailsForUser().Where(p => (p.UserId != SessionManager.GetInstance.ActiveUser.UserId && p.Gender == gender && p.ismarried == 0 && p.UserType.ToUpper() != "ADMIN") && p.Qualification.Trim().ToLower()==QualificationId.Trim().ToLower() && p.CasteId == castId && arr.Contains(p.HeightId.Value));
                    }
                    //11
                    else if (maxAge > 0 && minAge > 0 && castId == 0 && string.IsNullOrEmpty(QualificationId) && arr == null && string.IsNullOrEmpty(Income) && string.IsNullOrEmpty(JobLocation))
                    {
                        lst = objUser.GetUserDetailsForUser().Where(p => (p.UserId != SessionManager.GetInstance.ActiveUser.UserId && p.Gender == gender && p.ismarried == 0 && p.UserType.ToUpper() != "ADMIN") && p.Age >= minAge && p.Age <= maxAge);
                    }
                    //12
                    else if (maxAge > 0 && minAge == 0 && castId == 0 && string.IsNullOrEmpty(QualificationId) && arr != null && arr.Length > 0 && string.IsNullOrEmpty(Income) && string.IsNullOrEmpty(JobLocation))
                    {
                        lst = objUser.GetUserDetailsForUser().Where(p => (p.UserId != SessionManager.GetInstance.ActiveUser.UserId && p.Gender == gender && p.ismarried == 0 && p.UserType.ToUpper() != "ADMIN") && p.Age >= minAge && arr.Contains(p.HeightId.Value));
                    }
                    //13
                    else if (maxAge > 0 && minAge == 0 && castId == 0 && !string.IsNullOrEmpty(QualificationId) && arr == null && string.IsNullOrEmpty(Income) && string.IsNullOrEmpty(JobLocation))
                    {
                        lst = objUser.GetUserDetailsForUser().Where(p => (p.UserId != SessionManager.GetInstance.ActiveUser.UserId && p.Gender == gender && p.ismarried == 0 && p.UserType.ToUpper() != "ADMIN") && p.Age <= maxAge && p.Qualification.Trim().ToLower()==QualificationId.Trim().ToLower());
                    }
                    //14
                    else if (maxAge == 0 && minAge == 0 && castId > 0 && !string.IsNullOrEmpty(QualificationId) && arr == null && string.IsNullOrEmpty(Income) && string.IsNullOrEmpty(JobLocation))
                    {
                        lst = objUser.GetUserDetailsForUser().Where(p => (p.UserId != SessionManager.GetInstance.ActiveUser.UserId && p.Gender == gender && p.ismarried == 0 && p.UserType.ToUpper() != "ADMIN") && p.CasteId == castId && p.Qualification.Trim().ToLower()==QualificationId.Trim().ToLower());
                    }
                    //15
                    else if (maxAge >= 0 && minAge == 0 && castId == 0 && string.IsNullOrEmpty(QualificationId) && arr == null && string.IsNullOrEmpty(Income) && string.IsNullOrEmpty(JobLocation))
                    {
                        lst = objUser.GetUserDetailsForUser().Where(p => (p.UserId != SessionManager.GetInstance.ActiveUser.UserId && p.Gender == gender && p.ismarried == 0 && p.UserType.ToUpper() != "ADMIN") && p.Age <= maxAge);
                    }
                    //16
                    else if (maxAge == 0 && minAge > 0 && castId == 0 && string.IsNullOrEmpty(QualificationId) && arr == null && string.IsNullOrEmpty(Income) && string.IsNullOrEmpty(JobLocation))
                    {
                        lst = objUser.GetUserDetailsForUser().Where(p => (p.UserId != SessionManager.GetInstance.ActiveUser.UserId && p.Gender == gender && p.ismarried == 0 && p.UserType.ToUpper() != "ADMIN") && p.Age >= minAge);
                    }
                    // 17
                    else if (maxAge == 0 && minAge == 0 && castId > 0 && string.IsNullOrEmpty(QualificationId) && arr == null && string.IsNullOrEmpty(Income) && string.IsNullOrEmpty(JobLocation))
                    {
                        lst = objUser.GetUserDetailsForUser().Where(p => (p.UserId != SessionManager.GetInstance.ActiveUser.UserId && p.Gender == gender && p.ismarried == 0 && p.UserType.ToUpper() != "ADMIN") && p.CasteId == castId);
                    }
                    // 18
                    else if (maxAge == 0 && minAge == 0 && castId == 0 && !string.IsNullOrEmpty(QualificationId) && arr == null && string.IsNullOrEmpty(Income) && string.IsNullOrEmpty(JobLocation))
                    {
                        lst = objUser.GetUserDetailsForUser().Where(p => (p.UserId != SessionManager.GetInstance.ActiveUser.UserId && p.Gender == gender && p.ismarried == 0 && p.UserType.ToUpper() != "ADMIN") && p.Qualification.Trim().ToLower()==QualificationId.Trim().ToLower());
                    }
                    //19
                    else if (maxAge == 0 && minAge == 0 && castId == 0 && !string.IsNullOrEmpty(QualificationId) && arr == null && string.IsNullOrEmpty(Income) && string.IsNullOrEmpty(JobLocation))
                    {
                        lst = objUser.GetUserDetailsForUser().Where(p => (p.UserId != SessionManager.GetInstance.ActiveUser.UserId && p.Gender == gender && p.ismarried == 0 && p.UserType.ToUpper() != "ADMIN") && p.Qualification.Trim().ToLower()==QualificationId.Trim().ToLower());
                    }
                    // 20
                    else if (maxAge == 0 && minAge == 0 && castId == 0 && !string.IsNullOrEmpty(QualificationId) && arr != null && arr.Length > 0 && string.IsNullOrEmpty(Income) && string.IsNullOrEmpty(JobLocation))
                    {
                        lst = objUser.GetUserDetailsForUser().Where(p => (p.UserId != SessionManager.GetInstance.ActiveUser.UserId && p.Gender == gender && p.ismarried == 0 && p.UserType.ToUpper() != "ADMIN") && arr.Contains(p.HeightId.Value));
                    }
                }
                catch (Exception)
                {
                }
                ResponseModel response = new ResponseModel()
                {
                    DataResponse = lst,
                    Status = true
                };
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetUserDetails(string prefix)
        {
            string gender = SessionManager.GetInstance.ActiveUser.Gender == "M" ? "F" : "M";
            bool? IsActive = null;
            if (prefix.ToUpper()=="NEW USER")
            {
                IsActive = false;
            }
            if (SessionManager.GetInstance.ActiveUser.UserType.ToUpper()=="ADMIN")
            {
                var lst = objUser.Select_STP_GetUserDetails();
                if (IsActive!=null)
                {
                    lst = lst.Where(p => p.IsActive == true);
                }
                else
                {
                    lst = lst.Where(p => p.UserType.ToUpper()=="USER" && p.FirstName.Contains(prefix) 
                    || p.LName.Contains(prefix) || (p.FirstName + " " + p.LName).Contains(prefix) || p.CastName.Contains(prefix) && p.ismarried == 0);
                }
                ResponseModel response = new ResponseModel()
                {
                    DataResponse = lst,
                    Status = true
                };
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var lst = objUser.GetUserDetailsForUser().Where(p => (p.UserId != SessionManager.GetInstance.ActiveUser.UserId && p.Gender == gender && p.ismarried == 0 && p.UserType.ToUpper() != "ADMIN" && p.UserType.ToUpper()!="AGENT") && (p.FirstName.Contains(prefix) || p.LName.Contains(prefix) || (p.FirstName + " " + p.LName).Contains(prefix) || p.CastName.Contains(prefix)));
                ResponseModel response = new ResponseModel()
                {
                    DataResponse = lst,
                    Status = true
                };
                return Json(response, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult RemoveUser(int? UserId)
        {
            objUser.DeleteUser(UserId);
            ResponseModel response = new ResponseModel()
            {
                Status = true
            };
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Upload(int? UserId)
        {
            bool result = false;
            if (Request.Files.Count > 0)
            {
                string base64string = string.Empty;
                HttpPostedFileBase file = Request.Files[0];
                base64string = "Content/ProfileImage/" + SessionManager.GetInstance.ActiveUser.UserId + string.Format("{0:ddMMyyyyHHmmss}",DateTime.Now) +  ".jpg";
                if (UserId>0)
                {
                    base64string = "Content/ProfileImage/" + UserId + string.Format("{0:ddMMyyyyHHmmss}", DateTime.Now) + ".jpg";
                }
                file.SaveAs(Server.MapPath("~/"+ base64string));
                //model.Image1=imgArr;
                objUser.UploadImage(base64string, "P");
                if (string.IsNullOrEmpty(SessionManager.GetInstance.ActiveUser.Img1))
                {
                    SessionManager.GetInstance.ActiveUser.Img1 = base64string;
                }
                result = SessionManager.GetInstance.ActiveUser.IsActive==null?false: SessionManager.GetInstance.ActiveUser.IsActive.Value;
            }
            return Json(result);
        }



        [HttpPost]
        public ActionResult UploadA()
        {
            //bool result = false;
            string base64string = string.Empty;
            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase file = Request.Files[0];
                base64string = "Content/ProfileImage/" + SessionManager.GetInstance.ActiveUser.UserId + "_2.jpg";
                file.SaveAs(Server.MapPath("~/" + base64string));
                objUser.UploadImage(base64string, "M");
                SessionManager.GetInstance.ActiveUser.Img2 = base64string;
                //result = base64string;
            }
            return Json(base64string);
        }

        [HttpPost]
        public ActionResult UploadB()
        {
            //bool result = false;
            string base64string = string.Empty;
            if (Request.Files.Count > 0)
            {
                
                HttpPostedFileBase file = Request.Files[0];
                base64string = "Content/ProfileImage/" + SessionManager.GetInstance.ActiveUser.UserId + "_3.jpg";
                file.SaveAs(Server.MapPath("~/" + base64string));
                objUser.UploadImage(base64string, "K");
                SessionManager.GetInstance.ActiveUser.KImg = base64string;
                //result = true;
            }
            return Json(base64string);
        }
    }


    //public static class Queryable
    //{
    //    public static IQueryable<TSource> Where<TSource>(
    //        this IQueryable<TSource> source,
    //        Expression<Func<TSource, bool>> predicate)
    //    {
    //        return source.Provider.CreateQuery<TSource>(
    //            Expression.Call(
    //                null,
    //                ((MethodInfo)MethodBase.GetCurrentMethod()).MakeGenericMethod(
    //                    new Type[] { typeof(TSource) }),
    //                    new Expression[] { source.Expression, Expression.Quote(predicate) }));
    //    }
    //}

    
}