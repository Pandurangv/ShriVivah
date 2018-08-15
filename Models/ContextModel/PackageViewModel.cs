using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShriVivah.Models.ContextModel
{
    public class PackageViewModel
    {
        public long? PackageId { get; set; }

        public string PackageName { get; set; }

        public decimal? Price { get; set; }

        public string Gender { get; set; }

        public int AccessUsers { get; set; }

        public int Validity { get; set; }

        public bool? IsDelete { get; set; }
    }

    public class PackageResponse:Error
    {
        public IQueryable<PackageViewModel> PackageList { get; set; }
    }
}