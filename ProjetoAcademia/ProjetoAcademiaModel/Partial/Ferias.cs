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
    public class MD_Ferias
    {
        [DisplayName("Código")]
        public object id { get; set; }

        [DisplayName("Aluno")]
        [Required(ErrorMessage = "Informe o aluno", AllowEmptyStrings = false)]
        public object idAluno { get; set; }

        [DisplayName("Inicio das Férias")]
        [Required(ErrorMessage = "Informe a data inicio das férias", AllowEmptyStrings = false)]
        public object dataInicio { get; set; }

        [DisplayName("Fim das Férias")]
        [Required(ErrorMessage = "Informe a data fim das férias", AllowEmptyStrings = false)]
        public object dataFim { get; set; }

        [DisplayName("Data Fim Pagamento")]
        public object dataBaseFimPagamento { get; set; }
        
    }

    [MetadataType(typeof(MD_Ferias))]
    public partial class Ferias
    {

        public static void Incluir(Ferias oFerias)
        {
            using (PROJETOACADEMIAEntities db = new PROJETOACADEMIAEntities())
            {
                db.Ferias.Add(oFerias);
                db.SaveChanges();
            }
        }

        public static void Alterar(Ferias oFerias)
        {
            using (PROJETOACADEMIAEntities db = new PROJETOACADEMIAEntities())
            {
                db.Entry(oFerias).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public static void Excluir(Ferias oFerias)
        {
            using (PROJETOACADEMIAEntities db = new PROJETOACADEMIAEntities())
            {
                db.Entry(oFerias).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
        }
        public static void Excluir(int id)
        {
            using (PROJETOACADEMIAEntities db = new PROJETOACADEMIAEntities())
            {
                Ferias oExcluir = new Ferias();
                oExcluir.id = id;
                db.Entry(oExcluir).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
        }

        public static List<Ferias> SelecionarTodos()
        {
            using (PROJETOACADEMIAEntities db = new PROJETOACADEMIAEntities())
            {
                var res = (from p in db.Ferias select p).ToList();
                db.Dispose();
                return res;
            }
        }

        public static List<Ferias> SelecionarTodos(int idAluno)
        {
            using (PROJETOACADEMIAEntities db = new PROJETOACADEMIAEntities())
            {
                var res = (from p in db.Ferias where p.idAluno == idAluno select p).ToList();
                db.Dispose();
                return res;
            }
        }

        public static List<Ferias> SelecionarTodos(int idAluno, DateTime databaseFim)
        {
            using (PROJETOACADEMIAEntities db = new PROJETOACADEMIAEntities())
            {
                var res = (from p in db.Ferias where p.idAluno == idAluno && p.dataBaseFimPagamento==databaseFim select p).ToList();
                db.Dispose();
                return res;
            }
        }

        public static List<Ferias> SelecionarPeriodoIgual(int idAluno, DateTime data)
        {
            using (PROJETOACADEMIAEntities db = new PROJETOACADEMIAEntities())
            {
                var res = (from p in db.Ferias where p.idAluno == idAluno && p.dataInicio <= data && p.dataFim >= data select p).ToList();
                db.Dispose();
                return res;
            }
        }

        public static Ferias Selecionar(int id)
        {
            using (PROJETOACADEMIAEntities db = new PROJETOACADEMIAEntities())
            {
                var res = (from p in db.Ferias where p.id == id select p).ToList();
                var oRetorno = res.FirstOrDefault();
                return oRetorno;
            }
        }


    }
}
