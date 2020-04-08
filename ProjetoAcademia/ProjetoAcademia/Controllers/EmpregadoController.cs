using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjetoAcademiaModel;

namespace ProjetoAcademia.Controllers
{
    public class EmpregadoController : Controller
    {
        private PROJETOACADEMIAEntities db = new PROJETOACADEMIAEntities();

        // GET: /Empregado/
        public ActionResult Index()
        {
            return View(db.Empregado.ToList());
        }

        // GET: /Empregado/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empregado empregado = db.Empregado.Find(id);
            if (empregado == null)
            {
                return HttpNotFound();
            }
            return View(empregado);
        }

        // GET: /Empregado/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Empregado/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,identidade,cpf,nome,aulaGrupo,aulaMusculacao,senha,idPerfil")] Empregado empregado)
        {
            if (ModelState.IsValid)
            {
                db.Empregado.Add(empregado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(empregado);
        }

        // GET: /Empregado/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empregado empregado = db.Empregado.Find(id);
            if (empregado == null)
            {
                return HttpNotFound();
            }
            return View(empregado);
        }

        // POST: /Empregado/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,identidade,cpf,nome,aulaGrupo,aulaMusculacao,senha,idPerfil")] Empregado empregado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empregado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(empregado);
        }

        // GET: /Empregado/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empregado empregado = db.Empregado.Find(id);
            if (empregado == null)
            {
                return HttpNotFound();
            }
            return View(empregado);
        }

        // POST: /Empregado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Empregado empregado = db.Empregado.Find(id);
            db.Empregado.Remove(empregado);
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
