using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using AutoVerhuurJansen.Models;

namespace AutoVerhuurJansen.Controllers
{
    public class PDFController : Controller
    {
        private DB_Jansen db = new DB_Jansen();

        // GET: PDF/PDF/{id}
        public ActionResult PDF(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Verhuren verhuur = db.Verhuren.Find(id);
            if (verhuur == null)
            {
                return HttpNotFound();
            }

            //luuk sloopt alles
            PDFMaker PDFMaker = new PDFMaker();
            byte[] abytes = PDFMaker.PreparePDF(verhuur);

            return File(abytes, "application/pdf");
        }
    }
}