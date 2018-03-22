using ShriVivah.Models.DataModels;
using ShriVivah.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShriVivah.Models.ContextModel
{
    public class CastContextModel
    {
        ShreeVivahDbContext objData = new ShreeVivahDbContext();

        internal IQueryable<CastModel> GetCasts()
        {
            var query = (from c in objData.tblCasts
                         join r in objData.tblReligions
                         on c.ReligionId equals r.ReligionId
                         where c.IsDelete == false
                         orderby c.CastName ascending
                         select new CastModel
                         {
                             CastName = c.CastName,
                             ReligionName = r.ReligionName,
                             ReligionId = r.ReligionId,
                             CastId = c.CastId,
                             IsDelete = c.IsDelete,
                         });
            return query;
        }

        public int Save(tblCast tbl)
        {
            tbl.IsDelete = false;
            objData.tblCasts.Add(tbl);
            objData.SaveChanges();
            return tbl.CastId.Value;
        }

        internal void Update(tblCast model)
        {
            tblCast newobj = objData.tblCasts.Where(p => p.CastId == model.CastId && p.IsDelete == false).FirstOrDefault();
            newobj.CastName = model.CastName;
            newobj.ReligionId = model.ReligionId;
            objData.SaveChanges();
        }
    }

    public class CastDetails : Error
    {
        public IQueryable<CastModel> CastList { get; set; }
    }

}