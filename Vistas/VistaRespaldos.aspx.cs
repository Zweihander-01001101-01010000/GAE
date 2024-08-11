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
    public partial class VistaRespaldos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarRespaldos();
            }
        }

        private void CargarRespaldos()
        {
            string connectionString = "server=localhost;user id=root;password=Josue*10;database=gestordearchivos;port=3306;Connection Timeout=30;charset=utf8;";
            string query = "SELECT DISTINCT NombreRespaldo FROM respaldoArchivos";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);

                try
                {
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();

                    ddlRespaldos.Items.Clear();
                    while (reader.Read())
                    {
                        ddlRespaldos.Items.Add(reader["NombreRespaldo"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    // Manejar errores
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string nombreRespaldoSeleccionado = ddlRespaldos.SelectedValue;
            CargarArchivos(nombreRespaldoSeleccionado);
        }

        private void CargarArchivos(string nombreRespaldo)
        {
            string connectionString = "server=localhost;user id=root;password=Josue*10;database=gestordearchivos;port=3306;Connection Timeout=30;charset=utf8;";
            string query = "SELECT nombre_archivo, formato_archivo, descripcion, peso_archivo, fecha_subida FROM respaldoArchivos WHERE NombreRespaldo = @NombreRespaldo";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@NombreRespaldo", nombreRespaldo);

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
                    // Manejar errores
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            // Primero, crea una lista para almacenar los nombres de los archivos a eliminar
            List<string> nombresAEliminar = new List<string>();

            // Recorre cada fila del GridView para verificar si el CheckBox está seleccionado
            foreach (GridViewRow row in gvArchivos.Rows)
            {
                CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                if (chkSelect != null && chkSelect.Checked)
                {
                    // Obtén el nombre del archivo desde el GridView
                    string nombreArchivo = row.Cells[1].Text; // Asumiendo que el nombre del archivo está en la segunda columna
                    nombresAEliminar.Add(nombreArchivo);
                }
            }

            // Llama al método para eliminar los archivos seleccionados
            EliminarArchivos(nombresAEliminar);

            // Recarga los archivos después de eliminar
            string nombreRespaldoSeleccionado = ddlRespaldos.SelectedValue;
            CargarArchivos(nombreRespaldoSeleccionado);
        }

        private void EliminarArchivos(List<string> nombresAEliminar)
        {
            string connectionString = "server=localhost;user id=root;password=Josue*10;database=gestordearchivos;port=3306;Connection Timeout=30;charset=utf8;";
            string nombreRespaldoSeleccionado = ddlRespaldos.SelectedValue;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                foreach (string nombreArchivo in nombresAEliminar)
                {
                    string query = "DELETE FROM respaldoarchivos WHERE nombre_archivo = @nombre_archivo AND NombreRespaldo = @NombreRespaldo";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@nombre_archivo", nombreArchivo);
                        command.Parameters.AddWithValue("@NombreRespaldo", nombreRespaldoSeleccionado);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
        protected void DownloadFile(object sender, EventArgs e)
        {
            string nombreArchivo = (sender as LinkButton).CommandArgument;
            string connectionString = "server=localhost;user id=root;password=Josue*10;database=gestordearchivos;port=3306;Connection Timeout=30;charset=utf8;";
            string query = "SELECT archivo, formato_archivo FROM respaldoArchivos WHERE nombre_archivo = @nombreArchivo";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@nombreArchivo", nombreArchivo);

                try
                {
                    connection.Open();
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        byte[] archivoBytes = (byte[])reader["archivo"];
                        string archivoFormato = reader["formato_archivo"].ToString();

                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.ContentType = archivoFormato;
                        Response.AppendHeader("Content-Disposition", "attachment; filename=" + nombreArchivo);
                        Response.BinaryWrite(archivoBytes);
                        Response.Flush();
                        Response.End();
                    }
                }
                catch (Exception ex)
                {
                    // Manejar errores
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
    }
}