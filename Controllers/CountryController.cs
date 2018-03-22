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
    public class CountryController : Controller
    {
        public CountryModel objReligion { get; set; }
        
        public int PageSize { get { return 5; } }
        public int CurentPageIndex { get; set; }

        public CountryController()
        {
            objReligion = new CountryModel();
        }
        // GET: Country
        // GET: BloodGroup
        [MyAuthorizeAttribute(IsAdmin = true)]
        public ActionResult Index()
        {
            this.LoadIsAdmin();
            var countries = objReligion.GetCountrys();
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.CountryId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            return View("Index", filter);
        }

        public ActionResult CountryFirst()
        {
            IQueryable<tblCountry> users = (IQueryable<tblCountry>)Session["users"];
            int pageindex = 0;
            var filter = users.OrderBy(p => p.CountryId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = users;
            Session["pageindex"] = 0;
            CountryDetails obj = new CountryDetails()
            {
                Status = true,
                CountryList = filter
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(string prefix)
        {
            var countries = objReligion.GetCountrys().Where(p => p.CountryName.ToUpper() == prefix.ToUpper());
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.CountryId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            if (filter.Count() > 0)
            {
                CountryDetails obj = new CountryDetails()
                {
                    Status = true,
                    CountryList = filter
                };
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            else
            {
                CountryDetails obj = new CountryDetails()
                {
                    Status = false,
                    ErrorMessage = "आणखी माहिती उपलब्ध नाही"
                };
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Reset()
        {
            var countries = objReligion.GetCountrys();
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.CountryId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            return Json(filter, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CountryNext()
        {
            IQueryable<tblCountry> users = (IQueryable<tblCountry>)Session["users"];
            if (users != null)
            {
                int pageindex = Convert.ToInt32(Session["pageindex"]);
                pageindex++;
                var filter = users.OrderBy(p => p.CountryId).Skip(pageindex * PageSize).Take(PageSize);
                if (filter.Count() > 0)
                {
                    Session["pageindex"] = pageindex;
                    CountryDetails obj = new CountryDetails()
                    {
                        Status = true,
                        ErrorMessage = "",
                        CountryList = filter
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                    //return Json(filter, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    CountryDetails obj = new CountryDetails()
                    {
                        Status = false,
                        ErrorMessage = "आणखी माहिती उपलब्ध नाही"
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return CountryFirst();
            }
        }

        public ActionResult CountryPrev()
        {
            IQueryable<tblCountry> users = (IQueryable<tblCountry>)Session["users"];

            if (users != null)
            {
                int pageindex = Convert.ToInt32(Session["pageindex"]);
                if (pageindex > 0)
                {
                    pageindex--;
                    var filter = users.OrderBy(p => p.CountryId).Skip(pageindex * PageSize).Take(PageSize);
                    Session["pageindex"] = pageindex;
                    CountryDetails obj = new CountryDetails()
                    {
                        Status = true,
                        ErrorMessage = "",
                        CountryList = filter,
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    CountryDetails obj = new CountryDetails()
                    {
                        Status = false,
                        ErrorMessage = "तुम्ही पहिल्याच पानावर आहात.",
                    };
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return CountryFirst();
            }
        }

        public ActionResult CountryLast()
        {
            var users = objReligion.GetCountrys();
            CountryDetails obj = new CountryDetails();
            int pageindex = Convert.ToInt32(Session["pageindex"]);
            pageindex++;
            obj.Status = true;
            if (users != null)
            {
                Session["pageindex"] = pageindex;
                if ((users.Count() % PageSize) == 0)
                {
                    obj.CountryList = users.OrderBy(p => p.CountryId).Skip(users.Count() - 2).Take(PageSize);
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    int rem = users.Count() % PageSize;
                    obj.CountryList = users.OrderBy(p => p.CountryId).Skip(users.Count() - rem).Take(PageSize);
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return CountryFirst();
            }
        }

        public ActionResult AddCountry(tblCountry model)
        {
            var countries = objReligion.GetCountrys();
            var test = countries.Where(p => p.CountryName.ToUpper() == model.CountryName.ToUpper()).FirstOrDefault();
            CountryDetails obj = new CountryDetails();
            if (test != null)
            {
                obj.Status = false;
                obj.ErrorMessage = "हि माहिती आधीपासून उपलब्ध आहे.";
            }
            else
            {
                obj.Status = true;
                obj.ErrorMessage = "माहिती सेव केली आहे.";
                objReligion.Save(model);
            }
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.CountryId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            obj.CountryList = filter;
            return Json(obj, JsonRequestBehavior.AllowGet);
        }



        public ActionResult Update(tblCountry model)
        {
            var countries = objReligion.GetCountrys();
            var test = countries.Where(p => p.CountryName.ToUpper() == model.CountryName.ToUpper()).FirstOrDefault();
            CountryDetails obj = new CountryDetails();
            if (test != null)
            {
                obj.Status = false;
                obj.ErrorMessage = "हि माहिती आधीपासून उपलब्ध आहे.";
            }
            else
            {
                obj.Status = true;
                obj.ErrorMessage = "माहितीमध्ये बदल करण्यात आला आहे.";
                objReligion.Update(model);
            }
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.CountryId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            obj.CountryList = filter;
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(int BGId)
        {
            tblCountry tbl = objReligion.GetCountrys().Where(p => p.CountryId == BGId).FirstOrDefault();
            return Json(tbl, JsonRequestBehavior.AllowGet);
        }
    }
}