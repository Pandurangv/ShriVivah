using ShriVivah.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShriVivah.Models.ContextModel
{
    public class FeedbackModel
    {
        ShreeVivahDbContext objData = new ShreeVivahDbContext();


        internal IQueryable<tblContactDetails> GetFeedbacks()
        {
            var query = (from c in objData.tblContactDetailss
                         orderby c.ContactUsId ascending
                         select c);
            return query;
        }

        internal void Delete(int id)
        {
            tblContactDetails tbl = (from c in objData.tblContactDetailss
                         where c.ContactUsId ==id
                         select c).FirstOrDefault();
            if (tbl!=null)
            {
                objData.tblContactDetailss.Remove(tbl);
                objData.SaveChanges();
            }
        }
    }

    public class FeedbackDetails : Error
    {
        public IQueryable<tblContactDetails> ContactList { get; set; }
    }
}