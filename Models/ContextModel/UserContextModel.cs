using ShriVivah.Models.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Routing;

namespace ShriVivah.Models.ContextModel
{
    public class UserContextModel
    {

        public UserContextModel()
        {
            objData = new ShreeVivahDbContext();
        }

        public IQueryable<STP_SelectMessageUser> Select_STP_SelectMessageUser(int toUserId)
        {
            var parameters = new List<SqlParameter>();
            var param1 = new SqlParameter("UserId", toUserId);
            param1.DbType = System.Data.DbType.Int32;
            parameters.Add(param1); 
            //name.Value = toUserId;
            var result = objData.Database.SqlQuery<STP_SelectMessageUser>("[dbo].[STP_SelectMessageUser] @UserId", parameters.ToArray()).AsQueryable();
            return result;
        }

        internal bool MarriageDone(int userId)
        {
            bool success = false;
            tblUser user = objData.tblUsers.Where(p => p.UserId == userId).FirstOrDefault();
            if (user!=null)
            {
                user.ismarried = 1;
                objData.SaveChanges();
                success = true;
            }
            return success;
        }

        public void ChangePassword(STP_GetUserDetail user)
        {
            tblUser obju = (from tbl in objData.tblUsers where tbl.UserId == user.UserId select tbl).FirstOrDefault();
            obju.Password = user.Password;
            objData.SaveChanges();
        }

        public IQueryable<STP_GetUserDetail> Select_STP_GetUserDetails()
        {
            string commandText = "[dbo].[STP_GetUserDetails]";
            var result= objData.Database.SqlQuery<STP_GetUserDetail>(commandText).AsQueryable();
            
            return result;
        }

        public IQueryable<STP_GetUserDetail> Select_STP_GetUserDetails(SearchRequestModel request)
        {
            SqlParameter pUserId = new SqlParameter() { DbType=DbType.Int32,ParameterName="@UserId"};
            if (request.UserId==null)
            {
                pUserId.Value = DBNull.Value;
            }
            else
            {
                pUserId.Value = request.UserId;
            }
            SqlParameter pGender = new SqlParameter() { DbType = DbType.Int32, ParameterName = "@Gender" };
            if (string.IsNullOrEmpty(request.Gender) == false)
            {
                pUserId.Value = DBNull.Value;
            }
            else
            {
                pUserId.Value = request.Gender;
            }
            SqlParameter pIsMarried = new SqlParameter() { DbType=DbType.Boolean,ParameterName= "@IsMarried" };
            if (request.IsMarried==null)
            {
                pUserId.Value = DBNull.Value;
            }
            else
            {
                pUserId.Value = request.IsMarried;
            }
            SqlParameter pIsActive = new SqlParameter() { DbType=DbType.Boolean,ParameterName="@IsActive"};
            if (request.IsActive == null)
            {
                pUserId.Value = DBNull.Value;
            }
            else
            {
                pUserId.Value = request.IsActive;
            }
            SqlParameter pMinAge = new SqlParameter() { DbType=DbType.Int32,ParameterName="@MinAge"};
            if (request.MinAge == null)
            {
                pUserId.Value = DBNull.Value;
            }
            else
            {
                pUserId.Value = request.MinAge;
            }
            SqlParameter pMaxAge = new SqlParameter() { DbType=DbType.Int32,ParameterName="@MaxAge"};
            if (request.MaxAge == null)
            {
                pUserId.Value = DBNull.Value;
            }
            else
            {
                pUserId.Value = request.Gender;
            }
            SqlParameter pMinHeightId = new SqlParameter() { DbType=DbType.Int32,ParameterName="@MinHeightId"};
            if (request.MaxAge == null)
            {
                pUserId.Value = DBNull.Value;
            }
            else
            {
                pUserId.Value = request.Gender;
            }
            SqlParameter pMaxHeightId = new SqlParameter() { DbType=DbType.Int32,ParameterName="@MaxHeightId"};
            if (request.MaxAge == null)
            {
                pUserId.Value = DBNull.Value;
            }
            else
            {
                pUserId.Value = request.MaxHeightId;
            }
            SqlParameter pIncome = new SqlParameter() { DbType=DbType.String,ParameterName="@Income"};
            if (string.IsNullOrEmpty(request.Income) == false)
            {
                pUserId.Value = DBNull.Value;
            }
            else
            {
                pUserId.Value = request.Income;
            }
            SqlParameter pCity = new SqlParameter() { DbType=DbType.String,ParameterName="@City"};
            if (string.IsNullOrEmpty(request.Income) == false)
            {
                pUserId.Value = DBNull.Value;
            }
            else
            {
                pUserId.Value = request.City;
            }
            SqlParameter pQualification = new SqlParameter() { DbType=DbType.String,ParameterName= "@Qualification" };
            if (string.IsNullOrEmpty(request.Income) == false)
            {
                pUserId.Value = DBNull.Value;
            }
            else
            {
                pUserId.Value = request.Qualification;
            }
            SqlParameter pPageNo = new SqlParameter() { DbType=DbType.Int32,ParameterName="@PageNo"};
            if (request.MaxAge == null)
            {
                pUserId.Value = 1;
            }
            else
            {
                pUserId.Value = request.PageNo;
            }
            SqlParameter pPageSize = new SqlParameter() { DbType=DbType.Int32,ParameterName="@PageSize"};
            if (request.MaxAge == null)
            {
                pUserId.Value = 50;
            }
            else
            {
                pUserId.Value = request.PageSize;
            }
            string commandText = "[dbo].[STP_GetUserDetails_BackUp]";
            var result = objData.Database.SqlQuery<STP_GetUserDetail>(commandText).AsQueryable();

            return result;
        }

