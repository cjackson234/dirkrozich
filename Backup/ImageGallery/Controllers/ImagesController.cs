using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RozichMurals.Web.Models;

namespace RozichMurals.Web.Controllers
{   
    [Authorize]
    public class ImagesController : Controller
    {
        private RozichMuralsWebContext context = new RozichMuralsWebContext();
		private readonly IAlbumRepository albumRepository;
		private readonly IImageRepository imageRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public ImagesController() : this(new AlbumRepository(), new ImageRepository())
        {
        }

        public ImagesController(IAlbumRepository albumRepository, IImageRepository imageRepository)
        {
			this.albumRepository = albumRepository;
			this.imageRepository = imageRepository;
        }

        //
        // GET: /Images/

        public ViewResult Index()
        {
            return View("Annoyed",imageRepository.AllIncluding(image => image.Album));
            
        }

        //
        // GET: /Images/Details/5

        public ViewResult Details(int id)
        {
            return View(imageRepository.Find(id));
        }

        //
        // GET: /Images/Create

        public ActionResult Create()
        {
			ViewBag.PossibleAlbums = albumRepository.All;
            return View();
        } 

        //
        // POST: /Images/Create

        [HttpPost]
        public ActionResult Create(Image image, HttpPostedFileBase fileBase)
        {
            image.Path = fileBase.FileName;

            if (ModelState.IsValid) {
                string imagesDir = HttpContext.Server.MapPath("~/Content/uploadedimages/");
                fileBase.SaveAs(imagesDir + fileBase.FileName);
                imageRepository.InsertOrUpdate(image);
                imageRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleAlbums = albumRepository.All;
				return View();
			}
        }
        
        //
        // GET: /Images/Edit/5
 
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleAlbums = albumRepository.All;
             return View(imageRepository.Find(id));
        }

        //
        // POST: /Images/Edit/5

        [HttpPost]
        public ActionResult Edit(Image image, HttpPostedFileBase fileBase)
        {
            

            if (ModelState.IsValid)
            {
                if (fileBase != null)
                {
                    image.Path = fileBase.FileName;
                    string imagesDir = HttpContext.Server.MapPath("~/Content/uploadedimages/");
                    fileBase.SaveAs(imagesDir + fileBase.FileName);
                }
                
                context.Entry(image).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleAlbums = albumRepository.All;
				return View();
			}
        }

        //
        // GET: /Images/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(imageRepository.Find(id));
        }

        //
        // POST: /Images/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            imageRepository.Delete(id);
            imageRepository.Save();

            return RedirectToAction("Index");
        }
    }
}

