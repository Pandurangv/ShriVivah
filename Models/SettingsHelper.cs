using ShriVivah.Models.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ShriVivah.Models
{
    public static class SettingsHelper
    {

        public static string MailId { get { return ConfigurationManager.AppSettings["MailId"].ToString(); } }

        public static string Password { get { return ConfigurationManager.AppSettings["Password"].ToString(); } }

        public static string SMTP { get { return ConfigurationManager.AppSettings["SMTP"].ToString(); } }

        public static int Port { get { return Convert.ToInt32(ConfigurationManager.AppSettings["Port"].ToString()); } }


        public static LanguageResponse GetLanguageConfig 
        { 
            get {
                System.IO.StreamReader reader = new System.IO.StreamReader(HttpContext.Current.Server.MapPath("~/Content/MasterPage.json"));
                
                //string filedata = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~/Content/MasterPage.json"));
                string filedata = reader.ReadToEnd();
                LanguageResponse response = new JavaScriptSerializer().Deserialize<LanguageResponse>(filedata);
                return response;
            }
        }

        public static void SendMail(string mailId, string UserName = "",bool IsFeedback=false,string LoginId="",string Password="")
        {
            try
            {
                //System.Web.Mail.MailMessage message = new System.Web.Mail.MailMessage();
                //string fromEmail = "contact@varmalavivah.com";
                //string fromPW = "varmala753";
                //string toEmail = mailId;
                //const string SERVER = "relay-hosting.secureserver.net";
                //System.Web.Mail.MailMessage oMail = new System.Web.Mail.MailMessage();

                //oMail.From = fromEmail;
                //oMail.To = toEmail;
                //oMail.Subject = "Complete Profile";
                //if (IsFeedback)
                //{
                //    oMail.Subject = "Feedback";
                //}
                
                StringBuilder sb = new StringBuilder();
                sb.Append("<html><head></head><body><div><span>Hello " + UserName + ",</span><br/><span>Welcome to Varmalavivah.com.</span><br />");
                if (IsFeedback)
                {
                    sb.Append("<span>Thank you for your valuable feedback.We will try to improve our services. We will update you about our services through Varmalavivah.com.</span><br/>");
                    sb.Append("<span>Take a step towards meeting your Soulmate</span><br />");
                }
                else
                {
                    sb.Append("<span>Your registration was partially done. Please do login Using below credintials Login Id : ").Append(LoginId).Append(" And Password : ").Append(Password).Append(" and complete your profile");
                }
                sb.Append("<span>For matrimonial assistance,</span><br/>");
                sb.Append("<span>Call us at 8806369038</span><br />");
                sb.Append("<span>Timing: 10:00 am – 6:00pm(Monday to Saturday).Share your feedback with any query or comments on info@varmalavivah.com.</span><br />");
                sb.Append("<span>Best Regards</span><br/>");
                sb.Append("<span>www.varmalavivah.com</span>");
                sb.Append("<br/>Sent at: " + DateTime.Now);
                sb.Append("<div></body></html>");

                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                string fromEmail = "contact@varmalavivah.com";
                string fromPW = "Ganesh0511@";
                string toEmail = "truptikumbhar4@gmail.com";
                message.From = new MailAddress(fromEmail);
                message.To.Add(toEmail);
                message.Subject = "Complete Profile";
                if (IsFeedback)
                {
                    message.Subject = "Feedback";
                }

                message.Body = sb.ToString();
                message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                using (SmtpClient smtpClient = new SmtpClient("mail.varmalavivah.com", 25))
                {
                    smtpClient.EnableSsl = false;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    //smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new System.Net.NetworkCredential(fromEmail, fromPW);

                    smtpClient.Send(message.From.ToString(), message.To.ToString(),
                                    message.Subject, message.Body);
                }
                //oMail.BodyFormat = System.Web.Mail.MailFormat.Html; // enumeration
                //oMail.Priority = System.Web.Mail.MailPriority.High; // enumeration

                //oMail.Body = sb.ToString();
                //System.Web.Mail.SmtpMail.SmtpServer = SERVER;


                //System.Web.Mail.SmtpMail.Send(oMail);
                //oMail = null;	// free up resources
            }
            catch (Exception ex)
            {
            }
        }

        public static void SendMail(tblContactDetails model)
        {
            try
            {
                //System.Web.Mail.MailMessage message = new System.Web.Mail.MailMessage();
                //string fromEmail = "contact@varmalavivah.com";
                //string fromPW = "varmala753";
                //string toEmail = model.MailId;
                //const string SERVER = "relay-hosting.secureserver.net";
                //System.Web.Mail.MailMessage oMail = new System.Web.Mail.MailMessage();

                //oMail.From = fromEmail;
                //oMail.To = toEmail;
                ////oMail.Subject = "Complete Profile";
                //oMail.Subject = model.Name;

                StringBuilder sb = new StringBuilder();
                sb.Append("<html><head></head><body><div><span>Hello ,</span><br/><span>Welcome to Varmalavivah.com.</span><br />");
                sb.Append("<span>" + model.Description + "</span><br/>");
                sb.Append("<span>Please do login with your registered mail id.</span><br/>");
                sb.Append("<span>For matrimonial assistance,</span><br/>");
                sb.Append("<span>Call us at 8806369038</span><br />");
                sb.Append("<span>Timing: 10:00 am – 6:00pm(Monday to Saturday).Share your feedback with any query or comments on info@varmalavivah.com.</span><br />");
                sb.Append("<span>Best Regards</span><br/>");
                sb.Append("<span>varmalavivah.com</span>");
                sb.Append("<br/>Sent at: " + DateTime.Now);
                sb.Append("<div></body></html>");
                ////oMail.Body = 
                //oMail.BodyFormat = System.Web.Mail.MailFormat.Html; // enumeration
                //oMail.Priority = System.Web.Mail.MailPriority.High; // enumeration

                //oMail.Body = sb.ToString();
                //System.Web.Mail.SmtpMail.SmtpServer = SERVER;


                //System.Web.Mail.SmtpMail.Send(oMail);

                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
                string fromEmail = "contact@varmalavivah.com";
                string fromPW = "Ganesh0511@";
                string toEmail = "truptikumbhar4@gmail.com";
                message.From = new System.Net.Mail.MailAddress(fromEmail);
                message.To.Add(toEmail);
                message.Subject = "Hello";

                message.Body = sb.ToString();
                message.DeliveryNotificationOptions = System.Net.Mail.DeliveryNotificationOptions.OnFailure;

                using (System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient("mail.varmalavivah.com", 25))
                {
                    smtpClient.EnableSsl = false;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    //smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new System.Net.NetworkCredential(fromEmail, fromPW);

                    smtpClient.Send(message.From.ToString(), message.To.ToString(),
                                    message.Subject, message.Body);
                }
                //oMail = null;	// free up resources
            }
            catch (Exception ex)
            {
            }
        }

        public static void WriteLog(Exception ex, string Method)
        {
            try
            {
                 
            }
            catch (Exception resp)
            {
            }
        }
    }

    public class LanguageResponse
    {
        public List<LanguageOptions> menuprefix { get; set; }
    }


    public class LanguageOptions
    {
        public int id { get; set; }

        public string Alias { get; set; }

        public string MenuType { get; set; }

        public string Lan { get; set; }
    }

    public class MyAuthorizeAttribute : ActionFilterAttribute
    {

        public bool IsAdmin { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = filterContext.HttpContext.Session;
            if (session["ActiveUser"] != null)
            {
                tblUser user = (tblUser)filterContext.HttpContext.Session["ActiveUser"];
                if (user.UserType.Equals("Admin", StringComparison.CurrentCultureIgnoreCase)==false && IsAdmin)
                {
                    if (filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        filterContext.Result = new JsonResult
                        {
                            Data = new
                            {
                                Message = "your server session expired. you were logged out."
                            },
                            JsonRequestBehavior = JsonRequestBehavior.AllowGet
                        };
                    }
                    else
                    {//Redirect him to somewhere.
                        var redirectTarget = new System.Web.Routing.RouteValueDictionary(new { action = "Index", controller = "Home", area = "" });
                        filterContext.Result = new RedirectToRouteResult(redirectTarget);
                    }
                }
                else
                {
                    return; 
                }
                
            }
            else
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = new JsonResult
                    {
                        Data = new
                        {
                            Message = "your server session expired. you were logged out."
                        },
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else
                {//Redirect him to somewhere.
                    var redirectTarget = new System.Web.Routing.RouteValueDictionary(new { action = "Index", controller = "Home", area = "" });
                    filterContext.Result = new RedirectToRouteResult(redirectTarget);
                }
            }
            
        }

        
    }
}