using ShriVivah.Models.DataModels;
using ShriVivah.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ShriVivah.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }
    }

    public class ManageUserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public string MobileNo { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class BasicResponse : Error
    {
        public IQueryable<CastModel> CastData { get; set; }

        public IQueryable<tblOras> OrasData { get; set; }

        public IQueryable<tblHeight> HeightData { get; set; }

        public IQueryable<tblReligion> ReligionData { get; set; }

        public IQueryable<tblCountry> CountryData { get; set; }

        public IQueryable<StateContextModel> StateList { get; set; }

        public IQueryable<tblBloodGroup> BloodGroup { get; set; }

    }

    public class RegisterReuest
    {
        public RegisterReuest()
        {
            RelativeList = new List<RelativeModel>();
            UserFamily = new FamilyModel();
            RelativeList = new List<RelativeModel>();
            UserJobDetails = new JobDetailsModel();
            UserExpectation = new tblExpectation();
            UserData = new RegisterViewModel();
        }

        public RegisterViewModel UserData { get; set; }

        public FamilyModel UserFamily { get; set; }

        public List<RelativeModel> RelativeList { get; set; }

        public JobDetailsModel UserJobDetails { get; set; }

        public tblExpectation UserExpectation { get; set; }
    }

    public class RegisterResponse : Error
    {
        public IQueryable<STP_GetUserDetail> UserList { get; set; }

        public IQueryable<VendorTypeModel> VendorTypes { get; set; }

        public IQueryable<tblVendor> VendorsList { get; set; }


    }


    public class RegisterViewModel
    {
        public int? UserId { get; set; }

        public string RoleName { get; set; }

        public int? Age { get; set; }

        public int AgentId { get; set; }

        public string OTP { get; set; }

        public string Height { get; set; }

        public int? IsOwnHouse { get; set; }


        public string Pincode { get; set; }

        public string ReferenceName { get; set; }

        public string ReferenceContact { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "First name")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "*")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Mail Id")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public Guid? AspNetUserId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public int? BloodGroupId { get; set; }

        public int? CityId { get; set; }

        public int? ReligionId { get; set; }

        public int? CasteId { get; set; }

        public int SubCasteId { get; set; }

        public int? OrasId { get; set; }

        public int? NakshatraId { get; set; }

        //public byte[] Image1 { get; set; }

        //public byte[] Image2 { get; set; }

        public int? GanId { get; set; }

        public string Address { get; set; }

        public string Gender { get; set; }

        public string BirthName { get; set; }

        public string Hobbies { get; set; }

        public string UserExpectation { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public int? HeightId { get; set; }

        public int? Weight { get; set; }

        public string IdentificationMark { get; set; }

        public string PANNO { get; set; }

        public string MobileNo { get; set; }

        public bool? IsHandicapped { get; set; }

        public string HandicappedType { get; set; }

        public string Color { get; set; }

        public string Img1 { get; set; }

        public string Img2 { get; set; }

        public string KImg { get; set; }

        //public byte[] KundliImage { get; set; }

        public bool? IsDelete { get; set; }

        public string UserType { get; set; }

        public DateTime? DateofReg { get; set; }

        public int? MarritalStatus { get; set; }

        public int? NoOfChildrens { get; set; }

        public bool ChildLivingStatus { get; set; }

        public string PlaceOfBirth { get; set; }

        public string GotraName { get; set; }

        public string TimeofBirth { get; set; }

        public string  strTimeofBirth { get; set; }

        public string BodyType { get; set; }

        public int? QualificationId { get; set; }

        public string Income { get; set; }

        public FamilyModel FamilyDetails { get; set; }

        public JobDetailsModel JobDetails { get; set; }

        public System.Linq.IQueryable<RelativeModel> RelativeDetails { get; set; }

        public string OrasName { get; set; }

        public string BloodGroup { get; set; }

        public string Qualification { get; set; }

        public string Cast { get; set; }

        public bool IsInterCast { get; set; }

        public string ReligionName { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string Taluka { get; set; }

        public bool? IsActive { get; set; }

        public string District { get; set; }
        public string Achievements { get; internal set; }
    }

    public class MessageModel
    {
        public string FromorUserName { get; set; }

        public string ToUserName { get; set; }

        public int? FromUserId { get; set; }

        public int? ToUserId { get; set; }

        public DateTime? MessageDate { get; set; }

        public string Message { get; set; }

        public bool IsDelete { get; set; }
    }

    public class JobDetailsModel
    {
        public int? JobId { get; set; }

        public bool? IsJobOrBusiness { get; set; }

        public decimal? Salary { get; set; }

        public string Post { get; set; }

        public string CompanyName { get; set; }

        public string JobBusinessInfo { get; set; }

        public string JobBusinessInfo1 { get; set; }

        public bool PermanentOrContract { get; set; }

        public string JobType { get; set; }

        public string Income { get; set; }

        public bool? IsDelete { get; set; }

        public string JobLocation { get; set; }

        public int UserId { get; set; }
    }

    public class FamilyModel
    {
        public int? MemberId { get; set; }

        public string FathersName { get; set; }

        public string MothersName { get; set; }

        public string NoofBrothers { get; set; }

        public string NoOfSisters { get; set; }

        public int UserId { get; set; }

        public int Age { get; set; }

        public string MobileNo { get; set; }

        public bool? IsJob { get; set; }

        public string FathersIncome { get; set; }

        public string JobBusinessInfo { get; set; }

        public string JobBusinessInfo1 { get; set; }
    }

    public class RelativeModel
    {
        public int? RelativeId { get; set; }

        public string RelativeName { get; set; }

        public string ConcernPerson { get; set; }

        public string Relation { get; set; }

        public int UserId { get; set; }

        public string MobileNo { get; set; }

        public string RelativeAddress { get; set; }
    }
}
