using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoVerhuurJansen.Models;

namespace AutoVerhuurJansen.Controllers
{
    public class VerhuurLijjstController : Controller
    {
        private DB_Jansen db = new DB_Jansen();

        // GET: VerhuurLijjst
        public ActionResult Index()
        {
            //var verhuren = db.Verhuren.Include(v => v.Klanten).Include(v => v.Medewerkers).Include(v => v.Voertuigen);

            //Lijst van beschikbare Auto's
            List<Verhuren> beschikbareVoertuigen = new List<Verhuren>();
            //Lijst van auto's
            List<Verhuren> VerhuurLijst = db.Verhuren.ToList();
            //Kijken welke auto's gehuurd worden vanaf vandaag
            DateTime vandaag = DateTime.Now;
            
            foreach(var auto in VerhuurLijst)
            {
                //Als de begin datum voor vandaag is dan laat je hem niet zien
                if(auto.beginDatum < vandaag)
                {
                    //als de einddatum na vandaag is is die verhuring al gebeurd.
                    if(auto.eindDatum > vandaag)
                    {

                    }
                }
                else
                {
                    beschikbareVoertuigen.Add(auto);
                }
            }

            return View(beschikbareVoertuigen);
        }

        // GET: VerhuurLijjst/Details/5
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

        // GET: VerhuurLijjst/Create
        public ActionResult Create()
        {
            ViewBag.klantId = new SelectList(db.Klanten, "klantId", "voornaam");
            ViewBag.medewerkerId = new SelectList(db.Medewerkers, "medewerkerId", "voornaam");
            ViewBag.kenteken = new SelectList(db.Voertuigen, "kenteken", "merk");
            return View();
        }

        // POST: VerhuurLijjst/Create
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

            ViewBag.klantId = new SelectList(db.Klanten, "klantId", "voornaam", verhuren.klantId);
            ViewBag.medewerkerId = new SelectList(db.Medewerkers, "medewerkerId", "voornaam", verhuren.medewerkerId);
            ViewBag.kenteken = new SelectList(db.Voertuigen, "kenteken", "merk", verhuren.kenteken);
            return View(verhuren);
        }

        // GET: VerhuurLijjst/Edit/5
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
            ViewBag.klantId = new SelectList(db.Klanten, "klantId", "voornaam", verhuren.klantId);
            ViewBag.medewerkerId = new SelectList(db.Medewerkers, "medewerkerId", "voornaam", verhuren.medewerkerId);
            ViewBag.kenteken = new SelectList(db.Voertuigen, "kenteken", "merk", verhuren.kenteken);
            return View(verhuren);
        }

        // POST: VerhuurLijjst/Edit/5
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
            ViewBag.klantId = new SelectList(db.Klanten, "klantId", "voornaam", verhuren.klantId);
            ViewBag.medewerkerId = new SelectList(db.Medewerkers, "medewerkerId", "voornaam", verhuren.medewerkerId);
            ViewBag.kenteken = new SelectList(db.Voertuigen, "kenteken", "merk", verhuren.kenteken);
            return View(verhuren);
        }

        // GET: VerhuurLijjst/Delete/5
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

        // POST: VerhuurLijjst/Delete/5
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
