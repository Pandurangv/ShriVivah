using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShriVivah.Models.Entities;

namespace ShriVivah.Models.ContextModel
{
    public class VendorModel
    {
        public int? VendorId { get; set; }

        public string VendorName { get; set; }

        public string BusinessDescription { get; set; }

        public DateTime? RegistrationDate { get; set; }

        public string LogoImage { get; set; }

        public string ProfileImage { get; set; }

        public bool IsActive { get; set; }

        public string ProductImage { get; set; }

        public int ValidDays { get; set; }

        public int AgentId { get; set; }

        public tblUser Agent { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public string OwnerName { get; set; }

        public string Address { get; set; }

        public string ContactNo { get; set; }

        public string EmailId { get; set; }

        public string Pincode { get; set; }

        public bool IsDelete { get; set; }

        public int VendorTypeId { get; set; }

        public string VendorType { get; set; }

        

        public string City { get; set; }
        
        public string State { get; set; }

        public string District { get; set; }

        public string Taluka { get; set; }

        public string Country { get; set; }
    }



    public class VendorDetails : Error
    {
        public IQueryable<VendorModel> VendorList { get; set; }

        public IQueryable<UserRequests_Vendor> VendorListSPMO { get; set; }
    }
}