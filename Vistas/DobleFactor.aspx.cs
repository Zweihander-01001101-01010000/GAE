using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GAE.Vistas
{
    public partial class DobleFactor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void submit_Click(object sender, EventArgs e)
        {
            string doblefactor = Codigo.Text;
            if (doblefactor == "1")
            {
                Response.Redirect("../Vistas/MenuEmpleados.aspx");
            }
            else if (doblefactor == "2"){
                Response.Redirect("../Vistas/MenuJefatura.aspx");
            }
            else
            {
                lblMensaje.Text = "Codigo incorrectos.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Visible = true;
            }
        }
    }
}