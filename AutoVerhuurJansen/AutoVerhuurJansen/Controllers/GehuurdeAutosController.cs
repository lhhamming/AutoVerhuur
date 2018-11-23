using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoVerhuurJansen.Models;
using Microsoft.AspNet.Identity;

namespace AutoVerhuurJansen.Controllers
{
    public class GehuurdeAutosController : Controller
    {
        private DB_Jansen db = new DB_Jansen();

        // GET: GehuurdeAutos
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var currentK = db.Klanten.Where(k => k.AspNetUserID == userId).FirstOrDefault();
            var gehuurdeAutos = db.Verhuren.Where(k => k.klantId == currentK.klantId);
            //var verhuren = db.Verhuren.Include(v => v.Medewerkers).Include(v => v.Voertuigen).Include(v => v.Klanten);

            return View(gehuurdeAutos.ToList());
        }

        // GET: GehuurdeAutos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Verhuren verhuren = db.Verhuren.Find(id);
            if (verhuren == null)
            {
                return HttpNotFound();
            }
            return View(verhuren);
        }

        // GET: GehuurdeAutos/Create
        public ActionResult Create()
        {
            ViewBag.medewerkerId = new SelectList(db.Medewerkers, "medewerkerId", "voornaam");
            ViewBag.kenteken = new SelectList(db.Voertuigen, "kenteken", "merk");
            ViewBag.klantId = new SelectList(db.Klanten, "klantId", "voornaam");
            return View();
        }

        // POST: GehuurdeAutos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VerhuurId,klantId,kenteken,medewerkerId,beginDatum,eindDatum,afgehandeld")] Verhuren verhuren)
        {
            if (ModelState.IsValid)
            {
                db.Verhuren.Add(verhuren);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.medewerkerId = new SelectList(db.Medewerkers, "medewerkerId", "voornaam", verhuren.medewerkerId);
            ViewBag.kenteken = new SelectList(db.Voertuigen, "kenteken", "merk", verhuren.kenteken);
            ViewBag.klantId = new SelectList(db.Klanten, "klantId", "voornaam", verhuren.klantId);
            return View(verhuren);
        }

        // GET: GehuurdeAutos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Verhuren verhuren = db.Verhuren.Find(id);
            if (verhuren == null)
            {
                return HttpNotFound();
            }
            ViewBag.medewerkerId = new SelectList(db.Medewerkers, "medewerkerId", "voornaam", verhuren.medewerkerId);
            ViewBag.kenteken = new SelectList(db.Voertuigen, "kenteken", "merk", verhuren.kenteken);
            ViewBag.klantId = new SelectList(db.Klanten, "klantId", "voornaam", verhuren.klantId);
            return View(verhuren);
        }

        // POST: GehuurdeAutos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VerhuurId,klantId,kenteken,medewerkerId,beginDatum,eindDatum,afgehandeld")] Verhuren verhuren)
        {
            if (ModelState.IsValid)
            {
                db.Entry(verhuren).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.medewerkerId = new SelectList(db.Medewerkers, "medewerkerId", "voornaam", verhuren.medewerkerId);
            ViewBag.kenteken = new SelectList(db.Voertuigen, "kenteken", "merk", verhuren.kenteken);
            ViewBag.klantId = new SelectList(db.Klanten, "klantId", "voornaam", verhuren.klantId);
            return View(verhuren);
        }

        // GET: GehuurdeAutos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Verhuren verhuren = db.Verhuren.Find(id);
            if (verhuren == null)
            {
                return HttpNotFound();
            }
            return View(verhuren);
        }

        // POST: GehuurdeAutos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Verhuren verhuren = db.Verhuren.Find(id);
            db.Verhuren.Remove(verhuren);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
