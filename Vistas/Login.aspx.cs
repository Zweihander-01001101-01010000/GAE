using MySql.Data.MySqlClient;
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
        private string connectionString = "server=localhost;user id=root;password=Josue*10;database=gestordearchivos;port=3306;Connection Timeout=30;charset=utf8;";

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void submit_Click(object sender, EventArgs e)
        {
            string cedula1 = cedula.Text.Trim();
            string contrasena1 = contrasena.Text.Trim();

            if (!string.IsNullOrEmpty(cedula1) && !string.IsNullOrEmpty(contrasena1))
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        string query = "SELECT COUNT(*) FROM Usuario WHERE idUsuario = @Cedula AND Contraseña = @Contrasena";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Cedula", cedula1);
                            cmd.Parameters.AddWithValue("@Contrasena", contrasena1);

                            int count = Convert.ToInt32(cmd.ExecuteScalar());

                            if (count > 0)
                            {
                                // Redirigir al usuario a la página principal después de iniciar sesión
                                Response.Redirect("DobleFactor.aspx");
                            }
                            else
                            {
                                // Mostrar mensaje de error
                                lblMensaje.Text = "Cédula o contraseña incorrectos.";
                                lblMensaje.Visible = true;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Manejar excepciones
                        lblMensaje.Text = "Error: " + ex.Message;
                        lblMensaje.Visible = true;
                    }
                }
            }
            else
            {
                // Manejar entrada vacía
                lblMensaje.Text = "Por favor, ingrese su cédula y contraseña.";
                lblMensaje.Visible = true;
            }
        }
    }
}