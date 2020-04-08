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
    public class MD_TipoPerfil
    {
        [DisplayName("Código")]
        public object id { get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "Informe a descrição do perfil", AllowEmptyStrings = false)]
        [StringLength(100, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(Properties.Resources))]
        public object descricao { get; set; }

    }

    [MetadataType(typeof(MD_TipoPerfil))]
    public partial class TipoPerfil
    {

        public static void Incluir(TipoPerfil oTipoPerfil)
        {
            using (PROJETOACADEMIAEntities db = new PROJETOACADEMIAEntities())
            {
                db.TipoPerfil.Add(oTipoPerfil);
                db.SaveChanges();
            }
        }

        public static void Alterar(TipoPerfil oTipoPerfil)
        {
            using (PROJETOACADEMIAEntities db = new PROJETOACADEMIAEntities())
            {
                db.Entry(oTipoPerfil).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public static void Excluir(TipoPerfil oTipoPerfil)
        {
            using (PROJETOACADEMIAEntities db = new PROJETOACADEMIAEntities())
            {
                db.Entry(oTipoPerfil).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
        }
        public static void Excluir(int id)
        {
            using (PROJETOACADEMIAEntities db = new PROJETOACADEMIAEntities())
            {
                TipoPerfil oExcluir = new TipoPerfil();
                oExcluir.id = id;
                db.Entry(oExcluir).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
        }

        public static List<TipoPerfil> SelecionarTodos()
        {
            using (PROJETOACADEMIAEntities db = new PROJETOACADEMIAEntities())
            {
                var res = (from p in db.TipoPerfil select p).ToList();
                db.Dispose();
                return res;
            }
        }

        public static TipoPerfil Selecionar(int id)
        {
            using (PROJETOACADEMIAEntities db = new PROJETOACADEMIAEntities())
            {
                var res = (from p in db.TipoPerfil where p.id == id select p).ToList();
                var oRetorno = res.FirstOrDefault();
                return oRetorno;
            }
        }


    }
}
