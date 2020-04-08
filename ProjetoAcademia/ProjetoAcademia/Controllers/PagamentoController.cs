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
    public class PagamentoController : Controller
    {
        
        // GET: /Pagamento/
        public ActionResult Index(int idAluno)
        {
            ViewData["idAluno"] = idAluno;
            return View(Pagamento.SelecionarTodos(idAluno).OrderByDescending(x=>x.dataBaseInicio).ToList());
        }

        // GET: /Pagamento/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pagamento pagamento = Pagamento.Selecionar(id.Value);
            if (pagamento == null)
            {
                return HttpNotFound();
            }
            return View(pagamento);
        }

        // GET: /Pagamento/Create
        public ActionResult Create(int idAluno)
        {
            Pagamento pg = new Pagamento();
            pg.idAluno = idAluno;
            pg.valor = TipoPlano.Selecionar(Aluno.Selecionar(idAluno).tipoPlano).valor;

            return View(pg);
        }

        // POST: /Pagamento/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="id,idAluno,valor,dataProxima,dataBaseInicio,dataBaseFim")] Pagamento pagamento)
        {
            if (pagamento.valor > TipoPlano.Selecionar(Aluno.Selecionar(pagamento.idAluno).tipoPlano).valor)
            {
                ModelState.AddModelError("valor", "O valor pago não pode ser maior que o valor do plano atual");
            }

            if (ModelState.IsValid)
            {
            
                if (TipoPlano.Selecionar(Aluno.Selecionar(pagamento.idAluno).tipoPlano).descricao.Equals("MENSAL"))
                {
                    pagamento.dataBaseFim = pagamento.dataBaseInicio.AddMonths(1);
                    pagamento.dataProxima = pagamento.dataBaseInicio.AddMonths(1);
                }
                else
                {
                    pagamento.dataBaseFim = pagamento.dataBaseInicio.AddYears(1);
                    pagamento.dataProxima = pagamento.dataBaseInicio.AddYears(1);
                }

                pagamento.dataProxima = pagamento.dataProxima.AddDays(1);
                
                Pagamento.Incluir(pagamento);
                return RedirectToAction("Index", new { idAluno = pagamento.idAluno });
            }

            return View(pagamento);
        }

        // GET: /Pagamento/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pagamento pagamento = Pagamento.Selecionar(id.Value);
            if (pagamento == null)
            {
                return HttpNotFound();
            }
            return View(pagamento);
        }

        // POST: /Pagamento/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="id,idAluno,valor,dataProxima,dataBaseInicio,dataBaseFim")] Pagamento pagamento)
        {
            if (pagamento.valor > TipoPlano.Selecionar(Aluno.Selecionar(pagamento.idAluno).tipoPlano).valor)
            {
                ModelState.AddModelError("valor", "O valor pago não pode ser maior que o valor do plano atual");
            }

            if (ModelState.IsValid)
            {

                if (TipoPlano.Selecionar(Aluno.Selecionar(pagamento.idAluno).tipoPlano).descricao.Equals("MENSAL"))
                {
                    pagamento.dataBaseFim = pagamento.dataBaseInicio.AddMonths(1);
                    pagamento.dataProxima = pagamento.dataBaseInicio.AddMonths(1);
                }
                else
                {
                    pagamento.dataBaseFim = pagamento.dataBaseInicio.AddYears(1);
                    pagamento.dataProxima = pagamento.dataBaseInicio.AddYears(1);
                }

                pagamento.dataProxima = pagamento.dataProxima.AddDays(1);

                Pagamento.Alterar(pagamento);
                return RedirectToAction("Index", new { idAluno = pagamento.idAluno });
            }
            return View(pagamento);
        }

        // GET: /Pagamento/Delete/5
        public ActionResult Delete(int? id)
        {
            ViewData["Erro"] = "";

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pagamento pagamento = Pagamento.Selecionar(id.Value);
            if (pagamento == null)
            {
                return HttpNotFound();
            }

            if (Ferias.SelecionarTodos(pagamento.idAluno, pagamento.dataBaseFim).Count > 0)
            {
                ViewData["Erro"] = "Não é possivel excluir este pagamento pois existem férias lançadas para este contrato";
            }

            return View(pagamento);
        }

        // POST: /Pagamento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            int idAluno;
            Pagamento pagamento = Pagamento.Selecionar(id);
            idAluno = pagamento.idAluno;

            Pagamento.Excluir(pagamento);
            return RedirectToAction("Index", new { idAluno = idAluno });
        }

        
    }
}
