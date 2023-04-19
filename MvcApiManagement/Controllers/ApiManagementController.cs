using Microsoft.AspNetCore.Mvc;
using MvcApiManagement.Models;
using MvcApiManagement.Services;

namespace MvcApiManagement.Controllers
{
    public class ApiManagementController : Controller
    {
        private ServiceApiManagement service;

        public ApiManagementController(ServiceApiManagement service)
        {
            this.service = service;
        }

        public async Task<IActionResult> Empleados()
        {
            List<Empleado> data =
                await this.service.GetEmpleadosAsync();
            return View(data);
        }

        public IActionResult Departamentos()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> 
            Departamentos(string suscripcion)
        {
            List<Departamento> data =
                await this.service.GetDepartamentosAsync(suscripcion);
            return View(data);
        }
    }
}
