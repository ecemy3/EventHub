using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace eventhub.Controllers
{
    public class MapsController : Controller
    {
        private const string GoogleApiKey = "AIzaSyCTgtTY-LjfBoozIgMTCa0krF-8O_-g2Mk";  // Buraya kendi API anahtarınızı ekleyin

        // Index ActionResult, city parametresini alacak
        public async Task<ActionResult> Index(string city)
        {
            if (string.IsNullOrEmpty(city))
            {
                ViewBag.Message = "Şehir adı giriniz.";
                return View();
            }

            var location = await GetLocationAsync(city);

            // Tuple (0, 0) olup olmadığını kontrol ediyoruz
            if (location.HasValue && location.Value.Latitude != 0 && location.Value.Longitude != 0)
            {
                ViewBag.Latitude = location.Value.Latitude.ToString().Replace(',','.');
                ViewBag.Longitude = location.Value.Longitude.ToString().Replace(',', '.');
            }
            else
            {
                ViewBag.Message = "Geçerli bir şehir bulunamadı.";
            }

            return View();
        }

        private async Task<(double Latitude, double Longitude)?> GetLocationAsync(string city)
        {
            var client = new HttpClient();
            var apiUrl = $"https://maps.googleapis.com/maps/api/geocode/json?address={city}&key={GoogleApiKey}";

            var response = await client.GetStringAsync(apiUrl);
            var jsonResponse = JObject.Parse(response);
            var results = jsonResponse["results"];

            if (results.HasValues)
            {
                var lat = results[0]["geometry"]["location"]["lat"].ToObject<double>();
                var lng = results[0]["geometry"]["location"]["lng"].ToObject<double>();

                return (lat, lng);  // Burada bir Tuple döndürüyoruz
            }

            return null;  // Eğer şehir bulunamazsa null döner
        }
    }
}