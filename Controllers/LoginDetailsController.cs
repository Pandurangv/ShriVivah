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
    public class LoginDetailsController : Controller
    {
        LoginBLL objLogin = new LoginBLL();
        public LoginDetailsController()
        {

        }
        // GET: LoginDetails
        [MyAuthorize]
        [CustomView]
        public ActionResult Index()
        {
            this.LoadIsAdmin();
            return View("Index");
        }


        public ActionResult GetLoginDetails()
        {
            return Json(objLogin.GetLoginDetails(), JsonRequestBehavior.AllowGet);
        }

    }
}