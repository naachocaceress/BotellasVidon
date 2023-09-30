using FernetVidon.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly Vvoucher2Context _dbContext;

    public HomeController(ILogger<HomeController> logger, Vvoucher2Context dbContext)
    {
        _logger = logger;
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






    [HttpPost]
    public IActionResult GuardarDatos(Cliente cliente, string action)
    {
        if (ModelState.IsValid)
        {
            var clienteExiste = _dbContext.Clientes.FirstOrDefault(c => c.dni == cliente.dni);

            if (action == "comprobar")
            {
                if (clienteExiste == null)
                {
                    //ABRIR DIV
                    return BadRequest();

                }
                else
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

            }
            else if (action == "aceptar")
            {
                if (clienteExiste == null)
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
                else
                {
                    //YA EXISTE
                    return BadRequest();

                }
            }
            else 
                return BadRequest();

        }
        else
        {
            return BadRequest(ModelState);
        }        
    }
}
