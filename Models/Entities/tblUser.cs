﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShriVivah.Models.Entities
{
    [Table("tblOTP")]
    public class tblOTP
    {
        [Key]
        public int? OTPId { get; set; }

        public string OTP { get; set; }

        public int? UserId { get; set; }

        public DateTime? SendDate { get; set; }

        public bool? IsDelete { get; set; }
    }



    [Table("tblContactDetails")]
    public class tblContactDetails
    {
        [Key]
        public int? ContactUsId { get; set; }

        public string Name { get; set; }

        public string MailId { get; set; }

        public bool? MailSendingStatus { get; set; }

        public string Description { get; set; }

        public string MobileNo { get; set; }

        public string Sub { get; set; }
    }

    [Table("tblContactUs")]
    public class tblContactUs
    {
        [Key]
        public int? ContactUsId { get; set; }

        public string Name { get; set; }

        public string MailId { get; set; }

        public bool? MailSendingStatus { get; set; }

        public string Description { get; set; }
    }

    [Table("EventManagement")]
    public class EventManagement
    {
        [Key]
        public int? EventId { get; set; }

        public string EventName { get; set; }

        public string EventLocation { get; set; }

        public DateTime? EventDate { get; set; }

        public string EventDistrict { get; set; }

        public string EventState { get; set; }

        public int OrganizedBy { get; set; }

        public string ContactPerson { get; set; }

        public string MobileNo { get; set; }

        public bool IsDelete { get; set; }

        public string EventImage { get; set; }

    }

    [Table("tblVendor")]
    public class tblVendor
    {
        [Key]
        public int? VendorId { get; set; }

        public string VendorName { get; set; }

        public string BusinessDescription { get; set; }

        public DateTime? RegistrationDate { get; set; }

        public string LogoImage { get; set; }

        public string ProfileImage { get; set; }

        public int AgentId { get; set; }

        public string ProductImage { get; set; }

        public int ValidDays { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public string OwnerName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string ContactNo { get; set; }

        public string EmailId { get; set; }

        public string Pincode { get; set; }

        public bool IsDelete { get; set; }

        public int VendorTypeId { get; set; }

        //public string City { get; set; }

        public string State { get; set; }

        public bool IsActive { get; set; }

        public string District { get; set; }

        public string Taluka { get; set; }

        public string Country { get; set; }
    }

    [Table("tblExpectation")]
    public class tblExpectation
    {

        [Key]
        public int ExpectationId { get; set; }

        public string Qualification { get; set; }

        public int UserId { get; set; }

        public string Height { get; set; }

        public string Color { get; set; }

        public decimal Salary { get; set; }

        public decimal SalaryTo { get; set; }

        public bool JobOrBusiness { get; set; }

        public string Other { get; set; }

        public string OrasId { get; set; }

        public bool IsDelete { get; set; }

    }

    [Table("tblMessage")]
    public class tblMessages
    {
        [Key]
        public int? MessageId { get; set; }

        public string MessageText { get; set; }

        public int? ToUserId { get; set; }

        public int? FromUserId { get; set; }

        public DateTime? MessageDate { get; set; }

        public bool? IsDelete { get; set; }
    }

    [Table("tblFamilyDetails")]
    public class tblFamilyDetails
    {
        [Key]
        public int? FamilyMemId { get; set; }

        public string FathersName { get; set; }

        public string MothersName { get; set; }

        public int? Age { get; set; }

        public string MobileNo { get; set; }

        public string BotherInfo { get; set; }

        public string SisterInfo { get; set; }

        public int? UserId { get; set; }

        public bool? IsDelete { get; set; }

        public int NoofBrothers { get; set; }

        public int NoofSisters { get; set; }

        public bool? IsJob { get; set; }

        public string FathersIncome { get; set; }

        public string JobBusinessInfo { get; set; }

        public string JobBusinessInfo1 { get; set; }

        public string GrandFatherName { get; set; }

        public int? NoOfMBro { get; set; }

        public int? NoOfMSis { get; set; }
    }

    [Table("tblVendorType")]
    public class tblVendorType
    {
        [Key]
        public int? VendorTypeId { get; set; }

        public string VendorType { get; set; }

        public bool IsDelete { get; set; }

        public string TypeImagesPath { get; set; }
    }

    [Table("tblRelatived")]
    public class tblRelatived
    {
        [Key]
        public int? RelativeId { get; set; }

        public string RelativeSirName { get; set; }

        public string Address { get; set; }

        public string ConcernPerson { get; set; }

        public string Relation { get; set; }

        public int UserId { get; set; }

        public string MobileNo { get; set; }

        public bool IsDelete { get; set; }
    }

    

    [Table("tblVisitorDetails")]
    public class tblVisitorDetails
    {
        [Key]
        public int VisitorId { get; set; }

        public DateTime? VisitDate { get; set; }

        public int UserId { get; set; }

        public bool? IsDelete { get; set; }

        public int VisitedUserId { get; set; }

        public bool? IsSendRequest { get; set; }

        public bool? ApprovedImage { get; set; }

        public bool? ApprovedImageWithContact { get; set; }
    }

    [Table("LoginDetails")]
    public class LoginDetails
    {
        [Key]
        public int LoginId { get; set; }

        public DateTime? LoginDate { get; set; }

        public string LoginIP { get; set; }

        public string Location { get; set; }

        public string RequestDetails { get; set; }

        public int? UserId { get; set; }

    }

    [Table("Shortlist")]
    public class Shortlist
    {
        [Key]
        public long? ShortlistId { get; set; }

        public DateTime? RequestDate { get; set; }

        public int UserId { get; set; }

        public int RequestUserId { get; set; }

        public bool IsDelete { get; set; }

        public DateTime? CreatedDate { get; set; }

    }

    [Table("tblRequest")]
    public class tblRequest
    {
        [Key]
        public int? RequestId { get; set; }

        public DateTime? RequestDate { get; set; }

        public int RequestFrom { get; set; }

        public int RequestTo { get; set; }

        public string RequestStatus { get; set; }

        public bool IsDelete { get; set; }

        public int? FullAccess { get; set; }

        public int? OnlyImageFullAccess { get; set; }

        public DateTime? ConfirmDate { get; set; }
    }

    public class STP_SelectMessageUser
    {
        public int? Id { get; set; }

        public string FirstName { get; set; }

        public string LName { get; set; }
    }

    public class SP_GetAgentDetails
    {
        public int? UserId { get; set; }

        public string FirstName { get; set; }

        public string Address { get; set; }

        public string LName { get; set; }

        public string City { get; set; }

        public string UserType { get; set; }

        public string Password { get; set; }

        public string MailId { get; set; }

        public string MobileNo { get; set; }

        public string State { get; set; }

        public string PanchayatCode { get; set; }

        public string District { get; set; }

        public string Taluka { get; set; }
    }

    [Table("UserRequests")]
    public class UserRequests
    {
        [Key]
        public long UserRequestId { get; set; }

        public int? UserId { get; set; }

        public int? PanchayatId { get; set; }

        public int? VendorId { get; set; }

        public bool? IsUserRequest { get; set; }

        public DateTime? RequestDate { get; set; }

        public bool? IsApproved { get; set; }

        public bool? IsDelete { get; set; }

        public bool? IsAdminApproved { get; set; }

        public DateTime? ApproveDate { get; set; }

        public DateTime? AdminApproveDate { get; set; }
    }

    public class CityList
    {
        public int Id { get; set; }

        public string City { get; set; }
    }

    public class QualificationList
    {
        public int Id { get; set; }

        public string Qualification { get; set; }
    }


  

    public class STP_GetUserDetailCopy
    {
        public int? IsManglik { get; set; }

        public int? Jobdata { get; set; }

        public int? NoOfMBro { get; set; }

        public int? NoOfMSis { get; set; }

        public string SubCaste { get; set; }

        public string ReligionName { get; set; }

        public string CastName { get; set; }

        public bool? IsOwnShop { get; set; }

        public int? ReligionId { get; set; }

        public string BloodGroupName { get; set; }

        public bool IsAdhaarVerified { get; set; }

        public string DegreeName { get; set; }

        public string Height { get; set; }

        public int? UserId { get; set; }

        public string FirstName { get; set; }

        public string MName { get; set; }

        public string LName { get; set; }

        public int? BloodGroupId { get; set; }

        public int? CityId { get; set; }

        public int? CasteId { get; set; }

        public int? OrasId { get; set; }

        public string OrasName { get; set; }

        public string Address { get; set; }

        public string Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public int? HeightId { get; set; }

        public int? Weight { get; set; }

        public string IdentificationMark { get; set; }

        public string PANNO { get; set; }

        public string MobileNo { get; set; }

        public string MailId { get; set; }

        public string Color { get; set; }

        public bool? IsHandiCapped { get; set; }

        public string HandicapedType { get; set; }

        public bool? IsActive { get; set; }

        public string UserType { get; set; }

        public DateTime? DateofReg { get; set; }

        public int? MarritalStatus { get; set; }

        public int? NoOfChildrens { get; set; }

        public bool? ChildLivingStatus { get; set; }

        public string PlaceofBirth { get; set; }

        public string TimeofBirth { get; set; }

        public string BodyType { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public int? QualificationId { get; set; }


        public string Qualification { get; set; }
        public string Income { get; set; }

        public string Img1 { get; set; }

        public string Img2 { get; set; }

        public string KImg { get; set; }

        public string Gotra { get; set; }

        public string BirthName { get; set; }

        public string Hobbies { get; set; }

        public string Expectation { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string District { get; set; }

        public string Taluka { get; set; }

        public bool? IsIntercast { get; set; }

        public int? JobId { get; set; }

        public bool? IsJobOrBusiness { get; set; }

        public string Salary { get; set; }

        public string Post { get; set; }

        public string CompanyName { get; set; }

        public bool? PermanentOrContract { get; set; }

        public string JobType { get; set; }

        public string JobLocation { get; set; }

        public string Expr1 { get; set; }

        public string FathersName { get; set; }

        public int? FamilyMemId { get; set; }

        public string MothersName { get; set; }

        public int? Age { get; set; }

        //public string Expr2 { get; set; }

        public int? NoofBrothers { get; set; }

        public int? NoofSisters { get; set; }

        public bool? IsJob { get; set; }

        public string FathersIncome { get; set; }

        public string JobBusinessInfo { get; set; }

        public string JobBusinessInfo1 { get; set; }

        public string BotherInfo { get; set; }

        public string SisterInfo { get; set; }

        public int? ismarried { get; set; }

        public string BirthCountry { get; set; }

        public string BirthDistrict { get; set; }

        public string BirthState { get; set; }

        public string PanchayatCode { get; set; }

        //public string Name { get; set; }

        public int? PanchayatId { get; set; }

        public int? IsOwnHouse { get; set; }

        public string Achievements { get; set; }

        public string Pincode { get; set; }

        public string ReferenceName { get; set; }

        public string ReferenceContact { get; set; }

        public string BehalfOf { get; set; }

        public bool? IsSpec { get; set; }

        public int? TotalRecords { get; set; }

    }

    public class SP_GetShortlists
    {
        public int? ReqUserId { get; set; }

        public long? RowNum { get; set; }

        public string ReqUserName { get; set; }

        public int? RequestToUserId { get; set; }

        public string UserType { get; set; }

        public string Qualification { get; set; }

        public string City { get; set; }

        public string Income { get; set; }

        public string FirstName { get; set; }

        public string LName { get; set; }

        public int? TotalRecords { get; set; }

        public string Height { get; set; }

        public int? MarritalStatus { get; set; }

        public int? Age { get; set; }

        public int? Jobdata { get; set; }

        public DateTime? RequestDate { get; set; }

        public string Img1 { get; set; }

        public int? FullAccess { get; set; }

        public int? OnlyImageFullAccess { get; set; }

        public string Gender { get; set; }

    }

    public class SP_GetRequests
    {
        public int? RequestId { get; set; }

        public long? RowNum { get; set; }

        public int? UserId { get; set; }

        public string UserType { get; set; }

        public string Qualification { get; set; }

        public string City { get; set; }

        public string FirstName { get; set; }

        public string LName { get; set; }

        public int? TotalRecords { get; set; }

        public string Height { get; set; }

        public int? MarritalStatus { get; set; }

        public int? Age { get; set; }

        public int? Jobdata { get; set; }

        public DateTime? RequestDate { get; set; }

        public string Income { get; set; }

        public string Img1 { get; set; }

        public int? FullAccess { get; set; }

        public int? OnlyImageFullAccess { get; set; }

        public string Gender { get; set; }

    }


    public class STP_GetUserDetail
    {
        public string ReligionName { get; set; } 

        public string CastName { get; set; }

        public bool? IsOwnShop { get; set; }

        public int? ReligionId { get; set; }

        public string BloodGroupName { get; set; }

        public string DegreeName { get; set; }

        public string Height { get; set; }

        public int? UserId { get; set; }

        public string FirstName { get; set; }

        public string MName { get; set; }

        public string LName { get; set; }

        public int? BloodGroupId { get; set; }

        public int? CityId { get; set; }

        public int? CasteId { get; set; }

        public int? OrasId { get; set; }

        public string OrasName { get; set; }

        public string Address { get; set; }

        public string Gender { get; set; }
        
        public DateTime? DateOfBirth { get; set; }

        public int? HeightId { get; set; }

        public int? Weight { get; set; }

        public string IdentificationMark { get; set; }

        public string PANNO { get; set; }

        public string MobileNo { get; set; }

        public string MailId { get; set; }

        public string Color { get; set; }

        public bool? IsHandiCapped { get; set; }

        public string HandicapedType { get; set; }

        public bool? IsActive { get; set; }

        public string UserType { get; set; }

        public DateTime? DateofReg { get; set; }

        public int? MarritalStatus { get; set; }

        public int? NoOfChildrens { get; set; }

        public bool? ChildLivingStatus { get; set; }

        public string PlaceofBirth { get; set; }

        public string TimeofBirth { get; set; }

        public string BodyType { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public int? QualificationId { get; set; }


        public string Qualification { get; set; }
        public string Income { get; set; }

        public string Img1 { get; set; }

        public string Img2 { get; set; }

        public string KImg { get; set; }

        public string Gotra { get; set; }

        public string BirthName { get; set; }

        public string Hobbies { get; set; }

        public string Expectation { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string District { get; set; }

        public string Taluka { get; set; }

        public bool? IsIntercast { get; set; }

        public int? JobId { get; set; }

        public bool? IsJobOrBusiness { get; set; }

        public string Salary { get; set; }

        public string Post { get; set; }

        public string CompanyName { get; set; }

        public bool? PermanentOrContract { get; set; }

        public string JobType { get; set; }

        public string JobLocation { get; set; }

        public string Expr1 { get; set; }

        public string FathersName { get; set; }

        public int? FamilyMemId { get; set; }

        public string MothersName { get; set; }

        public int? Age { get; set; }

        public string Expr2 { get; set; }

        public int? NoofBrothers { get; set; }
        
        public int? NoofSisters { get; set; }

        public bool? IsJob { get; set; }

        public string FathersIncome { get; set; }

        public string JobBusinessInfo { get; set; }

        public string JobBusinessInfo1 { get; set; }

        public string BotherInfo { get; set; }

        public string SisterInfo { get; set; }

        public int? ismarried { get; set; }

        public string BirthCountry { get; set; }

        public string BirthDistrict { get; set; }

        public string BirthState { get; set; }

        public string PanchayatCode { get; set; }

        public string Name { get; set; }

        public int? PanchayatId { get; set; }

        public int? IsOwnHouse { get; set; }

        public string Achievements { get; set; }

        public string Pincode { get; set; }

        public string ReferenceName { get; set; }

        public string ReferenceContact { get; set; }

        public string BehalfOf { get; set; }

        public bool? IsSpec { get; set; }

        public int? IsManglik { get; set; }

        public int? Jobdata { get; set; }

        public int? NoOfMBro { get; set; }

        public int? NoOfMSis { get; set; }

        public string SubCaste { get; set; }

    }

    [Table("tblUser")]
    public class tblUser
    {
        [Key]
        public int? UserId { get; set; }

        public bool? IsOwnShop { get; set; }

        public string FirstName { get; set; }

        public string MName { get; set; }

        public string LName { get; set; }

        public bool? IsSpec { get; set; }

        public string BehalfOf { get; set; }

        public int? BloodGroupId { get; set; }

        public int? CityId { get; set; }

        public int? ReligionId { get; set; }

        public int? CasteId { get; set; }

        public string SubCaste { get; set; }

        public int? OrasId { get; set; }

        public int? NakshatraId { get; set; }

        //public byte[] Image1 { get; set; }

        //public byte[] Image2 { get; set; }

        public string Gotra { get; set; }

        public string BirthName { get; set; }

        public string Hobbies { get; set; }

        public string Expectation { get; set; }



        public int? GanId { get; set; }

        public string Address { get; set; }

        public string Gender { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public int? HeightId { get; set; }

        public int? Weight { get; set; }

        public string IdentificationMark { get; set; }

        public string PANNO { get; set; }

        public string MobileNo { get; set; }

        public string MobileNo1 { get; set; }

        public string MailId { get; set; }

        //public Guid? AspNetUserId { get; set; }

        public bool? IsHandiCapped { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string HandicapedType { get; set; }

        public string Color { get; set; }

        //public byte[] KundaliImage { get; set; }

        public bool? IsDelete { get; set; }

        public string Img1 { get; set; }

        public string Img2 { get; set; }

        public string KImg { get; set; }

        public bool? IsActive { get; set; }

        public string UserType { get; set; }

        public DateTime? DateofReg { get; set; }

        public int? MarritalStatus { get; set; }  //1=Unmarried 2=Widow 3=Divorcee 4=Separated

        public int? NoOfChildrens { get; set; }

        public string BodyType { get; set; }

        public bool? ChildLivingStatus { get; set; }

        public string PlaceofBirth { get; set; }

        public string TimeofBirth { get; set; }

        public int? QualificationId { get; set; }

        public string Qualification { get; set; }

        public string Income { get; set; }

        public bool IsIntercast { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string Taluka { get; set; }

        public string District { get; set; }

        public int? ismarried { get; set; }

        public string BirthCountry { get; set; }

        public string BirthDistrict { get; set; }

        public string BirthState { get; set; }

        public string PanchayatCode { get; set; }

        public int? IsOwnHouse { get; set; }

        public string Achievements { get; set; }

        public string Pincode { get; set; }

        public string ReferenceName { get; set; }

        public string ReferenceContact { get; set; }
    }

    [Table("tblJobDetails")]
    public class tblJobDetails
    {
        [Key]
        public int? JobId { get; set; }

        public bool? IsJobOrBusiness { get; set; }

        public decimal? Salary { get; set; }

        public string Post { get; set; }

        public string CompanyName { get; set; }

        public bool? PermanentOrContract { get; set; }

        public string JobType { get; set; }

        public string Income { get; set; }

        public bool? IsDelete { get; set; }

        public string JobLocation { get; set; }

        public int UserId { get; set; }


        public int? IsManglik { get; set; }

        public int? Jobdata { get; set; }

        //public string JobBusinessInfo { get; set; }

        //public string JobBusinessInfo1 { get; set; }
    }

    [Table("PackageMaster")]
    public class PackageMaster
    {
        [Key]
        public long? PackageId { get; set; }

        public string PackageName { get; set; }

        public decimal? Price { get; set; }

        public string Gender { get; set; }

        public int AccessUsers { get; set; }

        public int Validity { get; set; }

        public bool? IsDelete { get; set; }
    }

    [Table("UserPackage")]
    public class UserPackage
    {
        [Key]
        public long? UserPackageId { get; set; }

        public long? PackageId { get; set; }

        public decimal? Amount { get; set; }

        public DateTime? PackageDate { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int UserId { get; set; }

        public bool? IsDelete { get; set; }
    }


    [Table("Receipt")]
    public class Receipt
    {
        [Key]
        public long? ReceiptNo { get; set; }

        public DateTime? ReceiptDate { get; set; }

        public int? PaymentMethod { get; set; }

        public DateTime? PaymentDate { get; set; }

        public decimal? Amount { get; set; }

        public string OnlinePaymentFailure { get; set; }

        public int UserId { get; set; }

        public string FailureLog { get; set; }

        public DateTime? LoginDate { get; set; }

        public bool? IsDelete { get; set; }
    }

    [Table("ProfileImage")]
    public class ProfileImage
    {
        [Key]
        public long? ProfileImageId { get; set; }

        public int? UserId { get; set; }

        [Required(ErrorMessage = "*")]
        public string ImagePath { get; set; }

        public string Caption { get; set; }

        public string CreatedBy { get; set; }
    }

    [Table("tblQualification")]
    public class tblQualification
    {
        [Key]
        public int? QualificationId { get; set; }

        public string DegreeName { get; set; }

        public string BoardUni { get; set; }

        public decimal? Percentage { get; set; }

        public string Grade { get; set; }

        public int? UserId { get; set; }

        public bool? IsDelete { get; set; }


    }

    //[Table("aspnet_UsersInRole")]
    //public class aspnet_UsersInRole
    //{
        
    //    [ForeignKey("UserId")]
    //    public aspnet_Users UserId { get; set; }

    //    [ForeignKey("RoleId")]
    //    public aspnet_Roles RoleId { get; set; }
    //}

    //[Table("aspnet_Roles")]
    //public class aspnet_Roles
    //{
    //    [ForeignKey("ApplicationId")]
    //    public aspnet_Applications ApplicationId { get; set; }

    //    [Key]
    //    public Guid? RoleId { get; set; }

    //    public int MyProperty { get; set; }
    //}

    //[Table("aspnet_Applications")]
    //public class aspnet_Applications
    //{
    //    public string ApplicationName { get; set; }

    //    public string LoweredApplicationName { get; set; }

    //    [Key]
    //    public Guid? ApplicationId { get; set; }

    //    public string Description { get; set; }
    //}

    //[Table("aspnet_Users")]
    //public class aspnet_Users
    //{
    //    [ForeignKey("ApplicationId")]
    //    public aspnet_Applications ApplicationId { get; set; }

    //    [Key]
    //    public Guid? UserId { get; set; }

    //    public string UserName { get; set; }

    //    public string LoweredUserName { get; set; }

    //    public string MobileAlias { get; set; }

    //    public bool IsAnonymous { get; set; }

    //    public DateTime LastActivityDate { get; set; }


    //}


    [Table("tblBloodGroup")]
    public class tblBloodGroup
    {
        [Key]
        public int? BloodGroupId { get; set; }

        [Required(ErrorMessage="*")]
        public string BloodGroupName { get; set; }

        public bool? IsDelete { get; set; }
    }

    [Table("tblCity")]
    public class tblCity
    {

        [Key]
        public int? CityId { get; set; }

        public int? StateId { get; set; }

        [Required(ErrorMessage = "*")]
        public string CityName { get; set; }

        [Required(ErrorMessage="*")]
        [MaxLength(6,ErrorMessage="Length should be 6 digits")]
        [MinLength(6, ErrorMessage = "Length should be 6 digits.")]
        public string Pincode { get; set; }

        public bool? IsDelete { get; set; }
    }

    [Table("tblCast")]
    public class tblCast
    {

        [Key]
        public int? CastId { get; set; }

        public int? ReligionId { get; set; }

        [Required(ErrorMessage = "*")]
        public string CastName { get; set; }

        public bool? IsDelete { get; set; }
    }

    [Table("tblState")]
    public class tblState
    {
        
        [Key]
        public int? StateId { get; set; }

        public int? CountryId { get; set; }

        [Required(ErrorMessage = "*")]
        public string StateName { get; set; }

        public bool? IsDelete { get; set; }
    }

    [Table("SPMO_VIEW_UserRequests_Vendor")]
    public class UserRequests_Vendor
    {
        [Key]
        public long UserRequestID { get; set; }
        public int? UserID { get; set; }
        public int? PanchayatId { get; set; }
        public int? VendorId { get; set; }
        public bool IsUserRequest { get; set; }
        public DateTime? RequestDate { get; set; }
        public bool IsApproved { get; set; }
        public bool IsDelete { get; set; }
        public bool IsAdminApproved { get; set; }
        public string VendorName { get; set; }
        public string BusinessDescription { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string LogoImage { get; set; }
        public string ProfileImage { get; set; }
        public string ProductImage { get; set; }
        public int? ValidDays { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string OwnerName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ContactNo { get; set; }
        public string EmailId { get; set; }
        public string Pincode { get; set; }
        public string District { get; set; }
        public string Taluka { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public int? AgentId { get; set; }
        public int? VendorTypeId { get; set; }

        public string VendorType { get; set; }
    }

    [Table("tblOras")]
    public class tblOras
    {
        [Key]
        public int? OrasId { get; set; }

        [Required(ErrorMessage = "*")]
        public string OrasName { get; set; }

        public bool? IsDelete { get; set; }
    }

    [Table("tblHeight")]
    public class tblHeight
    {
        [Key]
        public int? HeightId { get; set; }

        [Required(ErrorMessage = "*")]
        public string Height { get; set; }

        public bool? IsDelete { get; set; }
    }



    [Table("tblReligion")]
    public class tblReligion
    {
        [Key]
        public int? ReligionId { get; set; }

        [Required(ErrorMessage = "*")]
        public string ReligionName { get; set; }

        public bool? IsDelete { get; set; }
    }

    [Table("tblCountry")]
    public class tblCountry
    {
        [Key]
        public int? CountryId { get; set; }

        [Required(ErrorMessage = "*")]
        public string CountryName { get; set; }

        public bool? IsDelete { get; set; }
    }

    [Table("tblGan")]
    public class tblGan
    {
        [Key]
        public int? GanId { get; set; }

        [Required(ErrorMessage = "*")]
        public string GanName { get; set; }

        public bool? IsDelete { get; set; }
    }

    [Table("tblGotra")]
    public class tblGotra
    {
        [Key]
        public int? GotraId { get; set; }

        [Required(ErrorMessage = "*")]
        public string GotraName { get; set; }

        public bool? IsDelete { get; set; }
    }

}