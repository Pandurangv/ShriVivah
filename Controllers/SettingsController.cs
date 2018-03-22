using ShriVivah.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShriVivah.Controllers
{
    public class SettingsController : Controller
    {
        // GET: Settings
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetMaratiTagPrefix()
        {
            var lst = SettingsHelper.GetLanguageConfig;
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
    }
}