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
    public class RolesController : Controller
    {
        private RozichMuralsWebContext context = new RozichMuralsWebContext();

        //
        // GET: /Roles/

        public ViewResult Index()
        {
            return View(context.Roles.Include(role => role.Users).ToList());
        }

        //
        // GET: /Roles/Details/5

        public ViewResult Details(string id)
        {
            Role role = context.Roles.Single(x => x.RoleName == id);
            return View(role);
        }

        //
        // GET: /Roles/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Roles/Create

        [HttpPost]
        public ActionResult Create(Role role)
        {
            if (ModelState.IsValid)
            {
                context.Roles.Add(role);
                context.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(role);
        }
        
        //
        // GET: /Roles/Edit/5
 
        public ActionResult Edit(string id)
        {
            Role role = context.Roles.Single(x => x.RoleName == id);
            return View(role);
        }

        //
        // POST: /Roles/Edit/5

        [HttpPost]
        public ActionResult Edit(Role role)
        {
            if (ModelState.IsValid)
            {
                context.Entry(role).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(role);
        }

        //
        // GET: /Roles/Delete/5
 
        public ActionResult Delete(string id)
        {
            Role role = context.Roles.Single(x => x.RoleName == id);
            return View(role);
        }

        //
        // POST: /Roles/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            Role role = context.Roles.Single(x => x.RoleName == id);
            context.Roles.Remove(role);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}