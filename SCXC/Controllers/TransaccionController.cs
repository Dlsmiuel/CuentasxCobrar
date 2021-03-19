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
    public class TransaccionController : Controller
    {
        private CuentasxCobrarEntities db = new CuentasxCobrarEntities();

        // GET: Transaccion
        public ActionResult Index()
        {
            var transaccions = db.Transaccions.Include(t => t.Cliente).Include(t => t.TipoDoc);
            return View(transaccions.ToList());
        }

        // GET: Transaccion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaccion transaccion = db.Transaccions.Find(id);
            if (transaccion == null)
            {
                return HttpNotFound();
            }
            return View(transaccion);
        }

        // GET: Transaccion/Create
        public ActionResult Create()
        {
            ViewBag.cliente_id = new SelectList(db.Clientes, "cliente_id", "nombre");
            ViewBag.tipoDoc_id = new SelectList(db.TipoDocs, "tipoDoc_id", "descripcion");
            return View();
        }

        // POST: Transaccion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "transaccion_id,tipoMovimiento,tipoDoc_id,numDoc,fecha,cliente_id,monto")] Transaccion transaccion)
        {
            if (ModelState.IsValid)
            {
                db.Transaccions.Add(transaccion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cliente_id = new SelectList(db.Clientes, "cliente_id", "nombre", transaccion.cliente_id);
            ViewBag.tipoDoc_id = new SelectList(db.TipoDocs, "tipoDoc_id", "descripcion", transaccion.tipoDoc_id);
            return View(transaccion);
        }

        // GET: Transaccion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaccion transaccion = db.Transaccions.Find(id);
            if (transaccion == null)
            {
                return HttpNotFound();
            }
            ViewBag.cliente_id = new SelectList(db.Clientes, "cliente_id", "nombre", transaccion.cliente_id);
            ViewBag.tipoDoc_id = new SelectList(db.TipoDocs, "tipoDoc_id", "descripcion", transaccion.tipoDoc_id);
            return View(transaccion);
        }

        // POST: Transaccion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "transaccion_id,tipoMovimiento,tipoDoc_id,numDoc,fecha,cliente_id,monto")] Transaccion transaccion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaccion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cliente_id = new SelectList(db.Clientes, "cliente_id", "nombre", transaccion.cliente_id);
            ViewBag.tipoDoc_id = new SelectList(db.TipoDocs, "tipoDoc_id", "descripcion", transaccion.tipoDoc_id);
            return View(transaccion);
        }

        // GET: Transaccion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaccion transaccion = db.Transaccions.Find(id);
            if (transaccion == null)
            {
                return HttpNotFound();
            }
            return View(transaccion);
        }

        // POST: Transaccion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transaccion transaccion = db.Transaccions.Find(id);
            db.Transaccions.Remove(transaccion);
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
