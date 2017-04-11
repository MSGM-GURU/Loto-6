using System;
using Lottery.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Lottery.Controllers
{

    public class HomeController : Controller
    {
        public IActionResult LoginPage()
        {
            var model = new UserViewModel();
            if(Request.Cookies["User"] != null) 
            {
                model = JsonConvert.DeserializeObject<UserViewModel>(Request.Cookies["User"]);
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult LoginPage(UserViewModel model, bool RememberMe)
        {
            if(model.Account == "admin" && model.Password == "123")
            {
                if(RememberMe)
                {
                    var userJson = JsonConvert.SerializeObject(new {Account = model.Account, Password = model.Password});
                    var cookies = new CookieOptions();
                    cookies.Expires = DateTime.Now.AddYears(10);
                    Response.Cookies.Append("User", userJson, cookies);
                }
                else
                {
                    Response.Cookies.Delete("User");
                }
                return RedirectToAction("Index");
            } 
            return View(new UserViewModel());
        }

        public IActionResult Index()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Test()
        {
            return View();
        }
    }
}
