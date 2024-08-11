using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Web;
using System.Net.Mail;
using System.Net;
using System.Data.SqlClient;

namespace GAE.Vistas
{
    public partial class agregarUsuario : System.Web.UI.Page
    {
        private string connectionString = "server=localhost;user id=root;password=Josue*10;database=gestordearchivos;port=3306;Connection Timeout=30;charset=utf8;";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDepartamentos();
                CargarRoles();
            }
        }

        protected void submit_Click(object sender, EventArgs e)
        {
            string nombre = nombreUsuario.Text.Trim();
            string apellidos = apellidosUsuario.Text.Trim();
            string cedula = cedulaUsuario.Text.Trim();
            string correo = correoUsuario.Text.Trim();
            string password = passwordUsuario.Text.Trim();
            string departamento1 = departamento.SelectedValue;
            string rol = rolUsuario.SelectedValue;

            if (!string.IsNullOrEmpty(nombre) && !string.IsNullOrEmpty(apellidos) && !string.IsNullOrEmpty(cedula) &&
                !string.IsNullOrEmpty(correo) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(departamento1) &&
                !string.IsNullOrEmpty(rol))
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();

                        string query = "INSERT INTO Usuario (idUsuario, Nombre, Apellidos, Correo, Contraseña, FK_idRole, FK_idDepartamento) VALUES (@Cedula, @Nombre, @Apellidos, @Correo, @Contraseña, @RolID, @DepartamentoID)";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Cedula", cedula);
                            cmd.Parameters.AddWithValue("@Nombre", nombre);
                            cmd.Parameters.AddWithValue("@Apellidos", apellidos);
                            cmd.Parameters.AddWithValue("@Correo", correo);
                            cmd.Parameters.AddWithValue("@Contraseña", password);
                            cmd.Parameters.AddWithValue("@RolID", rol);
                            cmd.Parameters.AddWithValue("@DepartamentoID", departamento1);
                            cmd.ExecuteNonQuery();
                        }

                        // Limpiar los campos de texto y mostrar un mensaje de éxito
                        LimpiarCampos();
                        Response.Write("<script>alert('Usuario agregado exitosamente');</script>");
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
                Response.Write("<script>alert('Por favor, complete todos los campos');</script>");
            }
        }

        private void CargarDepartamentos()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT idDepartamento, Departamento FROM Departamento"; // Cambié a "Departamento" para coincidir con el nombre correcto de la tabla
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        MySqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            departamento.Items.Add(new ListItem(reader["Departamento"].ToString(), reader["idDepartamento"].ToString()));
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Manejar excepciones
                    Response.Write("<script>alert('Error al cargar departamentos: " + ex.Message + "');</script>");
                }
            }
        }

        private void CargarRoles()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT idRole, Role FROM Role"; // Cambié a "Role" para coincidir con el nombre correcto de la tabla
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        MySqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            rolUsuario.Items.Add(new ListItem(reader["Role"].ToString(), reader["idRole"].ToString()));
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Manejar excepciones
                    Response.Write("<script>alert('Error al cargar roles: " + ex.Message + "');</script>");
                }
            }
        }

        private void LimpiarCampos()
        {
            nombreUsuario.Text = "";
            apellidosUsuario.Text = "";
            cedulaUsuario.Text = "";
            correoUsuario.Text = "";
            passwordUsuario.Text = "";
            departamento.SelectedIndex = 0;
            rolUsuario.SelectedIndex = 0;
        }
    }
}