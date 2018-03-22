using ShriVivah.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;


namespace ShriVivah.Models.ContextModel
{
    public class BloodGroupModel
    {
        //public long? BloodGroupId { get; set; }
        ShreeVivahDbContext objData = new ShreeVivahDbContext();
        //public string BloodGroupName { get; set; }

        internal IQueryable<tblBloodGroup> GetBloodGroups()
        {
            var query = (from c in objData.tblBloodGroups
                         orderby c.BloodGroupId ascending
                         select c);
            return query;
        }

        public int Save(tblBloodGroup tbl)
        {
            tbl.IsDelete = false;
            objData.tblBloodGroups.Add(tbl);
            objData.SaveChanges();
            return tbl.BloodGroupId.Value;
        }

        internal void Update(tblBloodGroup model)
        {
            tblBloodGroup newobj = GetBloodGroups().Where(p => p.BloodGroupId == model.BloodGroupId).FirstOrDefault();
            newobj.BloodGroupName = model.BloodGroupName;
            objData.SaveChanges();
        }

        internal IQueryable<tblQualification> GetEducations()
        {
            return (from tbl in objData.tblQualifications
                    where tbl.IsDelete == false
                    orderby tbl.DegreeName ascending
                    select tbl);
        }

        public int Save(tblQualification tbl)
        {
            tbl.IsDelete = false;
            objData.tblQualifications.Add(tbl);
            objData.SaveChanges();
            return tbl.QualificationId.Value;
        }

        internal void Update(tblQualification model)
        {
            tblQualification newobj = GetEducations().Where(p => p.QualificationId == model.QualificationId).FirstOrDefault();
            newobj.DegreeName = model.DegreeName;
            objData.SaveChanges();
        }
    }

    public class QualificationDetails : Error
    {
        public IQueryable<tblQualification> QualificationList { get; set; }
    }
    

    public class BGDetails : Error
    {
        public IQueryable<tblBloodGroup> BGList { get; set; }
    }
}