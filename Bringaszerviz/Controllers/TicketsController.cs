using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bringaszerviz.Models;

namespace Bringaszerviz.Controllers
{
    
    public class TicketsController : Controller
    {
        private BikeServiceContext db = new BikeServiceContext();

        //
        // GET: /Tickets/
        [Authorize(Roles = "Customer")]
        public ActionResult Index()
        {
            //var tickets = from s in db.Tickets select s;
            //return View(tickets.ToList());

            var curUser = (from u in db.UserProfiles
                               where u.UserName == User.Identity.Name
                               select u).Single();
            ViewBag.curUser = curUser;

            var mytickets = (from u in db.Tickets
                               where u.ownerID == curUser.UserId
                               select u);

            return View(mytickets.ToList());       
        }

        // GET: /Tickets/ListAll
        [Authorize(Roles = "Service")]
        public ActionResult ListAll()
        {
            var curUser = (from u in db.UserProfiles
                           where u.UserName == User.Identity.Name
                           select u).Single();
            ViewBag.curUser = curUser;

            var tickets = from s in db.Tickets select s;
            return View(tickets.ToList());
        }

        //
        // GET: /Tickets/Details/5
        [Authorize(Roles = "Customer")]
        public ActionResult Details(int id = 0)
        {
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        //
        // GET: /Tickets/Create
        [Authorize(Roles = "Customer")]
        public ActionResult Create()
        {
            var curUser = (from u in db.UserProfiles
                           where u.UserName == User.Identity.Name
                           select u).Single();
            ViewBag.Owner = curUser.UserName;
            return View();
        }

        //
        // POST: /Tickets/Create
        [HttpPost]
        [Authorize(Roles = "Customer")]
        public ActionResult Create(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                var curUser = (from u in db.UserProfiles
                               where u.UserName == User.Identity.Name
                               select u).Single();
                ticket.ownerID = curUser.UserId;
                ticket.solved = false;
                db.Tickets.Add(ticket);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ticket);
        }

        //
        // GET: /Tickets/Edit/5
        [Authorize(Roles = "Customer")]
        public ActionResult Edit(int id = 0)
        {
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        //
        // POST: /Tickets/Edit/5

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public ActionResult Edit(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ticket);
        }

        //
        // GET: /Tickets/Delete/5
        [Authorize(Roles = "Customer")]
        public ActionResult Delete(int id = 0)
        {
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        //
        // POST: /Tickets/Delete/5
        [Authorize(Roles = "Customer")]
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Ticket ticket = db.Tickets.Find(id);
            db.Tickets.Remove(ticket);

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}