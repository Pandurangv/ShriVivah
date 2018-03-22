using ShriVivah.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShriVivah.Models.ContextModel
{
    public class GanModel
    {
        ShreeVivahDbContext objData = new ShreeVivahDbContext();

        internal IQueryable<tblGan> GetGans()
        {
            var query = (from c in objData.tblGans
                         orderby c.GanId ascending
                         select c);
            return query;
        }

        public int Save(tblGan tbl)
        {
            tbl.IsDelete = false;
            objData.tblGans.Add(tbl);
            objData.SaveChanges();
            return tbl.GanId.Value;
        }

        internal void Update(tblGan model)
        {
            tblGan newobj = GetGans().Where(p => p.GanId == model.GanId).FirstOrDefault();
            newobj.GanName = model.GanName;
            objData.SaveChanges();
        }
    }

    public class GanDetails : Error
    {
        public IQueryable<tblGan> GanList { get; set; }
    }
}