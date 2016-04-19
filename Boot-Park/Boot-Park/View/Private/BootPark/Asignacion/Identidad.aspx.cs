using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Boot_Park.Controller.BootPark;
using System.Data;
using Ext.Net;
using Newtonsoft.Json;

namespace Boot_Park.View.Private.BootPark.Asignacion
{
    public partial class Identidad : System.Web.UI.Page
    {

        private ParametrizacionCOD parametro = new ParametrizacionCOD();
        //  private string pegeId = "53233";
        private string pegeId = "1";

        protected void Page_Load(object sender, EventArgs e)
        {
                cargarUsuarios();
        }

        [DirectMethod(Namespace = "parametro")]
        public void cargarUsuarios()
        {
            DataTable datos = parametro.consultarUsuariosHuellas();
            SUSUARIO.DataSource = datos;
            SUSUARIO.DataBind();
        }

        [DirectMethod(Namespace = "parametro")]
        public void cargarEtiquetasOUT() {
            SETIQUETAOUT.DataSource = parametro.consultarCarnetsDisponibles();
            SETIQUETAOUT.DataBind();
        }

        [DirectMethod(Namespace = "parametro")]
        public void cargarEtiquetasIN(string usuario) {
            STIQUETAIN.DataSource = parametro.consultarCarnetsEnUso(usuario);
            STIQUETAIN.DataBind();
        }

        [DirectMethod(Namespace = "parametro")]
        public bool vincularCarnetAlUsuario(string id, string usuario) {
            return parametro.registrarEtiquetaUsuario(id, "CARNET", usuario,"", "now()", pegeId);
        }

        [DirectMethod(Namespace = "parametro")]
        public bool desvincularCarnetAlUsuario(string id, string usuario) {
            return parametro.eliminarEtiquetaUsuario(id,"CARNET",usuario);
        }
        [DirectMethod(Namespace = "parametro")]
        public bool modificarCarnetUsuario(string idCarnet,string usuario,string motivo) {
            return parametro.actualizarEtiquetaUsuario(idCarnet, "Carnet",usuario,motivo,"now()",pegeId);
        }

        
        [DirectMethod(Namespace = "parametro")]
        public void registraHuellausuario(string data,string Index,string length ,string usuario)
        {

            if (parametro.registrarHuella(usuario,Convert.ToInt32(Index), data, 2,Convert.ToInt32(length), pegeId))
                {
                    X.Msg.Notify("Notificación", "Huella guardada exitosamente!").Show();
                    cargarUsuarios();
                }
                else
                {
                    X.Msg.Notify("Notificación", "Ha ocurrido un error!!").Show();
                }

        }

    }
    public class HuellaDactilar
    {
        public string Identidad { get; set; }
        public int FingerIndex { get; set; }
        public int Flag { get; set; }
        public string byTmpData { get; set; }
        public int TmpLength { get; set; }
    }
}