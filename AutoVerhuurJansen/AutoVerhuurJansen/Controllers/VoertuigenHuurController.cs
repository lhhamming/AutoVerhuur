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
    public class VoertuigenHuurController : Controller
    {
        private DB_Jansen db = new DB_Jansen();

        // GET: VoertuigenHuur
        public ActionResult Index(DateTime? startDate, DateTime? endDate)
        {

            if (startDate.HasValue && endDate.HasValue)
            {
                //var lodges = db.lodges.Where(c => !db.reserverings.Select(b => b.lodgeID).Contains(c.lodgeID));
                var voertuigen = db.Voertuigen.Where(l => l.Verhuren.All(r => r.eindDatum <= startDate || r.beginDatum >= endDate));



                return View(voertuigen);
            }

            else
            {
                startDate = DateTime.Now;
                endDate = DateTime.Parse("01/01/9999 00:00");

                var voertuigen = db.Voertuigen.Where(l => l.Verhuren.All(r => r.eindDatum <= startDate || r.beginDatum >= endDate));


                return View(voertuigen);
            }

        }

        public ActionResult Reserveer(string id, DateTime startDate, DateTime endDate)
        {


            var userId = User.Identity.GetUserId();

            var userID = db.Klanten.Where(k => k.AspNetUserID == userId).FirstOrDefault();


            if (ModelState.IsValid)
            {

                var huur = new Verhuren() { klantId = userID.klantId, kenteken = id, beginDatum = startDate, eindDatum = endDate, afgehandeld = false};
                db.Verhuren.Add(huur);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: VoertuigenHuur/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voertuigen voertuigen = db.Voertuigen.Find(id);
            if (voertuigen == null)
            {
                return HttpNotFound();
            }
            return View(voertuigen);
        }

        public ActionResult Rent()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Rent([Bind(Include = "beginDatum,eindDatum")]Verhuren verhuren, string id)
        {
            //Get the current userID
            var currentuser = User.Identity.GetUserId();
            //CurrentKlant
            //var klantidUser = db.Klanten.Where(k => k.AspNetUserID == currentuser).FirstOrDefault();
            verhuren.klantId = 0;
                //klantidUser.klantId;
            verhuren.afgehandeld = false;
            verhuren.kenteken = id;

            if (ModelState.IsValid)
            {
                db.Verhuren.Add(verhuren);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // GET: VoertuigenHuur/Create
        public ActionResult Create()
        {
            ViewBag.categorieId = new SelectList(db.Categorie, "categorieId", "categorieNaam");
            return View();
        }

        // POST: VoertuigenHuur/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "kenteken,categorieId,merk,type,Actief")] Voertuigen voertuigen)
        {
            if (ModelState.IsValid)
            {
                db.Voertuigen.Add(voertuigen);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.categorieId = new SelectList(db.Categorie, "categorieId", "categorieNaam", voertuigen.categorieId);
            return View(voertuigen);
        }

        // GET: VoertuigenHuur/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voertuigen voertuigen = db.Voertuigen.Find(id);
            if (voertuigen == null)
            {
                return HttpNotFound();
            }
            ViewBag.categorieId = new SelectList(db.Categorie, "categorieId", "categorieNaam", voertuigen.categorieId);
            return View(voertuigen);
        }

        // POST: VoertuigenHuur/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "kenteken,categorieId,merk,type,Actief")] Voertuigen voertuigen)
        {
            if (ModelState.IsValid)
            {
                db.Entry(voertuigen).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.categorieId = new SelectList(db.Categorie, "categorieId", "categorieNaam", voertuigen.categorieId);
            return View(voertuigen);
        }

        // GET: VoertuigenHuur/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voertuigen voertuigen = db.Voertuigen.Find(id);
            if (voertuigen == null)
            {
                return HttpNotFound();
            }
            return View(voertuigen);
        }

        // POST: VoertuigenHuur/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Voertuigen voertuigen = db.Voertuigen.Find(id);
            db.Voertuigen.Remove(voertuigen);
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
