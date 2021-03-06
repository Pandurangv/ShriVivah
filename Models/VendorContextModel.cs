﻿using ShriVivah.Models.ContextModel;
using ShriVivah.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShriVivah.Models
{
    public class VendorContextModel
    {
        ShreeVivahDbContext objData = new ShreeVivahDbContext();
        UserContextModel objUser = new UserContextModel();


        internal IQueryable<VendorModel> GetVendors()
        {
            //var agents = objUser.GetAgentDetails().ToList();
            var query = (from c in objData.tblVendors
                         join vendortype in objData.tblVendorTypes
                         on c.VendorTypeId equals vendortype.VendorTypeId
                         join tblagent in objData.tblUsers
                         on c.AgentId equals tblagent.UserId into tblAInners
                         from tblagent in tblAInners.DefaultIfEmpty()
                         orderby c.VendorId ascending
                         select new VendorModel
                         {
                             VendorTypeId = c.VendorTypeId,
                             VendorType = vendortype.VendorType,
                             IsDelete = c.IsDelete,
                             Address=c.Address,
                             BusinessDescription=c.BusinessDescription,
                             City=c.City,
                             ContactNo=c.ContactNo,
                             EmailId=c.EmailId,
                             ExpiryDate=c.ExpiryDate,
                             LogoImage=c.LogoImage,
                             OwnerName=c.OwnerName,
                             Pincode=c.Pincode,
                             ProductImage=c.ProductImage,
                             ProfileImage=c.ProfileImage,
                             RegistrationDate=c.RegistrationDate,
                             ValidDays=c.ValidDays,
                             VendorId=c.VendorId,
                             VendorName=c.VendorName,
                             Country=c.Country,
                             State=c.State,
                             District=c.District,
                             Taluka=c.Taluka,
                             Agent=tblagent,
                             IsActive=c.IsActive,
                         });
            return query;
        }

        internal IQueryable<UserRequests_Vendor> GetVendorsSPMO(long vendorTypeID=0)
        {
            if (vendorTypeID==0)
            {
                var query = from data in objData.viewUserRequests_Vendor
                            select data;
                if (SessionManager.GetInstance.ActiveUser.UserType.ToUpper()!="ADMIN")
                {
                    query = query.Where(p => p.AgentId == SessionManager.GetInstance.ActiveUser.UserId);
                }
                return query;
            }
            else
            {
                var query = from data in objData.viewUserRequests_Vendor
                            where data.VendorTypeId == vendorTypeID
                            select data;
                return query;
            }
        }

        internal IQueryable<UserRequests_Vendor> GetVendorsSPMO(int? vendorTypeID, string VendorType, string SearchCity, string SearchText)
        {
            var query = (from data in objData.viewUserRequests_Vendor
                         where data.IsApproved == true
                         select data).ToList();
            if (vendorTypeID != 0 || Convert.ToString(VendorType) != "")
                query = query.Where(s => s.VendorTypeId == vendorTypeID || s.VendorType.Contains(VendorType)).ToList();
            if (string.IsNullOrEmpty(SearchCity) == false)
                query = query.Where(v => v.City.Contains(SearchCity)).ToList();
            if (string.IsNullOrEmpty(SearchText) == false)
            {
                query = query.Where(x => (Convert.ToString(x.Country) + " " + Convert.ToString(x.Address) + " " + Convert.ToString(x.OwnerName) + " " + Convert.ToString(x.Pincode)).ToLower().Contains(SearchText.ToLower())).ToList();
            }
            return query.AsQueryable();
        }

        public int Save(VendorModel tbl)
        {
            tblVendor vendortype = new tblVendor()
            {
                IsDelete = false,
                VendorTypeId = tbl.VendorTypeId,
                Address=tbl.Address,
                BusinessDescription=tbl.BusinessDescription,
                City=tbl.City,
                ContactNo=tbl.ContactNo,
                EmailId=tbl.EmailId,
                ExpiryDate=tbl.ExpiryDate,
                //LogoImage=tbl.LogoImage,
                OwnerName=tbl.OwnerName,
                Pincode=tbl.Pincode,
                AgentId=tbl.AgentId,
                //ProductImage=tbl.ProductImage,
                //ProfileImage=tbl.ProfileImage,
                RegistrationDate=tbl.RegistrationDate,
                ValidDays=tbl.ValidDays,
                VendorName=tbl.VendorName,
                Country = tbl.Country,
                State = tbl.State,
                District = tbl.District,
                Taluka = tbl.Taluka,
            };
            //tbl.IsDelete = false;
            objData.tblVendors.Add(vendortype);
            objData.SaveChanges();

            if (SettingsManager.Instance.Branding=="SINDHI")
            {
                UserRequests request = new UserRequests()
                {
                    IsDelete = false,
                    IsAdminApproved = false,
                    IsApproved = false,
                    IsUserRequest = false,
                    PanchayatId = tbl.AgentId,
                    RequestDate = DateTime.Now,
                    UserId=0,
                    VendorId=vendortype.VendorId,
                };
                //tbl.IsDelete = false;
                objData.UserRequest.Add(request);
                objData.SaveChanges();
            }

            if (!string.IsNullOrEmpty(tbl.LogoImage) || !string.IsNullOrEmpty(tbl.ProfileImage) || !string.IsNullOrEmpty(tbl.ProductImage))
            {
                tbl.VendorId = vendortype.VendorId;
                ChangeVendorImagePath(tbl);
            }
            return vendortype.VendorId.Value;
        }

        private void ChangeVendorImagePath(VendorModel vendortype)
        {
            try
            {
                string path = HttpContext.Current.Server.MapPath("~/Content/VendorImages/");
                path = path + vendortype.VendorId.Value;
                System.IO.DirectoryInfo info = new System.IO.DirectoryInfo(path);
                info.Create();
                string destlogopath=string.Empty;
                string destprofilepath=string.Empty;
                string destproductpath=string.Empty;
                if (!string.IsNullOrEmpty(vendortype.LogoImage))
                {
                    destlogopath = vendortype.LogoImage.Replace("Temp", vendortype.VendorId.Value.ToString());
                    System.IO.File.Copy(HttpContext.Current.Server.MapPath("~/"+vendortype.LogoImage),HttpContext.Current.Server.MapPath("~/"+ destlogopath), true);
                }
                if (!string.IsNullOrEmpty(vendortype.ProfileImage))
                {
                    destprofilepath = vendortype.ProfileImage.Replace("Temp", vendortype.VendorId.Value.ToString());
                    System.IO.File.Copy(HttpContext.Current.Server.MapPath("~/"+vendortype.ProfileImage), HttpContext.Current.Server.MapPath("~/"+destprofilepath), true);    
                }
                if (!string.IsNullOrEmpty(vendortype.ProductImage))
                {
                    destproductpath = vendortype.ProductImage.Replace("Temp", vendortype.VendorId.Value.ToString());
                    System.IO.File.Copy(HttpContext.Current.Server.MapPath("~/" + vendortype.ProfileImage),HttpContext.Current.Server.MapPath("~/"+destproductpath), true);
                }
                if (!string.IsNullOrEmpty(destlogopath) || !string.IsNullOrEmpty(destproductpath) || !string.IsNullOrEmpty(destprofilepath))
                {
                    UpdateImagePath(destlogopath, destproductpath, destprofilepath, vendortype.VendorId);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void UpdateImagePath(string destlogopath, string destproductpath, string destprofilepath, int? VendorId)
        {
            tblVendor newobj = objData.tblVendors.Where(p => p.VendorId == VendorId).FirstOrDefault(); //GetVendorTypes().Where(p => p.VendorTypeId == model.VendorTypeId).FirstOrDefault();
            if (newobj != null)
            {
                newobj.LogoImage = destlogopath;
                newobj.ProductImage = destproductpath;
                newobj.ProfileImage = destprofilepath;
                objData.SaveChanges();
            }
        }

        internal void Update(VendorModel model)
        {
            tblVendor newobj = objData.tblVendors.Where(p => p.VendorId == model.VendorId).FirstOrDefault(); //GetVendorTypes().Where(p => p.VendorTypeId == model.VendorTypeId).FirstOrDefault();
            if (newobj!=null)
            {
                newobj.Address = model.Address;
                newobj.BusinessDescription = model.BusinessDescription;
                newobj.City = model.City;
                newobj.ContactNo = model.ContactNo;
                newobj.EmailId = model.EmailId;
                newobj.ExpiryDate = model.ExpiryDate;
                newobj.LogoImage = model.LogoImage;
                newobj.OwnerName = model.OwnerName;
                newobj.Pincode = model.Pincode;
                newobj.ProductImage = model.ProductImage;
                newobj.ProfileImage = model.ProfileImage;
                newobj.RegistrationDate = model.RegistrationDate;
                newobj.ValidDays = model.ValidDays;
                newobj.VendorName = model.VendorName;
                newobj.Country = model.Country;
                newobj.State = model.State;
                newobj.District = model.District;
                newobj.Taluka = model.Taluka;
                newobj.AgentId = model.AgentId;
                objData.SaveChanges();
            }
        }

        internal bool ActiveDeactiveVendorSPMO(int RequestId, bool IsActive)
        {
            bool success = false;
            UserRequests vendor = objData.UserRequest.Where(p => p.UserRequestId == RequestId).FirstOrDefault();
            if (vendor != null)
            {
                vendor.IsApproved = IsActive;
                objData.SaveChanges();
            }
            success = true;
            return success;
        }

        internal bool ActiveDeactiveVendor(int VendorId, bool IsActive)
        {
            bool success = false;
            tblVendor vendor = objData.tblVendors.Where(p => p.VendorId == VendorId).FirstOrDefault();
            if (vendor!=null)
            {
                vendor.IsActive = IsActive;
                objData.SaveChanges();
            }
            success = true;
            return success;
        }
    }
}