using ShriVivah.Models;
using ShriVivah.Models.ContextModel;
using ShriVivah.Models.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ShriVivah.Controllers
{
    public class PaymentController : Controller
    {
        LoginBLL objLogin = new LoginBLL();
        public PaymentController()
        {
            
        }
        // GET: Payment
        [MyAuthorizeAttribute(IsAdmin = false)]
        [CustomView]
        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult Transact()
        {
            var receipt = new ReceiptModel();
            string jsonresponse = string.Empty;
            try
            {
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                string guid = Guid.NewGuid().ToString();
                parameters.Add("MID", PaytmConstants.MID);
                parameters.Add("ORDER_ID", guid);
                parameters.Add("CUST_ID", "1");
                parameters.Add("CHANNEL_ID", PaytmConstants.CHANNEL_ID);
                parameters.Add("INDUSTRY_TYPE_ID", PaytmConstants.INDUSTRY_TYPE_ID);
                parameters.Add("WEBSITE", PaytmConstants.WEBSITE);
                parameters.Add("TXN_AMOUNT", "500");
                parameters.Add("CALLBACK_URL", "https://securegw-stage.paytm.in/theia/paytmCallback?ORDER_ID=" + guid);
                jsonresponse = paytm.CheckSum.generateCheckSum(SettingsManager.Instance.MERCHANT_KEY, parameters);
                string outputHTML = string.Empty;
                if (parameters.ContainsKey("ORDER_ID") && parameters.ContainsKey("MID"))
                {
                    outputHTML += "<form method='post' action='https://securegw-stage.paytm.in/theia/paytmCallback?ORDER_ID=" + guid + "' name='frmpaytm'>";
                    foreach (string key in parameters.Keys)
                    {
                        outputHTML += "<input type ='hidden' name='" + key + "' value='" + parameters[key] + "'>";
                    }
                    outputHTML += "<input type='hidden' name='CHECKSUMHASH' value='" + jsonresponse + "'>";
                    outputHTML += "</form>";
                }
                string paytmURL = "https://securegw-stage.paytm.in/theia/paytmCallback?ORDER_ID=" + jsonresponse;
                receipt.JsonData = jsonresponse;
                receipt.HTMLData = outputHTML;
                receipt.PayTmUrl = paytmURL;
                receipt.ORDER_ID = guid;
                string res = readHtmlPage(paytmURL, receipt);
                
            }
            catch (Exception ex)
            {

            }
            return Json(receipt,JsonRequestBehavior.AllowGet);
        }

        private String readHtmlPage(string url,ReceiptModel model)
        {
            //setup some variables end

            String result = "";
            String strPost = "MID=" + PaytmConstants.MID + "&MERCHANT_KEY=" + PaytmConstants.MERCHANT_KEY + "&INDUSTRY_TYPE_ID=" 
                + PaytmConstants.INDUSTRY_TYPE_ID + "&CHANNEL_ID=" + PaytmConstants.CHANNEL_ID + "&WEBSITE=" + PaytmConstants.WEBSITE
                + "&ORDER_ID=" + model.ORDER_ID + "&CUST_ID=1"
                + "&TXN_AMOUNT=500&CALLBACK_URL=" + url
                + "&CHECKSUMHASH=" + model.JsonData;
            StreamWriter myWriter = null;

            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);
            objRequest.Method = "POST";
            objRequest.ContentLength = strPost.Length;
            objRequest.ContentType = "application/x-www-form-urlencoded";

            try
            {
                myWriter = new StreamWriter(objRequest.GetRequestStream());
                myWriter.Write(strPost);
            }
            catch (Exception e)
            {
                return e.Message;
            }
            finally
            {
                myWriter.Close();
            }

            HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
            using (StreamReader sr =
               new StreamReader(objResponse.GetResponseStream()))
            {
                result = sr.ReadToEnd();

                // Close and clean up the StreamReader
                sr.Close();
            }
            return result;
        }


        [HttpPost]
        public ActionResult CreatePayment(ReceiptModel data)
        {
            String merchantKey = PaytmConstants.MERCHANT_KEY;
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            string[] custid = Guid.NewGuid().ToString().Split('-');
            string[] orderid = Guid.NewGuid().ToString().Split('-');
            parameters.Add("MID", PaytmConstants.MID);
            parameters.Add("CHANNEL_ID", "WEB");
            parameters.Add("INDUSTRY_TYPE_ID", "Retail");
            parameters.Add("WEBSITE", "WEBSTAGING");
            parameters.Add("EMAIL", "pandurangkumbhar59@gmail.com");
            parameters.Add("MOBILE_NO", "7040481106");
            parameters.Add("CUST_ID", custid[0]);
            parameters.Add("ORDER_ID", orderid[0]);
            parameters.Add("TXN_AMOUNT", "10");
            //parameters.Add("CALLBACK_URL", "url"); //This parameter is not mandatory. Use this to pass the callback url dynamically.

            string checksum = paytm.CheckSum.generateCheckSum(merchantKey, parameters);

            string paytmURL = "https://securegw-stage.paytm.in/theia/processTransaction?orderid=" + orderid[0];

            string outputHTML = "<html>";
            outputHTML += "<head>";
            outputHTML += "<title>Merchant Check Out Page</title>";
            outputHTML += "</head>";
            outputHTML += "<body>";
            outputHTML += "<center><h1>Please do not refresh this page...</h1></center>";
            outputHTML += "<form method='post' action='" + paytmURL + "' name='f1'>";
            outputHTML += "<table border='1'>";
            outputHTML += "<tbody>";
            foreach (string key in parameters.Keys)
            {
                outputHTML += "<input type='hidden' name='" + key + "' value='" + parameters[key] + "'>";
            }
            outputHTML += "<input type='hidden' name='CHECKSUMHASH' value='" + checksum + "'>";
            outputHTML += "</tbody>";
            outputHTML += "</table>";
            outputHTML += "<script type='text/javascript'>";
            outputHTML += "document.f1.submit();";
            outputHTML += "</script>";
            outputHTML += "</form>";
            outputHTML += "</body>";
            outputHTML += "</html>";

            ViewBag.htmlData = outputHTML;

            return View("PaymentPage");
        }

        [HttpPost]
        public ActionResult Payments(PaytmResponse data)
        {
            return View("PaytmResponse", data);
        }


        public ActionResult transactsuccess()
        {
            return View("Index");
        }

        
    }
}