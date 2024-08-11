using System;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.UI;
using MySql.Data.MySqlClient;

namespace GAE.Vistas
{
    public partial class SubirArchivo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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
                    idCarpeta.DataSource = reader;
                    idCarpeta.DataTextField = "carpeta";
                    idCarpeta.DataValueField = "carpeta";
                    idCarpeta.DataBind();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        protected void btnSubir_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("btnSubir_Click event triggered.");
            if (fileUpload.HasFile)
            {
                try
                {
                    HttpPostedFile archivo = fileUpload.PostedFile;
                    string nombreArchivo = idNombreArchivo.Text;
                    string formatoArchivo = Path.GetExtension(archivo.FileName).Replace(".", ""); // Ajuste aquí
                    byte[] contenidoArchivo = new byte[archivo.ContentLength];
                    archivo.InputStream.Read(contenidoArchivo, 0, archivo.ContentLength);

                    System.Diagnostics.Debug.WriteLine("File read successfully. Nombre: " + nombreArchivo + ", Formato: " + formatoArchivo);

                    string descripcion1 = descripcion.Text;
                    string CarpetaSeleccionada = idCarpeta.SelectedItem.Text;
                    long pesoArchivo = archivo.ContentLength; // Obtener el peso del archivo
                    DateTime fechaSubida = DateTime.Now; // Fecha de subida actual

                    // Llamada al método para guardar en la base de datos
                    GuardarArchivo(nombreArchivo, contenidoArchivo, formatoArchivo, descripcion1, CarpetaSeleccionada, pesoArchivo, fechaSubida);

                    lblMensaje.Text = "Archivo subido con éxito.";
                    lblMensaje.Visible = true;
                    System.Diagnostics.Debug.WriteLine("File uploaded successfully.");

                    // Limpiar los campos del formulario
                    descripcion.Text = "";
                    idNombreArchivo.Text = "";
                }
                catch (Exception ex)
                {
                    lblMensaje.Text = "Error al subir el archivo: " + ex.Message;
                    lblMensaje.Visible = true;
                    System.Diagnostics.Debug.WriteLine("Error uploading file: " + ex.Message);
                }
            }
            else
            {
                lblMensaje.Text = "Por favor, seleccione un archivo para subir.";
                lblMensaje.Visible = true;
                System.Diagnostics.Debug.WriteLine("No file selected for upload.");
            }
        }

        private void GuardarArchivo(string nombreArchivo, byte[] contenidoArchivo, string formatoArchivo, string descripcion1, string CarpetaSeleccionada, long pesoArchivo, DateTime fechaSubida)
        {
            string connectionString = "server=localhost;user id=root;password=Josue*10;database=gestordearchivos;port=3306;Connection Timeout=30;charset=utf8;";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO archivos (nombre_archivo, formato_archivo, descripcion, archivo, carpeta, peso_archivo, fecha_subida) VALUES (@nombre_archivo, @formato_archivo, @descripcion, @archivo, @carpeta, @peso_archivo, @fecha_subida)";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@nombre_archivo", nombreArchivo);
                    command.Parameters.AddWithValue("@formato_archivo", formatoArchivo);
                    command.Parameters.AddWithValue("@descripcion", descripcion1);
                    command.Parameters.AddWithValue("@archivo", contenidoArchivo);
                    command.Parameters.AddWithValue("@carpeta", CarpetaSeleccionada);
                    command.Parameters.AddWithValue("@peso_archivo", pesoArchivo);
                    command.Parameters.AddWithValue("@fecha_subida", fechaSubida);

                    command.ExecuteNonQuery();

                    System.Diagnostics.Debug.WriteLine("File saved to database successfully.");
                }
            }

            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error saving file to database: " + ex.Message);
                throw;
            }
        }
        protected void btnCrearCarpeta_Click(object sender, EventArgs e)
        {
            string nuevaCarpeta = txtNombreCarpeta.Text.Trim();

            if (!string.IsNullOrEmpty(nuevaCarpeta))
            {
                string connectionString = "server=localhost;user id=root;password=Josue*10;database=gestordearchivos;port=3306;Connection Timeout=30;charset=utf8;";
                try
                {
                    using (MySqlConnection con = new MySqlConnection(connectionString))
                    {
                        con.Open();
                        MySqlCommand cmd = new MySqlCommand("INSERT INTO carpetas (carpeta) VALUES (@carpeta)", con);
                        cmd.Parameters.AddWithValue("@carpeta", nuevaCarpeta);
                        cmd.ExecuteNonQuery();

                        // Actualizar el DropDownList
                        CargarCarpetas();

                        lblMensaje.Text = "Carpeta creada correctamente.";
                        lblMensaje.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    lblMensaje.Text = "Error al crear la carpeta: " + ex.Message;
                    lblMensaje.Visible = true;
                    System.Diagnostics.Debug.WriteLine("Error creating folder: " + ex.Message);
                }
            }
            else
            {
                lblMensaje.Text = "Ingrese un nombre para la carpeta.";
                lblMensaje.Visible = true;
            }
        }

        protected void btnEliminarCarpeta_Click(object sender, EventArgs e)
        {
            string carpetaEliminar = txtCarpetaEliminar.Text.Trim();

            if (!string.IsNullOrEmpty(carpetaEliminar))
            {
                string connectionString = "server=localhost;user id=root;password=Josue*10;database=gestordearchivos;port=3306;Connection Timeout=30;charset=utf8;";
                try
                {
                    using (MySqlConnection con = new MySqlConnection(connectionString))
                    {
                        con.Open();

                        // Primero, eliminamos los archivos en la carpeta si es necesario
                        MySqlCommand cmd = new MySqlCommand("DELETE FROM archivos WHERE carpeta = @carpeta", con);
                        cmd.Parameters.AddWithValue("@carpeta", carpetaEliminar);
                        cmd.ExecuteNonQuery();

                        // Luego, eliminamos la carpeta
                        cmd = new MySqlCommand("DELETE FROM carpetas WHERE carpeta = @carpeta", con);
                        cmd.Parameters.AddWithValue("@carpeta", carpetaEliminar);
                        cmd.ExecuteNonQuery();

                        // Actualizar el DropDownList
                        CargarCarpetas();

                        lblMensaje.Text = "Carpeta eliminada correctamente.";
                        lblMensaje.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    lblMensaje.Text = "Error al eliminar la carpeta: " + ex.Message;
                    lblMensaje.Visible = true;
                    System.Diagnostics.Debug.WriteLine("Error deleting folder: " + ex.Message);
                }
            }
            else
            {
                lblMensaje.Text = "Ingrese el nombre de la carpeta a eliminar.";
                lblMensaje.Visible = true;
            }
        }
    }
}
