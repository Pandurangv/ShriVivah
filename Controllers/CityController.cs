using ShriVivah.Models;
using ShriVivah.Models.ContextModel;
using ShriVivah.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShriVivah.Controllers
{
    public class CityController : Controller
    {
        public CityContextModel objCity { get; set; }
        
        public int PageSize { get { return 5; } }
        public int CurentPageIndex { get; set; }

        public CityController()
        {
            objCity = new CityContextModel();
        }
        // GET: City
        [MyAuthorizeAttribute(IsAdmin = true)]
        public ActionResult Index()
        {
            this.LoadIsAdmin();
            CountryModel objCountry = new CountryModel();
            var lst = objCountry.GetCountrys();
            List<SelectListItem> lstData = (from tbl in lst
                                            select new SelectListItem { Text = tbl.CountryName, Value = tbl.CountryId.ToString() }).ToList();

            lstData.Insert(0, new SelectListItem() { Text = "---देश निवडा---", Value = "0" });
            ViewBag.CountryId = lstData;
            lstData = new List<SelectListItem>();
            lstData.Add(new SelectListItem() { Text = "---राज्य निवडा---", Value = "0" });
            ViewBag.StateId = lstData;
            var countries = objCity.GetCities();
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.CityId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            return View("Index", filter);
        }

        public ActionResult CityFirst()
        {
            IQueryable<CityModel> users = (IQueryable<CityModel>)Session["users"];
            int pageindex = 0;
            var filter = users.OrderBy(p => p.CityId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = users;
            Session["pageindex"] = 0;
            CityDetails obj = new CityDetails()
            {
                Status = true,
                CityList = filter
            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(string prefix)
        {
            var countries = objCity.GetCities().Where(p => p.StateName.Contains(prefix) || p.CityName.Contains(prefix) || p.CountryName.Contains(prefix));
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.CityId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            if (filter.Count() > 0)
            {
                CityDetails obj = new CityDetails()
                {
                    Status = true,
                    CityList = filter
                };
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            else
            {
                CityDetails obj = new CityDetails()
                {
                    Status = false,
                    ErrorMessage = "आणखी माहिती उपलब्ध नाही"
                };
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Reset()
        {
            var countries = objCity.GetCities();
            int pageindex = 0;
            var filter = countries.OrderBy(p => p.CityId).Skip(pageindex * PageSize).Take(PageSize);
            Session["users"] = countries;
            Session["pageindex"] = 0;
            return Json(filter, JsonRequestBehavior.AllowGet);
        }
    }
}