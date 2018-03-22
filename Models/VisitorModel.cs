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
    }


    public class VisitorResponse : Error
    {
        public IQueryable<VisitorModel> VisitorDetails { get; set; }
    }
}