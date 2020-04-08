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
    public class FeriasController : Controller
    {


        // GET: /Ferias/
        public ActionResult Index(int idAluno)
        {
            ViewData["idAluno"] = idAluno;
            ViewData["Erro"] = "";

            if (TipoPlano.Selecionar(Aluno.Selecionar(idAluno).tipoPlano).descricao.Equals("MENSAL"))
            {
                ViewData["Erro"] = "O plano do aluno " + Aluno.Selecionar(idAluno).nome + " não possui direito a férias";
            }

            if (Pagamento.SelecionarTodos(idAluno).Count > 0)
            {
                Pagamento pg = Pagamento.SelecionarTodos(idAluno).OrderByDescending(x => x.dataProxima).ToList()[0];

                if (DateTime.Now >= pg.dataProxima)
                {
                    ViewData["Erro"] = "O aluno está inadimplente e não é possivel gerar novas férias";
                }
                else
                {
                    int qtdeFeriasExistentes = pg.dataProxima.AddDays(-1).Subtract(pg.dataBaseFim).Days;

                    if (qtdeFeriasExistentes >= 30)
                    {
                        ViewData["Erro"] = "O aluno já excedeu a quantidade permitida de dias de férias para o plano vigente";
                    }
                    else
                    {
                        if (Ferias.SelecionarTodos(idAluno, pg.dataBaseFim).Count >= 3)
                        {
                            ViewData["Erro"] = "O aluno já excedeu a quantidade permitida de períodos de férias";
                        }
                    }
                }

            }
            else
            {
                ViewData["Erro"] = "O aluno não possui plano vigente para solicitar férias";
            }



            return View(Ferias.SelecionarTodos(idAluno).OrderByDescending(x => x.dataInicio).ToList());
        }

        // GET: /Ferias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Ferias ferias = Ferias.Selecionar(id.Value);
            if (ferias == null)
            {
                return HttpNotFound();
            }
            return View(ferias);
        }

        // GET: /Ferias/Create
        public ActionResult Create(int idAluno)
        {
            if (TipoPlano.Selecionar(Aluno.Selecionar(idAluno).tipoPlano).descricao.Equals("MENSAL"))
            {
                return RedirectToAction("Index", "Ferias", new { idAluno = idAluno });
            }

            if (Pagamento.SelecionarTodos(idAluno).Count > 0)
            {
                Pagamento pg = Pagamento.SelecionarTodos(idAluno).OrderByDescending(x => x.dataProxima).ToList()[0];

                if (DateTime.Now >= pg.dataProxima)
                {
                    return RedirectToAction("Index", "Ferias", new { idAluno = idAluno });
                }
                else
                {
                    int qtdeFeriasExistentes = pg.dataProxima.AddDays(-1).Subtract(pg.dataBaseFim).Days;

                    if (qtdeFeriasExistentes >= 30)
                    {
                        return RedirectToAction("Index", "Ferias", new { idAluno = idAluno });
                    }
                    else
                    {
                        if (Ferias.SelecionarTodos(idAluno, pg.dataBaseFim).Count >= 3)
                        {
                            return RedirectToAction("Index", "Ferias", new { idAluno = idAluno });
                        }
                    }
                }
            }
            else
            {
                return RedirectToAction("Index", "Ferias", new { idAluno = idAluno });
            }

            Ferias ferias = new Ferias();

            ferias.idAluno = idAluno;
            ferias.dataInicio = DateTime.Now;
            ferias.dataFim = DateTime.Now;

            return View(ferias);
        }

        // POST: /Ferias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,idAluno,dataInicio,dataFim")] Ferias ferias)
        {

            if (ModelState.IsValid)
            {
                if (ferias.dataFim < ferias.dataInicio)
                {
                    ModelState.AddModelError("dataFim", "A data final não pode ser antes da data inicio das férias");
                    return View(ferias);
                }

                Pagamento pg = Pagamento.SelecionarTodos(ferias.idAluno).OrderByDescending(x => x.dataBaseFim).ToList()[0];

                if (ferias.dataInicio < pg.dataBaseInicio)
                {
                    ModelState.AddModelError("dataInicio", "A data inicial não pode ser antes da data do contrato atual");
                    return View(ferias);
                }

                if (ferias.dataFim > pg.dataBaseFim)
                {
                    ModelState.AddModelError("dataFim", "A data final não pode ser após a data do contrato atual");
                    return View(ferias);
                }


                if (Ferias.SelecionarPeriodoIgual(ferias.idAluno,ferias.dataInicio).Count>0)
                {
                    ModelState.AddModelError("dataInicio", "A data inicial não pode ser entre um período de férias já cadastrado");
                    return View(ferias);
                }

                if (Ferias.SelecionarPeriodoIgual(ferias.idAluno, ferias.dataFim).Count > 0)
                {
                    ModelState.AddModelError("dataFim", "A data final não pode ser entre um período de férias já cadastrado");
                    return View(ferias);
                }

                ferias.dataBaseFimPagamento = pg.dataBaseFim;

                int qtdeFeriasExistentes = pg.dataProxima.AddDays(-1).Subtract(pg.dataBaseFim).Days;

                int qtdeSolicitada = ferias.dataFim.Subtract(ferias.dataInicio).Days;

                qtdeFeriasExistentes = qtdeFeriasExistentes + qtdeSolicitada;

                if (qtdeFeriasExistentes > 30)
                {
                    ModelState.AddModelError("dataFim", "Não é possível adicionar os " + qtdeSolicitada + " dias de férias");
                    return View(ferias);
                }

                


                pg.dataProxima = pg.dataProxima.AddDays(qtdeSolicitada);

                Pagamento.Alterar(pg);

                Ferias.Incluir(ferias);
                return RedirectToAction("Index", new { idAluno = ferias.idAluno });
            }

            return View(ferias);
        }

        // GET: /Ferias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ferias ferias = Ferias.Selecionar(id.Value);
            if (ferias == null)
            {
                return HttpNotFound();
            }
            return View(ferias);
        }

        // POST: /Ferias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,idAluno,dataInicio,dataFim")] Ferias ferias)
        {
            if (ModelState.IsValid)
            {
                Ferias.Alterar(ferias);
                return RedirectToAction("Index");
            }
            return View(ferias);
        }

        // GET: /Ferias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ferias ferias = Ferias.Selecionar(id.Value);
            if (ferias == null)
            {
                return HttpNotFound();
            }
            return View(ferias);
        }

        // POST: /Ferias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ferias ferias = Ferias.Selecionar(id);
            int idAluno = ferias.idAluno;
            int qtdeSolicitada = ferias.dataInicio.Subtract(ferias.dataFim).Days; //negativo

            Pagamento pg = Pagamento.SelecionarTodos(ferias.idAluno).OrderByDescending(x => x.dataBaseFim).ToList()[0];
           
            pg.dataProxima = pg.dataProxima.AddDays(qtdeSolicitada);

            Pagamento.Alterar(pg);



            Ferias.Excluir(ferias);
            return RedirectToAction("Index", new { idAluno = idAluno });
        }

    }
}
