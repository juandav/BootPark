using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using Boot_Park.Controller;
using System.Data;

namespace Boot_Park
{
    public partial class Login : System.Web.UI.Page
    {

        private login account = new login();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {

                FindAccount();
            }
        }

        public void FindAccount() {
            DataTable data = account.FindAccount();
            accountStore.DataSource = data;
            accountStore.DataBind();
        }
        [DirectMethod(Namespace = "parametro")]
        public void direccionarDestop() {
            this.Response.Redirect("View/Private/Desktop.aspx");
        }

    }
}