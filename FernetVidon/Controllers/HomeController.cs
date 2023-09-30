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




    //VIEJO SI FUNCIONA


    [HttpPost]
    public IActionResult GuardarDatos(Cliente cliente)
    {
        if (ModelState.IsValid)
        {
            var clienteExiste = _dbContext.Clientes.FirstOrDefault(c => c.dni == cliente.dni);

            if (clienteExiste == null)
            {
                _dbContext.Clientes.Add(cliente);
                _dbContext.SaveChanges();
                clienteExiste = cliente; // Actualiza el clienteExiste con el objeto recién agregado
            }

            var botella = new Botellas
            {
                FechaGuardado = DateTime.Now,
                FechaVencimiento = null, // DateTime.Now.AddDays(30),
                Estado = "A",
                IdCliente = clienteExiste.IdCliente,
                IdSucursal = 1
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
            return BadRequest(ModelState);
        }
    }




















    //NUEVO NO FUNCIONA

    [HttpPost]
    public IActionResult GuardarDatos(Cliente cliente, int btnAceptar)
    {
        if (ModelState.IsValid)
        {
            var clienteExiste = _dbContext.Clientes.FirstOrDefault(c => c.dni == cliente.dni);

            if (btnAceptar == 0)
            {

                if (clienteExiste != null)
                {
                    var botella = new Botellas
                    {
                        FechaGuardado = DateTime.Now,
                        FechaVencimiento = null, // DateTime.Now.AddDays(30),
                        Estado = "A",
                        IdCliente = clienteExiste.IdCliente,
                        IdSucursal = 1
                    };

                    _dbContext.Botellas.Add(botella);
                    _dbContext.SaveChanges();

                    return Json(new { clienteExiste = true, BotellaId = botella.IdBotella, ClienteName = clienteExiste.Nombre });
                }
                else
                {
                    return Json(new { clienteExiste = false, BotellaId = 0, ClienteName = 0 });
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
                    IdSucursal = 1
                };

                _dbContext.Botellas.Add(botella);
                _dbContext.SaveChanges();

                // Verifica si la botella se guardó correctamente
                if (botella.IdBotella > 0)
                {
                    return Json(new { clienteExiste = true, BotellaId = botella.IdBotella, ClienteName = clienteExiste.Nombre });
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
