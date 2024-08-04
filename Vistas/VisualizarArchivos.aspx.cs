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
    }
}