using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GAE.Vistas
{
    public partial class agregarDepartamento : System.Web.UI.Page
    {
        private string connectionString = "server=localhost;user id=root;password=Josue*10;database=gestordearchivos;port=3306;Connection Timeout=30;charset=utf8;";

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void ingresar_Click(object sender, EventArgs e)
        {
            string departamento = txtNombreDepartamento.Text.Trim();

            if (!string.IsNullOrEmpty(departamento))
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();

                        string query = "INSERT INTO Departamento (Departamento) VALUES (@Departamento)";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Departamento", departamento);
                            cmd.ExecuteNonQuery();
                        }

                        // Opcionalmente, limpiar la caja de texto o mostrar un mensaje de éxito
                        txtNombreDepartamento.Text = "";
                        Response.Write("<script>alert('Departamento agregado exitosamente');</script>");
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
                // Manejar entrada vacía
                Response.Write("<script>alert('Por favor, ingrese un nombre para el departamento');</script>");
            }
        }

        protected void volverMenu_Click(object sender, EventArgs e)
        {
            Response.Redirect("MenuJefatura.aspx");
        }
    }
}