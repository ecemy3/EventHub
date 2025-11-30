using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eventhub.Models
{
    public class EventHubModel
    {
        Context c = new Context();
        public IEnumerable<EventCategory> eventCategoryList { get; set; }
        public IEnumerable<Event> eventList { get; set; }
        public List<EventCategory> eventCategories()
        {
            var v = c.EventCategories.ToList();
            return v.ToList();
        }
        public User userDetail { get; set; }
        public int userPoint { get; set; }
        public bool isAlreadyInEvent(int userID, int eventID)
        {
            var v = c.eventMembers.Where(m => m.ID == eventID && m.UserID == userID).ToList();
            if (v.Count > 0)
                return true;
            else
                return false;

        }
        public List<Event> EventForUser { get; set; }
        public List<MessageDetail> MessageForEvent { get; set; }
        public int userID { get; set; }
        public User GetUser(int id)
        {
            var v = c.Users.Find(id);
            return v;
        }
    
    }
}