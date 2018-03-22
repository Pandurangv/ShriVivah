using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShriVivah.Models
{
    public class VendorTypeModel
    {
        public int? VendorTypeId { get; set; }

        public string VendorType { get; set; }

        public bool IsDelete { get; set; }

        public string TypeImagesPath { get; set; }
    }
}