using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GAE.Vistas
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void submit_Click(object sender, EventArgs e)
        {
            string cedulaUsuario = cedula.Text;
            string contrasenaUsuario = contrasena.Text;

            if (cedulaUsuario == "1" && contrasenaUsuario == "1")
            {
                Response.Redirect("../Vistas/DobleFactor.aspx");
            }
            else
            {
                lblMensaje.Text = "Cedula o contraseña incorrectos.";
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Visible = true;
            }
        }
    }
}