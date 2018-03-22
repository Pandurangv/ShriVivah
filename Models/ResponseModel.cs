using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShriVivah.Models
{
    public class ResponseModel:Error
    {
        public ResponseModel()
        {
            //DataResponse = new IQueryable<Object>();
        }
        public IQueryable<Object> DataResponse { get; set; }
    }

    public class Error
    {
        public string ErrorMessage { get; set; }

        public bool? Status { get; set; }

        public string FilePath { get; set; }
    }
}