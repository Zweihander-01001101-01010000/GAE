using System;
using System.Configuration;
using System.Data.SqlClient;
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


            protected void submit_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("submit_Click event triggered.");
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
                    string formatoArchivo = Path.GetExtension(archivo.FileName).Replace(".", ""); // Cambiado para obtener la extensión del archivo subido
                    byte[] contenidoArchivo = new byte[archivo.ContentLength];
                    archivo.InputStream.Read(contenidoArchivo, 0, archivo.ContentLength);

                    System.Diagnostics.Debug.WriteLine("File read successfully. Nombre: " + nombreArchivo + ", Formato: " + formatoArchivo);

                    string descripcion1 = descripcion.Text;
                    string CarpetaSeleccionada = idCarpeta.SelectedItem.Text;

                    // Llamada al método para guardar en la base de datos
                    GuardarArchivo(nombreArchivo, contenidoArchivo, formatoArchivo, descripcion1, CarpetaSeleccionada);

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

        private void GuardarArchivo(string nombreArchivo, byte[] contenidoArchivo, string formatoArchivo, string descripcion1, string CarpetaSeleccionada)
        {
            string connectionString = "server=localhost;user id=root;password=Josue*10;database=gestordearchivos;port=3306;Connection Timeout=30;charset=utf8;";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO archivos (nombre_archivo, formato_archivo, descripcion, archivo, carpeta) VALUES (@nombre_archivo, @formato_archivo, @descripcion, @archivo, @carpeta)";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@nombre_archivo", nombreArchivo);
                    command.Parameters.AddWithValue("@formato_archivo", formatoArchivo);
                    command.Parameters.AddWithValue("@descripcion", descripcion1);
                    command.Parameters.AddWithValue("@archivo", contenidoArchivo);
                    command.Parameters.AddWithValue("@carpeta", CarpetaSeleccionada);

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
    }
}
