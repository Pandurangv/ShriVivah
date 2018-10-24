using System;
using System.Configuration;
using System.Web;
using System.Web.Optimization;

namespace ShriVivah
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            
            if (string.IsNullOrEmpty(SettingsManager.Instance.Branding))
            {
                bundles.Add(new StyleBundle("~/Content/commoncss").Include(
                      "~/Content/Main/bootstrap.min.css"));
                bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/FrontUI/bootstrap.css",
                      "~/Content/FrontUI/fonts/Shiv01.ttf",
                      "~/Content/FrontUI/fonts/MANGAL.ttf",
                      "~/Content/FrontUI/menu.css",
                      "~/Content/FrontUI/style.css",
                      "~/Content/FrontUI/font-awesome.min.css",
                      "~/Content/FrontUI/popuo-box.css"
                      ));

                bundles.Add(new ScriptBundle("~/bundles/Main").Include(
                "~/Scripts/Main/jquery.min.js",
                "~/Scripts/Main/bootstrap.min.js",
                "~/Scripts/Main/angular.min.js",
                "~/Scripts/Main/App.js"
                ));
            }
            else
            {
                bundles.Add(new StyleBundle("~/Content/commoncss").Include(
                     "~/Content/" + SettingsManager.Instance.Branding + "/FrontUI/bootstrap.min.css",
                      "~/Content/" + SettingsManager.Instance.Branding + "/FrontUI/font-awesome.min.css",
                      "~/Content/" + SettingsManager.Instance.Branding +"/FrontUI/animate.min.css",
                      "~/Content/" + SettingsManager.Instance.Branding + "/FrontUI/ionicons.min.css",
                      "~/Content/" + SettingsManager.Instance.Branding + "/FrontUI/owl.carousel.min.css",
                      "~/Content/" + SettingsManager.Instance.Branding + "/FrontUI/lightbox.min.css",
                      "~/Content/" + SettingsManager.Instance.Branding + "/FrontUI/style.css"
                      ));

                bundles.Add(new StyleBundle("~/Content/adminspmocommon").Include(
                     "~/Content/" + SettingsManager.Instance.Branding + "/UserDashboard/bootstrap.min.css"));
                bundles.Add(new StyleBundle("~/Content/adminspmocss").Include(
                      "~/Content/" + SettingsManager.Instance.Branding + "/FrontUI/font-awesome.min.css",
                      "~/Content/" + SettingsManager.Instance.Branding + "/UserDashboard/ionicons.min.css",
                      "~/Content/" + SettingsManager.Instance.Branding + "/UserDashboard/AdminLTE.min.css",
                      "~/Content/" + SettingsManager.Instance.Branding + "/UserDashboard/_all-skins.min.css",
                      "~/Content/" + SettingsManager.Instance.Branding + "/UserDashboard/style.css"
                      ));

                bundles.Add(new ScriptBundle("~/Scripts/pagejs").Include(
                     "~/Scripts/" + SettingsManager.Instance.Branding + "/FrontUI/jquery.min.js",
                      "~/Scripts/" + SettingsManager.Instance.Branding + "/FrontUI/jquery-migrate.min.js",
                      "~/Scripts/" + SettingsManager.Instance.Branding + "/FrontUI/bootstrap.bundle.min.js",
                      "~/Scripts/" + SettingsManager.Instance.Branding + "/FrontUI/easing.min.js",
                      "~/Scripts/" + SettingsManager.Instance.Branding + "/FrontUI/hoverIntent.js",
                      "~/Scripts/" + SettingsManager.Instance.Branding + "/FrontUI/superfish.min.js"
                      ));

                bundles.Add(new ScriptBundle("~/bundles/Main").Include(
                    "~/Scripts/Main/angular.min.js",
                    "~/Scripts/Main/App.js"
                ));
            }

            

            

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/FrontUI/jquery.min.js",
                        "~/Scripts/FrontUI/bootstrap.min.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/jquerymenus").Include(
                    "~/Scripts/FrontUI/megamenu.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/jquerymodel").Include(
                    "~/Scripts/FrontUI/jquery.leanModal.min.js",
                    "~/Scripts/FrontUI/easyResponsiveTabs.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryother").Include(
                    "~/Scripts/FrontUI/move-top.js",
                    "~/Scripts/FrontUI/easing.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryindex").Include(
                    "~/Scripts/FrontUI/jquery.magnific-popup.js",
                    "~/Scripts/HelperJs/IndexHelper.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/UserProfile").Include(
                   "~/Scripts/HelperJs/UserProfileHelper.js",
                   "~/Scripts/HelperJs/ValidationHelper.js"
               ));

            bundles.Add(new ScriptBundle("~/bundles/EditUserProfile").Include(
                   "~/Scripts/HelperJs/UserEditHelper.js",
                   "~/Scripts/HelperJs/ValidationHelper.js"
               ));

            bundles.Add(new ScriptBundle("~/bundles/Main/BarrierController").Include("~/Scripts/Main/BarrierController.js"));

            bundles.Add(new ScriptBundle("~/bundles/SignalR").Include(
                   "~/Scripts/jquery.signalR-2.2.0.min.js",
                   //"~/SignalR/hubs"
                   "~/Scripts/timer.js"
               ));

            //bundles.Add(new ScriptBundle("~/bundles/HelperJs/gmaps").Include("~/Scripts/HelperJs/gmaps.js"));

            bundles.Add(new ScriptBundle("~/bundles/HelperJs/VendorHelper").Include("~/Scripts/HelperJs/VendorHelper.js"));

            bundles.Add(new StyleBundle("~/Content/Admincss").Include(
                      "~/Content/AdminPanel/bootstrap.min.css",
                      "~/Content/AdminPanel/style.css",
                      "~/Content/AdminPanel/lines.css",
                      "~/Content/AdminPanel/font-awesome.css",
                      "~/Content/AdminPanel/custom.css"                      
                      ));

            bundles.Add(new ScriptBundle("~/bundles/Adminjqueryindex").Include(
                    "~/Scripts/AdminPanel/jquery.min.js",
                    "~/Scripts/AdminPanel/metisMenu.min.js",
                    "~/Scripts/AdminPanel/custom.js",
                    "~/Scripts/Main/angular.min.js",
                    "~/Scripts/Main/App.js",
                    "~/Scripts/HelperJs/common.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/common").Include(
                    "~/Scripts/HelperJs/common.js",
                    "~/Scripts/HelperJs/spin.js",
                    //"~/Scripts/HelperJs/fontSettings.js",
                    //"~/Scripts/HelperJs/FontHelper.js",
                    "~/Scripts/HelperJs/ShowCustomAlert.js"));

            bundles.Add(new ScriptBundle("~/bundles/Main/UserProfileController").Include("~/Scripts/Main/UserProfileController.js"));

            bundles.Add(new ScriptBundle("~/bundles/Main/Married").Include("~/Scripts/Main/MarriedController.js"));
        }
    }

    public class SettingsManager
    {
        private static SettingsManager _Instance = null;

        private SettingsManager()
        {

        }


        public void WriteLog(string Msg)
        {
            try
            {
                string filename = string.Format("{o:dd_MM_yyyy_hh_mm_ss}", DateTime.Now.Date);
                string filepath= HttpContext.Current.Server.MapPath("~/Content/Log/" + filename + ".txt");
                System.IO.StreamWriter writer = new System.IO.StreamWriter(new System.IO.FileStream(filepath, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite));
                writer.Write(Msg);
            }
            catch (Exception ex)
            {
            }
        }

        public static SettingsManager Instance
        {
            get {
                if (_Instance==null)
                {
                    _Instance = new SettingsManager();
                }
                return _Instance;
            }
        }

        public string Prefix
        {
            get
            {
                string Branding = string.Empty;
                if (ConfigurationManager.ConnectionStrings["Prefix"] != null)
                {
                    Branding = Convert.ToString(ConfigurationManager.ConnectionStrings["Prefix"].ConnectionString);
                }
                return Branding;
            }
        }

        public string Branding
        {
            get
            {
                string Branding = string.Empty;
                if (ConfigurationManager.ConnectionStrings["Branding"] != null)
                {
                    Branding=Convert.ToString(ConfigurationManager.ConnectionStrings["Branding"].ConnectionString);
                }
                return Branding;
            }
        }

        public string SMSUrl
        {
            get
            {
                string Branding = string.Empty;
                if (ConfigurationManager.ConnectionStrings["MSG91"] != null)
                {
                    Branding = Convert.ToString(ConfigurationManager.ConnectionStrings["MSG91"].ConnectionString);
                }
                return Branding;
            }
        }

        public string MsgLogin
        {
            get
            {
                string Branding = string.Empty;
                if (ConfigurationManager.ConnectionStrings["MsgLogin"] != null)
                {
                    Branding = Convert.ToString(ConfigurationManager.ConnectionStrings["MsgLogin"].ConnectionString);
                }
                return Branding;
            }
        }

        public string SindhuRegards
        {
            get
            {
                string Branding = string.Empty;
                if (ConfigurationManager.ConnectionStrings["SindhuRegards"] != null)
                {
                    Branding = Convert.ToString(ConfigurationManager.ConnectionStrings["SindhuRegards"]);
                }
                return Branding;
            }
        }

        public string SourcePathM
        {
            get
            {
                string Branding = string.Empty;
                if (ConfigurationManager.ConnectionStrings["SourcePathM"] != null)
                {
                    Branding = Convert.ToString(ConfigurationManager.ConnectionStrings["SourcePathM"].ConnectionString);
                }
                return Branding;
            }
        }
        public string SourcePathF
        {
            get
            {
                string Branding = string.Empty;
                if (ConfigurationManager.ConnectionStrings["SourcePathF"] != null)
                {
                    Branding = Convert.ToString(ConfigurationManager.ConnectionStrings["SourcePathF"].ConnectionString);
                }
                return Branding;
            }
        }

        public string SourcePath
        {
            get
            {
                string Branding = string.Empty;
                if (ConfigurationManager.ConnectionStrings["SourcePath"] != null)
                {
                    Branding = Convert.ToString(ConfigurationManager.ConnectionStrings["SourcePath"].ConnectionString);
                }
                return Branding;
            }
        }

        public int OTPLength
        {
            get
            {
                int len = 4;
                if (ConfigurationManager.ConnectionStrings["OTPLength"] != null)
                {
                    len = Convert.ToInt32(ConfigurationManager.ConnectionStrings["OTPLength"].ConnectionString);
                }
                return len;
            }
        }

        public string MsgPassword
        {
            get
            {
                string Branding = string.Empty;
                if (ConfigurationManager.ConnectionStrings["MsgPassword"] != null)
                {
                    Branding = Convert.ToString(ConfigurationManager.ConnectionStrings["MsgPassword"].ConnectionString);
                }
                return Branding;
            }
        }



        public string SenderId
        {
            get
            {
                string Branding = string.Empty;
                if (ConfigurationManager.ConnectionStrings["SenderId"] != null)
                {
                    Branding = Convert.ToString(ConfigurationManager.ConnectionStrings["SenderId"].ConnectionString);
                }
                return Branding;
            }
        }

        public string AuthKey
        {
            get
            {
                string Branding = string.Empty;
                if (ConfigurationManager.ConnectionStrings["AuthKey"] != null)
                {
                    Branding = Convert.ToString(ConfigurationManager.ConnectionStrings["AuthKey"].ConnectionString);
                }
                return Branding;
            }
        }

        //Title SMSUrl
        public string Title
        {
            get
            {
                string Branding = string.Empty;
                if (ConfigurationManager.ConnectionStrings["Title"] != null)
                {
                    Branding = Convert.ToString(ConfigurationManager.ConnectionStrings["Title"].ConnectionString);
                }
                return Branding;
            }
        }


        public string Address
        {
            get
            {
                string Branding = string.Empty;
                if (ConfigurationManager.ConnectionStrings["Address"] != null)
                {
                    Branding = Convert.ToString(ConfigurationManager.ConnectionStrings["Address"].ConnectionString);
                }
                return Branding;
            }
        }

        public string MobileNo
        {
            get
            {
                string Branding = string.Empty;
                if (ConfigurationManager.ConnectionStrings["MobileNo"] != null)
                {
                    Branding = Convert.ToString(ConfigurationManager.ConnectionStrings["MobileNo"].ConnectionString);
                }
                return Branding;
            }
        }

        public string MID
        {
            get
            {
                string Branding = string.Empty;
                if (ConfigurationManager.ConnectionStrings["MID"] != null)
                {
                    Branding = Convert.ToString(ConfigurationManager.ConnectionStrings["MID"].ConnectionString);
                }
                return Branding;
            }
        }

        public string WEBSITE
        {
            get
            {
                string Branding = string.Empty;
                if (ConfigurationManager.ConnectionStrings["WEBSITE"] != null)
                {
                    Branding = Convert.ToString(ConfigurationManager.ConnectionStrings["WEBSITE"].ConnectionString);
                }
                return Branding;
            }
        }

        public int Charges
        {
            get
            {
                int Branding=500;
                if (ConfigurationManager.ConnectionStrings["Charges"] != null)
                {
                    Branding = Convert.ToInt32(ConfigurationManager.ConnectionStrings["Charges"].ConnectionString);
                }
                return Branding;
            }
        }

        public string CHANNEL_ID
        {
            get
            {
                string Branding = string.Empty;
                if (ConfigurationManager.ConnectionStrings["CHANNEL_ID"] != null)
                {
                    Branding = Convert.ToString(ConfigurationManager.ConnectionStrings["CHANNEL_ID"].ConnectionString);
                }
                return Branding;
            }
        }

        public string INDUSTRY_TYPE_ID
        {
            get
            {
                string Branding = string.Empty;
                if (ConfigurationManager.ConnectionStrings["INDUSTRY_TYPE_ID"] != null)
                {
                    Branding = Convert.ToString(ConfigurationManager.ConnectionStrings["INDUSTRY_TYPE_ID"].ConnectionString);
                }
                return Branding;
            }
        }

        public string MERCHANT_KEY
        {
            get
            {
                string Branding = string.Empty;
                if (ConfigurationManager.ConnectionStrings["MERCHANT_KEY"] != null)
                {
                    Branding = Convert.ToString(ConfigurationManager.ConnectionStrings["MERCHANT_KEY"].ConnectionString);
                }
                return Branding;
            }
        }

        public string SMTP
        {
            get
            {
                string Branding = string.Empty;
                if (ConfigurationManager.ConnectionStrings["SMTP"] != null)
                {
                    Branding = Convert.ToString(ConfigurationManager.ConnectionStrings["SMTP"].ConnectionString);
                }
                return Branding;
            }
        }

        public string fromEmail
        {
            get
            {
                string Branding = string.Empty;
                if (ConfigurationManager.ConnectionStrings["fromEmail"] != null)
                {
                    Branding = Convert.ToString(ConfigurationManager.ConnectionStrings["fromEmail"].ConnectionString);
                }
                return Branding;
            }
        }

        public string fromPW
        {
            get
            {
                string Branding = string.Empty;
                if (ConfigurationManager.ConnectionStrings["fromPW"] != null)
                {
                    Branding = Convert.ToString(ConfigurationManager.ConnectionStrings["fromPW"].ConnectionString);
                }
                return Branding;
            }
        }
    }
}
