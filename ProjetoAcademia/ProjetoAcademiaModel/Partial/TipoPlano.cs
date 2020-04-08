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
    public class MD_TipoPlano
    {
        [DisplayName("Código")]
        public object id { get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "Informe a descrição do plano", AllowEmptyStrings = false)]
        [StringLength(20, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(Properties.Resources))]
        public object descricao { get; set; }

        [DisplayName("Valor")]
        [Required(ErrorMessage = "Informe o valor do plano", AllowEmptyStrings = false)]
        public object valor { get; set; }
                
    }

    [MetadataType(typeof(MD_TipoPlano))]
    public partial class TipoPlano
    {

        public static void Incluir(TipoPlano oTipoPlano)
        {
            using (PROJETOACADEMIAEntities db = new PROJETOACADEMIAEntities())
            {
                db.TipoPlano.Add(oTipoPlano);
                db.SaveChanges();
            }
        }

        public static void Alterar(TipoPlano oTipoPlano)
        {
            using (PROJETOACADEMIAEntities db = new PROJETOACADEMIAEntities())
            {
                db.Entry(oTipoPlano).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public static void Excluir(TipoPlano oTipoPlano)
        {
            using (PROJETOACADEMIAEntities db = new PROJETOACADEMIAEntities())
            {
                db.Entry(oTipoPlano).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
        }
        public static void Excluir(int id)
        {
            using (PROJETOACADEMIAEntities db = new PROJETOACADEMIAEntities())
            {
                TipoPlano oExcluir = new TipoPlano();
                oExcluir.id = id;
                db.Entry(oExcluir).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
        }

        public static List<TipoPlano> SelecionarTodos()
        {
            using (PROJETOACADEMIAEntities db = new PROJETOACADEMIAEntities())
            {
                var res = (from p in db.TipoPlano select p).ToList();
                db.Dispose();
                return res;
            }
        }

        public static TipoPlano Selecionar(int id)
        {
            using (PROJETOACADEMIAEntities db = new PROJETOACADEMIAEntities())
            {
                var res = (from p in db.TipoPlano where p.id == id select p).ToList();
                var oRetorno = res.FirstOrDefault();
                return oRetorno;
            }
        }


    }
}
