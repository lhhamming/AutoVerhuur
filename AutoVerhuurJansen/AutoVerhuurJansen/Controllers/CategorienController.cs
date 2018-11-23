using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;  
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoVerhuurJansen.Models;

namespace AutoVerhuurJansen.Controllers
{
    public class CategorienController : Controller
    {
        private DB_Jansen db = new DB_Jansen();

        // GET: Categorien
        public ActionResult Index()
        {
            ViewBag.doublename = null;
            var prijshistory = db.Categorie.Include(v => v.PrijsHistorie);
            return View(prijshistory.ToList());
        }

        // GET: Categorien/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categorie categorie = db.Categorie.Find(id);
            if (categorie == null)
            {
                return HttpNotFound();
            }
            return View(categorie);
        }

        // GET: Categorien/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categorien/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "categorieId,categorieNaam,aantalPersonen,aantalKoffers,categorieprijs")] Categorie categorie)
        {
            if (ModelState.IsValid)
            {
                    db.Categorie.Add(categorie);
                    try{
                    db.SaveChanges();
                    }
                    catch (DbUpdateException ex)
                    {
                        ViewBag.doublename = "There is already an item with that name. Please use a different one";
                        return View();
                    }
                    return RedirectToAction("Index");

            }

            return View(categorie);
        }

        // GET: Categorien/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categorie categorie = db.Categorie.Find(id);
            if (categorie == null)
            {
                return HttpNotFound();
            }
            return View(categorie);
        }

        // POST: Categorien/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "categorieId,categorieNaam,aantalPersonen,aantalKoffers")] Categorie categorie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categorie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categorie);
        }

        // GET: Categorien/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categorie categorie = db.Categorie.Find(id);
            if (categorie == null)
            {
                return HttpNotFound();
            }
            return View(categorie);
        }

        // POST: Categorien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Categorie categorie = db.Categorie.Find(id);
            db.Categorie.Remove(categorie);
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
