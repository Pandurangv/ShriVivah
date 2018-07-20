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
    public class MessageController : Controller
    {
        UserContextModel objUser = new UserContextModel();
        ShriVivah.Models.Entities.ShreeVivahDbContext objData = new Models.Entities.ShreeVivahDbContext();
        // GET: Message
        [MyAuthorizeAttribute(IsAdmin=false)]
        [CustomView]
        public ActionResult Index(int? UserId)
        {
            this.LoadIsAdmin();
            string gender = SessionManager.GetInstance.ActiveUser.Gender == "M" ? "F" : "M";
            List<SelectListItem> lst = new List<SelectListItem>();
            lst = (from tbl in objUser.Select_STP_GetUserDetails() 
                              where tbl.UserId != SessionManager.GetInstance.ActiveUser.UserId && tbl.Gender == gender && tbl.UserType.ToUpper() != "ADMIN"
                              select new SelectListItem
                              {
                                  Text=tbl.FirstName + " " + tbl.LName,
                                  Value=tbl.UserId.Value.ToString(),
                              }).ToList();
            lst.Insert(0, new SelectListItem() { Value="0",Text="----Select User----"});
            ViewBag.UserId = lst;
            ViewBag.SelectedUserId = UserId==null?0:UserId;
            ViewBag.ActiveUserId = SessionManager.GetInstance.ActiveUser.UserId;
            return View("Index");
        }

        public ActionResult SendMessage(int fromUserId,int toUserId,string msg)
        { 
            ShriVivah.Models.Entities.tblMessages objmsg = new Models.Entities.tblMessages() { 
                FromUserId=fromUserId,
                ToUserId=toUserId,
                MessageText=msg,
                IsDelete=false,
                MessageDate=DateTime.Now.Date
            };
            objData.tblMessages.Add(objmsg);
            objData.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
            //Clients.All.readMessage(ReadMessage(toUserId));
        }
        
    }

    
}