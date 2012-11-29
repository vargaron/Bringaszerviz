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
    public class OffersController : Controller
    {
        private BikeServiceContext db = new BikeServiceContext();

        //
        // GET: /Offers/
        public ActionResult Index()
        {
            return View(db.Offers.ToList());
        }

        // GET: /Offers/ListAll
        [Authorize(Roles = "Customer")]
        public ActionResult ListAll(int ticketId)
        {
            var ticket = (from t in db.Tickets where t.ticketID == ticketId select t).Single();

            var curUser = (from u in db.UserProfiles
                           where u.UserName == User.Identity.Name
                           select u).Single();
            ViewBag.curUser = curUser;

            var myoffers = ticket.offers;

            return View(myoffers.ToList());
        }

        //
        // GET: /Offers/Details/5

        public ActionResult Details(int id = 0)
        {
            Offer offer = db.Offers.Find(id);
            if (offer == null)
            {
                return HttpNotFound();
            }
            return View(offer);
        }

        //
        // GET: /Offers/Create
        [Authorize(Roles = "Service")]
        public ActionResult Create(int ticketId)
        {
            Offer offer = new Offer();
            offer.deadline = DateTime.Now.AddDays(1);
            ViewBag.ticketId = ticketId;

            return View(offer);
        }

        //
        // POST: /Offers/Create
        [Authorize(Roles = "Service")]
        [HttpPost]
        public ActionResult Create(Offer offer, int ticketId)
        {
            if (ModelState.IsValid)
            {
                var curUser = (from u in db.UserProfiles
                               where u.UserName == User.Identity.Name
                               select u).Single();
                offer.serviceID = curUser.UserId;

                var ticket = (from t in db.Tickets where t.ticketID == ticketId select t).Single();
                offer.ticket = ticket;
                offer.accepted = false;

                db.Offers.Add(offer);
                db.SaveChanges();
                return RedirectToAction("ListAll", "Tickets");

            }

            return View(offer);
        }

        //
        // GET: /Offers/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Offer offer = db.Offers.Find(id);
            if (offer == null)
            {
                return HttpNotFound();
            }
            
            return View(offer);
        }

        //
        // POST: /Offers/Edit/5

        [HttpPost]
        public ActionResult Edit(Offer offer)
        {
            if (ModelState.IsValid)
            {
                var curUser = (from u in db.UserProfiles
                               where u.UserName == User.Identity.Name
                               select u).Single();
                offer.serviceID = curUser.UserId;
                
                db.Entry(offer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListAll", "Tickets");
            }
            return View(offer);
        }

        //
        // GET: /Offers/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Offer offer = db.Offers.Find(id);
            if (offer == null)
            {
                return HttpNotFound();
            }
            return View(offer);
        }

        //
        // POST: /Offers/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Offer offer = db.Offers.Find(id);
            db.Offers.Remove(offer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult Accept(int id)
        {
            Offer offer = db.Offers.Find(id);
            offer.accepted = true;
            offer.ticket.solved = true;

            db.SaveChanges();
            return RedirectToAction("ListAll", new { ticketId = offer.ticket.ticketID });
        }
    }
}