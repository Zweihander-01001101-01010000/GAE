using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GAE.Vistas
{
    public partial class SubirRespaldo : System.Web.UI.Page
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
            string query = "SELECT nombre_Archivo, archivo, formato_archivo, descripcion FROM archivos";

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
                        Response.ContentType = GetContentType(formatoArchivo);
                        Response.AddHeader("Content-Disposition", $"attachment; filename={nombreArchivo}.{formatoArchivo}");
                        Response.BinaryWrite(archivoBytes);
                        Response.End();
                    }
                }
            }
        }

        private string GetContentType(string extension)
        {
            switch (extension.ToLower())
            {
                case "pdf": return "application/pdf";
                case "doc": return "application/msword";
                case "docx": return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                case "txt": return "text/plain";
                default: return "application/octet-stream";
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string CarpetaSeleccionada = ddlDepartamento.SelectedItem.Text;

            string connectionString = "server=localhost;user id=root;password=Josue*10;database=gestordearchivos;port=3306;Connection Timeout=30;charset=utf8;";
            string query = "SELECT nombre_Archivo, archivo, formato_archivo, descripcion FROM archivos WHERE carpeta = @CarpetaSeleccionada";

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

        protected void btnRespaldar_Click(object sender, EventArgs e)
        {
            string nombreRespaldo = TxtNombreRespaldo.Text;
            if (string.IsNullOrEmpty(nombreRespaldo))
            {
                // Manejar el caso cuando no se ingresa un nombre de respaldo
                return;
            }

            string connectionString = "server=localhost;user id=root;password=Josue*10;database=gestordearchivos;port=3306;Connection Timeout=30;charset=utf8;";
            foreach (GridViewRow row in gvArchivos.Rows)
            {
                CheckBox chkSeleccionar = (CheckBox)row.FindControl("chkSeleccionar");
                if (chkSeleccionar != null && chkSeleccionar.Checked)
                {
                    string nombreArchivo = row.Cells[1].Text;
                    string querySelect = "SELECT nombre_Archivo, archivo, formato_archivo, descripcion, carpeta FROM archivos WHERE nombre_Archivo = @nombreArchivo";
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();
                        MySqlCommand commandSelect = new MySqlCommand(querySelect, connection);
                        commandSelect.Parameters.AddWithValue("@nombreArchivo", nombreArchivo);

                        using (MySqlDataReader reader = commandSelect.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string archivoNombre = reader["nombre_Archivo"].ToString();
                                byte[] archivoBytes = (byte[])reader["archivo"];
                                string archivoFormato = reader["formato_archivo"].ToString();
                                string archivoDescripcion = reader["descripcion"].ToString();
                                string carpeta = reader["carpeta"].ToString();

                                reader.Close();  // Cerrar el DataReader antes de la siguiente operación

                                string queryInsert = "INSERT INTO respaldoArchivos (nombre_archivo, archivo, formato_archivo, descripcion, carpeta, NombreRespaldo) VALUES (@nombre_archivo, @archivo, @formato_archivo, @descripcion, @carpeta, @NombreRespaldo)";
                                using (MySqlCommand commandInsert = new MySqlCommand(queryInsert, connection))
                                {
                                    commandInsert.Parameters.AddWithValue("@nombre_archivo", archivoNombre);
                                    commandInsert.Parameters.AddWithValue("@archivo", archivoBytes);
                                    commandInsert.Parameters.AddWithValue("@formato_archivo", archivoFormato);
                                    commandInsert.Parameters.AddWithValue("@descripcion", archivoDescripcion);
                                    commandInsert.Parameters.AddWithValue("@carpeta", carpeta);
                                    commandInsert.Parameters.AddWithValue("@NombreRespaldo", nombreRespaldo);

                                    commandInsert.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
