using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ShriVivah.Models;

namespace ShriVivah
{
    public class MyHub : Hub
    {
        ShriVivah.Models.Entities.ShreeVivahDbContext objData = null;
        public MyHub()
        {
            if (objData==null)
            {
                objData = new Models.Entities.ShreeVivahDbContext();    
            }
        }

        public string ReadMessage(int toUserId)
        {
            var result = (from tbl in objData.tblMessages
                          join tblU in objData.tblUsers
                          on tbl.FromUserId equals tblU.UserId
                          where tbl.FromUserId == toUserId
                          && tbl.IsDelete == false
                          select new MessageModel
                          {
                              FromUserId = tbl.FromUserId,
                              ToUserId = tbl.ToUserId,
                              Message = tbl.MessageText,
                              FromorUserName = tblU.FirstName + " " + tblU.LName
                          }).ToList();
            result.AddRange(from tbl in objData.tblMessages
                            join tblU in objData.tblUsers
                            on tbl.ToUserId equals tblU.UserId
                            where tbl.ToUserId == toUserId
                             && tbl.IsDelete == false
                            select new MessageModel
                            {
                                FromUserId = tbl.FromUserId,
                                ToUserId = tbl.ToUserId,
                                Message = tbl.MessageText,
                                FromorUserName = tblU.FirstName + " " + tblU.LName
                            });
            return JsonConvert.SerializeObject(result);
        }

        public void SendMessage(string msg,int fromUserId,int toUserId)
        {
            ShriVivah.Models.Entities.tblMessages objmsg = new Models.Entities.tblMessages() { 
                FromUserId=fromUserId,
                ToUserId=toUserId,
                MessageText=msg,
                IsDelete=false,
            };
            objData.tblMessages.Add(objmsg);
            objData.SaveChanges();
            //Clients.All.readMessage(ReadMessage(toUserId));
        }
    }
}