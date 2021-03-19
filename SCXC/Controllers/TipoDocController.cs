using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SCXC.Models;

namespace SCXC.Controllers
{
    public class TipoDocController : Controller
    {
        private CuentasxCobrarEntities db = new CuentasxCobrarEntities();

        // GET: TipoDoc
        public ActionResult Index()
        {
            return View(db.TipoDocs.ToList());
        }

        // GET: TipoDoc/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoDoc tipoDoc = db.TipoDocs.Find(id);
            if (tipoDoc == null)
            {
                return HttpNotFound();
            }
            return View(tipoDoc);
        }

        // GET: TipoDoc/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoDoc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tipoDoc_id,descripcion,cuentaContable,estado")] TipoDoc tipoDoc)
        {
            if (ModelState.IsValid)
            {
                db.TipoDocs.Add(tipoDoc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoDoc);
        }

        // GET: TipoDoc/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoDoc tipoDoc = db.TipoDocs.Find(id);
            if (tipoDoc == null)
            {
                return HttpNotFound();
            }
            ViewBag.estado = new SelectList(db.Clientes, "cliente_id", "estado");
            return View(tipoDoc);
        }

        // POST: TipoDoc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tipoDoc_id,descripcion,cuentaContable,estado")] TipoDoc tipoDoc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoDoc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.estado = new SelectList(db.Clientes, "cliente_id", "estado");
            return View(tipoDoc);
        }

        // GET: TipoDoc/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoDoc tipoDoc = db.TipoDocs.Find(id);
            if (tipoDoc == null)
            {
                return HttpNotFound();
            }
            return View(tipoDoc);
        }

        // POST: TipoDoc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoDoc tipoDoc = db.TipoDocs.Find(id);
            db.TipoDocs.Remove(tipoDoc);
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
