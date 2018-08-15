using ShriVivah.Models;
using ShriVivah.Models.ContextModel;
using ShriVivah.Models.DataModels;
using ShriVivah.Models.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShriVivah.Controllers
{
    public class BarrierController : Controller
    {

        public PackageContextModel objPackage { get; set; }


        public BarrierController()
        {
            objPackage = new PackageContextModel();
        }

        // GET: Barrier
        [MyAuthorizeAttribute(IsAdmin = true)]
        [CustomView]
        public ActionResult Index()
        {
            this.LoadIsAdmin();
            this.loadViewBag();
            return View("Index");
        }

        public ActionResult GetBarriers()
        {
            return Json(objPackage.GetPackages(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Save(PackageViewModel model)
        {
            return Json(objPackage.Save(model),JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Update(PackageViewModel model)
        {
            return Json(objPackage.Save(model), JsonRequestBehavior.AllowGet);
        }
    }
}