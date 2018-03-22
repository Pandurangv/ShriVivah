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
    public class GanController : Controller
    {
        public GanModel objGan { get; set; }
        
        public int PageSize { get { return 5; } }
        public int CurentPageIndex { get; set; }

        public GanController()
        {
            objGan = new GanModel();
        }
        // GET: Gan
        [MyAuthorizeAttribute(IsAdmin = true)]
        public ActionResult Index()
        {
            this.LoadIsAdmin();
            var countries = objGan.GetGans();
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.GanId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            return View("Index", filter);
        }

        public ActionResult GanFirst()
        {
            IQueryable<tblGan> users = (IQueryable<tblGan>)Session["users"];
            int pageindex = 0;
            var filter = users.OrderBy(p => p.GanId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = users;
            Session["pageindex"] = 0;
            GanDetails obj = new GanDetails()
            {
                Status = true,
                GanList = filter
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(string prefix)
        {
            var countries = objGan.GetGans().Where(p => p.GanName.ToUpper() == prefix.ToUpper());
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.GanId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            if (filter.Count() > 0)
            {
                GanDetails obj = new GanDetails()
                {
                    Status = true,
                    GanList = filter
                };
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            else
            {
                GanDetails obj = new GanDetails()
                {
                    Status = false,
                    ErrorMessage = "No Records to display"
                };
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Reset()
        {
            var countries = objGan.GetGans();
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.GanId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            return Json(filter, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GanNext()
        {
            IQueryable<tblGan> users = (IQueryable<tblGan>)Session["users"];
            if (users != null)
            {
                int pageindex = Convert.ToInt32(Session["pageindex"]);
                pageindex++;
                var filter = users.OrderBy(p => p.GanId).Skip(pageindex * PageSize).Take(PageSize);
                if (filter.Count() > 0)
                {
                    Session["pageindex"] = pageindex;
                    GanDetails obj = new GanDetails()
                    {
                        Status = true,
                        ErrorMessage = "",
                        GanList = filter
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                    //return Json(filter, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    GanDetails obj = new GanDetails()
                    {
                        Status = false,
                        ErrorMessage = "आणखी माहिती उपलब्ध नाही"
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return GanFirst();
            }
        }

        public ActionResult GanPrev()
        {
            IQueryable<tblGan> users = (IQueryable<tblGan>)Session["users"];

            if (users != null)
            {
                int pageindex = Convert.ToInt32(Session["pageindex"]);
                if (pageindex > 0)
                {
                    pageindex--;
                    var filter = users.OrderBy(p => p.GanId).Skip(pageindex * PageSize).Take(PageSize);
                    Session["pageindex"] = pageindex;
                    GanDetails obj = new GanDetails()
                    {
                        Status = true,
                        ErrorMessage = "",
                        GanList = filter,
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    GanDetails obj = new GanDetails()
                    {
                        Status = false,
                        ErrorMessage = "You are already on first page",
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return GanFirst();
            }
        }

        public ActionResult GanLast()
        {
            var users = objGan.GetGans();
            GanDetails obj = new GanDetails();
            int pageindex = Convert.ToInt32(Session["pageindex"]);
            pageindex++;
            obj.Status = true;
            if (users != null)
            {
                Session["pageindex"] = pageindex;
                if ((users.Count() % PageSize) == 0)
                {
                    obj.GanList = users.OrderBy(p => p.GanId).Skip(users.Count() - 2).Take(PageSize);
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    int rem = users.Count() % PageSize;
                    obj.GanList = users.OrderBy(p => p.GanId).Skip(users.Count() - rem).Take(PageSize);
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return GanFirst();
            }
        }

        public ActionResult AddGan(tblGan model)
        {
            var countries = objGan.GetGans();
            var test = countries.Where(p => p.GanName.ToUpper() == model.GanName.ToUpper()).FirstOrDefault();
            GanDetails obj = new GanDetails();
            if (test != null)
            {
                obj.Status = false;
                obj.ErrorMessage = "Record already exist.";
            }
            else
            {
                obj.Status = true;
                obj.ErrorMessage = "Record saved successfully.";
                objGan.Save(model);
            }
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.GanId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            obj.GanList = filter;
            return Json(obj, JsonRequestBehavior.AllowGet);
        }



        public ActionResult Update(tblGan model)
        {
            var countries = objGan.GetGans();
            var test = countries.Where(p => p.GanName.ToUpper() == model.GanName.ToUpper()).FirstOrDefault();
            GanDetails obj = new GanDetails();
            if (test != null)
            {
                obj.Status = false;
                obj.ErrorMessage = "Record already exist.";
            }
            else
            {
                obj.Status = true;
                obj.ErrorMessage = "Record updated successfully.";
                objGan.Update(model);
            }
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.GanId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            obj.GanList = filter;
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int BGId)
        {
            tblGan tbl = objGan.GetGans().Where(p => p.GanId == BGId).FirstOrDefault();
            return Json(tbl, JsonRequestBehavior.AllowGet);
        }
    }
}