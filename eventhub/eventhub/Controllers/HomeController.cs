using eventhub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace eventhub.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        Context c = new Context();
        // GET: Home
        public string userNames()
        {
            string cookieName = FormsAuthentication.FormsCookieName;
            HttpCookie authCookie = HttpContext.Request.Cookies[cookieName];
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
            string UserName = ticket.Name.ToString();
            return UserName;
        }
        public List<Event> events(User user) 
        {
            List<Event> events = new List<Event>();
            Event e = new Event();
            var v = c.Events.Where(m => m.status == 1).ToList();
            string location = user.Location;//Giriş yapmış kullanıcının lokasyonunu al.
            string[] interests = user.Interests.Split(',');//Giriş yapmış kullanıcının ilgi alanlarını bir diziye at.
            foreach (var item in v)//Lokasyona ve ilgil alanı aynı olanlara öncelik verecek. Sadece ilgi alanı var ise onu sonradan ekleyecek.
            {
                foreach (var interest in interests)
                {
                    if (item.EventCategory.EventCategoryName == interest && item.Location == location)//Hem lokation hem interest aynıysa öncelikli alacak.
                    {
                        events.Add(item);
                    }
                    else if (item.EventCategory.EventCategoryName == interest)//Sadece ilgi alanı aynıysa eklenecek.
                    {
                        events.Add(item);
                    }
                }
                if (item.Location == location)//Sadece location aynıysa ekleyecek.
                {
                    events.Add(item);
                }
                else
                    { events.Add(item); }//Eğer hiçbir şey şartı karşılamıyorsada diğer eventler eklenecek.
            }
            return events.Distinct().ToList();
        }
        public ActionResult Index()
        {
            EventHubModel model = new EventHubModel();
            string userID = userNames();
            var q = c.Users.Where(m => m.Username == userID).ToList();
            model.userDetail = q[0];
            int id = q[0].UserID;
            model.eventList = events(q[0]);
            var list = c.Scores.Where(m => m.UserID == id).ToList();
            int score = list[0].score;
            model.userPoint = score;
            return View(model);
        }
    }
}