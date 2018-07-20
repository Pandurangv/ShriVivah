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
    public class EventManagementController : Controller
    {
        EventManagementContextModel objEvents = new EventManagementContextModel();
        UserContextModel objUser = new UserContextModel();
        // GET: EventManagement
        [MyAuthorizeAttribute(IsAdmin = false)]
        [CustomView]
        public ActionResult Index()
        {
            this.LoadIsAdmin();
            StateModel objState = new StateModel();

            List<SelectListItem>  lst = (from tbl in objUser.GetAgentDetails()
                   select new SelectListItem
                   {
                       Value = tbl.UserId.ToString(),
                       Text = tbl.FirstName + " " + tbl.LName
                   }).ToList();

            lst.Insert(0, new SelectListItem() { Value = "0", Text = SettingsManager.Instance.Branding == "SPMO" ? "---Select Organizer---" : "----माहिती देणारा ----" });
            ViewBag.AgentId = lst;

            lst = (from tbl in objState.GetStates()
                   select new SelectListItem
                   {
                       Value = tbl.StateId.ToString(),
                       Text = tbl.StateName
                   }).ToList();

            lst.Insert(0, new SelectListItem() { Value = "0", Text = SettingsManager.Instance.Branding == "SPMO" ? "---Select State---" : "----माहिती देणारा ----" });
            ViewBag.StateId = lst;
            return View("Index");
        }

        public ActionResult GetFutureEvents()
        {
            EventResponse response = new EventResponse() { Status = true };
            response.EventList = objEvents.GetFutureEvents();
            //response.AgentList = objUser.GetAgentDetails().ToList();
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [MyAuthorizeAttribute(IsAdmin = false)]
        public ActionResult GetEvents()
        {
            EventResponse response = new EventResponse() { Status = true };
            response.EventList = objEvents.GetEvents().ToList();
            //response.AgentList = objUser.GetAgentDetails().ToList();
            return Json(response,JsonRequestBehavior.AllowGet);
        }

        [MyAuthorizeAttribute(IsAdmin = false)]
        [HttpPost]
        public ActionResult Save(EventManagementModel model)
        {
            EventResponse response = new EventResponse() { Status = false };
            if (objEvents.Save(model) > 0)
                response.Status = true;
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}