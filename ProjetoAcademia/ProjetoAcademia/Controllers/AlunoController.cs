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
    public class AlunoController : Controller
    {
        
        // GET: /Aluno/
        public ActionResult Index()
        {
            return View(Aluno.SelecionarTodos());
        }

        // GET: /Aluno/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aluno aluno = Aluno.Selecionar(id.Value);
            if (aluno == null)
            {
                return HttpNotFound();
            }
            return View(aluno);
        }

        // GET: /Aluno/Create
        public ActionResult Create()
        {
            ViewBag.TipoPlano = new SelectList(TipoPlano.SelecionarTodos(), "id", "Descricao");
            return View();
        }

        // POST: /Aluno/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,identidade,cpf,nome,endereco,tipoPlano")] Aluno aluno)
        {
            ViewBag.TipoPlano = new SelectList(TipoPlano.SelecionarTodos(), "id", "Descricao", aluno.tipoPlano);
            
            if (ModelState.IsValid)
            {
                aluno.nome = aluno.nome.ToUpper();
                Aluno.Incluir(aluno);
                return RedirectToAction("Index");
            }

            return View(aluno);
        }

        // GET: /Aluno/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aluno aluno = Aluno.Selecionar(id.Value);
            if (aluno == null)
            {
                return HttpNotFound();
            }
            ViewBag.TipoPlano = new SelectList(TipoPlano.SelecionarTodos(), "id", "Descricao", aluno.tipoPlano);

            return View(aluno);
        }

        // POST: /Aluno/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,identidade,cpf,nome,endereco,tipoPlano")] Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                aluno.nome = aluno.nome.ToUpper();
                Aluno.Alterar(aluno);
                return RedirectToAction("Index");
            }
            ViewBag.TipoPlano = new SelectList(TipoPlano.SelecionarTodos(), "id", "Descricao", aluno.tipoPlano);

            return View(aluno);
        }

        // GET: /Aluno/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewData["Erro"] = "";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aluno aluno = Aluno.Selecionar(id.Value);
            if (aluno == null)
            {
                return HttpNotFound();
            }

            if (Pagamento.SelecionarTodos(id.Value).Count > 0)
            {
                ViewData["Erro"] = "Não é possível excluir este aluno pois existem pagamentos lançados para o mesmo";
            }

            return View(aluno);
        }

        // POST: /Aluno/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Aluno aluno = Aluno.Selecionar(id);
            Aluno.Excluir(aluno);
            return RedirectToAction("Index");
        }

       
    }
}
