using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShriVivah.Models.Entities;

namespace ShriVivah.Models
{
    public class ResponseModel:Error
    {
        public ResponseModel()
        {
            //DataResponse = new IQueryable<Object>();
        }
        public IQueryable<Object> DataResponse { get; set; }

        

        public List<STP_GetUserDetail> UserList { get; set; }
        public Object ModelObject { get; set; }

        public string CityList { get; set; }
        public IQueryable<CityList> UserCityList { get; internal set; }
        public IQueryable<QualificationList> QualificationList { get; internal set; }
        public IQueryable<tblHeight> HeightList { get; internal set; }
        public IQueryable<tblOras> HeightOrasList { get; internal set; }
    }

    public class Error
    {
        public string ErrorMessage { get; set; }

        public bool? Status { get; set; }

        public string FilePath { get; set; }
    }
}