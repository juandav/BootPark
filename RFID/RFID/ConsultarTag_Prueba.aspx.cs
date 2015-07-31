using RFID.CLASE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace RFID
{
    public partial class ConsultarTag_Prueba : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string valor;
                string antena;
                // Cada vez que se vaya a consultar debes abrir la conexion y cerrarla al finalizar para evitar 
                // errores de conexion con otros formularios. 
                RFIDCL r = new RFIDCL("192.168.1.250", "27011", "RFID");
                r.consultartag();
                valor = r.Tag;
                antena = r.Antena;
                r.CloseConnection();



            }
        }
    }
}