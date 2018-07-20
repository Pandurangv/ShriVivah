using ShriVivah.Models.ContextModel;
using ShriVivah.Models.DataModels;
using ShriVivah.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShriVivah.Models
{
    public class EventManagementContextModel
    {
        ShreeVivahDbContext objData = new ShreeVivahDbContext();
        UserContextModel objUser = new UserContextModel();


        internal IQueryable<EventManagementModel> GetEvents()
        {
            //var agents = objUser.GetAgentDetails().ToList();
            var query = (from c in objData.EventManagements
                         join user in objData.tblUsers
                         on c.OrganizedBy equals user.UserId
                         where c.IsDelete==false
                         orderby c.EventDate descending
                         select new EventManagementModel
                         {
                             ContactPerson=c.ContactPerson,
                             EventDate=c.EventDate,
                             EventId =c.EventId,
                             EventDistrict=c.EventDistrict,
                             EventLocation=c.EventLocation,
                             EventName=c.EventName,
                             EventState=c.EventState,
                             IsDelete=c.IsDelete,
                             MobileNo=c.MobileNo,
                             OrganizedBy=c.OrganizedBy,
                             OrganizerName=user.FirstName + " " + user.LName  
                             
                         });
            return query;
        }

        public List<EventManagementModel> GetFutureEvents()
        {
            var dt = DateTime.Now.Date;
            return (from tbl in GetEvents() 
                    where dt < tbl.EventDate
                    select tbl).ToList();
        }

        
        //internal IQueryable<UserRequests_Vendor> GetVendorsSPMO(int? vendorTypeID, string VendorType, string SearchCity, string SearchText)
        //{
        //    var query = (from data in objData.viewUserRequests_Vendor
        //                 where data.IsApproved == true
        //                 select data).ToList();
        //    if (vendorTypeID != 0 || Convert.ToString(VendorType) != "")
        //        query = query.Where(s => s.VendorTypeId == vendorTypeID || s.VendorType.Contains(VendorType)).ToList();
        //    if (string.IsNullOrEmpty(SearchCity) == false)
        //        query = query.Where(v => v.City.Contains(SearchCity)).ToList();
        //    if (string.IsNullOrEmpty(SearchText) == false)
        //    {
        //        query = query.Where(x => (Convert.ToString(x.Country) + " " + Convert.ToString(x.Address) + " " + Convert.ToString(x.OwnerName) + " " + Convert.ToString(x.Pincode)).ToLower().Contains(SearchText.ToLower())).ToList();
        //    }
        //    return query.AsQueryable();
        //}

        public int Save(EventManagementModel c)
        {
            EventManagement vendortype = new EventManagement()
            {
                IsDelete = false,
                ContactPerson = c.ContactPerson,
                EventDate = c.EventDate,
                EventId = c.EventId,
                EventDistrict = c.EventDistrict,
                EventLocation = c.EventLocation,
                EventName = c.EventName,
                EventState = c.EventState,
                MobileNo = c.MobileNo,
                OrganizedBy = c.OrganizedBy,
            };
            objData.EventManagements.Add(vendortype);
            return objData.SaveChanges();
        }

        
        internal void Update(EventManagementModel model)
        {
            EventManagement newobj = objData.EventManagements.Where(p => p.EventId == model.EventId).FirstOrDefault(); //GetVendorTypes().Where(p => p.VendorTypeId == model.VendorTypeId).FirstOrDefault();
            if (newobj != null)
            {
                newobj.ContactPerson = model.ContactPerson;
                newobj.EventDate = model.EventDate;
                newobj.EventDistrict = model.EventDistrict;
                newobj.EventLocation = model.EventLocation;
                newobj.EventName = model.EventName;
                newobj.EventState = model.EventState;
                newobj.MobileNo = model.MobileNo;
                newobj.OrganizedBy = model.OrganizedBy;
                objData.SaveChanges();
            }
        }

        

        
    }
}