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
                    // Obtén el archivo y su extensión
                    HttpPostedFile archivo = fileUpload.PostedFile;
                    string nombreArchivo = Path.GetFileName(archivo.FileName);
                    string formatoArchivo = Path.GetExtension(nombreArchivo).Replace(".", "");
                    byte[] contenidoArchivo = new byte[archivo.ContentLength];
                    archivo.InputStream.Read(contenidoArchivo, 0, archivo.ContentLength);

                    System.Diagnostics.Debug.WriteLine("File read successfully.");

                    // Obtén otros valores del formulario
                    string descripcion1 = descripcion.Text;
                    descripcion.Text = " ";
                    horaInicio.Text = " ";

                    // Llama al método para guardar en la base de datos
                    GuardarArchivo(nombreArchivo, contenidoArchivo, formatoArchivo, descripcion1);

                    lblMensaje.Text = "Archivo subido con éxito.";
                    lblMensaje.Visible = true;
                    System.Diagnostics.Debug.WriteLine("File uploaded successfully.");
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

        private void GuardarArchivo(string nombreArchivo, byte[] contenidoArchivo, string formatoArchivo, string descripcion1)
        {
            // Define la cadena de conexión aquí
            string connectionString = "server=localhost;user id=root;password=Josue*10;database=gestordearchivos;port=3306;Connection Timeout=30;charset=utf8;";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO archivos (nombre_Archivo, formato_archivo, descripcion, archivo) VALUES (@nombre_archivo, @formato_archivo, @descripcion, @archivo)";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@nombre_archivo", nombreArchivo);
                    command.Parameters.AddWithValue("@formato_archivo", formatoArchivo);
                    command.Parameters.AddWithValue("@descripcion", descripcion1);
                    command.Parameters.AddWithValue("@archivo", contenidoArchivo);

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
