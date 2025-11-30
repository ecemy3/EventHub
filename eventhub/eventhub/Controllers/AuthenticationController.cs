using eventhub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;
using System.Web.UI.WebControls;
using System.Text;

namespace eventhub.Controllers
{
    public class AuthenticationController : Controller
    {
        private Context c = new Context();
        public string fileName()
        {
            string yeniAd = DateTime.Now.ToString();
            yeniAd = yeniAd.Replace(' ','-').Replace('.', '-').Replace(':','-');
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
                Value = kv.Value,
                Text = kv.Value
            }).ToList();

            return items;
        }
        // GET: Authentication
        [HttpGet]
        public ActionResult Register()
        {
            ViewBag.iller = iller();
            return View();
        }
        [HttpPost]
        public ActionResult Register(User u)
        {
            ViewBag.iller = iller();
            if (Request.Files.Count > 0)
            {
                var v = c.Users.Where(m => m.Username == u.Username).ToList();
                if (v.Count != 0)
                {
                    ViewBag.sonuc = 2;//Zaten var üye adı.
                }
                else
                {
                    string path = fileName();
                    Request.Files[0].SaveAs(Server.MapPath("~/assets/pImage/" + path));
                    u.Interests = Request.Form["Interests"];
                    u.ProfilePicture = path;
                    u.Password = MD5Sifrele(u.Password);
                    c.Users.Add(u);
                    int sonuc = c.SaveChanges();
                    if (sonuc == 1)
                    {
                        try
                        {
                            // E-posta gönderiminde kullanılacak SMTP sunucusu ve port numarası
                            string smtpServer = "smtp.gmail.com";
                            int port = 587;

                            // E-posta gönderiminde kullanılacak kimlik bilgileri
                            string fromAddress = "berilisalar10@gmail.com";
                            string password = "vfgc exea vyyj mqze"; // Gmail uygulama şifrenizi buraya girin

                            // Alıcı e-posta adresi
                            string[] toAddresses = { u.Email };

                            // E-posta başlığı ve HTML içeriği
                            string subject = "Event Hub Message Services" + u.FirstName + " " + u.LastName;
                            string body = "The account has been created successfully.";

                            // SMTP istemcisini oluştur
                            using (SmtpClient client = new SmtpClient(smtpServer, port))
                            {
                                // Kimlik bilgilerini ayarla
                                client.Credentials = new NetworkCredential(fromAddress, password);
                                client.EnableSsl = true; // Gmail için SSL zorunludur

                                // E-posta gönderme işlemi
                                using (MailMessage mail = new MailMessage())
                                {
                                    mail.From = new MailAddress(fromAddress);

                                    // Alıcıları ekleyin
                                    foreach (var toAddress in toAddresses)
                                    {
                                        mail.To.Add(toAddress);
                                    }

                                    mail.Subject = subject;
                                    mail.Body = body;
                                    mail.IsBodyHtml = true; // HTML içerik kullanılacaksa true olarak ayarla

                                    client.Send(mail);
                                    ViewBag.sonuc = 1;
                                    //Eğer kayıt oluşturulduysa otomatik olarak 20 puan verilecek.Çünkü giriş yaptığında 20 puan kazanması gerekiyor.
                                    var lastUser = c.Users.Where(m => m.Username == u.Username).First();
                                    Score s = new Score();
                                    s.score = 20;
                                    s.UserID=lastUser.UserID;
                                    c.Scores.Add(s);
                                    c.SaveChanges();
                                    return View();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                           
                    }
                    else
                        ViewBag.sonuc = 0;
                }
               
               
            }
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User u)
        {
            if (u.Username=="ecos" && u.Password=="ecem123")
            {
                FormsAuthentication.SetAuthCookie(u.Username, false);
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                string pw = MD5Sifrele(u.Password);
                var v = c.Users.Where(m => m.Username == u.Username && m.Password == pw).ToList();
                if (v.Count!=0)
                {
                    FormsAuthentication.SetAuthCookie(u.Username, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.sonuc = "<div style='background-color: #f8d7da; color: #721c24; padding: 15px; margin: 20px 0; border: 1px solid #f5c6cb; border-radius: 5px; font-family: Arial, sans-serif; font-size: 16px;'>The username or password is incorrect.</div>";
                    return View();
                }
            }
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Authentication");
        }
            
    }
}