using ShriVivah.Models.DataModels;
using ShriVivah.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShriVivah.Models.ContextModel
{
    public class StateModel
    {
        ShreeVivahDbContext objData = new ShreeVivahDbContext();

        internal IQueryable<StateContextModel> GetStates()
        {
            var query = (from s in objData.tblStates
                         join c in objData.tblCounties
                         on s.CountryId equals c.CountryId
                         where s.IsDelete==false
                         orderby s.StateId ascending
                         select new StateContextModel
                         { 
                            CountryName=c.CountryName,
                            StateName=s.StateName,
                            CountryId=s.CountryId,
                            StateId=s.StateId,
                            IsDelete=s.IsDelete,
                         });
            return query;
        }

        public int Save(tblState tbl)
        {
            tbl.IsDelete = false;
            objData.tblStates.Add(tbl);
            objData.SaveChanges();
            return tbl.StateId.Value;
        }

        internal void Update(tblState model)
        {
            tblState newobj = objData.tblStates.Where(p => p.StateId == model.StateId && p.IsDelete == false).FirstOrDefault();
            newobj.StateName = model.StateName;
            newobj.CountryId = model.CountryId;
            objData.SaveChanges();
        }
    }

    public class StateDetails : Error
    {
        public IQueryable<StateContextModel> StateList { get; set; }
    }


}