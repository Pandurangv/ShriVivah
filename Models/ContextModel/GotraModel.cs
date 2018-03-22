using ShriVivah.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShriVivah.Models.ContextModel
{
    public class GotraModel
    {
        ShreeVivahDbContext objData = new ShreeVivahDbContext();
        //public string BloodGroupName { get; set; }

        internal IQueryable<tblGotra> GetGotras()
        {
            var query = (from c in objData.tblGotras
                         orderby c.GotraId ascending
                         select c);
            return query;
        }

        public int Save(tblGotra tbl)
        {
            tbl.IsDelete = false;
            objData.tblGotras.Add(tbl);
            objData.SaveChanges();
            return tbl.GotraId.Value;
        }

        internal void Update(tblGotra model)
        {
            tblGotra newobj = GetGotras().Where(p => p.GotraId == model.GotraId).FirstOrDefault();
            newobj.GotraName = model.GotraName;
            objData.SaveChanges();
        }
    }

    public class GotraDetails : Error
    {
        public IQueryable<tblGotra> GotraList { get; set; }
    }
}