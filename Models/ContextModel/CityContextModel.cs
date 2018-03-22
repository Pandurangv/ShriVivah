using ShriVivah.Models.DataModels;
using ShriVivah.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShriVivah.Models.ContextModel
{
    public class CityContextModel
    {
        ShreeVivahDbContext objData = new ShreeVivahDbContext();

        internal IQueryable<CityModel> GetCities()
        {
            var query = (from c in objData.tblCities
                         join s in objData.tblStates
                         on c.StateId equals s.StateId
                         join tblC in objData.tblCounties
                         on s.CountryId equals tblC.CountryId
                         where s.IsDelete == false
                         orderby c.CityId ascending
                         select new CityModel
                         {
                             CityId=c.CityId,
                             CityName = c.CityName,
                             StateName = s.StateName,
                             StateId = s.StateId,
                             Pincode = c.Pincode,
                             IsDelete = c.IsDelete,
                             CountryName=tblC.CountryName
                         });
            return query;
        }

        public int Save(tblCity tbl)
        {
            tbl.IsDelete = false;
            objData.tblCities.Add(tbl);
            objData.SaveChanges();
            return tbl.CityId.Value;
        }

        internal void Update(tblCity model)
        {
            tblCity newobj = objData.tblCities.Where(p => p.CityId == model.CityId && p.IsDelete == false).FirstOrDefault();
            newobj.CityName = model.CityName;
            newobj.StateId = model.StateId;
            newobj.Pincode = model.Pincode;
            objData.SaveChanges();
        }
    }

    public class CityDetails : Error
    {
        public IQueryable<CityModel> CityList { get; set; }
    }


}