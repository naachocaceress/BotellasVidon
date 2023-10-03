using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Windows.Forms.LinkLabel;
using VidonVouchers.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;

namespace VidonVouchers
{
    public partial class Botellas_Carga : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string sucu = Request.QueryString["sucursal"];

            string connectionString = ConfigurationManager.ConnectionStrings["VVoucher2ConnectionString"].ConnectionString;

            // Establecer la conexión con la base de datos
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Consulta para obtener el último ID insertado
                string query = "SELECT nombre from SUCURSALES where idSucursal = " + sucu;
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    lblSucu.InnerText = command.ExecuteScalar().ToString();
                }

            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            HtmlGenericControl miDiv = FindControl("otrosCampos") as HtmlGenericControl;
            string conexion = ConfigurationManager.ConnectionStrings["VVoucher2ConnectionString"].ConnectionString;

            if (miDiv != null)
            {
                string displayValue = miDiv.Style["display"];

                if (displayValue == "none")
                {
                    using (SqlConnection connection = new SqlConnection(conexion))
                    {
                        connection.Open();
                        string query = "SELECT COUNT(*) FROM Clientes.Cliente WHERE dni = @dnia";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@dnia", dni.Value);

                            object idCliente = command.ExecuteScalar();
                            int count = (int)command.ExecuteScalar();

                            if (count > 0)
                            {
                                GuardarBotella();

                                lblNombre.Text = "Hola " + ObtenerNombre() + "! Tu numero fernetero"; // Reemplaza con el nombre real del cliente
                                lblNumero.Text = ObtenerUltimoIdInsertado().ToString(); // Reemplaza con el número real de la botella

                                // Muestra el modal utilizando JavaScript
                                string script = @"<script type='text/javascript'> 
                                    $(document).ready(function () {
                                        $('#staticBackdrop').modal('show');
                                    });
                                </script>";

                                ScriptManager.RegisterStartupScript(this, GetType(), "ShowModal", script, false);
                            }
                            else
                            {
                                if (miDiv != null)
                                {
                                    // Establece el estilo "display" en "block"
                                    miDiv.Style["display"] = "block";
                                }
                            }
                        }
                    }
                }
                else if (displayValue == "block")
                {                   
                    using (SqlConnection connection = new SqlConnection(conexion))
                    {
                        connection.Open();
                        string query = "SELECT COUNT(*) FROM Clientes.Cliente WHERE dni = @dnia";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@dnia", dni.Value);

                            object idCliente = command.ExecuteScalar();
                            int count = (int)command.ExecuteScalar();

                            if (count > 0)
                            {
                                lblNombre.Text = "Error"; // Reemplaza con el nombre real del cliente
                                lblNumero.Text = "Cliente ya existe"; // Reemplaza con el número real de la botella

                                // Muestra el modal utilizando JavaScript
                                string script = @"<script type='text/javascript'> 
                                    $(document).ready(function () {
                                        $('#staticBackdrop').modal('show');
                                    });
                                </script>";

                                ScriptManager.RegisterStartupScript(this, GetType(), "ShowModal", script, false);
                            }
                            else
                            {
                                GuardarCliente();
                                GuardarBotella();

                                lblNombre.Text = "Hola " + ObtenerNombre() + "! Tu numero fernetero"; // Reemplaza con el nombre real del cliente
                                lblNumero.Text = ObtenerUltimoIdInsertado().ToString(); // Reemplaza con el número real de la botella

                                // Muestra el modal utilizando JavaScript
                                string script = @"<script type='text/javascript'> 
                                    $(document).ready(function () {
                                        $('#staticBackdrop').modal('show');
                                    });
                                </script>";

                                ScriptManager.RegisterStartupScript(this, GetType(), "ShowModal", script, false);
                               
                            }
                        }
                    }
                }
            }
        }

        public void GuardarCliente()
        {
            SqlDataSource1.InsertParameters["dni"].DefaultValue = dni.Value;
            SqlDataSource1.InsertParameters["nombre"].DefaultValue = nombre.Value;
            SqlDataSource1.InsertParameters["apellido"].DefaultValue = apellido.Value;
            SqlDataSource1.InsertParameters["fechaNacimiento"].DefaultValue = date.Value;
            SqlDataSource1.InsertParameters["nroTelefono"].DefaultValue = tel.Value;
            SqlDataSource1.Insert();
        }

        public void GuardarBotella()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["VVoucher2ConnectionString"].ConnectionString;
            string idCliente = "";

            // Establecer la conexión con la base de datos
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Consulta para obtener el último ID insertado
                string query = "SELECT idCliente from Clientes.Cliente where dni = " + dni.Value;
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    idCliente = command.ExecuteScalar().ToString();
                }
            }

            SqlDataSource2.InsertParameters["mozo"].DefaultValue = mozo.Value;
            SqlDataSource2.InsertParameters["fechaGuardado"].DefaultValue = DateTime.Now.ToString();
            SqlDataSource2.InsertParameters["fechaVencimiento"].DefaultValue = null;
            SqlDataSource2.InsertParameters["idCliente"].DefaultValue = idCliente;
            SqlDataSource2.InsertParameters["idSucursal"].DefaultValue = Request.QueryString["sucursal"]; ;
            SqlDataSource2.Insert();
        }

        private int ObtenerUltimoIdInsertado()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["VVoucher2ConnectionString"].ConnectionString;

            // Establecer la conexión con la base de datos
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Consulta para obtener el último ID insertado
                string query = "SELECT IDENT_CURRENT('Clientes.Botellas') AS LastID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        private string ObtenerNombre()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["VVoucher2ConnectionString"].ConnectionString;

            // Establecer la conexión con la base de datos
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Consulta para obtener el último ID insertado
                string query = "SELECT Nombre from Clientes.Cliente where dni = " + dni.Value;
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    return command.ExecuteScalar().ToString();
                }
            }
        }

    }
}