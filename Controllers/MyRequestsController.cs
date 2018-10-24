using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShriVivah.Models;
using ShriVivah.Models.ContextModel;
using ShriVivah.Models.Entities;
using System.Web.Script.Serialization;
using ShriVivah.Models.Filters;

namespace ShriVivah.Controllers
{
    public class MyRequestsController : Controller
    {
        UserContextModel objUser = new UserContextModel();
        // GET: MyRequests
        [CustomView]
        [MyAuthorize(IsAdmin =false)]
        public ActionResult Index()
        {
            return View("Index");
        }

        [HttpPost]
        public ActionResult SearchRequests(SearchRequestModel model)
        {
            ResponseModel obj = new ResponseModel();
            obj.DataResponse = objUser.Select_SP_GetRequests(model);
            return Json(obj,JsonRequestBehavior.AllowGet);
        }
    }
}