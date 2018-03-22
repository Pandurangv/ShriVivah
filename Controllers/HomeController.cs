using ShriVivah.Models;
using ShriVivah.Models.ContextModel;
using ShriVivah.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ShriVivah.Controllers
{
    public class HomeController : Controller
    {
        public int PageSize { get { return 10; } }

        public int CurentPageIndex { get; set; }

        ShriVivah.Models.Entities.ShreeVivahDbContext objData = new Models.Entities.ShreeVivahDbContext();

        public ActionResult ReadMessage(int toUserId)
        {
            var objuser=new UserContextModel();
            var data = objuser.Select_STP_SelectMessageUser(toUserId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Married()
        {
            this.loadViewBag();
            return View("Married");
        }

        public ActionResult GetAllMessgaes(int? UserId, int? ToUserId)
        {
            var response= objData.tblMessages.Where(p => (p.FromUserId == UserId || p.ToUserId == UserId) && (p.FromUserId == ToUserId || p.ToUserId == ToUserId));
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            this.loadViewBag();
            return View();
        }

        public ActionResult Contact()
        {
            this.loadViewBag();
            return View();
        }

        public ActionResult PrivacyPolicy()
        {
            return View("PrivacyPolicy");
        }

        public ActionResult MailTest()
        {
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
            string fromEmail = "contact@varmalavivah.com";
            string fromPW = "Ganesh0511@";
            string toEmail = "truptikumbhar4@gmail.com";
            message.From = new MailAddress(fromEmail);
            message.To.Add(toEmail);
            message.Subject = "Hello";
            message.Body = "Hello Bob ";
            message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            using (SmtpClient smtpClient = new SmtpClient("mail.varmalavivah.com", 25))
            {
                smtpClient.EnableSsl = true;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                //smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(fromEmail, fromPW);
                
                smtpClient.Send(message.From.ToString(), message.To.ToString(),
                                message.Subject, message.Body);
            }
            return View("MailTest");
        }

        public ActionResult MailTest1()
        {
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
            string fromEmail = "contact@varmalavivah.com";
            string fromPW = "Ganesh0511@";
            string toEmail = "truptikumbhar4@gmail.com";
            message.From = new MailAddress(fromEmail);
            message.To.Add(toEmail);
            message.Subject = "Hello";
            
            message.Body = "Hello Bob ";
            message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            using (SmtpClient smtpClient = new SmtpClient("mail.varmalavivah.com", 25))
            {
                smtpClient.EnableSsl = false;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                //smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(fromEmail, fromPW);

                smtpClient.Send(message.From.ToString(), message.To.ToString(),
                                message.Subject, message.Body);
            }
            return View("MailTest");
        }

        public ActionResult MailTest2()
        {
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
            string fromEmail = "contact@varmalavivah.com";
            string fromPW = "Ganesh0511@";
            string toEmail = "truptikumbhar4@gmail.com";
            message.From = new MailAddress(fromEmail);
            message.To.Add(toEmail);
            message.Subject = "Hello";
            message.Body = "Hello Bob ";
            message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            using (SmtpClient smtpClient = new SmtpClient("mail.varmalavivah.com", 25))
            {
                smtpClient.EnableSsl = true;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                //smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(fromEmail, fromPW);

                smtpClient.Send(message.From.ToString(), message.To.ToString(),
                                message.Subject, message.Body);
            }
            return View("MailTest");
        }

        public ActionResult MailTest3()
        {
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
            string fromEmail = "contact@varmalavivah.com";
            string fromPW = "Ganesh0511@";
            string toEmail = "truptikumbhar4@gmail.com";
            message.From = new MailAddress(fromEmail);
            message.To.Add(toEmail);
            message.Subject = "Hello";
            message.Body = "Hello Bob ";
            message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            using (SmtpClient smtpClient = new SmtpClient("mail.varmalavivah.com", 25))
            {
                smtpClient.EnableSsl = false;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                //smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(fromEmail, fromPW);

                smtpClient.Send(message.From.ToString(), message.To.ToString(),
                                message.Subject, message.Body);
            }
            return View("MailTest");
        }

        public ActionResult MailTest5()
        {
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
            string fromEmail = "contact@varmalavivah.com";
            string fromPW = "Admin.varm@147";
            string toEmail = "truptikumbhar4@gmail.com";
            message.From = new MailAddress(fromEmail);
            message.To.Add(toEmail);
            message.Subject = "Hello";
            message.Body = "Hello Bob ";
            message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            using (SmtpClient smtpClient = new SmtpClient("mail.varmalavivah.com", 25))
            {
                smtpClient.EnableSsl = true;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                //smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(fromEmail, fromPW);

                smtpClient.Send(message.From.ToString(), message.To.ToString(),
                                message.Subject, message.Body);
            }
            return View("MailTest");
        }

        public ActionResult MailTest6()
        {
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
            string fromEmail = "contact@varmalavivah.com";
            string fromPW = "Ganesh0511@";
            string toEmail = "truptikumbhar4@gmail.com";
            message.From = new MailAddress(fromEmail);
            message.To.Add(toEmail);
            message.Subject = "Hello";
            message.Body = "Hello Bob ";
            message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            using (SmtpClient smtpClient = new SmtpClient("mail.varmalavivah.com", 25))
            {
                smtpClient.EnableSsl = false;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                //smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(fromEmail, fromPW);

                smtpClient.Send(message.From.ToString(), message.To.ToString(),
                                message.Subject, message.Body);
            }
            return View("MailTest");
        }

        public ActionResult MailTest7()
        {
            System.Web.Mail.MailMessage message = new System.Web.Mail.MailMessage();
            string fromEmail = "contact@varmalavivah.com";
            string fromPW = "Ganesh0511@";
            string toEmail = "truptikumbhar4@gmail.com";
            const string SERVER = "mail.varmalavivah.com";
            System.Web.Mail.MailMessage oMail = new System.Web.Mail.MailMessage();
            
            oMail.From = fromEmail;
            oMail.To = toEmail;
            oMail.Subject = "Test email subject";
            oMail.BodyFormat = System.Web.Mail.MailFormat.Html;	// enumeration
            oMail.Priority = System.Web.Mail.MailPriority.High;	// enumeration
            message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", 587);
            oMail.Body = "Sent at: " + DateTime.Now;
            System.Web.Mail.SmtpMail.SmtpServer = SERVER;
            
            System.Web.Mail.SmtpMail.Send(oMail); 
            oMail = null;	// free up resources
            return View("MailTest");
        }

        public ActionResult Terms()
        {
            this.loadViewBag();
            return View("Terms");
        }

        UserContextModel objUser = new UserContextModel();

        public ActionResult Search()
        {
            this.loadViewBag();
            RegisterViewModel reguser=SessionManager.GetInstance.SearchUser;
            IQueryable<STP_GetUserDetail> userdetails = null; 
            if (reguser!=null)
            {
                if (SessionManager.GetInstance.ActiveUser != null)
                {
                    userdetails = objUser.GetUserDetailsForUser().Where(p => p.UserId != SessionManager.GetInstance.ActiveUser.UserId && p.UserType.ToUpper()!="ADMIN");
                }
                else
                {
                    userdetails = objUser.GetUserDetailsForUser().Where(p => p.UserType.ToUpper() != "ADMIN");
                }
                SessionManager.GetInstance.SearchUser = null;
            }
            else
            {
                if (SessionManager.GetInstance.ActiveUser!=null)
                {
                    string gender=SessionManager.GetInstance.ActiveUser.Gender == "M" ? "F" : "M";
                    userdetails = objUser.GetUserDetailsForUser().Where(p => p.UserId != SessionManager.GetInstance.ActiveUser.UserId && p.Gender == gender && p.UserType.ToUpper() != "ADMIN");
                }
                else
                {
                    userdetails = objUser.GetUserDetailsForUser().Where(p => p.UserType.ToUpper() != "ADMIN");
                }
            }
            var filter = userdetails.OrderBy(p => p.UserId).Skip(0 * PageSize).Take(PageSize);
            Session["pageindex"] = 0;
            Session["users"] = userdetails;
            return View("Search",filter);
        }

        public ActionResult LoadMore()
        {
            string gender = "";
            if (SessionManager.GetInstance.ActiveUser!=null)
            {
                gender = SessionManager.GetInstance.ActiveUser.Gender == "M" ? "F" : "M";    
            }
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
                        ErrorMessage = "आणखी माहिती उपलब्ध नाही"
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                Session["pageindex"] = 0;
                IQueryable<STP_GetUserDetail> userdetails = null;
                if (!string.IsNullOrEmpty(gender))
                {
                    userdetails = objUser.GetUserDetailsForUser().Where(p => p.UserId != SessionManager.GetInstance.ActiveUser.UserId && p.Gender == gender && p.UserType.ToUpper() != "ADMIN");    
                }
                else
                {
                    userdetails = objUser.GetUserDetailsForUser().Where(p => p.UserType.ToUpper() != "ADMIN");
                }
                Session["users"] = userdetails;
                var filter = userdetails.OrderBy(p => p.UserId).Skip(0 * PageSize).Take(PageSize);
                return View("UserRequests", filter);
            }
        }

        

        [HttpPost]
        public ActionResult SaveContactUs(tblContactDetails tbl)
        {
            bool test= objUser.Save(tbl);
            SettingsHelper.SendMail(tbl.MailId,tbl.Name,true);
            return Json(test, JsonRequestBehavior.AllowGet);
        }

        

        public ActionResult SendRequest(int RequestUserId)
        {
            if (SessionManager.GetInstance.ActiveUser!=null)
            {
                ResponseModel model = new ResponseModel()
                {
                    ErrorMessage = "Your request has been sent for approval.",
                    Status = false,
                };
                bool Success=false;
                var requestedUser = objUser.Select_STP_GetUserDetails().Where(p => p.UserId == RequestUserId).FirstOrDefault();
                if (requestedUser.Gender.ToUpper()==SessionManager.GetInstance.ActiveUser.Gender.ToUpper())
                {
                    Success = false;
                    model.ErrorMessage = "Not able to send request.";
                }
                else
                {
                     Success= objUser.SendRequest(RequestUserId, SessionManager.GetInstance.ActiveUser.UserId);
                     if (Success==false)
                     {
                         model.ErrorMessage = "Request already sent for approval.";
                     }
                }
                model.Status = Success;
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new ResponseModel() { Status = false, ErrorMessage = "Please do login before send request." });
            }
        }

        //public ActionResult SearchUser(int ReligionId, int CastId, int OrasId, int QualificationId,string Gender,bool IsSearchPage)
        //{
        //    if (IsSearchPage)
        //    {
        //        var userdetails = objUser.Select_STP_GetUserDetails(ReligionId, CastId, OrasId, QualificationId, Gender);
        //        return Json(userdetails, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        RegisterViewModel reguser = new RegisterViewModel()
        //        {
        //            ReligionId = ReligionId,
        //            CasteId = CastId,
        //            OrasId = OrasId,
        //            QualificationId = QualificationId,
        //            Gender = Gender,
        //        };
        //        SessionManager.GetInstance.SearchUser = reguser;
        //        return Json(true, JsonRequestBehavior.AllowGet);
        //    }
        //}
    }

    public static class LoadViewData
    {
        public static void loadViewBag(this ControllerBase target)
        {
            target.ViewBag.VendorTypes = Newtonsoft.Json.JsonConvert.SerializeObject(new VendorTypeContextModel().GetVendorTypes());
            target.ViewBag.IsLogin = false;
            if (SessionManager.GetInstance.ActiveUser != null)
            {
                target.ViewBag.IsLogin = true;
                target.ViewBag.Gender = SessionManager.GetInstance.ActiveUser.Gender;
            }
            ReligionModel objReligion = new ReligionModel();
            var lst = objReligion.GetReligions();
            List<SelectListItem> lstData = (from tbl in lst
                                            select new SelectListItem { Text = tbl.ReligionName, Value = tbl.ReligionId.ToString() }).ToList();

            lstData.Insert(0, new SelectListItem() { Text = "---Select Religion---", Value = "0" });
            target.ViewBag.ReligionId = lstData;
            OrasModel oras = new OrasModel();
            lstData = (from tbl in oras.GetOrass()
                       select new SelectListItem { Text = tbl.OrasName, Value = tbl.OrasId.ToString() }).ToList();

            lstData.Insert(0, new SelectListItem() { Text = "---Select Rashi---", Value = "0" });

            target.ViewBag.OrasId = lstData;
            
            BloodGroupModel bg = new BloodGroupModel();

            lstData = (from tbl in bg.GetEducations()
                       select new SelectListItem { Text = tbl.DegreeName, Value = tbl.QualificationId.ToString() }).ToList();

            lstData.Insert(0, new SelectListItem() { Text = "---Select Qualification---", Value = "0" });

            CastContextModel objC = new CastContextModel();
            lstData = (from tbl in objC.GetCasts()
                       select new SelectListItem { Text = tbl.CastName, Value = tbl.CastId.ToString() }).ToList();

            lstData.Insert(0, new SelectListItem() { Text = "---जात निवडा---", Value = "0" });
            target.ViewBag.CasteId = lstData;

            target.ViewBag.QualificationId = lstData;
        }

        public static void LoadIsAdmin(this ControllerBase target)
        {
            try
            {
                target.ViewBag.IsAdmin = false;
                var user = new UserContextModel();
                var users = user.Select_STP_GetUserDetails();
                target.ViewBag.Gender = "M";
                target.ViewBag.TotalUsers = users.Where(p=>p.UserType.ToUpper()=="USER").Count();
                var dt = DateTime.Now.Date.AddDays(-15);
                target.ViewBag.MarriedBrideCount = users.Where(p => p.Gender.ToUpper() == "F" && p.UserType.ToUpper() == "USER" && p.ismarried==1).Count();
                target.ViewBag.MarriedGroomCount = users.Where(p => p.Gender.ToUpper() == "M" && p.UserType.ToUpper() == "USER" && p.ismarried == 1).Count();
                target.ViewBag.BridesCount = users.Where(p => p.Gender.ToUpper() == "F" && p.UserType.ToUpper() == "USER").Count();
                target.ViewBag.BridesCount = target.ViewBag.BridesCount - target.ViewBag.MarriedBrideCount;
                target.ViewBag.GroomCount = users.Where(p => p.Gender.ToUpper() == "F" && p.UserType.ToUpper() == "USER").Count();
                target.ViewBag.GroomCount = target.ViewBag.GroomCount - target.ViewBag.MarriedGroomCount;
                
                //target.ViewBag.NewRegisterd = users.Where(p => dt <= p.DateofReg.Value.Date).Count();
                target.ViewBag.UserId = SessionManager.GetInstance.ActiveUser.UserId;
                target.ViewBag.UserName = "";
                if (SessionManager.GetInstance.ActiveUser != null)
                {
                    bool IsAdmin = SessionManager.GetInstance.ActiveUser.UserType.Equals("User") == true ? false : true;
                    target.ViewBag.IsAdmin = IsAdmin;
                    target.ViewBag.UserName = SessionManager.GetInstance.ActiveUser.FirstName + " " + SessionManager.GetInstance.ActiveUser.LName;
                    if (IsAdmin == false)
                    {
                        target.ViewBag.Gender = SessionManager.GetInstance.ActiveUser.Gender;
                    }
                    target.ViewBag.VisitorCount = user.GetVisitors().Count();
                }
            }
            catch (Exception)
            {
            }
        }
    }
}