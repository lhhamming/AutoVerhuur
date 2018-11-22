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
    public class VoertuigenLijstController : Controller
    {
        private DB_Jansen db = new DB_Jansen();

        // GET: VoertuigenLijst
        public ActionResult Index()
        {
            var voertuigen = db.Voertuigen.Include(v => v.Categorie);
            return View(voertuigen.ToList());
        }

        // GET: VoertuigenLijst/Details/5
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

        // GET: VoertuigenLijst/Create
        public ActionResult Create()
        {
            ViewBag.categorieId = new SelectList(db.Categorie, "categorieId", "categorieNaam");
            return View();
        }

        // POST: VoertuigenLijst/Create
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

        // GET: VoertuigenLijst/Edit/5
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

        // POST: VoertuigenLijst/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "kenteken,categorieId,Actief")] Voertuigen voertuigen)
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

        // GET: VoertuigenLijst/Delete/5
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

        // POST: VoertuigenLijst/Delete/5
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
