using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using SCXC.Models;

namespace SCXC.Controllers
{
    public class AsientosContablesController : Controller
    {
        private CuentasxCobrarEntities db = new CuentasxCobrarEntities();

        // GET: AsientosContables
        public ActionResult Index()
        {
            var asientosContables = db.AsientosContables.Include(a => a.Cliente);
            return View(asientosContables.ToList());
        }

        // GET: AsientosContables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AsientosContable asientosContable = db.AsientosContables.Find(id);
            if (asientosContable == null)
            {
                return HttpNotFound();
            }
            return View(asientosContable);
        }

        // GET: AsientosContables/Create
        public ActionResult Create()
        {
            ViewBag.idClient = new SelectList(db.Clientes, "cliente_id", "nombre");
            return View();
        }

        // POST: AsientosContables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Description,IdAuxiliarSystem,MovementType,EntryDate,Status,idClient")] AsientosContable asientosContable)
        {
            if (ModelState.IsValid)
            {
                db.AsientosContables.Add(asientosContable);
                db.SaveChanges();
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri("https://d57b22c5989c.ngrok.io/api/");
                    var request = new AsientosContablesClase()
                    {
                        Description = asientosContable.Description,
                        IdAuxiliarSystem = asientosContable.IdAuxiliarSystem,
                        MovementType = asientosContable.MovementType,
                        EntryDate = asientosContable.EntryDate,
                        Status = asientosContable.Status
                    };
                    var response = client.PostAsJsonAsync<AsientosContablesClase>("accountingEntry", request);
                    response.Wait();

                    var postResult = response.Result;
                    if (postResult.IsSuccessStatusCode)
                    {
                        TempData["alertMessage"] = "Whatever you want to alert the user with";
                    }
                }
                return RedirectToAction("Index");
            }           

            ViewBag.idClient = new SelectList(db.Clientes, "cliente_id", "nombre", asientosContable.idClient);
            return View(asientosContable);
        }

        // GET: AsientosContables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AsientosContable asientosContable = db.AsientosContables.Find(id);
            if (asientosContable == null)
            {
                return HttpNotFound();
            }
            ViewBag.idClient = new SelectList(db.Clientes, "cliente_id", "nombre", asientosContable.idClient);
            return View(asientosContable);
        }

        // POST: AsientosContables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Description,IdAuxiliarSystem,MovementType,EntryDate,Status,idClient")] AsientosContable asientosContable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(asientosContable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idClient = new SelectList(db.Clientes, "cliente_id", "nombre", asientosContable.idClient);
            return View(asientosContable);
        }

        // GET: AsientosContables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AsientosContable asientosContable = db.AsientosContables.Find(id);
            if (asientosContable == null)
            {
                return HttpNotFound();
            }
            return View(asientosContable);
        }

        // POST: AsientosContables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AsientosContable asientosContable = db.AsientosContables.Find(id);
            db.AsientosContables.Remove(asientosContable);
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
