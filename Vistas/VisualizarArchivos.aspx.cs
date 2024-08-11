using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GAE.Vistas
{
    public partial class VisualizarArchivos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarArchivos();
                CargarCarpetas();
            }
        }

        private void CargarCarpetas()
        {
            string connectionString = "server=localhost;user id=root;password=Josue*10;database=gestordearchivos;port=3306;Connection Timeout=30;charset=utf8;";
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("SELECT carpeta FROM carpetas", con);
                try
                {
                    con.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    ddlDepartamento.DataSource = reader;
                    ddlDepartamento.DataTextField = "carpeta";
                    ddlDepartamento.DataValueField = "carpeta";
                    ddlDepartamento.DataBind();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        
        private void CargarArchivos()
        {
            string connectionString = "server=localhost;user id=root;password=Josue*10;database=gestordearchivos;port=3306;Connection Timeout=30;charset=utf8;";
            string query = "SELECT nombre_Archivo, archivo, formato_archivo, descripcion, peso_archivo, fecha_subida FROM archivos";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                gvArchivos.DataSource = dataTable;
                gvArchivos.DataBind();
            }
        }

        protected void DownloadFile(object sender, EventArgs e)
        {
            string nombreArchivo = (sender as LinkButton).CommandArgument;
            string connectionString = "server=localhost;user id=root;password=Josue*10;database=gestordearchivos;port=3306;Connection Timeout=30;charset=utf8;";
            string query = "SELECT archivo, formato_archivo FROM archivos WHERE nombre_Archivo = @nombreArchivo";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@nombreArchivo", nombreArchivo);

                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        byte[] archivoBytes = (byte[])reader["archivo"];
                        string formatoArchivo = reader["formato_archivo"].ToString();

                        Response.Clear();
                        Response.ContentType = "application/octet-stream";
                        Response.AddHeader("Content-Disposition", $"attachment; filename={nombreArchivo}.{formatoArchivo}");
                        Response.BinaryWrite(archivoBytes);
                        Response.End();
                    }
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string CarpetaSeleccionada = ddlDepartamento.SelectedItem.Text;

          
            string connectionString = "server=localhost;user id=root;password=Josue*10;database=gestordearchivos;port=3306;Connection Timeout=30;charset=utf8;";

            string query = "SELECT nombre_Archivo, archivo, formato_archivo, descripcion, peso_archivo, fecha_subida  FROM archivos WHERE carpeta = @CarpetaSeleccionada";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
             
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@CarpetaSeleccionada", CarpetaSeleccionada);

                connection.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                gvArchivos.DataSource = dataTable;
                gvArchivos.DataBind();
            }
        }

    }
}