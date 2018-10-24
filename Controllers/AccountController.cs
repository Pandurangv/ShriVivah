using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

using Microsoft.Owin.Security;
using ShriVivah.Models;
using System.Web.Security;
using ShriVivah.Models.ContextModel;
using ShriVivah.Models.Entities;
using System.Resources;
using System.Text;
using ShriVivah.Models.Filters;

namespace ShriVivah.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        public AccountController()
        {
            objUser = new UserContextModel();
        }

        UserContextModel objUser = null;

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ForgotPassword(LoginViewModel model)
        { 
            var user = objUser.GetUser().Where(p => p.UserName.ToUpper() == model.UserName.ToUpper()).FirstOrDefault();
            if (SettingsManager.Instance.Branding=="SINDHI")
            {
                user = objUser.GetUser().Where(p => p.MobileNo.ToUpper() == model.MobileNo.ToUpper()).FirstOrDefault();
            }
            if (user != null)
            {
                if (!string.IsNullOrEmpty(user.MailId))
                {
                    bool status = false;
                    StringBuilder sb = new StringBuilder();
                    if (SettingsManager.Instance.Branding == "SINDHI")
                    {
                        sb.Append("Please do login Using below credintials Login Id : ").Append(user.MobileNo).Append(" And Password : ").Append(user.Password).Append(" and complete your profile, ").Append(SettingsManager.Instance.SindhuRegards);
                        string message = HttpUtility.UrlEncode(sb.ToString());
                        objUser.SendUserSMS(user.MobileNo, message);
                        status = true;
                    }
                    else
                    {
                        status= objUser.SendMail(user);
                    }
                    if (status)
                    {
                        return Json(new ResponseModel() { Status = true, ErrorMessage = SettingsManager.Instance.Branding == "SINDHI" ? Resources.SPMOResources.ForgotPassword : "तुमचा पासवर्ड तुमच्या मेल वरती पाठवला आहे, तुमचा नवीन पासवर्ड वापरून लॉगिन करा " }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new ResponseModel() { Status = false, ErrorMessage = SettingsManager.Instance.Branding == "SINDHI" ? Resources.SPMOResources.ContactAdmin : "कृपया व्यवस्थापनाशी संपर्क करा." }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new ResponseModel() { Status = false, ErrorMessage = SettingsManager.Instance.Branding == "SINDHI" ? Resources.SPMOResources.ContactAdmin : "कृपया व्यवस्थापनाशी संपर्क करा." }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new ResponseModel() { Status = false, ErrorMessage = SettingsManager.Instance.Branding == "SINDHI" ? Resources.SPMOResources.InvalidUserName : "अवैध लॉग-इन नाव." }, JsonRequestBehavior.AllowGet);
            }
        }
        
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel model)
        {
            STP_GetUserDetail user = null;
            if (SettingsManager.Instance.Branding!="SINDHI")
            {
                user = objUser.Select_STP_GetUserDetails().Where(p => p.UserName.ToUpper() == model.UserName.ToUpper() && p.Password.ToUpper() == model.Password.ToUpper() && p.ismarried == 0).FirstOrDefault();
            }
            else
            {
                user=objUser.Select_STP_GetUserDetails().Where(p => p.MobileNo.ToUpper() == model.MobileNo.ToUpper() && p.Password.ToUpper() == model.Password.ToUpper() && p.ismarried == 0).FirstOrDefault();
            }
            if (user!=null)
            {
                string IP= Request.ServerVariables["REMOTE_ADDR"];
                var objLoginDetails = new LoginDetails()
                {
                    Location = "",
                    LoginDate = DateTime.Now,
                    LoginIP = IP,
                    UserId = user.UserId,
                };
                objUser.SaveLoginDetails(objLoginDetails);
                if (Convert.ToBoolean(user.IsActive) && user.UserType.ToUpper() != "AGENT")
                {
                    SessionManager.GetInstance.ActiveUser = user;
                    var lst = new List<RegisterViewModel>();
                    lst.Add(new RegisterViewModel() { UserId = user.UserId, FirstName = user.FirstName, Gender = user.Gender,UserType=user.UserType });
                    return Json(new ResponseModel() { Status = true, ErrorMessage = "", DataResponse = lst.AsQueryable() }, JsonRequestBehavior.AllowGet);
                }
                else if (!Convert.ToBoolean(user.IsActive) && user.UserType.ToUpper() == "USER" && user.DateOfBirth==null)
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
                        lst.Add(new RegisterViewModel() { UserId = user.UserId, FirstName = user.FirstName, Gender = user.Gender,UserType = "ADMIN" });
                        return Json(new ResponseModel() { Status = true, ErrorMessage = "", DataResponse = lst.AsQueryable() }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        if (user.UserType.ToUpper() == "AGENT")
                        {
                            if (SettingsManager.Instance.Branding == "SINDHI")
                            {
                                SessionManager.GetInstance.ActiveUser = user;
                                var lst = new List<RegisterViewModel>();
                                lst.Add(new RegisterViewModel() { UserId = user.UserId, FirstName = user.FirstName, Gender = user.Gender,UserType= "AGENT" });
                                return Json(new ResponseModel() { Status = true, ErrorMessage = "", DataResponse = lst.AsQueryable() }, JsonRequestBehavior.AllowGet);
                            }
                            else
                            {
                                return Json(new ResponseModel() { Status = false, ErrorMessage = SettingsManager.Instance.Branding == "SINDHI" ? Resources.SPMOResources.AgentLogin : "प्रतिनिधी लॉगिन साठी अद्याप सुविधा नाहीत." }, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {
                            return Json(new ResponseModel() { Status = false, ErrorMessage = SettingsManager.Instance.Branding == "SINDHI" ? Resources.SPMOResources.AccountActivate : "आपले खाते 48 तासांच्या आत सक्रिय होईल ." }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
            }
            else
            {
                return Json(new ResponseModel() { Status = false, ErrorMessage = SettingsManager.Instance.Branding=="SINDHI"? Resources.SPMOResources.InvalidUserNamePassword :"अवैध लॉग-इन नाव किंवा पासवर्ड." }, JsonRequestBehavior.AllowGet);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        [CustomView]
        public ActionResult Register()
        {
            UserContextModel objUser = new UserContextModel();
            this.loadViewBag();
            CountryModel objCountry = new CountryModel();
            var lst = objCountry.GetCountrys();
            List<SelectListItem> lstData = (from tbl in lst
                                            select new SelectListItem { Text = tbl.CountryName, Value = tbl.CountryId.ToString() }).ToList();

            lstData.Insert(0, new SelectListItem() { Text = "---Select Country---", Value = "0" });
            ViewBag.CountryId = lstData;
            List<SelectListItem> lstPanchayat = (from tbl in objUser.GetPanchayatList()
                   select new SelectListItem
                   {
                       Value = tbl.UserId.ToString(),
                       Text = tbl.FirstName + " " + tbl.LName + " " + tbl.City + " " + tbl.Address + " " + tbl.State + " " + tbl.PanchayatCode
                   }).ToList();
            ViewBag.AgentId = lstPanchayat;
            return View("Register");
        }

        public ActionResult MailConfirmation(bool OTP)
        {
            ViewBag.OTPVerify = OTP;
            return View("Register");
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult VerifyOTP(string OTP)
        {
            STP_GetUserDetail user = (STP_GetUserDetail)Session["beforeVerify"];
            if (user!=null)
            {
                ResponseModel test = objUser.VerifyUserOTP(user, OTP);
                SessionManager.GetInstance.ActiveUser = user;
                return Json(test, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new ResponseModel() { Status=false,ErrorMessage=""}, JsonRequestBehavior.AllowGet);
            }
        }

        
        public ActionResult GenerateOTP()
        { 
            RegisterViewModel user= (RegisterViewModel)Session["beforeVerify"];
            if (user != null)
            {
                objUser.SendOTP(user);
                return Json(new ResponseModel() { Status = false, ErrorMessage = "" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new ResponseModel() { Status = false, ErrorMessage = "" }, JsonRequestBehavior.AllowGet);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult BasicDetails()
        {
            OrasModel oras = new OrasModel();
            CastContextModel objC = new CastContextModel();
            ReligionModel objReligion = new ReligionModel();
            CountryModel objCountry = new CountryModel();
            StateModel objState = new StateModel();
            BloodGroupModel objB = new BloodGroupModel();
            BasicResponse response = new BasicResponse()
            {
                CastData = objC.GetCasts(),
                OrasData = oras.GetOrass(),
                BloodGroup = objB.GetBloodGroups(),
                CountryData = objCountry.GetCountrys(),
                HeightData = oras.GetHeights(),
                ReligionData=objReligion.GetReligions(),
                StateList=objState.GetStates(),
                
            };
            return Json(response,JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult RegisterAndroidUser(RegisterReuest model)
        {
            objUser.SaveAndroidUser(model);
            var userdetails = objUser.Select_STP_GetUserDetails().Where(p => p.UserType.ToUpper() != "ADMIN" && p.UserType.ToUpper() != "AGENT" && p.Gender.ToUpper().Trim()!=model.UserData.Gender.ToUpper().Trim() && p.ismarried==0);
            return Json(userdetails,JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult RegisterUserForOTP(RegisterViewModel model)
        {
            return Json(objUser.SendSMS(null,model));
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult RegisterUser(RegisterViewModel model)
        {
            tblUser user = null;
            if (SettingsManager.Instance.Branding=="SINDHI")
            {
                user = objUser.GetUser().Where(p => p.MobileNo.ToUpper() == model.MobileNo.ToUpper()).FirstOrDefault();
                if (user == null)
                {
                    model.RoleName = "User";
                    int userid = objUser.Save(model);
                    if (userid > 0)
                    {
                        user = objUser.GetUser().Where(p => p.UserName.ToUpper() == model.UserName.ToUpper()).FirstOrDefault();
                        model.UserId = userid;
                        PendingUsersController pending = new PendingUsersController();
                        pending.SendSMS(userid);
                        SessionManager.GetInstance.ActiveUser = new STP_GetUserDetail() {UserId=user.UserId,Password=user.Password,FirstName=user.FirstName,MName=user.MName,LName=user.LName,MailId=user.MailId,MobileNo=user.MobileNo,UserType=user.UserType,Gender=model.Gender };

                        //objUser.SendOTP(model);
                        return Json(new ResponseModel() { Status = true, ErrorMessage = SettingsManager.Instance.Branding == "SINDHI" ? Resources.SPMOResources.RegistrationPage : "उमेदवार माहिती यशस्वीपणे जतन केले आहे." }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new ResponseModel() { Status = false, ErrorMessage = SettingsManager.Instance.Branding == "SINDHI" ? Resources.SPMOResources.RegistrationFailed : "उमेदवार नोंदणी करणे अशक्य." }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new ResponseModel() { Status = false, ErrorMessage = SettingsManager.Instance.Branding == "SINDHI" ? Resources.SPMOResources.SameUserExist : "लॉग-इन नाव आधीपासून दुसर्या उमेदवार वापरले." }, JsonRequestBehavior.AllowGet);
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
                        user = objUser.GetUser().Where(p => p.UserName.ToUpper() == model.UserName.ToUpper()).FirstOrDefault();
                        model.UserId = userid;
                        PendingUsersController pending = new PendingUsersController();
                        pending.SendSMS(userid);
                        SessionManager.GetInstance.ActiveUser = new STP_GetUserDetail() { UserId = user.UserId, Password = user.Password, FirstName = user.FirstName, MName = user.MName, LName = user.LName, MailId = user.MailId, MobileNo = user.MobileNo, Gender = user.Gender }; 

                        //objUser.SendOTP(model);
                        return Json(new ResponseModel() { Status = true, ErrorMessage = SettingsManager.Instance.Branding == "SINDHI" ? Resources.SPMOResources.RegistrationPage : "उमेदवार माहिती यशस्वीपणे जतन केले आहे." }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new ResponseModel() { Status = false, ErrorMessage = SettingsManager.Instance.Branding == "SINDHI" ? Resources.SPMOResources.RegistrationFailed : "उमेदवार नोंदणी करणे अशक्य." }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new ResponseModel() { Status = false, ErrorMessage = SettingsManager.Instance.Branding == "SINDHI" ? Resources.SPMOResources.SameUserExist : "लॉग-इन नाव आधीपासून दुसर्या उमेदवार वापरले." }, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [AllowAnonymous]
        public ActionResult ValidateUserName(string username)
        {
            tblUser user = objUser.GetUser().Where(p => p.UserName.ToUpper() == username.ToUpper()).FirstOrDefault();
            if (user==null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        [AllowAnonymous]
        public ActionResult LogOff()
        {
            SessionManager.GetInstance.ActiveUser = null;
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }


        
    }

}