using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;

namespace Boot_Park.View.Private
{
    public partial class Desktop : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                X.Call("accountPege");
            }
        }

        [DirectMethod(Namespace = "login")]
        public void createAccountSession(string PEGE_ID)
        {

            Session["accountSessionId"] = PEGE_ID;
        }

        protected void Logout_Click(object sender, DirectEventArgs e)
        {
            // Logout from Authenticated Session
            this.Response.Redirect("../../Login.aspx");
        }

    }
}