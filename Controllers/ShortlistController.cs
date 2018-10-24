using ShriVivah.Models;
using ShriVivah.Models.ContextModel;
using ShriVivah.Models.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShriVivah.Controllers
{
    public class ShortlistController : Controller
    {
        UserContextModel objUser = new UserContextModel();
        // GET: Shortlist9
        [MyAuthorize]
        [CustomView]
        public ActionResult Index()
        {
            return View("Index");
        }

        [HttpPost]
        public ActionResult GetShortlistUser(SearchRequestModel model)
        {
            ResponseModel obj = new ResponseModel();
            obj.DataResponse = objUser.Select_SP_GetShortlists(model);
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
    }
}