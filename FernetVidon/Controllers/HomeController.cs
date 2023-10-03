using FernetVidon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Diagnostics;

public class HomeController : Controller
{
    private readonly string cadenaSQL;

    public HomeController(IConfiguration config)
    {
        cadenaSQL = config.GetConnectionString("DefaultConnection");
    }

    private readonly Vvoucher2Context _dbContext;

    public HomeController( Vvoucher2Context dbContext)
    {
        _dbContext = dbContext;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Guardadas()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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
        return Json(new { data = lista });
    }



    [HttpPost]
    public IActionResult GuardarDatos(Cliente cliente)
    {
        if (ModelState.IsValid)
        {
            var clienteExiste = _dbContext.Clientes.FirstOrDefault(c => c.dni == cliente.dni);

            if (clienteExiste != null)
            {
                var botella = new Botellas
                {
                    FechaGuardado = DateTime.Now,
                    FechaVencimiento = null, // DateTime.Now.AddDays(30),
                    Estado = "A",
                    IdCliente = clienteExiste.IdCliente,
                    IdSucursal = 1,
                    QuienGuardoMozo = Request.Form["QuienGuardoMozo"]
                };

                _dbContext.Botellas.Add(botella);
                _dbContext.SaveChanges();

                // Verifica si la botella se guardó correctamente
                if (botella.IdBotella > 0)
                {
                    return Json(new { BotellaId = botella.IdBotella, ClienteName = clienteExiste.Nombre });
                }
                else
                {
                    // En caso de error, devuelve un estado de error
                    return BadRequest();
                }

                
            }
            else
            {
                return Json(new { BotellaId = 0, ClienteName = "" });


                //_dbContext.Clientes.Add(cliente);
                //_dbContext.SaveChanges();
                //clienteExiste = cliente; // Actualiza el clienteExiste con el objeto recién agregado
                //var botella = new Botellas
                //{
                //    FechaGuardado = DateTime.Now,
                //    FechaVencimiento = null, // DateTime.Now.AddDays(30),
                //    Estado = "A",
                //    IdCliente = clienteExiste.IdCliente,
                //    IdSucursal = 1,
                //    QuienGuardoMozo = Request.Form["QuienGuardoMozo"]
                //};

                //_dbContext.Botellas.Add(botella);
                //_dbContext.SaveChanges();

                //// Verifica si la botella se guardó correctamente
                //if (botella.IdBotella > 0)
                //{
                //    return Json(new { BotellaId = botella.IdBotella, ClienteName = clienteExiste.Nombre });
                //}
                //else
                //{
                //    // En caso de error, devuelve un estado de error
                //    return BadRequest();
                //}
            }

            
        }
        else
        {
            return BadRequest(ModelState);
        }
    }


    [HttpPost]
    public IActionResult GuardarCliente(Cliente cliente)
    {
        if (ModelState.IsValid)
        {
            var clienteExiste = _dbContext.Clientes.FirstOrDefault(c => c.dni == cliente.dni);

            if (clienteExiste != null)
            {
                var botella = new Botellas
                {
                    FechaGuardado = DateTime.Now,
                    FechaVencimiento = null, // DateTime.Now.AddDays(30),
                    Estado = "A",
                    IdCliente = clienteExiste.IdCliente,
                    IdSucursal = 1,
                    QuienGuardoMozo = Request.Form["QuienGuardoMozo"]
                };

                _dbContext.Botellas.Add(botella);
                _dbContext.SaveChanges();

                // Verifica si la botella se guardó correctamente
                if (botella.IdBotella > 0)
                {
                    return Json(new { BotellaId = botella.IdBotella, ClienteName = clienteExiste.Nombre });
                }
                else
                {
                    // En caso de error, devuelve un estado de error
                    return BadRequest();
                }


            }
            else
            {
                _dbContext.Clientes.Add(cliente);
                _dbContext.SaveChanges();
                clienteExiste = cliente; // Actualiza el clienteExiste con el objeto recién agregado

                var botella = new Botellas
                {
                    FechaGuardado = DateTime.Now,
                    FechaVencimiento = null, // DateTime.Now.AddDays(30),
                    Estado = "A",
                    IdCliente = clienteExiste.IdCliente,
                    IdSucursal = 1,
                    QuienGuardoMozo = Request.Form["QuienGuardoMozo"]
                };

                _dbContext.Botellas.Add(botella);
                _dbContext.SaveChanges();

                // Verifica si la botella se guardó correctamente
                if (botella.IdBotella > 0)
                {
                    return Json(new { BotellaId = botella.IdBotella, ClienteName = clienteExiste.Nombre });
                }
                else
                {
                    // En caso de error, devuelve un estado de error
                    return BadRequest();
                }
            }
        }
        else
        {
            return BadRequest(ModelState);
        }
    }


}