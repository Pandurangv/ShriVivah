using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShriVivah.Models.DataModels
{
    public class StateContextModel
    {
        public int? StateId { get; set; }

        public int? CountryId { get; set; }

        [Required(ErrorMessage = "*")]
        public string StateName { get; set; }

        public string CountryName { get; set; }

        public bool? IsDelete { get; set; }
    }
}