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

        public IActionResult Carga()
        {
            string sucursal = HttpContext.Request.Query["sucursal"];

            var sucursalModel = _dbContext.Sucursales.FirstOrDefault(s => s.IdSucursal == int.Parse(sucursal));

            if (sucursalModel != null)
            {
                ViewData["Sucursal"] = sucursalModel.Nombre;
            }
            else
            {
                ViewData["Sucursal"] = "Sucursal no encontrada"; // O una acción a tomar en caso de no encontrar la sucursal
            }

            return View();
        }



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
            // Busca la botella por su ID
            var botella = _dbContext.Botellas.FirstOrDefault(b => b.IdBotella == idBotella);

            if (botella != null)
            {
                // Cambia el estado de la botella
                if (botella.Estado == "A")
                {
                    botella.Estado = "B";
                }
                else if (botella.Estado == "B")
                {
                    botella.Estado = "A";
                }

                // Guarda los cambios en la base de datos
                _dbContext.SaveChanges();
            }

            // Devuelve una respuesta vacía
            return new EmptyResult();
        }

    }
}