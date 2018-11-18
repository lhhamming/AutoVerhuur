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
    public class PrijsHistoriesController : Controller
    {
        private DB_Jansen db = new DB_Jansen();

        // GET: PrijsHistories
        public ActionResult Index()
        {
            var prijsHistorie = db.PrijsHistorie.Include(p => p.Categorie);
            var cPrijs = prijsHistorie.Where(p => p.eindDatum == null);
            return View(cPrijs.ToList());
        }

        // GET: PrijsHistories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrijsHistorie prijsHistorie = db.PrijsHistorie.Find(id);
            if (prijsHistorie == null)
            {
                return HttpNotFound();
            }
            return View(prijsHistorie);
        }

        // GET: PrijsHistories/Create
        public ActionResult Create()
        {
            ViewBag.categorieId = new SelectList(db.Categorie, "categorieId", "categorieNaam");
            return View();
        }

        // POST: PrijsHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "prijsCategorieId,categorieId,prijsPerDag")] PrijsHistorie model)
        {

            PrijsHistorie prijsHistorie = new PrijsHistorie();

            if (ModelState.IsValid)
            {

                prijsHistorie.categorieId = model.categorieId;
                prijsHistorie.beginDatum = DateTime.Now;
                prijsHistorie.prijsPerDag = model.prijsPerDag;


                db.PrijsHistorie.Add(prijsHistorie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.categorieId = new SelectList(db.Categorie, "categorieId", "categorieNaam", model.categorieId);
            return View(model);
        }


        public ActionResult ChangePrice(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrijsHistorie prijsHistorie = db.PrijsHistorie.Find(id);
            if (prijsHistorie == null)
            {
                return HttpNotFound();
            }
            return View(prijsHistorie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePrice([Bind(Include = "prijsCategorieId,categorieId,beginDatum,prijsPerDag")]PrijsHistorie model, decimal newvalue)
        {
            PrijsHistorie prijsHistorie = new PrijsHistorie();

            if (ModelState.IsValid)
            {

                model.eindDatum = DateTime.Now.AddDays(-1);
                db.Entry(model).State = EntityState.Modified;

                prijsHistorie.prijsPerDag = newvalue;
                prijsHistorie.beginDatum = DateTime.Now;
                prijsHistorie.categorieId = model.categorieId;

                db.PrijsHistorie.Add(prijsHistorie);

                db.SaveChanges();
                return RedirectToAction("Index");
            }


            return View(model);

        }

        // GET: PrijsHistories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrijsHistorie prijsHistorie = db.PrijsHistorie.Find(id);
            if (prijsHistorie == null)
            {
                return HttpNotFound();
            }
            ViewBag.categorieId = new SelectList(db.Categorie, "categorieId", "categorieNaam", prijsHistorie.categorieId);
            return View(prijsHistorie);
        }

        // POST: PrijsHistories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "prijsCategorieId,categorieId,beginDatum,eindDatum,prijsPerDag")] PrijsHistorie prijsHistorie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prijsHistorie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.categorieId = new SelectList(db.Categorie, "categorieId", "categorieNaam", prijsHistorie.categorieId);
            return View(prijsHistorie);
        }

        // GET: PrijsHistories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrijsHistorie prijsHistorie = db.PrijsHistorie.Find(id);
            if (prijsHistorie == null)
            {
                return HttpNotFound();
            }
            return View(prijsHistorie);
        }

        // POST: PrijsHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PrijsHistorie prijsHistorie = db.PrijsHistorie.Find(id);
            db.PrijsHistorie.Remove(prijsHistorie);
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
