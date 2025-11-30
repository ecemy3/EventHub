using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eventhub.Models;
using Newtonsoft.Json;
using System.Web.Security;
namespace eventhub.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private List<SelectListItem> categories()
        {
            var items = c.EventCategories
                 .Select(category => new SelectListItem
                 {
                     Value = category.EventCategoryID.ToString(), // Value alanı (örnek: kategori ID'si)
                     Text = category.EventCategoryName           // Text alanı (örnek: kategori adı)
                 })
                 .ToList();

            return items;
        }
        private List<SelectListItem> iller()
        {
            // JSON dosyasını okuyun
            string filePath = Server.MapPath("~/assets/iller.json"); // JSON dosyasının yolu
            string jsonData = System.IO.File.ReadAllText(filePath);

            // JSON'u Dictionary<string, string> olarak deserialize edin
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonData);

            // Dictionary'i List<SelectListItem> nesnesine dönüştürün
            var items = dictionary.Select(kv => new SelectListItem
            {
                Value = kv.Value,
                Text = kv.Value
            }).ToList();

            return items;
        }
        public string userNames()
        {
            string cookieName = FormsAuthentication.FormsCookieName;
            HttpCookie authCookie = HttpContext.Request.Cookies[cookieName];
            FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
            string UserName = ticket.Name.ToString();
            return UserName;
        }
        public string fileName()
        {
            string yeniAd = DateTime.Now.ToString();
            yeniAd = yeniAd.Replace(' ', '-').Replace('.', '-').Replace(':', '-');
            string yol = yeniAd + ".png";
            return yol;
        }
        private Context c = new Context();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult EventList()
        {
            var eventList = c.Events.ToList();
            var eventCategory=c.EventCategories.ToList();
            EventHubModel model = new EventHubModel();
            model.eventList = eventList;
            model.eventCategoryList = eventCategory;
            return View(model);
        }
        [HttpGet]
        public ActionResult EventAdd()
        {
            ViewBag.category = categories();
            ViewBag.iller = iller();
            return View();
        }
        [HttpPost]
        public ActionResult EventAdd(Event e)
        {
            ViewBag.category = categories();
            ViewBag.iller = iller();
            if (Request.Files.Count > 0)
            {
                string a = fileName();
                e.EventBanner = a;
                Request.Files[0].SaveAs(Server.MapPath("~/assets/eBanner/" + a));
                e.status = 1;
                string userID = userNames();
                var v = c.Users.Where(m => m.Username == userID).ToList();
                e.UserID = v[0].UserID;
                c.Events.Add(e);
                int sonuc = c.SaveChanges();
                var lastEventID = c.Events.ToList().LastOrDefault();
                Message message = new Message();
                message.EventID = lastEventID.ID;
                c.Messages.Add(message);
                c.SaveChanges();
                var lastMessage = c.Messages.ToList().LastOrDefault();
                MessageDetail detail = new MessageDetail();
                detail.MessageID = lastMessage.MessageID;
                detail.Comment = "Welcome to the event messaging page.";
                detail.UserID = v[0].UserID;
                c.MessageDetails.Add(detail);
                c.SaveChanges();
                if (sonuc == 1)
                {
                    ViewBag.sonuc = 1;
                    return View();
                }
                else
                {
                    ViewBag.sonuc = 0;
                    return View();
                }

            }
            else
            {
                ViewBag.sonuc = 0;
                return View();
            }
        }
        [HttpGet]
        public ActionResult EventUpdate(int id=0)
        {
            ViewBag.category = categories();
            ViewBag.iller = iller();
            var v = c.Events.Find(id);
            return View(v);
        }
        [HttpPost]
        public ActionResult EventUpdate(Event e)
        {
            ViewBag.category = categories();
            ViewBag.iller = iller();
            if (Request.Files.Count > 0)
            {
                var v = c.Events.Find(e.ID);
                v.EventName = e.EventName;
                v.EventCategoryID = e.EventCategoryID;
                v.Date = e.Date;
                v.Description = e.Description;
                v.Location = e.Location;
                v.status = 1;
                string a = fileName();
                v.EventBanner = a;
                Request.Files[0].SaveAs(Server.MapPath("~/assets/eBanner/" + a));
                int sonuc = c.SaveChanges();
                if (sonuc == 1)
                {
                    ViewBag.sonuc = 1;
                    return View();
                }
                else
                {
                    ViewBag.sonuc = 0;
                    return View();
                }

            }
            else
            {
                ViewBag.sonuc = 0;
                return View();
            }
        }
        public ActionResult EventStatus(int id)
        {
            var v = c.Events.Find(id);
            v.status = 1;
            c.SaveChanges();
            return RedirectToAction("EventList","Admin");
        }
        public ActionResult EventRemove(int id)
        {

            var v = c.Events.Find(id);
            c.Events.Remove(v);
            c.SaveChanges();
            return RedirectToAction("EventList", "Admin");
        }
    }
}