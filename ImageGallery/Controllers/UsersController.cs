using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RozichMurals.Web.Models;

namespace RozichMurals.Web.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private RozichMuralsWebContext context = new RozichMuralsWebContext();

        //
        // GET: /Users/

        public ViewResult Index()
        {
            return View(context.Users.Include(user => user.Roles).ToList());
        }

        //
        // GET: /Users/Details/5

        public ViewResult Details(string id)
        {
            User user = context.Users.Single(x => x.UserName == id);
            return View(user);
        }

        //
        // GET: /Users/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Users/Create

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                context.Users.Add(user);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(user);
        }
        
        //
        // GET: /Users/Edit/5
 
        public ActionResult Edit(string id)
        {
            User user = context.Users.Single(x => x.UserName == id);
            return View(user);
        }

        //
        // POST: /Users/Edit/5

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                context.Entry(user).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        //
        // GET: /Users/Delete/5
 
        public ActionResult Delete(string id)
        {
            User user = context.Users.Single(x => x.UserName == id);
            return View(user);
        }

        //
        // POST: /Users/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            User user = context.Users.Single(x => x.UserName == id);
            context.Users.Remove(user);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}