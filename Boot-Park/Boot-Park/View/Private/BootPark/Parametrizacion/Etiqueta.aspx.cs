using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Boot_Park.Controller.BootPark;
using System.Data;
using Ext.Net;
using rfid.io;

namespace Boot_Park.View.Private.BootPark.Parametrizacion
{
    public partial class Etiqueta : System.Web.UI.Page
    {

        private ParametrizacionCOD parametro = new ParametrizacionCOD();
        private string pegeId = "53233";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarEtiquetas();
            }
        }

        [DirectMethod(Namespace = "parametro")]
        public void cargarEtiquetas()
        {
            DataTable datos = parametro.consultarEtiquetas();
            SETIQUETA.DataSource = datos;
            SETIQUETA.DataBind();
        }

        [DirectMethod(Namespace = "parametro")]
        public void crearEtiqueta(string tipo, string etiqueta, string descripcion, string observacion, string estado)
        {
            bool response = parametro.registrarEtiqueta(tipo, etiqueta, descripcion, observacion, estado, pegeId);

            if (response)
            {
                WREGISTRO.Hide();
                X.Msg.Notify("Notificación", "Etiqueta agregado exitosamente!").Show();
            }
            else
            {
                X.Msg.Notify("Notificación", "Ha ocurrido un error!!").Show();
            }

            BindData();
        }

        [DirectMethod(Namespace = "parametro")]
        public void modificarEtiqueta(string id, string tipo, string etiqueta, string descripcion, string observacion, string estado)
        {
            bool response = parametro.actualizarEtiqueta(id, tipo, etiqueta, descripcion, observacion, estado, pegeId);

            if (response)
            {
                X.Msg.Notify("Notificación", "Etiqueta actualizado exitosamente!").Show();
            }
            else
            {
                X.Msg.Notify("Notificación", "Ha ocurrido un error!!").Show();
            }
            GPETIQUETA.Store.Primary.CommitChanges();
        }

        [DirectMethod(Namespace = "parametro")]
        public void eliminarEtiqueta(string id, string tipo)
        {

            bool response = parametro.eliminalEtiqueta(id, tipo);

            if (response)
            {
                X.Msg.Notify("Notificación", "Etiqueta eliminado exitosamente!").Show();
            }
            else
            {
                X.Msg.Notify("Notificación", "Ha ocurrido un error!!").Show();
            }
            BindData();
        }
        [DirectMethod(Namespace = "parametro")]
        public void validarTarjeta(string id, string tipo)
        {
            string estado = parametro.validarEtiqueta(id, tipo).Rows[0]["ETIQUETAEXISTE"].ToString();
            if (estado=="true")
            {
                   X.Msg.Notify("Notificación"," Este " + tipo + " ya esta registrado..").Show();
                   TFETIQ_ETIQUETA.Text = "";
            }
          
        }
        [DirectMethod(Namespace = "parametro")]
        public string detectarTag()
        {
            RFID r = new RFID("192.168.1.250", "27011");
            string response = r.iniciarDeteccion();

            if (response.Equals("YES"))
            {
                string etiqueta = r.Tag;
                TFETIQ_ETIQUETA.SetValue(etiqueta);
                validarTarjeta(etiqueta, CBETIQ_TIPO.Text);
            }

            return response;
        }

        private void BindData()
        {
            GPETIQUETA.Store.Primary.DataSource = parametro.consultarEtiquetas();
            GPETIQUETA.Store.Primary.DataBind();
        }
    }
}