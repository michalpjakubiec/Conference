using Conference.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.IO;

namespace Conference.Controllers
{
    public class ConferenceController : Controller
    {
        static List<ConferenceUser> conferenceUserList = new List<ConferenceUser>();
        public IActionResult Index()
        {

            return View();
        }

        [HttpGet]
        public IActionResult AddUser()
        {
            return View(conferenceUserList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public IActionResult AddUser(ConferenceUser conferenceUser)
        {
            using (var stream = new FileStream($"wwwroot/images/{conferenceUser.Avatar.FileName}", FileMode.Create))
            {
                conferenceUser.Avatar.CopyTo(stream);
            }

            conferenceUserList.Add(conferenceUser);

            JsonSerializer serializer = new JsonSerializer { Formatting = Formatting.Indented };
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;
            using (StreamWriter sw = new StreamWriter(@"ConferenceUsers.json"))
            {
                serializer.Serialize(sw, conferenceUser);
            }

            return RedirectToAction("AddUser");
        } 
    }
}