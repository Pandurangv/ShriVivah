using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShriVivah.Models
{
    public class RequestModel
    {
        public int? RequestId { get; set; }

        public DateTime? RequestDate { get; set; }

        public int RequestFrom { get; set; }

        public int RequestTo { get; set; }

        public string RequestStatus { get; set; }

        public string UserName { get; set; }

        public string Address { get; set; }

        public string ReligionName { get; set; }

        public string CastName { get; set; }

        public int ReligionId { get; set; }

        public int CastId { get; set; }

        public string InOut { get; set; }

    }
}