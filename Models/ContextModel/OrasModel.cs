using ShriVivah.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShriVivah.Models.ContextModel
{
    public class OrasModel
    {
        ShreeVivahDbContext objData = new ShreeVivahDbContext();
        //public string BloodGroupName { get; set; }

        internal IQueryable<tblOras> GetOrass()
        {
            var query = (from c in objData.tblOrass
                         orderby c.OrasId ascending
                         select c);
            return query;
        }

        internal IQueryable<tblHeight> GetHeights()
        {
            var query = (from c in objData.tblHeights
                         orderby c.HeightId ascending
                         select c);
            return query;
        }




        public int Save(tblOras tbl)
        {
            tbl.IsDelete = false;
            objData.tblOrass.Add(tbl);
            objData.SaveChanges();
            return tbl.OrasId.Value;
        }

        internal void Update(tblOras model)
        {
            tblOras newobj = GetOrass().Where(p => p.OrasId == model.OrasId).FirstOrDefault();
            newobj.OrasName = model.OrasName;
            objData.SaveChanges();
        }
    }

    public class OrasDetails : Error
    {
        public IQueryable<tblOras> OrasList { get; set; }
    }
}