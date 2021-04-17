using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
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
        public ActionResult Create([Bind(Include = "idAsiento,AccountSeat,account,movementType,entryDate,seatAmount,status,idClient,idAuxiliarSystem")] AsientosContable asientosContable)
        {
            if (ModelState.IsValid)
            {
                db.AsientosContables.Add(asientosContable);
                db.SaveChanges();
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
        public ActionResult Edit([Bind(Include = "idAsiento,AccountSeat,account,movementType,entryDate,seatAmount,status,idClient,idAuxiliarSystem")] AsientosContable asientosContable)
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

        /*public async Task<ActionResult> Contabilizar (int? id)
        {
            if(id == null)
            {
                return HttpNotFound();
            }

            var asientoscontables = await db.AsientosContables.FindAsync(id);

            if(asientoscontables == null)
            {
                return HttpNotFound();
            }

            var data = new
            {
                description = asientoscontables.description,
                idAuxiliarSystem = asientoscontables.idAuxiliarSystem,
                account = asientoscontables.account,
                movementType = asientoscontables.movementType,
                entryDate = asientoscontables.entryDate,
                seatAmount = asientoscontables.seatAmount,
                status = asientoscontables.status
            };

            var httpClient = new HttpClient();
            var stringContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("https://d57b22c5989c.ngrok.io//api/accountingEntry", stringContent);

            var contents = response.Content.ReadAsStringAsync().IsCompleted;
            var result = await response.Content.ReadAsStringAsync();
            dynamic deserialized = JsonConvert.DeserializeObject(result);
            dynamic newID = (int)deserialized.id;

            if (contents)
            {
                asientoscontables.idClient = newID;

                try
                {
                    db.Entry(asientoscontables).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SCXC(asientoscontables.idClient))
                    {
                        return HttpNotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));   
            }
            return View(asientoscontables);
        }*/
    }
}