        internal void SaveAndroidUser(RegisterReuest model)
        {
            tblUser user = new tblUser()
            {
                FirstName = model.UserData.FirstName,
                MName = model.UserData.MiddleName,
                LName = model.UserData.LastName,
                MailId = model.UserData.Email,
                MobileNo = model.UserData.MobileNo,
                UserName = model.UserData.UserName,
                Password = model.UserData.Password,
                UserType = model.UserData.RoleName,
                IsDelete = false,
                IsActive = model.UserData.IsActive == null ? true : model.UserData.IsActive,
                Address = model.UserData.Address,
                BloodGroupId = model.UserData.BloodGroupId,
                BodyType = model.UserData.BodyType,
                CasteId = model.UserData.CasteId,
                ChildLivingStatus = model.UserData.ChildLivingStatus,
                Color = model.UserData.Color,
                DateOfBirth = model.UserData.DateOfBirth,
                DateofReg = DateTime.Now.Date,
                Gender = model.UserData.Gender,
                HandicapedType = model.UserData.HandicappedType,
                HeightId = model.UserData.HeightId,
                IdentificationMark = model.UserData.IdentificationMark,
                IsIntercast = model.UserData.IsInterCast,
                IsHandiCapped = model.UserData.IsHandicapped,
                District = model.UserData.District,
                Taluka = model.UserData.Taluka,
                MarritalStatus = model.UserData.MarritalStatus,
                NoOfChildrens = model.UserData.NoOfChildrens,
                OrasId = model.UserData.OrasId,
                PANNO = model.UserData.PANNO,
                PlaceofBirth = model.UserData.PlaceOfBirth,
                QualificationId = 0,
                Qualification = model.UserData.Qualification,
                ReligionId = model.UserData.ReligionId,

                TimeofBirth = model.UserData.strTimeofBirth,
                Weight = model.UserData.Weight,
                State = model.UserData.State,
                Country = model.UserData.Country,
                BirthName = model.UserData.BirthName,
                City = model.UserData.City,
                Hobbies = model.UserData.Hobbies,
                Expectation = model.UserData.UserExpectation,
                Income = model.UserData.Income,
                Gotra = model.UserData.GotraName,
            };
            objData.tblUsers.Add(user);
            objData.SaveChanges();

            if (SettingsManager.Instance.Branding == "SPMO")
            {
                UserRequests request = new UserRequests()
                {
                    IsDelete = false,
                    IsAdminApproved = false,
                    IsApproved = false,
                    IsUserRequest = false,
                    PanchayatId = model.UserData.AgentId,
                    RequestDate = DateTime.Now,
                    UserId = user.UserId.Value,
                    VendorId = 0,
                };
                //tbl.IsDelete = false;
                objData.UserRequest.Add(request);
                objData.SaveChanges();
            }

            model.UserFamily.UserId = user.UserId.Value;
            Update(model.UserFamily);

            model.UserExpectation.UserId = user.UserId.Value;
            Save(model.UserExpectation);

            model.UserJobDetails.UserId = user.UserId.Value;
            tblJobDetails objfamily = new tblJobDetails()
            {
                UserId = model.UserJobDetails.UserId,
                IsJobOrBusiness = true,
                Income = model.UserJobDetails.Income,
                CompanyName = model.UserJobDetails.CompanyName,
                JobLocation = model.UserJobDetails.JobLocation,
            };
            UpdateJobLocation(objfamily);
            Update(model.RelativeList, user.UserId.Value);
            
        }

        internal void SaveLoginDetails(LoginDetails objLoginDetails)
        {
            objData.LoginDetailss.Add(objLoginDetails);
            objData.SaveChanges();
        }

        

        internal ResponseModel SendSMS(STP_GetUserDetail user, RegisterViewModel model=null)
        {
            
            //Multiple mobiles numbers separated by comma
            string mobileNumber = user != null ? user.MobileNo : model.MobileNo;
            //Sender ID,While using route4 sender id should be 6 characters long.
            
            //Your message to send, Add URL encoding here.
            StringBuilder sb = new StringBuilder();
            string message = string.Empty;
            if (user != null)
            {
                if (SettingsManager.Instance.Branding == "SPMO")
                {
                    sb.Append("Please do login Using below credintials Login Id : ").Append(user.MobileNo).Append(" And Password : ").Append(user.Password).Append(" and complete your profile, Regards Sindhi Hindu, www.sindhihindu.com 9273763490");
                }
                else
                {
                    sb.Append("Please do login Using below credintials Login Id : ").Append(user.UserName).Append(" And Password : ").Append(user.Password).Append(" and complete your profile, Regards Trupti, www.Varmalavivah.com 8806369038");
                }
                message = HttpUtility.UrlEncode(sb.ToString());
            }
            else
            {
                string uid = Guid.NewGuid().ToString();
                string charat = "";
                string otp = "";// uid.ToString().Substring(0, 5);
                for (int i = 0; i < uid.Length; i++)
                {
                    charat = uid.Substring(i, 1);
                    int res = 0;
                    if (int.TryParse(charat, out res))
                    {
                        otp += charat;
                        if (otp.Length == 3)
                        {
                            break;
                        }
                    }
                }

                model.OTP = otp;
                SessionManager.GetInstance.RegisterUser = model;
                
                sb.Append("Please do not share OTP with any other. Your OTP is " + otp + " Regards, Sindhi Hindu");
                message = HttpUtility.UrlEncode(sb.ToString());
            }
            
            var response= SendUserSMS(mobileNumber, message);
            response.ModelObject = model;
            return response;
        }

