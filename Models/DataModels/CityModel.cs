using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShriVivah.Models.DataModels
{
    public class CityModel
    {
        public int? CityId { get; set; }

        public int? StateId { get; set; }

        [Required(ErrorMessage = "*")]
        public string CityName { get; set; }

        [Required(ErrorMessage = "*")]
        [MaxLength(6, ErrorMessage = "Length should be 6 digits")]
        [MinLength(6, ErrorMessage = "Length should be 6 digits")]
        public string Pincode { get; set; }

        public string StateName { get; set; }

        public string CountryName { get; set; }

        public bool? IsDelete { get; set; }
    }
}