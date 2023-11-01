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

    }
}