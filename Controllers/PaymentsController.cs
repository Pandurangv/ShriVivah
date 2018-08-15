using ShriVivah.Models.ContextModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShriVivah.Controllers
{
    public class PaymentsController : Controller
    {
        // GET: Payments
        [HttpPost]
        public ActionResult Index(PaytmResponse data)
        {
            return View("PaytmResponse", data);
        }
    }
}