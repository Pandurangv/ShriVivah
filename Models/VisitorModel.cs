using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShriVivah.Models
{
    public class VisitorModel
    {
        public int? VisitorId { get; set; }

        public DateTime? VisitDate { get; set; }

        public int UserId { get; set; }

        public int VisitedUserId { get; set; }

        public string UserName { get; set; }

        public string Address { get; set; }

        public string ReligionName { get; set; }

        public string CastName { get; set; }

        public int ReligionId { get; set; }

        public int CastId { get; set; }
        public string PanchayatCode { get; set; }

        public string Height { get; set; }

        public int? MarritalStatus { get; set; }

        public string FirstName { get; set; }

        public string LName { get; set; }

        public string City { get; set; }

        public string BehalfOf { get; set; }

        public string Qualification { get; set; }

        public string Img1 { get; set; }

        public string Income { get; set; }
    }


    public class VisitorResponse : Error
    {
        public IQueryable<VisitorModel> VisitorDetails { get; set; }
    }
}