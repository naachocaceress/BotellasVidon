using FernetVidon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace FernetVidon.Controllers
{
    public class GuardadosController : Controller
    {
        private readonly string cadenaSQL;

        public GuardadosController(IConfiguration config)
        {
            cadenaSQL = config.GetConnectionString("DefaultConnection");
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListaCliente()
        {
            List<Cliente> lista = new List<Cliente>();

            using (var conexion = new SqlConnection(cadenaSQL))
            {
                conexion.Open();
                var cmd = new SqlCommand("select * from Clientes.Cliente", conexion);
                cmd.CommandType = System.Data.CommandType.Text; //REVISAR

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Cliente
                        {
                            IdCliente = Convert.ToInt32(dr["IdCliente"]),
                            dni = Convert.ToInt32(dr["dni"]),
                            Nombre = dr["Nombre"].ToString(),
                            Apellido = dr["Apellido"].ToString(),
                            FechaNacimiento = (DateTime)dr["FechaNacimiento"],
                            NumeroTelefono = dr["NumeroTelefono"].ToString()
                        });
                    }
                }
            }
            return Json(new {data = lista});
        }


    }
}
