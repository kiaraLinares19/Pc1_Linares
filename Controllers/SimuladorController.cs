using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pc1_Linares.Data;
using Pc1_Linares.Models;

namespace Pc1_Linares.Controllers
{
    public class SimuladorController : Controller
    {
        private readonly AppDbContext _context;

        public SimuladorController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int productId)
        {
            var producto = await _context.ProductosCredito.FindAsync(productId);
            if (producto == null) return NotFound();

            var vm = new SimulacionViewModel
            {
                ProductId = producto.Id,
                ProductoNombre = producto.Nombre,
                Tea = producto.Tea,
                ComisionPct = producto.ComisionPct,
                MinMeses = producto.MinMeses,
                MaxMeses = producto.MaxMeses
            };

            return View("~/Views/Productos/Simulador.cshtml", vm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(SimulacionViewModel vm)
        {
            var producto = await _context.ProductosCredito.FindAsync(vm.ProductId);
            if (producto == null)
            {
                ModelState.AddModelError("", "El producto seleccionado no existe.");
                return View("~/Views/Productos/Simulador.cshtml", vm);
            }

            if (vm.Meses < producto.MinMeses || vm.Meses > producto.MaxMeses)
            {
                ModelState.AddModelError("Meses", $"El plazo debe estar entre {producto.MinMeses} y {producto.MaxMeses} meses.");
            }

            if (!ModelState.IsValid)
            {

                vm.ProductoNombre = producto.Nombre;
                vm.Tea = producto.Tea;
                vm.ComisionPct = producto.ComisionPct;
                vm.MinMeses = producto.MinMeses;
                vm.MaxMeses = producto.MaxMeses;
                return View("~/Views/Productos/Simulador.cshtml", vm);
            }

            decimal tea = producto.Tea;
            decimal tem = (decimal)Math.Pow((double)(1 + tea), 1.0 / 12) - 1;
            tem = Math.Round(tem, 6, MidpointRounding.AwayFromZero);

            decimal i = tem;
            int n = vm.Meses;
            decimal P = vm.Monto;

            decimal cuota = P * (i * (decimal)Math.Pow((double)(1 + i), n))
                            / ((decimal)Math.Pow((double)(1 + i), n) - 1);
            cuota = Math.Round(cuota, 2, MidpointRounding.AwayFromZero);

            decimal comision = Math.Round(P * producto.ComisionPct, 2, MidpointRounding.AwayFromZero);

            decimal costoTotal = Math.Round(cuota * n + comision, 2, MidpointRounding.AwayFromZero);

          
            vm.ProductoNombre = producto.Nombre;
            vm.Tea = producto.Tea;
            vm.ComisionPct = producto.ComisionPct;
            vm.MinMeses = producto.MinMeses;
            vm.MaxMeses = producto.MaxMeses;
            vm.TEM = tem;
            vm.Cuota = cuota;
            vm.Comision = comision;
            vm.CostoTotal = costoTotal;

            return View("~/Views/Productos/Simulador.cshtml", vm);
        }
    }
}
