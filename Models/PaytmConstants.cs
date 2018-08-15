using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ShriVivah.Models
{
    public static class PaytmConstants
    {
        public static string MID = ConfigurationManager.ConnectionStrings["MID"].ConnectionString;
        public static string MERCHANT_KEY = ConfigurationManager.ConnectionStrings["MERCHANT_KEY"].ConnectionString;
        public static string INDUSTRY_TYPE_ID = "Retail";
        public static string CHANNEL_ID = "WEB";
        public static string WEBSITE = "http://sindhihindu.com/";
    }
}