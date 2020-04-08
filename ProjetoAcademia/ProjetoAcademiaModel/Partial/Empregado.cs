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
    public class MD_Empregado
    {
        [DisplayName("Código")]
        public object id { get; set; }

        [DisplayName("Identidade")]
        [Required(ErrorMessage = "Informe a identidade do empregado", AllowEmptyStrings = false)]
        [StringLength(20, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(Properties.Resources))]
        public object identidade { get; set; }

        [DisplayName("CPF")]
        [Required(ErrorMessage = "Informe o cpf do empregado", AllowEmptyStrings = false)]
        [StringLength(20, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(Properties.Resources))]
        public object cpf { get; set; }

        [DisplayName("Nome")]
        [Required(ErrorMessage = "Informe o nome do empregado", AllowEmptyStrings = false)]
        [StringLength(200, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(Properties.Resources))]
        public object nome { get; set; }

        [DisplayName("Aula Grupo")]
        [Required(ErrorMessage = "Informe se é professor de aula em grupo", AllowEmptyStrings = false)]
        public object aulaGrupo { get; set; }

        [DisplayName("Aula Musculação")]
        [Required(ErrorMessage = "Informe se é professor de musculação", AllowEmptyStrings = false)]
        public object aulaMusculacao { get; set; }

        [DisplayName("Senha")]
        [Required(ErrorMessage = "Informe a senha do empregado", AllowEmptyStrings = false)]
        [StringLength(20, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(Properties.Resources))]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public object senha { get; set; }

        [DisplayName("Perfil")]
        [Required(ErrorMessage = "Informe o perfil do empregado", AllowEmptyStrings = false)]
        public object idPerfil { get; set; }

    }

    [MetadataType(typeof(MD_Empregado))]
    public partial class Empregado
    {

        public static void Incluir(Empregado oEmpregado)
        {
            using (PROJETOACADEMIAEntities db = new PROJETOACADEMIAEntities())
            {
                db.Empregado.Add(oEmpregado);
                db.SaveChanges();
            }
        }

        public static void Alterar(Empregado oEmpregado)
        {
            using (PROJETOACADEMIAEntities db = new PROJETOACADEMIAEntities())
            {
                db.Entry(oEmpregado).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public static void Excluir(Empregado oEmpregado)
        {
            using (PROJETOACADEMIAEntities db = new PROJETOACADEMIAEntities())
            {
                db.Entry(oEmpregado).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
        }
        public static void Excluir(int id)
        {
            using (PROJETOACADEMIAEntities db = new PROJETOACADEMIAEntities())
            {
                Empregado oExcluir = new Empregado();
                oExcluir.id = id;
                db.Entry(oExcluir).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
        }

        public static List<Empregado> SelecionarTodos()
        {
            using (PROJETOACADEMIAEntities db = new PROJETOACADEMIAEntities())
            {
                var res = (from p in db.Empregado select p).ToList();
                db.Dispose();
                return res;
            }
        }

        public static Empregado Selecionar(int id)
        {
            using (PROJETOACADEMIAEntities db = new PROJETOACADEMIAEntities())
            {
                var res = (from p in db.Empregado where p.id == id select p).ToList();
                var oRetorno = res.FirstOrDefault();
                return oRetorno;
            }
        }


    }
}
