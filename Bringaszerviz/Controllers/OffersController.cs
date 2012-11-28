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

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Offers/Create

        [HttpPost]
        public ActionResult Create(Offer offer)
        {
            if (ModelState.IsValid)
            {
                db.Offers.Add(offer);
                db.SaveChanges();
                return RedirectToAction("Index");
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
                db.Entry(offer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
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
    }
}