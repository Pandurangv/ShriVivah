using ShriVivah.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShriVivah.Models.ContextModel
{
    public class VendorTypeContextModel
    {
        ShreeVivahDbContext objData = new ShreeVivahDbContext();

        internal IQueryable<VendorTypeModel> GetVendorTypes()
        {
            var query = (from c in objData.tblVendorTypes
                         orderby c.VendorTypeId ascending
                         select new VendorTypeModel
                         {
                             VendorTypeId=c.VendorTypeId,
                             VendorType=c.VendorType,
                            IsDelete=c.IsDelete,
                            TypeImagesPath=c.TypeImagesPath,
                         });
            return query;
        }

        public int Save(VendorTypeModel tbl)
        {
            tblVendorType vendortype=new tblVendorType()
            {
                IsDelete=false,
                VendorType=tbl.VendorType,
                VendorTypeId=tbl.VendorTypeId,
                TypeImagesPath = tbl.TypeImagesPath,
            };
            //tbl.IsDelete = false;
            objData.tblVendorTypes.Add(vendortype);
            objData.SaveChanges();
            return vendortype.VendorTypeId.Value;
        }

        internal void Update(VendorTypeModel model)
        {
            tblVendorType newobj = objData.tblVendorTypes.Where(p => p.VendorTypeId == model.VendorTypeId).FirstOrDefault(); //GetVendorTypes().Where(p => p.VendorTypeId == model.VendorTypeId).FirstOrDefault();
            newobj.VendorType = model.VendorType;
            if (!string.IsNullOrEmpty(model.TypeImagesPath) )
            {
                newobj.TypeImagesPath = model.TypeImagesPath;
            }
            objData.SaveChanges();
        }
    }

    public class VendorTypeDetails : Error
    {
        public IQueryable<VendorTypeModel> VendorTypeList { get; set; }
    }
    
}