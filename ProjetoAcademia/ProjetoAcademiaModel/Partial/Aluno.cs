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
    public class MD_Aluno
    {
        [DisplayName("Código")]
        public object id { get; set; }

        [DisplayName("Identidade")]
        [Required(ErrorMessage = "Informe a identidade do aluno", AllowEmptyStrings = false)]
        [Range(0, float.MaxValue, ErrorMessage = "Digite apenas números")]
        [StringLength(9, MinimumLength = 9, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(Properties.Resources))]
        public object identidade { get; set; }

        [DisplayName("CPF")]
        [Required(ErrorMessage = "Informe o cpf do aluno", AllowEmptyStrings = false)]
        [Range(0, float.MaxValue, ErrorMessage = "Digite apenas números")]
        [StringLength(11, MinimumLength = 11, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(Properties.Resources))]
        public object cpf { get; set; }

        [DisplayName("Nome")]
        [Required(ErrorMessage = "Informe o nome do aluno", AllowEmptyStrings = false)]
        [StringLength(200, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(Properties.Resources))]
        public object nome { get; set; }
        
        [DisplayName("Endereço")]
        [Required(ErrorMessage = "Informe o endereço do aluno", AllowEmptyStrings = false)]
        [StringLength(1000, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(Properties.Resources))]
        public object endereco { get; set; }

        [DisplayName("Plano")]
        [Required(ErrorMessage = "Informe o plano do aluno", AllowEmptyStrings = false)]
        public object tipoPlano { get; set; }
       

        
    }

    [MetadataType(typeof(MD_Aluno))]
    public partial class Aluno
    {

        public static void Incluir(Aluno oAluno)
        {
            using (PROJETOACADEMIAEntities db = new PROJETOACADEMIAEntities())
            {
                db.Aluno.Add(oAluno);
                db.SaveChanges();
            }
        }

        public static void Alterar(Aluno oAluno)
        {
            using (PROJETOACADEMIAEntities db = new PROJETOACADEMIAEntities())
            {
                db.Entry(oAluno).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public static void Excluir(Aluno oAluno)
        {
            using (PROJETOACADEMIAEntities db = new PROJETOACADEMIAEntities())
            {
                db.Entry(oAluno).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
        }
        public static void Excluir(int id)
        {
            using (PROJETOACADEMIAEntities db = new PROJETOACADEMIAEntities())
            {
                Aluno oExcluir = new Aluno();
                oExcluir.id = id;
                db.Entry(oExcluir).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
        }

        public static List<Aluno> SelecionarTodos()
        {
            using (PROJETOACADEMIAEntities db = new PROJETOACADEMIAEntities())
            {
                var res = (from p in db.Aluno select p).ToList();
                db.Dispose();
                return res;
            }
        }

        public static Aluno Selecionar(int id)
        {
            using (PROJETOACADEMIAEntities db = new PROJETOACADEMIAEntities())
            {
                var res = (from p in db.Aluno where p.id == id select p).ToList();
                var oRetorno = res.FirstOrDefault();
                return oRetorno;
            }
        }

       
    }
}
