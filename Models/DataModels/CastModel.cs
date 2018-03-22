using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShriVivah.Models.DataModels
{
    public class CastModel
    {
        public int? CastId { get; set; }

        public int? ReligionId { get; set; }

        [Required(ErrorMessage = "*")]
        public string CastName { get; set; }

        public string ReligionName { get; set; }

        public bool? IsDelete { get; set; }
    }
}