        public ResponseModel SendUserSMS(string mobileNumber, string message)
        {
            ResponseModel response = new ResponseModel() { Status = false };//bool flag = false;
            string authKey = SettingsManager.Instance.AuthKey;
            //Prepare you post parameters
            StringBuilder sbPostData = new StringBuilder();
            sbPostData.AppendFormat("tranxid={0}", authKey);
            sbPostData.AppendFormat("&login={0}", SettingsManager.Instance.MsgLogin);
            sbPostData.AppendFormat("&psw={0}", SettingsManager.Instance.MsgPassword);

            sbPostData.AppendFormat("&sender={0}", SettingsManager.Instance.SenderId);
            sbPostData.AppendFormat("&mobile={0}", mobileNumber);

            sbPostData.AppendFormat("&message={0}", message);
            sbPostData.AppendFormat("&ack={0}", "YES");

            try
            {
                //Call Send SMS API
                string sendSMSUri = SettingsManager.Instance.SMSUrl;
                //Create HTTPWebrequest
                HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(sendSMSUri);
                //Prepare and Add URL Encoded data
                UTF8Encoding encoding = new UTF8Encoding();
                byte[] data = encoding.GetBytes(sbPostData.ToString());
                //Specify post method
                httpWReq.Method = "POST";
                httpWReq.ContentType = "application/x-www-form-urlencoded";
                httpWReq.ContentLength = data.Length;
                using (Stream stream = httpWReq.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                //Get the response
                HttpWebResponse webresponse = (HttpWebResponse)httpWReq.GetResponse();
                StreamReader reader = new StreamReader(webresponse.GetResponseStream());
                string responseString = reader.ReadToEnd();


                //Close the response
                reader.Close();
                webresponse.Close();
                response.Status = true;
                response.ErrorMessage = responseString;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                //MessageBox.Show(ex.Message.ToString());
            }
            return response;
        }

        public int Save(RegisterViewModel model)
        {
            tblUser user = new tblUser()
            {
                FirstName = model.FirstName,
                MName = model.MiddleName,
                LName = model.LastName,
                MailId = model.Email,
                MobileNo = model.MobileNo,
                UserName = model.UserName,
                Password = model.Password,
                UserType = model.RoleName,
                IsDelete = false,
                IsActive = SettingsManager.Instance.Branding == "SPMO" ? false : (model.IsActive == null ? true : model.IsActive),
                Gender = model.Gender,
                BehalfOf=model.BehalfOf,
            };
            objData.tblUsers.Add(user);
            objData.SaveChanges();

            UserRequests userRequest = new UserRequests()
            {
                UserId = user.UserId.Value,
                PanchayatId = model.AgentId,
                VendorId = 0,
                IsUserRequest = true,
                RequestDate = DateTime.Now,
                IsApproved = false,
                IsDelete = false,
                IsAdminApproved = false
            };
            objData.UserRequest.Add(userRequest);

            user.PanchayatCode = user.Gender=="M"? "SPMOM-" + user.UserId : "SPMOF-"  + user.UserId;

            objData.SaveChanges();

            return user.UserId.Value;
        }

        public ShreeVivahDbContext objData { get; set; }

        //internal void SetUserRole(Guid guid, Guid guid_2)
        //{
        //    //objData.aspnet_UsersInRoles.Add(new aspnet_UsersInRole() { RoleId = guid_2, UserId = guid });
        //    //objData.SaveChanges();
        //}

        //public IQueryable<RequestModel> GetSendAndReceivedRequestDetails()
        //{
        //    var userrequests = from tbl in objData.tblRequests
        //                       join tblu in objData.tblUsers
        //                       on tbl.RequestTo equals tblu.UserId
        //                       where tbl.RequestTo == SessionManager.GetInstance.ActiveUser.UserId
        //                       select new RequestModel { 
        //                            Address=tblu.Address,
        //                            RequestDate=tbl.RequestDate,
                                    
        //                       };
        //}


        internal IQueryable<tblUser> GetUser()
        {
            var result= (from tbl in objData.tblUsers
                    where tbl.IsDelete == false
                    orderby tbl.UserId descending
                    select tbl);
            return result;
        }

        internal IQueryable<STP_GetUserDetail> GetUserDetailsForUser()
        {
            var result = Select_STP_GetUserDetails().Where(p=>p.IsActive==true);
            return result;
        }

        internal IQueryable<SP_GetAgentDetails> GetPanchayatList()
        {
            string commandText = "[dbo].[SP_GetAgentDetails]";
            var result = objData.Database.SqlQuery<SP_GetAgentDetails>(commandText).AsQueryable();

            return result;
        }

        internal IQueryable<STP_GetUserDetail> GetAgentDetails()
        {
            int cYear = DateTime.Now.Date.Year;
            var result = (from tbl in objData.tblUsers
                          where tbl.IsDelete == false
                          && tbl.UserType.ToUpper()=="AGENT"
                          select new STP_GetUserDetail
                          {
                              Address = tbl.Address,
                              DateofReg = tbl.DateofReg,
                              MailId = tbl.MailId,
                              FirstName = tbl.FirstName,
                              //IsDelete = tbl.IsDelete,
                              IsHandiCapped = tbl.IsHandiCapped,
                              LName = tbl.LName,
                              MobileNo = tbl.MobileNo,
                              UserId = tbl.UserId,
                              UserType = tbl.UserType,
                              City = tbl.City,
                              IsActive = tbl.IsActive,
                              Country = tbl.Country,
                              State = tbl.State,
                              Taluka=tbl.Taluka,
                              District=tbl.District,
                              UserName=tbl.UserName,
                              PanchayatCode=tbl.PanchayatCode,
                              Name=tbl.FirstName + " " + tbl.LName
                          });

            
            return result;
        }

        internal IQueryable<RegisterViewModel> GetUserDetails(int ReligionId = 0, int CastId = 0, int OrasId = 0, int QualificationId = 0, string Gender = "")
        {
            int cYear = DateTime.Now.Date.Year;
            var result = (from tbl in objData.tblUsers
                          join tblB in objData.tblBloodGroups
                          on tbl.BloodGroupId equals tblB.BloodGroupId into tblBInners
                          from tblB in tblBInners.DefaultIfEmpty()
                          join tblR in objData.tblReligions
                          on tbl.ReligionId equals tblR.ReligionId into tblRInners
                          from tblR in tblRInners.DefaultIfEmpty()
                          join tblC in objData.tblCasts
                          on tbl.CasteId equals tblC.CastId into tblCInners
                          from tblC in tblCInners.DefaultIfEmpty()
                          join tblO in objData.tblOrass
                          on tbl.OrasId equals tblO.OrasId into tblOInners
                          from tblO in tblOInners.DefaultIfEmpty()
                          join tblQ in objData.tblQualifications
                          on tbl.QualificationId equals tblQ.QualificationId into tblQInners
                          from tblQ in tblQInners.DefaultIfEmpty()
                          join tblH in objData.tblHeights
                          on tbl.HeightId equals tblH.HeightId into tblHInners
                          from tblH in tblHInners.DefaultIfEmpty()
                          where tbl.IsDelete == false
                          select new RegisterViewModel
                          {
                              Address = tbl.Address,
                              BloodGroup = tblB.BloodGroupName,
                              BloodGroupId = tbl.BloodGroupId,
                              BodyType = tbl.BodyType,
                              Cast = tblC.CastName,
                              CasteId = tbl.CasteId,
                              Color = tbl.Color,
                              DateOfBirth = tbl.DateOfBirth,
                              DateofReg = tbl.DateofReg,
                              Email = tbl.MailId,
                              FirstName = tbl.FirstName,
                              Gender = tbl.Gender,
                              HandicappedType = tbl.HandicapedType,
                              IdentificationMark = tbl.IdentificationMark,
                              Img1 = tbl.Img1,
                              Img2 = tbl.Img2,
                              Income = tbl.Income,
                              IsDelete = tbl.IsDelete,
                              IsHandicapped = tbl.IsHandiCapped,
                              KImg = tbl.KImg,
                              LastName = tbl.LName,
                              MarritalStatus = tbl.MarritalStatus,
                              MiddleName = tbl.MName,
                              MobileNo = tbl.MobileNo,
                              NoOfChildrens = tbl.NoOfChildrens,
                              Age = cYear - (tbl.DateOfBirth == null ? 0 : tbl.DateOfBirth.Value.Year),
                              Height = tblH.Height,
                              HeightId = tblH.HeightId == null ? 0 : tblH.HeightId.Value,
                              Weight = tbl.Weight,
                              OrasId = tbl.OrasId,
                              OrasName = tblO.OrasName,
                              PlaceOfBirth = tbl.PlaceofBirth,
                              Qualification = tblQ.DegreeName,
                              QualificationId = tblQ.QualificationId.Value,
                              ReligionName = tblR.ReligionName,
                              ReligionId = tbl.ReligionId,
                              UserId = tbl.UserId,
                              UserType = tbl.UserType,
                              GotraName = tbl.Gotra,
                              BirthName = tbl.BirthName,
                              IsInterCast = tbl.IsIntercast,
                              City = tbl.City,
                              IsActive = tbl.IsActive,
                              TimeofBirth = tbl.TimeofBirth,
                              Country = tbl.Country,
                              State = tbl.State,
                              Taluka = tbl.Taluka,
                              District = tbl.District,
                              Hobbies = tbl.Hobbies,
                              UserExpectation = tbl.Expectation,
                              JobDetails = (from tblJob in objData.tblJobDetailss
                                            where tblJob.UserId == tbl.UserId
                                            select new JobDetailsModel
                                            {
                                                CompanyName = tblJob.CompanyName,
                                                Income = tblJob.Income,
                                                IsDelete = tblJob.IsDelete,
                                                IsJobOrBusiness = tblJob.IsJobOrBusiness,
                                                JobId = tblJob.JobId,
                                                JobLocation = tblJob.JobLocation,
                                                //JobBusinessInfo=tblJob.JobBusinessInfo,
                                                UserId = tblJob.UserId
                                            }).FirstOrDefault(),
                              FamilyDetails = (from tblfamily in objData.tblFamilyDetailss
                                               where tblfamily.UserId == tbl.UserId
                                               select new FamilyModel
                                               {
                                                   FathersName = tblfamily.FathersName,
                                                   MothersName = tblfamily.MothersName,
                                                   NoofBrothers = tblfamily.BotherInfo,
                                                   NoOfSisters = tblfamily.SisterInfo,
                                                   IsJob = tblfamily.IsJob,
                                                   FathersIncome = tblfamily.FathersIncome,
                                                   JobBusinessInfo = tblfamily.JobBusinessInfo,

                                                   MobileNo = tblfamily.MobileNo
                                               }).FirstOrDefault(),
                              RelativeDetails = (from tblRel in objData.tblRelativeds
                                                 where tblRel.UserId == tbl.UserId
                                                 select new RelativeModel
                                                 {
                                                     Relation = tblRel.Relation,
                                                     RelativeName = tblRel.RelativeSirName,
                                                     RelativeId = tblRel.RelativeId,
                                                     RelativeAddress = tblRel.Address
                                                 })

                          });
            return result;
        }

        internal List<ProfileImage> GetAllImages()
        {
            var lst= objData.tblProfileImages.Where(p => p.UserId == SessionManager.GetInstance.ActiveUser.UserId).ToList();
            lst.Insert(0, new ProfileImage() { ProfileImageId = 0, UserId = SessionManager.GetInstance.ActiveUser.UserId, ImagePath = SessionManager.GetInstance.ActiveUser.Img1 });
            return lst;
        }

        public int OTP
        {
            get
            {
                int _Otp = 0;
                Random random = new Random(0);
                _Otp = random.Next(100000);
                return _Otp;
            }
        }

        internal bool SendOTP(RegisterViewModel model)
        {
            try
            {
                int otp=OTP;
                string body = "Hello :" + model.UserName;
                body += "<br/>";
                body += "your OTP for Shree Vivah : " + otp ;
                body += "http://localhost:1110/Account/MailConfirmation?OTP=true";

                body += "Thanks";

                var smtp = new System.Net.Mail.SmtpClient(SettingsHelper.SMTP, SettingsHelper.Port);
                {
                    //smtp.Host = "smtp.gmail.com";
                    //smtp.Port = 465;
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    smtp.Credentials = new NetworkCredential(SettingsHelper.MailId, SettingsHelper.Password);
                    //smtp.UseDefaultCredentials = false;
                    smtp.Timeout = 20000;
                }

                smtp.Send(SettingsHelper.MailId, model.Email, "OTP for Shree Vivah", body);
                //objData.tblOTPs.Add(new tblOTP() { IsDelete=false,OTP=Convert.ToString(otp),SendDate=DateTime.Now.Date,UserId=model.UserId});
                //objData.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<tblOTP> GetOTPS()
        {
            return (from tbl in objData.tblOTPs
                    where tbl.IsDelete == false
                    select tbl);
        }

        internal ResponseModel VerifyUserOTP(STP_GetUserDetail user, string OTP)
        {
            tblOTP tbl = GetOTPS().Where(p => p.UserId == user.UserId && p.OTP == OTP).FirstOrDefault();
            if (tbl==null)
            {
                return new ResponseModel() { Status=false, ErrorMessage= SettingsManager.Instance.Branding == "SPMO" ? Resources.SPMOResources.InvalidOTP : "Invalid OTP, Please feel valid OTP" };
            }
            else
            {
                TimeSpan days = tbl.SendDate.Value.Subtract(DateTime.Now.Date);
                if (days.TotalDays>1)
                {
                    tbl.IsDelete = true;
                    objData.SaveChanges();
                    return new ResponseModel() { Status = false, ErrorMessage = SettingsManager.Instance.Branding == "SPMO" ? Resources.SPMOResources.OTPExpired : "OTP has been expired Please Generate OTP again" };
                }
                else
                {
                    return new ResponseModel() { Status = true, ErrorMessage = SettingsManager.Instance.Branding == "SPMO" ? Resources.SPMOResources.MailConfirmation : "your mail id was confirmed. Please complete your profile." };
                }
            }
        }
        public bool SaveMultipleProfileImages(List<ProfileImage> listOfProfileImage)
        {
            bool saveStatus = false;
            try
            {
                foreach (var pi in listOfProfileImage)
                {
                    objData.tblProfileImages.Add(pi);
                    objData.SaveChanges();
                }
                saveStatus = true;
            }
            catch (Exception ex)
            {
                throw;
            }
            
            return saveStatus;
        }
        public void UploadImage(string img, string imgtype)
        {
            STP_GetUserDetail user = SessionManager.GetInstance.ActiveUser;
            if (user != null)
            {
                tblUser obju = (from tbl in objData.tblUsers where tbl.UserId == user.UserId select tbl).FirstOrDefault();
                if (obju != null)
                {
                    switch (imgtype)
                    {
                        case "P":
                            if (string.IsNullOrEmpty(obju.Img1))
                            {
                                obju.Img1 = img;
                            }
                            else
                            {
                                ProfileImage pimg = new ProfileImage() {
                                    ImagePath = img,
                                    UserId=user.UserId,
                                };
                                objData.tblProfileImages.Add(pimg);
                            }
                            break;
                        case "M":
                            if (!string.IsNullOrEmpty(img))
                            {
                                obju.Img2 = img;
                            }
                            break;
                        case "K":
                            if (!string.IsNullOrEmpty(img))
                            {
                                obju.KImg = img;
                            }
                            break;
                    }
                    objData.SaveChanges();
                }
            }
        }

        internal bool Save(tblContactDetails tbl)
        {
            objData.tblContactDetailss.Add(tbl);
            return objData.SaveChanges()>0?true:false;
        }

        internal void UpdateUserProfile(RegisterViewModel model)
        {
            STP_GetUserDetail user = SessionManager.GetInstance.ActiveUser;
            if (user != null)
            {
                tblUser obju = (from tbl in objData.tblUsers where tbl.UserId == user.UserId select tbl).FirstOrDefault();
                if (obju != null)
                {
                    obju.City = model.City;
                    obju.Address = model.Address;
                    obju.MobileNo = model.MobileNo;
                    obju.BirthName = model.BirthName;
                    obju.PlaceofBirth = model.PlaceOfBirth;
                    obju.MailId = model.Email;
                    obju.Weight = model.Weight;
                    obju.Taluka = model.Taluka;
                    obju.District = model.District;
                    obju.IsActive = true;
                    obju.Achievements = model.Achievements;
                    obju.Pincode = model.Pincode;
                    obju.IsOwnHouse = model.IsOwnHouse;
                    obju.ReferenceName = model.ReferenceName;
                    obju.ReferenceContact = model.ReferenceContact;
                    objData.SaveChanges();
                }
            }
        }

        public static void SaveJpeg(string path, Image img, int quality)
        {
            if (quality < 0 || quality > 100)
                throw new ArgumentOutOfRangeException("quality must be between 0 and 100.");

            // Encoder parameter for image quality 
            EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            // JPEG image codec 
            ImageCodecInfo jpegCodec = GetEncoderInfo("image/jpeg");
            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;
            img.Save(path, jpegCodec, encoderParams);
        }

        /// <summary> 
        /// Returns the image codec with the given mime type 
        /// </summary> 
        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats 
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec 
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];

            return null;
        }



        public void Update(RegisterViewModel model)
        {

            STP_GetUserDetail user = SessionManager.GetInstance.ActiveUser;
            if (user!=null)
            {
                tblUser obju = (from tbl in objData.tblUsers where tbl.UserId == user.UserId select tbl).FirstOrDefault();
                if (obju != null)
                {
                    obju.Address = model.Address;
                    obju.BloodGroupId = model.BloodGroupId;
                    obju.BodyType = model.BodyType;
                    obju.CasteId = model.CasteId;
                    obju.ChildLivingStatus = model.ChildLivingStatus;
                    obju.Color = model.Color;
                    obju.DateOfBirth = model.DateOfBirth;
                    obju.DateofReg = DateTime.Now.Date;
                    if (SettingsManager.Instance.Branding != "SPMO")
                    {
                        obju.Gender = model.Gender;
                    }
                    obju.HandicapedType = model.HandicappedType;
                    obju.HeightId = model.HeightId;
                    obju.IdentificationMark = model.IdentificationMark;
                    obju.IsIntercast = model.IsInterCast;
                    obju.IsHandiCapped = model.IsHandicapped;
                    obju.District = model.District;
                    obju.Taluka = model.Taluka;
                    obju.MarritalStatus = model.MarritalStatus;
                    obju.NoOfChildrens = model.NoOfChildrens;
                    obju.OrasId = model.OrasId;
                    obju.PANNO = model.PANNO;
                    obju.PlaceofBirth = model.PlaceOfBirth;
                    obju.QualificationId = 0;
                    obju.Qualification = model.Qualification;
                    obju.ReligionId = model.ReligionId;

                    obju.TimeofBirth = model.strTimeofBirth;
                    obju.Weight = model.Weight;
                    obju.State = model.State;
                    obju.Country = model.Country;
                    obju.BirthName = model.BirthName;
                    obju.City = model.City;
                    obju.Hobbies = model.Hobbies;
                    obju.Expectation = model.UserExpectation;
                    obju.Income = model.Income;
                    obju.Gotra = model.GotraName;
                    obju.IsActive = true;
                    obju.Pincode = model.Pincode;
                    obju.Achievements = model.Achievements;
                    obju.IsOwnHouse = model.IsOwnHouse;
                    obju.ReferenceName = model.ReferenceName;
                    obju.ReferenceContact = model.ReferenceContact;
                    if (SettingsManager.Instance.Branding != "SPMO")
                    {
                        obju.IsActive = false;
                    }
                    obju.IsSpec = model.IsSpec;
                    obju.IsOwnShop = model.IsOwnShop;
                    

                    var req= objData.UserRequest.Where(p => p.UserId == model.UserId).FirstOrDefault();
                    if (req != null)
                    {
                        req.IsApproved = true;
                        req.IsAdminApproved = true;
                    }
                    objData.SaveChanges();
                }
            }
        }
        internal void UpdateParentContact(FamilyModel family)
        {
            STP_GetUserDetail objuser = SessionManager.GetInstance.ActiveUser;
            if (objuser != null)
            {
                tblFamilyDetails objfamily = objData.tblFamilyDetailss.Where(p => p.UserId == objuser.UserId).FirstOrDefault();
                if (objfamily != null)
                {
                    objfamily.MobileNo = family.MobileNo;
                    objData.SaveChanges();
                }
            }
        }

        internal void UpdateJobLocation(tblJobDetails job)
        {
            STP_GetUserDetail objuser = SessionManager.GetInstance.ActiveUser;
            if (objuser != null)
            {
                tblJobDetails objfamily = objData.tblJobDetailss.Where(p => p.UserId == objuser.UserId).FirstOrDefault();
                if (objfamily != null)
                {
                    objfamily.JobLocation = job.JobLocation;
                    objfamily.Income = job.Income;
                    objData.SaveChanges();
                }
                else
                {
                    objfamily = new tblJobDetails()
                    {
                        UserId = objuser.UserId.Value,
                        IsJobOrBusiness = true,
                        Income = job.Income,
                        CompanyName=job.CompanyName,
                        JobLocation = job.JobLocation,
                    };
                    objData.tblJobDetailss.Add(objfamily);
                    objData.SaveChanges();
                }
            }
        }

        internal void ApproveRequest(int visitorId, bool approvedImage, bool approvedImageWithContact)
        {
            tblVisitorDetails visitor = objData.tblVisitorDetailss.Where(p => p.VisitorId == visitorId).FirstOrDefault();
            if (visitor!=null)
            {
                visitor.ApprovedImage = approvedImage;
                visitor.ApprovedImageWithContact = approvedImageWithContact;
                objData.SaveChanges();
            }
        }

        public void Update(FamilyModel family)
        {
            STP_GetUserDetail objuser = SessionManager.GetInstance.ActiveUser;
            if (objuser!=null)
            {
                tblFamilyDetails objfamily = objData.tblFamilyDetailss.Where(p => p.UserId == objuser.UserId).FirstOrDefault();
                if (objfamily!=null)
                {
                    objfamily.MothersName = family.MothersName;
                    objfamily.FathersName = family.FathersName;
                    objfamily.BotherInfo = family.NoofBrothers;
                    objfamily.SisterInfo = family.NoOfSisters;
                    objfamily.MobileNo = family.MobileNo;
                    objfamily.FathersIncome = family.FathersIncome;
                    objfamily.JobBusinessInfo = family.JobBusinessInfo;
                }
                else
                {
                    objfamily = new tblFamilyDetails() 
                    { 
                        FathersName=family.FathersName,
                        MothersName=family.MothersName,
                        BotherInfo=family.NoofBrothers,
                        SisterInfo=family.NoOfSisters,
                        UserId=objuser.UserId,
                        MobileNo=family.MobileNo,
                        FathersIncome=family.FathersIncome,
                        JobBusinessInfo=family.JobBusinessInfo,
                        IsDelete=false,
                    };
                    objData.tblFamilyDetailss.Add(objfamily);
                }
                objData.SaveChanges();
            }
        }

        public void Update(List<RelativeModel> relatives,int UserId=0)
        {
            STP_GetUserDetail objuser = SessionManager.GetInstance.ActiveUser;
            tblUser user = null;
            if (UserId>0)
            {
                user = objData.tblUsers.Where(p => p.UserId == UserId).FirstOrDefault();
            }
            
            if (user != null)
            {
                foreach (RelativeModel item in relatives)
                {
                    tblRelatived objr = new tblRelatived() 
                    { 
                        RelativeSirName=item.RelativeName,
                        Address=item.RelativeAddress,
                        Relation=item.Relation,
                        UserId=objuser.UserId.Value,
                        MobileNo=item.MobileNo,
                    };
                    objData.tblRelativeds.Add(objr);
                }
                objData.SaveChanges();
            }
        }



        public void Save(tblExpectation tbl)
        {
            STP_GetUserDetail objuser = SessionManager.GetInstance.ActiveUser;
            if (objuser != null)
            {
                objData.tblExpectations.Add(tbl);
                objData.SaveChanges();
            }
        }

        internal bool SendRequest(int RequestUserId, int? UserId)
        {
            bool Success = false;
            int cnt = objData.tblRequests.Where(p => p.RequestFrom == UserId && p.RequestTo == RequestUserId).Count();
            int cnt1 = objData.tblRequests.Where(p => p.RequestFrom == RequestUserId && p.RequestTo == UserId).Count();
            if (cnt>0 || cnt1>0)
            {
                Success = false;
            }
            else
            {
                tblRequest userrequest = new tblRequest()
                {
                    RequestDate = DateTime.Now.Date,
                    RequestFrom = Convert.ToInt32(UserId),
                    RequestTo = RequestUserId,
                    RequestStatus = "Pending"
                };

                objData.tblRequests.Add(userrequest);
                objData.SaveChanges();
                Success = true;
            }
            
            return Success;
        }

        internal IQueryable<RequestModel> GetUserReqeusts()
        {
            var result = (from tbl in objData.tblRequests
                         join tblu in objData.tblUsers
                         on tbl.RequestTo equals tblu.UserId
                         join tblr in objData.tblReligions
                         on tblu.ReligionId equals tblr.ReligionId
                         join tblcast in objData.tblCasts
                         on tblu.CasteId equals tblcast.CastId
                         where tbl.IsDelete == false &&
                         tbl.RequestFrom == SessionManager.GetInstance.ActiveUser.UserId
                         select new RequestModel { 
                            Address=tblu.Address,
                            CastId=tblcast.CastId.Value,
                            CastName=tblcast.CastName,
                            ReligionId=tblr.ReligionId.Value,
                            ReligionName=tblr.ReligionName,
                            RequestDate=tbl.RequestDate,
                            RequestFrom = tbl.RequestFrom,
                            RequestId=tbl.RequestId,
                            UserName=tblu.FirstName + " " + tblu.LName,
                            RequestStatus=tbl.RequestStatus,
                            RequestTo= SessionManager.GetInstance.ActiveUser.UserId.Value,
                            InOut="Out"
                         }).Union(from tbl in objData.tblRequests
                         join tblu in objData.tblUsers
                         on tbl.RequestFrom equals tblu.UserId
                         join tblr in objData.tblReligions
                         on tblu.ReligionId equals tblr.ReligionId
                         join tblcast in objData.tblCasts
                         on tblu.CasteId equals tblcast.CastId
                         where tbl.IsDelete == false &&
                         tbl.RequestTo == SessionManager.GetInstance.ActiveUser.UserId
                         select new RequestModel { 
                            Address=tblu.Address,
                            CastId=tblcast.CastId.Value,
                            CastName=tblcast.CastName,
                            ReligionId=tblr.ReligionId.Value,
                            ReligionName=tblr.ReligionName,
                            RequestDate=tbl.RequestDate,
                            RequestFrom = tbl.RequestFrom,
                            RequestId=tbl.RequestId,
                            UserName=tblu.FirstName + " " + tblu.LName,
                            RequestStatus=tbl.RequestStatus,
                            RequestTo=SessionManager.GetInstance.ActiveUser.UserId.Value,
                            InOut = "In"
                         });

            return result;
        }

        internal void UpdateRequest(int RequestId)
        {
            tblRequest request = objData.tblRequests.Where(p => p.RequestId==RequestId).FirstOrDefault();
            if (request!=null)
            {
                request.RequestStatus = "Approved";
                objData.SaveChanges();
            }
        }

        public IQueryable<VisitorModel> GetVisitors()
        { 
            var result=(from tbl in objData.tblVisitorDetailss
                         join tblu in objData.tblUsers
                         on tbl.VisitedUserId equals tblu.UserId
                         join tblr in objData.tblReligions
                         on tblu.ReligionId equals tblr.ReligionId
                         join tblcast in objData.tblCasts
                         on tblu.CasteId equals tblcast.CastId
                         where tbl.IsDelete == false &&
                         tbl.UserId == SessionManager.GetInstance.ActiveUser.UserId
                         && tblu.UserId != SessionManager.GetInstance.ActiveUser.UserId
                        select new VisitorModel { 
                            Address=tblu.Address,
                            CastId=tblcast.CastId.Value,
                            CastName=tblcast.CastName,
                            ReligionId=tblr.ReligionId.Value,
                            ReligionName=tblr.ReligionName,
                            VisitDate=tbl.VisitDate,
                            UserId = SessionManager.GetInstance.ActiveUser.UserId.Value,
                            VisitedUserId=tbl.VisitedUserId,
                            UserName=tblu.FirstName + " " + tblu.LName,
                            VisitorId=tbl.VisitorId,
                         });

            return result;
        }

        internal void InsertVisitorDetails(int ProfileId)
        {
            var useralreadyvisited= objData.tblVisitorDetailss.Where(p => p.VisitedUserId == SessionManager.GetInstance.ActiveUser.UserId && p.UserId==ProfileId).FirstOrDefault();
            if (useralreadyvisited==null)
            {
                tblVisitorDetails visitor = new tblVisitorDetails()
                {
                    IsDelete = false,
                    UserId = ProfileId,
                    VisitDate = DateTime.Now.Date,
                    VisitedUserId = SessionManager.GetInstance.ActiveUser.UserId.Value,
                    IsSendRequest=true,
                };
                objData.tblVisitorDetailss.Add(visitor);
                objData.SaveChanges();
            }
        }

        internal void Save(tblJobDetails job)
        {
            STP_GetUserDetail objuser = SessionManager.GetInstance.ActiveUser;
            if (objuser != null)
            {
                job.UserId = objuser.UserId.Value;
                job.IsDelete = false;
                objData.tblJobDetailss.Add(job);
                objData.SaveChanges();
            }
        }

        internal void SetActive(int UserId, bool IsActive,bool IsAgent=false)
        {
            tblUser user = null;
            if (IsAgent)
            {
                user = objData.tblUsers.Where(p => p.UserId == UserId).FirstOrDefault();
                user.IsActive = IsActive;
            }
            else
            {
                if (SettingsManager.Instance.Branding == "SPMO")
                {
                    UserRequests userdata = objData.UserRequest.Where(p => p.UserId == UserId).FirstOrDefault();
                    user= objData.tblUsers.Where(p => p.UserId == UserId).FirstOrDefault();
                    if (!string.IsNullOrEmpty(user.MobileNo))
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("Your account has been activated. Please do login Using your credintials Login Id : ").Append(user.MobileNo).Append(" And Password : ").Append(user.Password).Append(" and find your matching, Regards Sindhi Hindu, www.sindhihindu.com 9273763490");
                        string message = HttpUtility.UrlEncode(sb.ToString());
                        SendUserSMS(user.MobileNo, message);
                    }
                    userdata.IsApproved = IsActive;
                }
                else
                {
                    user = objData.tblUsers.Where(p => p.UserId == UserId).FirstOrDefault();
                    user.IsActive = IsActive;
                }
            }
            objData.SaveChanges();
            
        }

