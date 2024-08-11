using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GAE.Vistas
{
    public partial class SubirRespaldo : System.Web.UI.Page
    {
        private string connectionString = "server=localhost;user id=root;password=Josue*10;database=gestordearchivos;port=3306;Connection Timeout=30;charset=utf8;";

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
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                MySqlCommand cmd = new MySqlCommand("SELECT DISTINCT carpeta FROM archivos", con);
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
                    Console.WriteLine("Error al cargar carpetas: " + ex.Message);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Error al cargar carpetas.');", true);
                }
            }
        }

        private void CargarArchivos()
        {
            string query = "SELECT nombre_archivo, archivo, formato_archivo, descripcion, peso_archivo, fecha_subida FROM archivos";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                try
                {
                    connection.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    gvArchivos.DataSource = dataTable;
                    gvArchivos.DataBind();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al cargar archivos: " + ex.Message);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Error al cargar archivos.');", true);
                }
            }
        }

        protected void DownloadFile(object sender, EventArgs e)
        {
            string nombreArchivo = (sender as LinkButton).CommandArgument;
            string query = "SELECT archivo, formato_archivo FROM archivos WHERE nombre_archivo = @nombreArchivo";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@nombreArchivo", nombreArchivo);

                try
                {
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
                        else
                        {
                            Console.WriteLine("No se encontró el archivo: " + nombreArchivo);
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Archivo no encontrado.');", true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al descargar archivo: " + ex.Message);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Error al descargar archivo.');", true);
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

            string query = "SELECT nombre_archivo, archivo, formato_archivo, descripcion, peso_archivo, fecha_subida FROM archivos WHERE carpeta = @CarpetaSeleccionada";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@CarpetaSeleccionada", CarpetaSeleccionada);

                try
                {
                    connection.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    gvArchivos.DataSource = dataTable;
                    gvArchivos.DataBind();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al filtrar archivos por carpeta: " + ex.Message);
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Error al filtrar archivos por carpeta.');", true);
                }
            }
        }

        protected void btnRespaldar_Click(object sender, EventArgs e)
        {
            string nombreRespaldo = TxtNombreRespaldo.Text;
            if (string.IsNullOrEmpty(nombreRespaldo))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Por favor, ingrese un nombre para el respaldo.');", true);
                return;
            }

            foreach (GridViewRow row in gvArchivos.Rows)
            {
                CheckBox chkSeleccionar = (CheckBox)row.FindControl("chkSeleccionar");
                if (chkSeleccionar != null && chkSeleccionar.Checked)
                {
                    string nombreArchivo = row.Cells[1].Text;
                    Console.WriteLine("Respaldo de archivo seleccionado: " + nombreArchivo);

                    string querySelect = "SELECT nombre_archivo, archivo, formato_archivo, descripcion, carpeta, peso_archivo, fecha_subida FROM archivos WHERE nombre_archivo = @nombreArchivo";

                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();
                        MySqlCommand commandSelect = new MySqlCommand(querySelect, connection);
                        commandSelect.Parameters.AddWithValue("@nombreArchivo", nombreArchivo);

                        try
                        {
                            using (MySqlDataReader reader = commandSelect.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    string archivoNombre = reader["nombre_archivo"]?.ToString() ?? string.Empty;
                                    byte[] archivoBytes = reader["archivo"] as byte[];
                                    string archivoFormato = reader["formato_archivo"]?.ToString() ?? string.Empty;
                                    string archivoDescripcion = reader["descripcion"]?.ToString() ?? string.Empty;
                                    string carpeta = reader["carpeta"]?.ToString() ?? string.Empty;
                                    long peso_archivo = reader.GetInt64(reader.GetOrdinal("peso_archivo"));
                                    DateTime fecha_subida = reader.GetDateTime(reader.GetOrdinal("fecha_subida"));

                                    if (archivoBytes == null || string.IsNullOrEmpty(archivoNombre))
                                    {
                                        Console.WriteLine("Error: archivo no encontrado o datos incompletos para el respaldo: " + nombreArchivo);
                                        continue;
                                    }

                                    reader.Close();  // Cerrar el DataReader antes de la siguiente operación

                                    string queryInsert = "INSERT INTO respaldoArchivos (nombre_archivo, archivo, formato_archivo, descripcion, carpeta, NombreRespaldo, peso_archivo, fecha_subida) VALUES (@nombre_archivo, @archivo, @formato_archivo, @descripcion, @carpeta, @NombreRespaldo, @peso_archivo, @fecha_subida)";
                                    using (MySqlCommand commandInsert = new MySqlCommand(queryInsert, connection))
                                    {
                                        commandInsert.Parameters.AddWithValue("@nombre_archivo", archivoNombre);
                                        commandInsert.Parameters.AddWithValue("@archivo", archivoBytes);
                                        commandInsert.Parameters.AddWithValue("@formato_archivo", archivoFormato);
                                        commandInsert.Parameters.AddWithValue("@descripcion", archivoDescripcion);
                                        commandInsert.Parameters.AddWithValue("@carpeta", carpeta);
                                        commandInsert.Parameters.AddWithValue("@NombreRespaldo", nombreRespaldo);
                                        commandInsert.Parameters.AddWithValue("@peso_archivo", peso_archivo);
                                        commandInsert.Parameters.AddWithValue("@fecha_subida", fecha_subida);

                                        commandInsert.ExecuteNonQuery();
                                        Console.WriteLine("Archivo respaldado exitosamente: " + archivoNombre);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("No se encontró el archivo para el respaldo: " + nombreArchivo);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error al realizar el respaldo del archivo: " + nombreArchivo + ". " + ex.Message);
                            ScriptManager.RegisterStartupScript(this, GetType(), "alertMessage", "alert('Error al respaldar archivo: " + nombreArchivo + "');", true);
                        }
                    }
                }
            }
        }
    }
}