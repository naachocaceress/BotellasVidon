using OfficeOpenXml;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using Button = System.Web.UI.WebControls.Button;

namespace VidonVouchers
{
    public partial class Botellas_Gestion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string sucu = Request.QueryString["sucursal"];

            ObtenerSucursal(sucu);
            Tabla(sucu);
        }

        private void ObtenerSucursal(string sucu)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["VVoucher2ConnectionString"].ConnectionString;

            // Establecer la conexión con la base de datos
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Consulta para obtener el último ID insertado
                string query = "SELECT nombre from SUCURSALES where idSucursal = " + sucu;
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        lblSucu.InnerText = result.ToString();
                    }
                    else
                    {
                        // Manejo de caso en el que no se encontraron resultados
                        lblSucu.InnerText = "No se encontraron resultados.";
                    }
                }

            }
        }

        private void Tabla(string sucu)
        {
            if (txtDni.Text == "" && txtId.Text == "")
            {
                string connectionString = ConfigurationManager.ConnectionStrings["VVoucher2ConnectionString"].ConnectionString;
                string query = "SELECT b.idBotella as idBotella, c.nombre, c.apellido, c.dni, b.fechaGuardado as fechaGuar, b.quienGuardoMozo as mozo, b.estado " +
                    "FROM Clientes.Botellas as b " +
                    "JOIN Clientes.Cliente as c ON b.idCliente = c.idCliente " +
                    "WHERE b.idSucursal = @sucu " +
                    "ORDER BY idBotella DESC";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@sucu", sucu); // Agrega el parámetro @sucu de forma segura.

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    gvBotellas.DataSource = dataTable;
                    gvBotellas.DataBind();
                }
            }
            else if (txtId.Text != "")
            {
                porId();
            }
            else if (txtDni.Text != "")
            {
                porDni();
            }

        }

        #region Botones en table

        protected void btnEstado_Click(object sender, EventArgs e)
        {
            string sucu = Request.QueryString["sucursal"];

            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer; // Obtiene la fila del GridView

            if (row != null && row.RowType == DataControlRowType.DataRow)
            {
                int rowIndex = row.RowIndex; // Obtiene el índice de la fila en el GridView

                if (gvBotellas.DataKeys != null && gvBotellas.DataKeys.Count > rowIndex)
                {
                    string idBotella = gvBotellas.DataKeys[rowIndex].Value.ToString(); // Obtiene el valor de la clave de datos (idBotella) de esa fila

                    string estadoActual = ObtenerEstadoDesdeBD(idBotella);

                    string nuevoEstado = (estadoActual == "A") ? "B" : "A";

                    ActualizarEstadoEnBD(idBotella, nuevoEstado);

                    Tabla(sucu);
                }
            }
        }

        private string ObtenerEstadoDesdeBD(string idBotella)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["VVoucher2ConnectionString"].ConnectionString;
            string query = "Select estado from Clientes.Botellas where idBotella = @id ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", idBotella); // Agrega el parámetro @sucu de forma segura.

                try
                {
                    connection.Open();
                    string estado = (string)command.ExecuteScalar(); // Ejecuta la consulta y obtén el resultado

                    if (estado != null)
                    {
                        return estado; // Devuelve el estado obtenido de la base de datos
                    }
                    else
                    {
                        return "Estado no encontrado"; // Por ejemplo
                    }
                }
                catch (Exception ex)
                {
                    return "Error al obtener el estado"; // Por ejemplo
                }
            }

        }

        private void ActualizarEstadoEnBD(string idBotella, string nuevoEstado)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["VVoucher2ConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);

            string query = "UPDATE Clientes.Botellas SET estado = @nuevoEstado WHERE idBotella = @idBotella";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@nuevoEstado", nuevoEstado);
            command.Parameters.AddWithValue("@idBotella", idBotella);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        #endregion

        #region Filtros

        protected void btnId_Click(object sender, EventArgs e)
        {
            porId();
        }

        private void porId()
        {
            txtDni.Text = null;
            string sucu = Request.QueryString["sucursal"];
            string idBotella = txtId.Text.Trim(); // Obtener el valor del TextBox y eliminar espacios en blanco.

            // Verificar si el TextBox está vacío antes de ejecutar la consulta.
            if (!string.IsNullOrEmpty(idBotella))
            {
                string connectionString = ConfigurationManager.ConnectionStrings["VVoucher2ConnectionString"].ConnectionString;
                string query = "SELECT b.idBotella as idBotella, c.nombre, c.apellido, c.dni, b.fechaGuardado as fechaGuar, b.quienGuardoMozo as mozo, b.estado " +
                    "FROM Clientes.Botellas as b " +
                    "JOIN Clientes.Cliente as c ON b.idCliente = c.idCliente " +
                    "WHERE b.idSucursal = @sucu AND idBotella = @idBotella " +
                    "ORDER BY idBotella DESC";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@sucu", sucu); // Agrega el parámetro @sucu de forma segura.
                    command.Parameters.AddWithValue("@idBotella", idBotella); // Agrega el parámetro @sucu de forma segura.

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    gvBotellas.DataSource = dataTable;
                    gvBotellas.DataBind();
                }
            }
        }


        protected void btnDni_Click(object sender, EventArgs e)
        {
            porDni();
        }

        private void porDni()
        {
            txtId.Text = null;
            string sucu = Request.QueryString["sucursal"];
            string dni = txtDni.Text.Trim(); // Obtener el valor del TextBox y eliminar espacios en blanco.

            // Verificar si el TextBox está vacío antes de ejecutar la consulta.
            if (!string.IsNullOrEmpty(dni))
            {
                string connectionString = ConfigurationManager.ConnectionStrings["VVoucher2ConnectionString"].ConnectionString;
                string query = "SELECT b.idBotella as idBotella, c.nombre, c.apellido, c.dni, b.fechaGuardado as fechaGuar, b.quienGuardoMozo as mozo, b.estado " +
                    "FROM Clientes.Botellas as b " +
                    "JOIN Clientes.Cliente as c ON b.idCliente = c.idCliente " +
                    "WHERE b.idSucursal = @sucu AND c.dni = @dni " +
                    "ORDER BY idBotella DESC";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@sucu", sucu); // Agrega el parámetro @sucu de forma segura.
                    command.Parameters.AddWithValue("@dni", dni); // Agrega el parámetro @sucu de forma segura.

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    gvBotellas.DataSource = dataTable;
                    gvBotellas.DataBind();
                }
            }
        }



        protected void btnExportExcelBotellas_Click(object sender, EventArgs e)
        {
            ExportarGridViewAExcel(gvBotellas);
        }

        private void ExportarGridViewAExcel(GridView gridView)
        {
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Datos");

                for (int i = 0; i < gridView.Columns.Count; i++)
                {
                    string columnName = gridView.Columns[i].HeaderText;
                    worksheet.Cells[1, i + 1].Value = columnName;
                }

                for (int i = 0; i < gridView.Rows.Count; i++)
                {
                    for (int j = 0; j < gridView.Columns.Count; j++)
                    {
                        string cellValue = gridView.Rows[i].Cells[j].Text;

                        if (double.TryParse(cellValue, out double numericValue))
                        {
                            worksheet.Cells[i + 2, j + 1].Value = numericValue;
                        }
                        else
                        {
                            worksheet.Cells[i + 2, j + 1].Value = cellValue;
                        }
                    }
                }

                worksheet.Cells.AutoFitColumns();
                byte[] content = package.GetAsByteArray();

                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("Content-Disposition", "attachment; filename=datos.xlsx");
                Response.BinaryWrite(content);
                Response.End();
            }
        }


        protected void ddlPageSizeBotellas_SelectedIndexChanged(object sender, EventArgs e)
        {
            //RecopilarDatos(1, "Vouchers", "VOUCHER", DropDownSucursales1, ddlEstadoVch1, dateDesde, dateHasta, txtMontoMinimo, txtMontoMaximo, txtId, txtCliente, ddlPageSizeVouchersCreados, gvVouchersCreados);
        }

        protected void gvBotellas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            string sucu = Request.QueryString["sucursal"];

            gvBotellas.PageIndex = e.NewPageIndex;
            Tabla(sucu);
        }

        #endregion


    }
}





