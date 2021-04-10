using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DevCard_MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DevCard_MVC.Controllers
{
    public class HomeController : Controller
    {

        private readonly List<Service> _services = new List<Service>()
        {
            new Service(1, "نقره ای"),
            new Service(2, "طلا ای"),
            new Service(3, "پلاتین"),
            new Service(4, "الماس"),
        };

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Contact()
        {
            var model = new Contact()
            {
                Services = new SelectList(_services, "Id", "Name")
            };
            return View(model);
        }

        // [HttpPost]
        // public JsonResult Contact(IFormCollection form)
        // {
        //     var name = form["name"];
        //     return Json(Ok());
        // }

        // [HttpPost]
        // public JsonResult Contact(Contact form)
        // {
        //     Console.WriteLine(form.ToString());
        //     return Json(Ok());
        // }

        [HttpPost]
        public IActionResult Contact(Contact model)
        {
            //-- fill services choices model --//
            model.Services = new SelectList(_services, "Id", "Name");

            //-- validate in server side --//
            if (!ModelState.IsValid)
            {
                ViewBag.error = "اطلاعات وارد شده صحیح نمی باشد";
                return View(model);
            }

            //-- clear fields --//
            ModelState.Clear();

            //-- generate new Contact with services choices only --//
            model = new Contact()
            {
                Services = new SelectList(_services, "Id", "Name")
            };
            ViewBag.success = "اطلاعات وارد شده به درستی ثبت شد";
            return View(model);
            
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
