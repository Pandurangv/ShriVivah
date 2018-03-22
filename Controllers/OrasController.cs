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
    public class OrasController : Controller
    {
        public OrasModel objOras { get; set; }
        
        public int PageSize { get { return 5; } }
        public int CurentPageIndex { get; set; }

        public OrasController()
        {
            objOras = new OrasModel();
        }
        // GET: Gan
        [MyAuthorizeAttribute(IsAdmin = true)]
        public ActionResult Index()
        {
            this.LoadIsAdmin();
            var countries = objOras.GetOrass();
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.OrasId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            return View("Index", filter);
        }

        public ActionResult OrasFirst()
        {
            IQueryable<tblOras> users = (IQueryable<tblOras>)Session["users"];
            int pageindex = 0;
            var filter = users.OrderBy(p => p.OrasId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = users;
            Session["pageindex"] = 0;
            OrasDetails obj = new OrasDetails()
            {
                Status = true,
                OrasList = filter
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(string prefix)
        {
            var countries = objOras.GetOrass().Where(p => p.OrasName.ToUpper() == prefix.ToUpper());
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.OrasId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            if (filter.Count() > 0)
            {
                OrasDetails obj = new OrasDetails()
                {
                    Status = true,
                    OrasList = filter
                };
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            else
            {
                OrasDetails obj = new OrasDetails()
                {
                    Status = false,
                    ErrorMessage = "आणखी माहिती उपलब्ध नाही."
                };
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Reset()
        {
            var countries = objOras.GetOrass();
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.OrasId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            return Json(filter, JsonRequestBehavior.AllowGet);
        }

        public ActionResult OrasNext()
        {
            IQueryable<tblOras> users = (IQueryable<tblOras>)Session["users"];
            if (users != null)
            {
                int pageindex = Convert.ToInt32(Session["pageindex"]);
                pageindex++;
                var filter = users.OrderBy(p => p.OrasId).Skip(pageindex * PageSize).Take(PageSize);
                if (filter.Count() > 0)
                {
                    Session["pageindex"] = pageindex;
                    OrasDetails obj = new OrasDetails()
                    {
                        Status = true,
                        ErrorMessage = "",
                        OrasList = filter
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                    //return Json(filter, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    OrasDetails obj = new OrasDetails()
                    {
                        Status = false,
                        ErrorMessage = "आणखी माहिती उपलब्ध नाही"
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return OrasFirst();
            }
        }

        public ActionResult OrasPrev()
        {
            IQueryable<tblOras> users = (IQueryable<tblOras>)Session["users"];

            if (users != null)
            {
                int pageindex = Convert.ToInt32(Session["pageindex"]);
                if (pageindex > 0)
                {
                    pageindex--;
                    var filter = users.OrderBy(p => p.OrasId).Skip(pageindex * PageSize).Take(PageSize);
                    Session["pageindex"] = pageindex;
                    OrasDetails obj = new OrasDetails()
                    {
                        Status = true,
                        ErrorMessage = "",
                        OrasList = filter,
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    OrasDetails obj = new OrasDetails()
                    {
                        Status = false,
                        ErrorMessage = "तुम्ही पहिल्याच पानावर आहात",
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return OrasFirst();
            }
        }

        public ActionResult OrasLast()
        {
            var users = objOras.GetOrass();
            OrasDetails obj = new OrasDetails();
            int pageindex = Convert.ToInt32(Session["pageindex"]);
            pageindex++;
            obj.Status = true;
            if (users != null)
            {
                Session["pageindex"] = pageindex;
                if ((users.Count() % PageSize) == 0)
                {
                    obj.OrasList = users.OrderBy(p => p.OrasId).Skip(users.Count() - 2).Take(PageSize);
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    int rem = users.Count() % PageSize;
                    obj.OrasList = users.OrderBy(p => p.OrasId).Skip(users.Count() - rem).Take(PageSize);
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return OrasFirst();
            }
        }

        public ActionResult AddOras(tblOras model)
        {
            var countries = objOras.GetOrass();
            var test = countries.Where(p => p.OrasName.ToUpper() == model.OrasName.ToUpper()).FirstOrDefault();
            OrasDetails obj = new OrasDetails();
            if (test != null)
            {
                obj.Status = false;
                obj.ErrorMessage = "हि माहिती आधीपासून उपलब्ध आहे.";
            }
            else
            {
                obj.Status = true;
                obj.ErrorMessage = "माहिती सेव केली आहे.";
                objOras.Save(model);
            }
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.OrasId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            obj.OrasList = filter;
            return Json(obj, JsonRequestBehavior.AllowGet);
        }



        public ActionResult Update(tblOras model)
        {
            var countries = objOras.GetOrass();
            var test = countries.Where(p => p.OrasName.ToUpper() == model.OrasName.ToUpper()).FirstOrDefault();
            OrasDetails obj = new OrasDetails();
            if (test != null)
            {
                obj.Status = false;
                obj.ErrorMessage = "हि माहिती आधीपासून उपलब्ध आहे.";
            }
            else
            {
                obj.Status = true;
                obj.ErrorMessage = "माहिती सेव केली आहे.";
                objOras.Update(model);
            }
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.OrasId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            obj.OrasList = filter;
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int BGId)
        {
            tblOras tbl = objOras.GetOrass().Where(p => p.OrasId == BGId).FirstOrDefault();
            return Json(tbl, JsonRequestBehavior.AllowGet);
        }
    }
}