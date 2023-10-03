using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace VidonVouchers
{
    public partial class Botellas_Gestion : System.Web.UI.Page
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
    }
}