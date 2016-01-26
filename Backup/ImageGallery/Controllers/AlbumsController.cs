using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RozichMurals.Web.Models;

namespace RozichMurals.Web.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IAlbumRepository albumRepository;

        // If you are using Dependency Injection, you can delete the following constructor
        public AlbumsController()
            : this(new CategoryRepository(), new AlbumRepository())
        {
        }

        public AlbumsController(ICategoryRepository categoryRepository, IAlbumRepository albumRepository)
        {
            this.categoryRepository = categoryRepository;
            this.albumRepository = albumRepository;
        }

        //
        // GET: /Albums/

        public ViewResult Index()
        {
            return View(albumRepository.AllIncluding(album => album.Category, album => album.Images));
        }

        //
        // GET: /Albums/Details/5

        public ViewResult Details(int id)
        {
            return View(albumRepository.Find(id));
        }

        //
        // GET: /Albums/Create

        public ActionResult Create()
        {
            ViewBag.PossibleCategories = categoryRepository.All;
            return View();
        }

        //
        // POST: /Albums/Create

        [HttpPost]
        public ActionResult Create(Album album, HttpPostedFileBase fileBase)
        {
            album.ThumbImage = fileBase.FileName;
            if (ModelState.IsValid && fileBase != null)
            {
                string imagesDir = HttpContext.Server.MapPath("~/Content/uploadedimages/");
                fileBase.SaveAs(imagesDir + fileBase.FileName);
                albumRepository.InsertOrUpdate(album);
                albumRepository.Save();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.PossibleCategories = categoryRepository.All;
                return View();
            }
        }

        //
        // GET: /Albums/Edit/5

        public ActionResult Edit(int id)
        {
            ViewBag.PossibleCategories = categoryRepository.All;
            return View(albumRepository.Find(id));
        }

        //
        // POST: /Albums/Edit/5

        [HttpPost]
        public ActionResult Edit(Album album, HttpPostedFileBase fileBase)
        {
            
            if (ModelState.IsValid)
            {
                if(fileBase != null)
                {
                    album.ThumbImage = fileBase.FileName;
                    var imagesDir = HttpContext.Server.MapPath("~/Content/uploadedimages/");
                    fileBase.SaveAs(imagesDir + fileBase.FileName);
                }
                
                albumRepository.InsertOrUpdate(album);
                albumRepository.Save();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.PossibleCategories = categoryRepository.All;
                return View();
            }
        }

        //
        // GET: /Albums/Delete/5

        public ActionResult Delete(int id)
        {
            return View(albumRepository.Find(id));
        }

        //
        // POST: /Albums/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            albumRepository.Delete(id);
            albumRepository.Save();

            return RedirectToAction("Index");
        }
    }
}

