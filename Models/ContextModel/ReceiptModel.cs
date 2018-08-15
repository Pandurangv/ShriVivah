using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShriVivah.Models.ContextModel
{
    public class ReceiptModel
    {
        public string MobileNumber { get; set; }

        public string Email { get; set; }

        public decimal Amount { get; set; }
        public string JsonData { get; internal set; }
        public string PayTmUrl { get; internal set; }
        public string ORDER_ID { get; internal set; }
        public string HTMLData { get; internal set; }
    }

    public class PaytmResponse
    {
        public string MID { get; set; }
        public string ORDERID { get; set; }
        public string TXNAMOUNT { get; set; }
        public string CURRENCY { get; set; }
        public string TXNID { get; set; }
        public string BANKTXNID { get; set; }
        public string STATUS { get; set; }
        public string RESPCODE { get; set; }
        public string RESPMSG { get; set; }
        public string TXNDATE { get; set; }
        public string GATEWAYNAME { get; set; }
        public string BANKNAME { get; set; }
        public string PAYMENTMODE { get; set; }
        public string CHECKSUMHASH { get; set; }
    }
}