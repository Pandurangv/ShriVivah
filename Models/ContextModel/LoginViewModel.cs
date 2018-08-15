using ShriVivah.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShriVivah.Models.ContextModel
{
    public class LoginDetailsViewModel
    {
        public int LoginId { get; set; }

        public DateTime? LoginDate { get; set; }

        public string LoginIP { get; set; }

        public string Location { get; set; }

        public string RequestDetails { get; set; }

        public int? UserId { get; set; }

        public string UserName { get; set; }

        public string MobileNo { get; set; }
    }

    public class LoginResponse:Error
    {
        public IQueryable<LoginDetailsViewModel> LoginDetails { get; set; }
    }

    public class LoginBLL
    {
        public ShreeVivahDbContext ObjData { get; set; }

        public LoginBLL()
        {
            ObjData = new ShreeVivahDbContext();
        }

        public LoginResponse GetLoginDetails()
        {
            LoginResponse response = new LoginResponse();
            response.LoginDetails = (from tbl in ObjData.LoginDetailss
                                     join tbluser in ObjData.tblUsers
                                     on tbl.UserId equals tbluser.UserId
                                     select new LoginDetailsViewModel
                                     {
                                         Location=tbl.Location,
                                         LoginDate=tbl.LoginDate,
                                         LoginIP=tbl.LoginIP,
                                         MobileNo=tbluser.MobileNo,
                                         UserId=tbl.UserId,
                                         UserName=tbluser.FirstName + " " + tbluser.LName
                                     });
            return response;
        }
    }
}