        internal bool SendWelcomeMail(tblUser model)
        {
            try
            {
                int otp = OTP;
                string body = "Hello :" + model.UserName;
                body += "<br/>";
                body += "Your account has been activated Please login and search your partner ";
                body += "http://localhost:1110";

                body += "Thanks";

                var smtp = new System.Net.Mail.SmtpClient(SettingsHelper.SMTP, SettingsHelper.Port);
                {
                    //smtp.Host = "smtp.gmail.com";
                    //smtp.Port = 465;
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    smtp.Credentials = new NetworkCredential(SettingsHelper.MailId, SettingsHelper.Password);
                    //smtp.UseDefaultCredentials = false;
                    smtp.Timeout = 20000;
                }

                smtp.Send(SettingsHelper.MailId, model.MailId, "OTP for Shree Vivah", body);
                objData.tblOTPs.Add(new tblOTP() { IsDelete = false, OTP = Convert.ToString(otp), SendDate = DateTime.Now.Date, UserId = model.UserId });
                objData.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void SaveAgent(RegisterViewModel model)
        {
            try
            {
                tblUser user = new tblUser() 
                { 
                    UserType=model.UserType,
                    FirstName=model.FirstName,
                    LName=model.LastName,
                    UserName=model.UserName,
                    Password=model.Password,
                    MobileNo=model.MobileNo,
                    MailId=model.Email,
                    City=model.City,
                    Taluka=model.Taluka,
                    District=model.District,
                    State=model.State,
                    Address=model.Address,
                    IsActive=model.IsActive,
                    IsDelete=false,

                };
                objData.tblUsers.Add(user);
                objData.SaveChanges();
                user.PanchayatCode = SettingsManager.Instance.Prefix + user.UserId.ToString();
                objData.SaveChanges();
            }
            catch (Exception ex)
            {
            }
        }

        internal void UpdateAgent(RegisterViewModel model)
        {
            try
            {
                tblUser userold = objData.tblUsers.Where(p => p.UserId == model.UserId).FirstOrDefault();
                if (userold != null)
                {
                    userold.UserType = model.UserType;
                    userold.FirstName = model.FirstName;
                    userold.LName = model.LastName;
                    userold.UserName = model.UserName;
                    userold.Password = model.Password;
                    userold.MobileNo = model.MobileNo;
                    userold.MailId = model.Email;
                    userold.City = model.City;
                    userold.Taluka = model.Taluka;
                    userold.District = model.District;
                    userold.State = model.State;
                    userold.Address = model.Address;
                    userold.IsActive = model.IsActive;
                    userold.IsDelete = false;
                    objData.SaveChanges();
                }
            }
            catch (Exception ex)
            {
            }
        }

        internal int? UpdateUserByAdmin(RegisterViewModel model)
        {
            tblUser user = objData.tblUsers.Where(p => p.UserId == model.UserId).FirstOrDefault();
            if (user!=null)
            {
                user.Address = model.Address;
                user.BirthName = model.BirthName;
                user.BloodGroupId = model.BloodGroupId;
                user.BodyType = model.BodyType;
                user.CasteId = model.CasteId;
                user.ChildLivingStatus = model.ChildLivingStatus;
                user.City = model.City;
                user.Color = model.Color;
                user.Country = model.Country;
                user.DateOfBirth = model.DateOfBirth;
                user.District = model.District;
                user.FirstName = model.FirstName;
                user.Gender = model.Gender;
                user.Gotra = model.GotraName;
                user.HandicapedType = model.HandicappedType;
                user.HeightId = model.HeightId;
                user.Income = model.Income;
                user.IsHandiCapped = model.IsHandicapped;
                user.IsIntercast = model.IsInterCast;
                user.LName = model.LastName;
                user.MailId = model.Email;
                user.MarritalStatus = model.MarritalStatus;
                user.MName = model.MiddleName;
                user.MobileNo = model.MobileNo;
                user.NoOfChildrens = model.NoOfChildrens;
                user.OrasId = model.OrasId;
                user.PlaceofBirth = model.PlaceOfBirth;
                user.QualificationId = model.QualificationId;
                user.Qualification = model.Qualification;
                user.ReligionId = model.ReligionId;
                user.State = model.State;
                user.Taluka = model.Taluka;

                user.TimeofBirth = model.strTimeofBirth;
                user.Weight = model.Weight;
                objData.SaveChanges();
                return user.UserId;
            }
            else
            {
                return 0;
            }
            
        }

        internal void UpdateUserByAdmin(FamilyModel family,int UserId)
        {
            tblFamilyDetails user = objData.tblFamilyDetailss.Where(p => p.UserId == UserId).FirstOrDefault();
            if (user != null)
            {
                user.FathersName = family.FathersName;
                user.FathersIncome = family.FathersIncome;
                user.IsJob = family.IsJob;
                user.JobBusinessInfo = family.JobBusinessInfo;
                user.MobileNo = family.MobileNo;
                user.MothersName = family.MothersName;
                user.BotherInfo = family.NoofBrothers;
                user.SisterInfo = family.NoOfSisters;
                objData.SaveChanges();
            }
            else
            {
                user = new tblFamilyDetails();
                user.FathersName = family.FathersName;
                user.FathersIncome = family.FathersIncome;
                user.IsJob = family.IsJob;
                user.JobBusinessInfo = family.JobBusinessInfo;
                user.MobileNo = family.MobileNo;
                user.MothersName = family.MothersName;
                user.BotherInfo = family.NoofBrothers;
                user.SisterInfo = family.NoOfSisters;
                user.UserId = UserId;
                objData.tblFamilyDetailss.Add(user);
                objData.SaveChanges();
            }
        }

        internal void SaveUserByAdmin(tblJobDetails job,int UserId)
        {
            tblJobDetails user = objData.tblJobDetailss.Where(p => p.UserId == UserId).FirstOrDefault();
            if (user != null)
            {
                user.CompanyName = job.CompanyName;
                user.Income = job.Income;
                user.IsJobOrBusiness = job.IsJobOrBusiness;
                user.JobLocation = job.JobLocation;
                objData.SaveChanges();
            }
            else
            {
                user = new tblJobDetails()
                {
                    CompanyName = job.CompanyName,
                    Income = job.Income,
                    IsJobOrBusiness = job.IsJobOrBusiness,
                    JobLocation = job.JobLocation,
                    UserId=UserId,
                };
                objData.tblJobDetailss.Add(user);
                objData.SaveChanges();
            }
        }

        internal IQueryable<RelativeModel> GetRelativeDetails(int ProfileId)
        {
            var result = (from tbl in objData.tblRelativeds
                          where tbl.IsDelete == false
                           && tbl.UserId == ProfileId
                          select new RelativeModel
                          {
                              ConcernPerson = tbl.ConcernPerson,
                              MobileNo = tbl.MobileNo,
                              Relation = tbl.Relation,
                              RelativeAddress = tbl.Address,
                              RelativeId = tbl.RelativeId,
                              RelativeName = tbl.RelativeSirName,
                              UserId = ProfileId,
                          });
            return result;
        }

        internal bool SendMail(tblUser user)
        {
            bool status = false;
            try
            {
                Random random = new Random(0);
                int password= random.Next(999999999);
                System.Web.Mail.MailMessage message = new System.Web.Mail.MailMessage();
                string fromEmail = "contact@varmalavivah.com";
                string fromPW = "varmala753";
                //string toEmail = "truptikumbhar4@gmail.com";
                const string SERVER = "relay-hosting.secureserver.net";
                System.Web.Mail.MailMessage oMail = new System.Web.Mail.MailMessage();

                oMail.From = fromEmail;
                oMail.To = user.MailId;
                oMail.Subject = "New Password";
                oMail.BodyFormat = System.Web.Mail.MailFormat.Html;	// enumeration
                oMail.Priority = System.Web.Mail.MailPriority.High;	// enumeration

                StringBuilder sb = new StringBuilder();
                sb.Append("Hi ").Append(user.FirstName).Append(",").Append("\n");
                sb.Append("New Password : ").Append(password).Append("\n");
                sb.Append("Thanks,");
                sb.Append("Admin Varmalavivah, Pune");
                oMail.Body = sb.ToString();
                System.Web.Mail.SmtpMail.SmtpServer = SERVER;

                System.Web.Mail.SmtpMail.Send(oMail);
                oMail = null;	// free up resources
                status = true;
                UpdatePassword(user,password.ToString());
            }
            catch (Exception ex)
            {
                
            }
            return status;
        }

        private void UpdatePassword(tblUser user,string password)
        {
            tblUser tbl = objData.tblUsers.Where(p => p.UserId == user.UserId).FirstOrDefault();
            if (tbl!=null)
            {
                tbl.Password = password;
                objData.SaveChanges();
            }
        }

        internal void DeleteUser(int? userId)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand() { Connection = con, CommandType = System.Data.CommandType.StoredProcedure, CommandText = "DeleteUserProfile" };
            cmd.Parameters.Add(new SqlParameter() { DbType = System.Data.DbType.Int32, Direction = System.Data.ParameterDirection.Input, Value = userId, ParameterName = "@UserId" });
            cmd.ExecuteNonQuery();
        }
    }

    public class SearchRequestModel
    {
        public int? UserId { get; set; }
        public string Gender { get; set; }
        public bool? IsMarried { get; set; }
        public bool? IsActive { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
        public int? MinHeightId { get; set; }
        public int? MaxHeightId { get; set; }
        public string Income { get; set; }
        public string City { get; set; }
        public string Qualification { get; set; }
        public int? PageNo { get; set; }
        public bool? PageSize { get; set; }
    }

    public class UserDetails : Error
    {
        public IQueryable<STP_GetUserDetail> UserList { get; set; }
    }
}