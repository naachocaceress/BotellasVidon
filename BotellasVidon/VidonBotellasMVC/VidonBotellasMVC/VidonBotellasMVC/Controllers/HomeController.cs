using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VidonBotellasMVC.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace VidonBotellasMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly string cadenaSQL;
        private readonly Vvoucher2Context _dbContext;

        public HomeController(IConfiguration config, Vvoucher2Context dbContext)
        {
            cadenaSQL = config.GetConnectionString("conexion");
            _dbContext = dbContext;
        }

        public IActionResult Botellas_Carga()
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






        public IActionResult Botellas_Gestion()
        {
            string sucursal = HttpContext.Request.Query["sucursal"];
            var sucursalModel = _dbContext.Sucursales.FirstOrDefault(s => s.IdSucursal == int.Parse(sucursal));

            if (sucursalModel != null)
            {
                ViewData["Sucursal"] = sucursalModel.Nombre;
            }
            else
            {
                ViewData["Sucursal"] = "Sucursal no encontrada";
            }

            return View();
        }

        public class BotellaClienteViewModel
        {
            public IEnumerable<VidonBotellasMVC.Models.Botella> Botellas { get; set; }
            public IEnumerable<VidonBotellasMVC.Models.Cliente> Clientes { get; set; }
        }


        public IActionResult GetBotellasData(int sucursal)
        {
            var query = from b in _dbContext.Botellas
                        join c in _dbContext.Clientes on b.IdCliente equals c.IdCliente
                        where b.IdSucursal == sucursal
                        orderby b.IdBotella descending
                        select new
                        {
                            idBotella = b.NumeroBotella,
                            nombre = c.Nombre,
                            apellido = c.Apellido,
                            dni = c.Dni,
                            fechaGuar = b.FechaGuardado,
                            mozo = b.QuienGuardoMozo,
                            estado = b.Estado
                        };

            var botellas = query.ToList();

            return Json(botellas);
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