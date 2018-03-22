using ShriVivah.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShriVivah.Models.ContextModel
{
    public class ReligionModel
    {
        ShreeVivahDbContext objData = new ShreeVivahDbContext();

        internal IQueryable<tblReligion> GetReligions()
        {
            var query = (from c in objData.tblReligions
                         orderby c.ReligionId ascending
                         select c);
            return query;
        }

        public int Save(tblReligion tbl)
        {
            tbl.IsDelete = false;
            objData.tblReligions.Add(tbl);
            objData.SaveChanges();
            return tbl.ReligionId.Value;
        }

        internal void Update(tblReligion model)
        {
            tblReligion newobj = GetReligions().Where(p => p.ReligionId == model.ReligionId).FirstOrDefault();
            newobj.ReligionName = model.ReligionName;
            objData.SaveChanges();
        }
    }

    public class ReligionDetails : Error
    {
        public IQueryable<tblReligion> ReligionList { get; set; }
    }
}