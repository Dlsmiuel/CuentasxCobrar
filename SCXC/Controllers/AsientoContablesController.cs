using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SCXC.Models;

namespace SCXC.Controllers
{
    public class AsientoContablesController : Controller
    {
        private CuentasxCobrarEntities db = new CuentasxCobrarEntities();

        // GET: AsientoContables
        public async Task<ActionResult> Index()
        {
            var asientoContables = db.AsientoContables.Include(a => a.Cliente);
            return View(await asientoContables.ToListAsync());
        }

        // GET: AsientoContables/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AsientoContable asientoContable = await db.AsientoContables.FindAsync(id);
            if (asientoContable == null)
            {
                return HttpNotFound();
            }
            return View(asientoContable);
        }

        // GET: AsientoContables/Create
        public ActionResult Create()
        {
            ViewBag.idClient = new SelectList(db.Clientes, "cliente_id", "nombre");
            return View();
        }

        // POST: AsientoContables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "description,AccountSeat,account,movementType,entryDate,seatAmount,status,idClient,idAuxiliarSystem")] AsientoContable asientoContable)
        {
            if (ModelState.IsValid)
            {
                db.AsientoContables.Add(asientoContable);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.idClient = new SelectList(db.Clientes, "cliente_id", "nombre", asientoContable.idClient);
            return View(asientoContable);
        }

        // GET: AsientoContables/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AsientoContable asientoContable = await db.AsientoContables.FindAsync(id);
            if (asientoContable == null)
            {
                return HttpNotFound();
            }
            ViewBag.idClient = new SelectList(db.Clientes, "cliente_id", "nombre", asientoContable.idClient);
            return View(asientoContable);
        }

        // POST: AsientoContables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "description,AccountSeat,account,movementType,entryDate,seatAmount,status,idClient,idAuxiliarSystem")] AsientoContable asientoContable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(asientoContable).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.idClient = new SelectList(db.Clientes, "cliente_id", "nombre", asientoContable.idClient);
            return View(asientoContable);
        }

        // GET: AsientoContables/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AsientoContable asientoContable = await db.AsientoContables.FindAsync(id);
            if (asientoContable == null)
            {
                return HttpNotFound();
            }
            return View(asientoContable);
        }

        // POST: AsientoContables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            AsientoContable asientoContable = await db.AsientoContables.FindAsync(id);
            db.AsientoContables.Remove(asientoContable);
            await db.SaveChangesAsync();
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
