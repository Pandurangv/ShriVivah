using ShriVivah.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShriVivah.Models.DataModels
{
    public class EventManagementModel
    {
        public int? EventId { get; set; }

        public string EventName { get; set; }

        public string EventLocation { get; set; }

        public DateTime? EventDate { get; set; }

        public string EventDistrict { get; set; }

        public string EventState { get; set; }

        public int OrganizedBy { get; set; }

        public string OrganizerName { get; set; }

        public string ContactPerson { get; set; }

        public string MobileNo { get; set; }

        public bool IsDelete { get; set; }
    }


    public class EventResponse:Error
    {
        public List<EventManagementModel> EventList { get; set; }

        public List<STP_GetUserDetail> AgentList { get; set; }
    }
}