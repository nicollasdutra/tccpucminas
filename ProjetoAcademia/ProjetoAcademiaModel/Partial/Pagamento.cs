using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;

namespace ProjetoAcademiaModel
{
    public class MD_Pagamento
    {
        [DisplayName("Código")]
        public object id { get; set; }

        [DisplayName("Aluno")]
        [Required(ErrorMessage = "Informe o aluno", AllowEmptyStrings = false)]
        public object idAluno { get; set; }

        [DisplayName("Valor")]
        [Required(ErrorMessage = "Informe o valor", AllowEmptyStrings = false)]
        public object valor { get; set; }

        [DisplayName("Próximo Pagamento")]
        public object dataProxima { get; set; }

        [DisplayName("Data do Pagamento")]
        [Required(ErrorMessage = "Informe a data do pagamento", AllowEmptyStrings = false)]
        public object dataBaseInicio { get; set; }

        [DisplayName("Data Fim")]
        public object dataBaseFim { get; set; }

    }

    [MetadataType(typeof(MD_Pagamento))]
    public partial class Pagamento
    {

        public static void Incluir(Pagamento oPagamento)
        {
            using (PROJETOACADEMIAEntities db = new PROJETOACADEMIAEntities())
            {
                db.Pagamento.Add(oPagamento);
                db.SaveChanges();
            }
        }

        public static void Alterar(Pagamento oPagamento)
        {
            using (PROJETOACADEMIAEntities db = new PROJETOACADEMIAEntities())
            {
                db.Entry(oPagamento).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public static void Excluir(Pagamento oPagamento)
        {
            using (PROJETOACADEMIAEntities db = new PROJETOACADEMIAEntities())
            {
                db.Entry(oPagamento).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
        }
        public static void Excluir(int id)
        {
            using (PROJETOACADEMIAEntities db = new PROJETOACADEMIAEntities())
            {
                Pagamento oExcluir = new Pagamento();
                oExcluir.id = id;
                db.Entry(oExcluir).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
        }

        public static List<Pagamento> SelecionarTodos()
        {
            using (PROJETOACADEMIAEntities db = new PROJETOACADEMIAEntities())
            {
                var res = (from p in db.Pagamento select p).ToList();
                db.Dispose();
                return res;
            }
        }

        public static List<Pagamento> SelecionarTodos(int idAluno)
        {
            using (PROJETOACADEMIAEntities db = new PROJETOACADEMIAEntities())
            {
                var res = (from p in db.Pagamento where p.idAluno == idAluno select p).ToList();
                db.Dispose();
                return res;
            }
        }

        public static Pagamento Selecionar(int id)
        {
            using (PROJETOACADEMIAEntities db = new PROJETOACADEMIAEntities())
            {
                var res = (from p in db.Pagamento where p.id == id select p).ToList();
                var oRetorno = res.FirstOrDefault();
                return oRetorno;
            }
        }


    }
}
