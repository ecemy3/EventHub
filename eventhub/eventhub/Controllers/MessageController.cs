using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using eventhub.Models;

namespace eventhub.Controllers
{
    public class MessageController : Controller
    {
        Context c = new Context();
        public string userNames()
        {
            string cookieName = FormsAuthentication.FormsCookieName;
            HttpCookie authCookie = HttpContext.Request.Cookies[cookieName];
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
            string UserName = ticket.Name.ToString();
            return UserName;
        }
        // GET: Message
        public ActionResult Index()
        {
            EventHubModel eventHubModel = new EventHubModel();
            var v = c.Events.Where(m => m.status == 1).ToList();
            eventHubModel.eventList = v;
            return View(eventHubModel);
        }
        public ActionResult EventMessage(int id=0)
        {
            EventHubModel model = new EventHubModel();
            if (id == 0)
            {
                return View();
            }
            else
            {
                //Tüm mesaj atılanları gösteriyor.
                string userName = userNames();
                var v = c.Users.Where(m => m.Username == userName).FirstOrDefault();
                int userID = v.UserID;
                List<Event> list = new List<Event>();
                var messageList = c.MessageDetails.Where(m => m.UserID == id).ToList();
                    foreach (var message in messageList)
                    {
                        var query = c.Events.Where(m => m.ID == message.Message.EventID).FirstOrDefault();
                        list.Add(query);
                    }
                    
                    list = list.Distinct().ToList();
                    if (list.Count == 0)
                    {
                        model.EventForUser = null;
                    }
                    else
                    {
                        model.EventForUser = list;
                    }
                    
                    //
                    var messageDetailList = c.MessageDetails.Where(m => m.Message.EventID == id).ToList();
                    model.MessageForEvent = messageDetailList;
                    model.userID = userID;
                    return View(model);
            }
           
        }
        [HttpPost]
        public ActionResult SendMessage(int id)
        {
            MessageDetail detail=new MessageDetail();
            detail.MessageID = id;
            detail.Comment = Request.Form["comment"].ToString();
            string userName = userNames();
            var v = c.Users.Where(m => m.Username == userName).FirstOrDefault();
            int userID = v.UserID;
            detail.UserID = userID;
            c.MessageDetails.Add(detail);
            c.SaveChanges();
            return RedirectToAction("EventMessage", "Message", new { id = id });
        }
    }
}