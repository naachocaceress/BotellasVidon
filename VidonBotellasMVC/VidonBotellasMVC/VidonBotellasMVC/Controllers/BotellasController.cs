using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VidonBotellasMVC.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace VidonBotellasMVC.Controllers
{
    public class BotellasController : Controller
    {
        private readonly string cadenaSQL;
        private readonly Vvoucher2Context _dbContext;

        public BotellasController(IConfiguration config, Vvoucher2Context dbContext)
        {
            cadenaSQL = config.GetConnectionString("conexion");
            _dbContext = dbContext;
        }


        public class BotellaClienteViewModelCarga
        {
            public Botella Botella { get; set; }
            public Cliente Cliente { get; set; }
        }


        public IActionResult Carga()
        {
            string sucursal = HttpContext.Request.Query["sucursal"];
            int idSucursal = int.Parse(sucursal);

            var sucursalModel = _dbContext.Sucursales.FirstOrDefault(s => s.IdSucursal == idSucursal);


            if (sucursalModel != null)
            {
                ViewData["SucursalId"] = sucursalModel.IdSucursal;
                ViewData["Sucursal"] = sucursalModel.Nombre;
            }
            else
            {
                ViewData["Sucursal"] = "Sucursal no encontrada";
            }

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Crear(BotellaClienteViewModelCarga viewModel, string sucursal, string miCampoOculto)
        {
            Cliente cliente;
            if (miCampoOculto == "0")
            {
                cliente = _dbContext.Clientes.FirstOrDefault(c => c.Dni == viewModel.Cliente.Dni);
                if (cliente == null)
                {
                    TempData["Message"] = "ClienteNoExiste";
                    TempData["Dni"] = viewModel.Cliente.Dni;
                    TempData["QuienGuardoMozo"] = viewModel.Botella.QuienGuardoMozo;
                    return RedirectToAction(nameof(Carga), new { sucursal = sucursal });
                }
            }
            else
            {
                _dbContext.Clientes.Add(viewModel.Cliente);
                await _dbContext.SaveChangesAsync();
                cliente = viewModel.Cliente;
            }

            viewModel.Botella.IdCliente = cliente.IdCliente;

            int idSucursal = int.Parse(sucursal);

            int numeroBotella = await _dbContext.Botellas
            .Where(b => b.IdSucursal == idSucursal)
            .Select(b => (int?)b.NumeroBotella) 
            .MaxAsync() + 1 ?? 1;

            viewModel.Botella.NumeroBotella = numeroBotella;
            viewModel.Botella.FechaGuardado = DateTime.Today;
            viewModel.Botella.IdSucursal = int.Parse(sucursal);
            viewModel.Botella.Estado = "A";

            _dbContext.Botellas.Add(viewModel.Botella);

            await _dbContext.SaveChangesAsync();

            TempData["ShowModal"] = "True";
            TempData["numeroBotella"] = numeroBotella;
            TempData["nombre"] = cliente.Nombre;

            return RedirectToAction(nameof(Carga), new { sucursal = sucursal });
        }


        //--


        public class BotellaClienteViewModel
        {
            public IEnumerable<Botella> Botellas { get; set; }
            public IEnumerable<Cliente> Clientes { get; set; }
        }

        public async Task<IActionResult> Gestion()
        {
            string sucursalParam = HttpContext.Request.Query["sucursal"];
            if (!int.TryParse(sucursalParam, out int sucursalId))
            {
                ViewData["Sucursal"] = "Sucursal no válida";
                return View(await _dbContext.Botellas.ToListAsync());
            }

            var sucursalModel = _dbContext.Sucursales.FirstOrDefault(s => s.IdSucursal == sucursalId);

            if (sucursalModel != null)
            {
                ViewData["Sucursal"] = sucursalModel.Nombre;

                var botellas = (from b in _dbContext.Botellas
                                join c in _dbContext.Clientes on b.IdCliente equals c.IdCliente
                                where b.IdSucursal == sucursalId
                                select b).ToList();

                var clientes = _dbContext.Clientes.ToList();

                var viewModel = new BotellaClienteViewModel
                {
                    Botellas = botellas,
                    Clientes = clientes
                };

                return View(viewModel);
            }
            else
            {
                ViewData["Sucursal"] = "Sucursal no encontrada";
                return View(await _dbContext.Botellas.ToListAsync());
            }
        }


        [HttpPost]
        public IActionResult CambiarEstadoBotella(int idBotella)
        {
            var botella = _dbContext.Botellas.FirstOrDefault(b => b.IdBotella == idBotella);

            if (botella != null)
            {
                if (botella.Estado == "A")
                {
                    botella.Estado = "B";
                }
                else if (botella.Estado == "B")
                {
                    botella.Estado = "A";
                }

                _dbContext.SaveChanges();
            }

            return new EmptyResult();
        }

        public IActionResult Redirect()
        {
            return View();
        }

    }
}