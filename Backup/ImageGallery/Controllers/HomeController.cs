using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RozichMurals.Web.Helpers;
using RozichMurals.Web.Models;

namespace RozichMurals.Web.Controllers
{
    public class HomeController : Controller
    {
        private  IAlbumRepository albumRepository;
        private  IImageRepository imageRepository;
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your quintessential app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            return View(new Contact());
        }

        [HttpPost]
        public ActionResult Contact(Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return View(contact);
            }


            new Email().Send(contact);

            return RedirectToAction("ContactConfirm");
        }

        public ActionResult ContactConfirm()
        {
            return View();
        }

        public ActionResult Murals()
        {
            imageRepository = new ImageRepository();
            albumRepository = new AlbumRepository();
            return View(albumRepository.AllIncluding(m => m.Images).OrderBy(m=> m.OrderNumber));

        }

        public ActionResult InTheStudio()
        {
            return View();

        }

        public ActionResult Illustration()
        {
            imageRepository = new ImageRepository();
            albumRepository = new AlbumRepository();
            return View(albumRepository.AllIncluding(m => m.Images).OrderBy(m => m.OrderNumber));

        }

        public ActionResult Bio()
        {
            return View();

        }
        public ActionResult InTheMedia()
        {
            return View();
        }

        public ActionResult Affiliates()
        {
            return View();

        }
    }
}
