using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace GAE.Vistas
{
    public partial class agregarRoles : System.Web.UI.Page
    {
        private string connectionString = "server=localhost;user id=root;password=Josue*10;database=gestordearchivos;port=3306;Connection Timeout=30;charset=utf8;";

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void ingresar_Click(object sender, EventArgs e)
        {
            string role = Role.Text.Trim();

            if (!string.IsNullOrEmpty(role))
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();

                        string query = "INSERT INTO Role (Role) VALUES (@Role)";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Role", role);
                            cmd.ExecuteNonQuery();
                        }

                        // Limpiar el campo de texto o mostrar un mensaje de éxito
                        Role.Text = "";
                        Response.Write("<script>alert('Rol agregado exitosamente');</script>");
                    }
                    catch (Exception ex)
                    {
                        // Manejar excepciones
                        Response.Write("<script>alert('Error: " + ex.Message + "');</script>");
                    }
                }
            }
            else
            {
                // Manejar la entrada vacía
                Response.Write("<script>alert('Por favor, ingrese un nombre para el rol');</script>");
            }
        }

        protected void volverMenu_Click(object sender, EventArgs e)
        {
            Response.Redirect("MenuJefatura.aspx");
        }
    }
}