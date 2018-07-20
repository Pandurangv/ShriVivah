using ShriVivah.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShriVivah.Controllers
{
    
    public class UserRequestController : Controller
    {
        VendorContextModel objRequests = new VendorContextModel();
        public UserRequestController()
        {

        }
        // GET: UserRequest
        [MyAuthorizeAttribute(IsAdmin = false)]
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult GetVendorRequest()
        //{

        //}
    }
}