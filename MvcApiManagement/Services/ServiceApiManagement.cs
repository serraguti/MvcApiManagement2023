using MvcApiManagement.Models;
using System.Net.Http.Headers;
using System.Web;

namespace MvcApiManagement.Services
{
    public class ServiceApiManagement
    {
        private MediaTypeWithQualityHeaderValue Header;
        private string UrlApiEmpleados;
        private string UrlApiDepartamentos;

        public ServiceApiManagement(IConfiguration configuration)
        {
            this.Header =
                new MediaTypeWithQualityHeaderValue("application/json");
            this.UrlApiDepartamentos = configuration.GetValue<string>
                ("ApiUrls:ApiDepartamentos");
            this.UrlApiEmpleados =
                configuration.GetValue<string>("ApiUrls:ApiEmpleados");
        }

        public async Task<List<Empleado>> GetEmpleadosAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                //DEBEMOS ENVIAR UNA CADENA VACIAL FINAL DEL REQUEST
                var queryString =
                    HttpUtility.ParseQueryString(string.Empty);
                string request =
                    "/api/empleados?" + queryString;

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                //DEBEMOS INDICAR QUE NO UTILIZAMOS CACHE PARA LAS PETICIONES
                client.DefaultRequestHeaders.CacheControl =
                    CacheControlHeaderValue.Parse("no-cache");
                //LA PETICION AL API MANAGEMENT SE REALIZA MEDIANTE 
                //URL + REQUEST, ES DECIR, NO LLEVA BaseAddress
                HttpResponseMessage response =
                    await client.GetAsync(this.UrlApiEmpleados + request);
                if (response.IsSuccessStatusCode)
                {
                    List<Empleado> empleados =
                        await response.Content.ReadAsAsync<List<Empleado>>();
                    return empleados;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<List<Departamento>> 
            GetDepartamentosAsync(string suscripcion)
        {
            using (HttpClient client = new HttpClient())
            {
                //DEBEMOS ENVIAR UNA CADENA VACIAL FINAL DEL REQUEST
                var queryString =
                    HttpUtility.ParseQueryString(string.Empty);
                string request =
                    "/api/departamentos?" + queryString;

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                //DEBEMOS INDICAR QUE NO UTILIZAMOS CACHE PARA LAS PETICIONES
                client.DefaultRequestHeaders.CacheControl =
                    CacheControlHeaderValue.Parse("no-cache");
                //AÑADIMOS A LA CABECERA LA CLAVE DE SUBSCRIPCION
                client.DefaultRequestHeaders.Add
                    ("Ocp-Apim-Subscription-Key", suscripcion);
                //LA PETICION AL API MANAGEMENT SE REALIZA MEDIANTE 
                //URL + REQUEST, ES DECIR, NO LLEVA BaseAddress
                HttpResponseMessage response =
                    await client.GetAsync(this.UrlApiDepartamentos + request);
                if (response.IsSuccessStatusCode)
                {
                    List<Departamento> departamentos =
                        await response.Content.ReadAsAsync<List<Departamento>>();
                    return departamentos;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
