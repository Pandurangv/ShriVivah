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
    public class FeedbackController : Controller
    {
        public FeedbackModel objReligion { get; set; }

        public int PageSize { get { return 10; } }
        public int CurentPageIndex { get; set; }
        // GET: Feedback
        public FeedbackController()
        {
            objReligion = new FeedbackModel();
        }
        // GET: Feedback
        // GET: BloodGroup
        [MyAuthorizeAttribute(IsAdmin = true)]
        public ActionResult Index()
        {
            this.LoadIsAdmin();
            var countries = objReligion.GetFeedbacks();
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.ContactUsId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            return View("Index", filter);
        }


        public ActionResult Search(string prefix)
        {
            var countries = objReligion.GetFeedbacks().Where(p => p.MailId.ToUpper() == prefix.ToUpper());
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.ContactUsId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            if (filter.Count() > 0)
            {
                FeedbackDetails obj = new FeedbackDetails()
                {
                    Status = true,
                    ContactList = filter
                };
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            else
            {
                FeedbackDetails obj = new FeedbackDetails()
                {
                    Status = false,
                    ErrorMessage = "आणखी माहिती उपलब्ध नाही"
                };
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Reset()
        {
            var countries = objReligion.GetFeedbacks();
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.ContactUsId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            return Json(filter, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int Id)
        {
            objReligion.Delete(Id);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Reply(tblContactDetails model)
        {
            SettingsHelper.SendMail(model);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FeedbackFirst()
        {
            IQueryable<tblContactDetails> users = (IQueryable<tblContactDetails>)Session["users"];
            int pageindex = 0;
            var filter = users.OrderBy(p => p.ContactUsId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = users;
            Session["pageindex"] = 0;
            FeedbackDetails obj = new FeedbackDetails()
            {
                Status = true,
                ContactList = filter
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult FeedbackNext()
        {
            IQueryable<tblContactDetails> users = (IQueryable<tblContactDetails>)Session["users"];
            if (users != null)
            {
                int pageindex = Convert.ToInt32(Session["pageindex"]);
                pageindex++;
                var filter = users.OrderBy(p => p.ContactUsId).Skip(pageindex * PageSize).Take(PageSize);
                if (filter.Count() > 0)
                {
                    Session["pageindex"] = pageindex;
                    FeedbackDetails obj = new FeedbackDetails()
                    {
                        Status = true,
                        ErrorMessage = "",
                        ContactList = filter
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                    //return Json(filter, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    FeedbackDetails obj = new FeedbackDetails()
                    {
                        Status = false,
                        ErrorMessage = "आणखी माहिती उपलब्ध नाही"
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return FeedbackFirst();
            }
        }

        public ActionResult FeedbackPrev()
        {
            IQueryable<tblContactDetails> users = (IQueryable<tblContactDetails>)Session["users"];

            if (users != null)
            {
                int pageindex = Convert.ToInt32(Session["pageindex"]);
                if (pageindex > 0)
                {
                    pageindex--;
                    var filter = users.OrderBy(p => p.ContactUsId).Skip(pageindex * PageSize).Take(PageSize);
                    Session["pageindex"] = pageindex;
                    FeedbackDetails obj = new FeedbackDetails()
                    {
                        Status = true,
                        ErrorMessage = "",
                        ContactList = filter,
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    FeedbackDetails obj = new FeedbackDetails()
                    {
                        Status = false,
                        ErrorMessage = "तुम्ही पहिल्याच पानावर आहात.",
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return FeedbackFirst();
            }
        }

        public ActionResult FeedbackLast()
        {
            var users = objReligion.GetFeedbacks();
            FeedbackDetails obj = new FeedbackDetails();
            int pageindex = Convert.ToInt32(Session["pageindex"]);
            pageindex++;
            obj.Status = true;
            if (users != null)
            {
                Session["pageindex"] = pageindex;
                if ((users.Count() % PageSize) == 0)
                {
                    obj.ContactList = users.OrderBy(p => p.ContactUsId).Skip(users.Count() - 2).Take(PageSize);
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    int rem = users.Count() % PageSize;
                    obj.ContactList = users.OrderBy(p => p.ContactUsId).Skip(users.Count() - rem).Take(PageSize);
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return FeedbackFirst();
            }
        }

        
    }
}