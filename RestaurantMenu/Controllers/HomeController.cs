using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestaurantMenu.UI.Models;
using RestaurantMenu.UI.Repository;
using RestaurantMenu.UI.Service;

namespace RestaurantMenu.UI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Menu()
        {
            ViewBag.isEditable = false;
            return View();
        }

        public IActionResult EditableMenu()
        {
            ViewBag.isEditable = true;
            return View("Menu");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
