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
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnBotellasGestor_Click(object sender, EventArgs e)
        {
            buscarSucursales("Gestion");          
        }

        protected void btnBotellasCarga_Click(object sender, EventArgs e)
        {
            buscarSucursales("Carga");
        }

        private void buscarSucursales(string tipo)
        {
            // Establece la conexión a la base de datos
            string connectionString = ConfigurationManager.ConnectionStrings["VVoucher2ConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Define la consulta SQL para obtener las sucursales
                string query = "SELECT idSucursal, nombre FROM SUCURSALES";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Recorre los registros y crea enlaces para cada sucursal
                        while (reader.Read())
                        {
                            int idSucursal = reader.GetInt32(0);
                            string nombreSucursal = reader.GetString(1);

                            if (idSucursal != 1)
                            {
                                // Crea un nuevo enlace y configúralo
                                HyperLink linkSucursal = new HyperLink();
                                linkSucursal.ID = "lnkSucursal_" + idSucursal; // Asigna un ID único al enlace
                                linkSucursal.Text = nombreSucursal;
                                linkSucursal.CssClass = "btn btn-space";
                                linkSucursal.Style["background-color"] = "#0c8444";
                                linkSucursal.Style["color"] = "white";
                                linkSucursal.Style["width"] = "80%";
                                linkSucursal.NavigateUrl = "VBotellas/Botellas-" + tipo + ".aspx?sucursal=" + idSucursal; // Especifica la URL a la que se redirigirá

                                Panel1.Controls.Add(linkSucursal);
                            }
                        }
                    }
                }
            }

            string script = @"<script type='text/javascript'> 
                         $(document).ready(function () {
                             $('#myModal').modal('show');
                         });
                     </script>";

            ScriptManager.RegisterStartupScript(this, GetType(), "ShowModal", script, false);
        }


    }
}