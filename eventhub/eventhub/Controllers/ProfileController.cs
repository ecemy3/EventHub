using eventhub.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;

namespace eventhub.Controllers
{
    public class ProfileController : Controller
    {
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
        public static string MD5Sifrele(string sifrelenecekMetin)
        {

            // MD5CryptoServiceProvider sınıfının bir örneğini oluşturduk.
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            //Parametre olarak gelen veriyi byte dizisine dönüştürdük.
            byte[] dizi = Encoding.UTF8.GetBytes(sifrelenecekMetin);
            //dizinin hash'ini hesaplattık.
            dizi = md5.ComputeHash(dizi);
            //Hashlenmiş verileri depolamak için StringBuilder nesnesi oluşturduk.
            StringBuilder sb = new StringBuilder();
            //Her byte'i dizi içerisinden alarak string türüne dönüştürdük.

            foreach (byte ba in dizi)
            {
                sb.Append(ba.ToString("x2").ToLower());
            }

            //hexadecimal(onaltılık) stringi geri döndürdük.
            return sb.ToString();
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
                Value = kv.Key,
                Text = kv.Value
            }).ToList();

            return items;
        }
        Context c = new Context();
        // GET: Profile
        [HttpGet]
        public ActionResult Index()
        {
            string userID = userNames();
            var v = c.Users.Where(m => m.Username == userID).ToList();
            int id = v[0].UserID;
            var userDetail = c.Users.Find(id);
            ViewBag.iller = iller();
            return View(userDetail);
        }
        [HttpPost]
        public ActionResult Index(User u)
        {
            var v = c.Users.Find(u.UserID);
            ViewBag.iller = iller();
            if (Request.Files.Count > 0)
            {

                string path = fileName();
                Request.Files[0].SaveAs(Server.MapPath("~/assets/pImage/" + path));
                v.Password = MD5Sifrele(u.Password);
                v.Location = u.Location;
                v.Interests = Request.Form["Interests"];
                v.ProfilePicture = path;
                int sonuc = c.SaveChanges();
                if (sonuc == 1)
                {
                    ViewBag.sonuc = 1;
                }
                else
                    ViewBag.sonuc = 0;
            }
            return View(v);
        }
    }
}