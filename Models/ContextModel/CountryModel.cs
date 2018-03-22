using ShriVivah.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShriVivah.Models.ContextModel
{
    public class CountryModel
    {
        ShreeVivahDbContext objData = new ShreeVivahDbContext();

        internal IQueryable<tblCountry> GetCountrys()
        {
            var query = (from c in objData.tblCounties
                         orderby c.CountryId ascending
                         select c);
            return query;
        }

        public int Save(tblCountry tbl)
        {
            tbl.IsDelete = false;
            objData.tblCounties.Add(tbl);
            objData.SaveChanges();
            return tbl.CountryId.Value;
        }

        internal void Update(tblCountry model)
        {
            tblCountry newobj = GetCountrys().Where(p => p.CountryId == model.CountryId).FirstOrDefault();
            newobj.CountryName = model.CountryName;
            objData.SaveChanges();
        }
    }

    public class CountryDetails : Error
    {
        public IQueryable<tblCountry> CountryList { get; set; }
    }
}