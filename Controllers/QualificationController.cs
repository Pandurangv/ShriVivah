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
    public class QualificationController : Controller
    {
        public BloodGroupModel objOras { get; set; }
        
        public int PageSize { get { return 5; } }
        public int CurentPageIndex { get; set; }

        public QualificationController()
        {
            objOras = new BloodGroupModel();
        }

        [MyAuthorizeAttribute(IsAdmin =true)]
        public ActionResult Educations()
        {
            return Json(objOras.GetEducations(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Education()
        {
            return Json(objOras.GetEducations(), JsonRequestBehavior.AllowGet);
        }

        // GET: Gan
        [MyAuthorizeAttribute(IsAdmin = true)]
        public ActionResult Index()
        {
            this.LoadIsAdmin();
            var countries = objOras.GetEducations();
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.QualificationId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            return View("Index", filter);
        }

        public ActionResult QualificationFirst()
        {
            IQueryable<tblQualification> users = (IQueryable<tblQualification>)Session["users"];
            int pageindex = 0;
            var filter = users.OrderBy(p => p.QualificationId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = users;
            Session["pageindex"] = 0;
            QualificationDetails obj = new QualificationDetails()
            {
                Status = true,
                QualificationList = filter
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(string prefix)
        {
            var countries = objOras.GetEducations().Where(p => p.DegreeName.ToUpper() == prefix.ToUpper());
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.QualificationId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            if (filter.Count() > 0)
            {
                QualificationDetails obj = new QualificationDetails()
                {
                    Status = true,
                    QualificationList = filter
                };
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            else
            {
                QualificationDetails obj = new QualificationDetails()
                {
                    Status = false,
                    ErrorMessage = "No Records to display"
                };
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Reset()
        {
            var countries = objOras.GetEducations();
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.QualificationId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            return Json(filter, JsonRequestBehavior.AllowGet);
        }

        public ActionResult QualificationNext()
        {
            IQueryable<tblQualification> users = (IQueryable<tblQualification>)Session["users"];
            if (users != null)
            {
                int pageindex = Convert.ToInt32(Session["pageindex"]);
                pageindex++;
                var filter = users.OrderBy(p => p.QualificationId).Skip(pageindex * PageSize).Take(PageSize);
                if (filter.Count() > 0)
                {
                    Session["pageindex"] = pageindex;
                    QualificationDetails obj = new QualificationDetails()
                    {
                        Status = true,
                        ErrorMessage = "",
                        QualificationList = filter
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                    //return Json(filter, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    QualificationDetails obj = new QualificationDetails()
                    {
                        Status = false,
                        ErrorMessage = "आणखी माहिती उपलब्ध नाही"
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return QualificationFirst();
            }
        }

        public ActionResult QualificationPrev()
        {
            IQueryable<tblQualification> users = (IQueryable<tblQualification>)Session["users"];

            if (users != null)
            {
                int pageindex = Convert.ToInt32(Session["pageindex"]);
                if (pageindex > 0)
                {
                    pageindex--;
                    var filter = users.OrderBy(p => p.QualificationId).Skip(pageindex * PageSize).Take(PageSize);
                    Session["pageindex"] = pageindex;
                    QualificationDetails obj = new QualificationDetails()
                    {
                        Status = true,
                        ErrorMessage = "",
                        QualificationList = filter,
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    QualificationDetails obj = new QualificationDetails()
                    {
                        Status = false,
                        ErrorMessage = "You are already on first page",
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return QualificationFirst();
            }
        }

        public ActionResult QualificationLast()
        {
            var users = objOras.GetEducations();
            QualificationDetails obj = new QualificationDetails();
            int pageindex = Convert.ToInt32(Session["pageindex"]);
            pageindex++;
            obj.Status = true;
            if (users != null)
            {
                Session["pageindex"] = pageindex;
                if ((users.Count() % PageSize) == 0)
                {
                    obj.QualificationList = users.OrderBy(p => p.QualificationId).Skip(users.Count() - 2).Take(PageSize);
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    int rem = users.Count() % PageSize;
                    obj.QualificationList = users.OrderBy(p => p.QualificationId).Skip(users.Count() - rem).Take(PageSize);
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return QualificationFirst();
            }
        }

        public ActionResult AddOras(tblQualification model)
        {
            var countries = objOras.GetEducations();
            var test = countries.Where(p => p.DegreeName.ToUpper() == model.DegreeName.ToUpper()).FirstOrDefault();
            QualificationDetails obj = new QualificationDetails();
            if (test != null)
            {
                obj.Status = false;
                obj.ErrorMessage = "Record already exist.";
            }
            else
            {
                obj.Status = true;
                obj.ErrorMessage = "Record saved successfully.";
                objOras.Save(model);
            }
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.QualificationId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            obj.QualificationList = filter;
            return Json(obj, JsonRequestBehavior.AllowGet);
        }



        public ActionResult Update(tblQualification model)
        {
            var countries = objOras.GetEducations();
            var test = countries.Where(p => p.DegreeName.ToUpper() == model.DegreeName.ToUpper()).FirstOrDefault();
            QualificationDetails obj = new QualificationDetails();
            if (test != null)
            {
                obj.Status = false;
                obj.ErrorMessage = "Record already exist.";
            }
            else
            {
                obj.Status = true;
                obj.ErrorMessage = "Record updated successfully.";
                objOras.Update(model);
            }
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.QualificationId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            obj.QualificationList = filter;
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int BGId)
        {
            tblQualification tbl = objOras.GetEducations().Where(p => p.QualificationId == BGId).FirstOrDefault();
            return Json(tbl, JsonRequestBehavior.AllowGet);
        }
    }
}