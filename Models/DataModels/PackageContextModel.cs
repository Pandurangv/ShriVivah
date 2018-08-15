using ShriVivah.Models.ContextModel;
using ShriVivah.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShriVivah.Models.DataModels
{
    public class PackageContextModel
    {
        ShreeVivahDbContext objData = new ShreeVivahDbContext();
        public PackageResponse GetPackages()
        {
            var response = new PackageResponse();
            response.PackageList=(from tbl in objData.PackageMasters
                                  where tbl.IsDelete == false
                                  select new PackageViewModel {
                                      PackageId=tbl.PackageId,
                                      AccessUsers=tbl.AccessUsers,
                                      Gender=tbl.Gender,
                                      IsDelete=tbl.IsDelete,
                                      PackageName=tbl.PackageName,
                                      Price=tbl.Price,
                                      Validity=tbl.Validity
                                  });
            return response;
        }

        public long Save(PackageViewModel tbl)
        {
            PackageMaster vendortype = new PackageMaster()
            {
                AccessUsers = tbl.AccessUsers,
                Gender = tbl.Gender,
                IsDelete = false,
                PackageName = tbl.PackageName,
                Price = tbl.Price,
                Validity = tbl.Validity
            };
            //tbl.IsDelete = false;
            objData.PackageMasters.Add(vendortype);
            objData.SaveChanges();
            return vendortype.PackageId.Value;
        }

        internal void Update(PackageViewModel model)
        {
            PackageMaster newobj = objData.PackageMasters.Where(p => p.PackageId == model.PackageId).FirstOrDefault(); //GetVendorTypes().Where(p => p.VendorTypeId == model.VendorTypeId).FirstOrDefault();
            if (newobj!=null)
            {
                newobj.PackageName = model.PackageName;
                newobj.Price = model.Price;
                newobj.Gender = model.Gender;
                newobj.Validity = 1;
                objData.SaveChanges();
            }
        }
    }
}