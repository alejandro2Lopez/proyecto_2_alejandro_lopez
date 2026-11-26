using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto2.Data;
using Proyecto2.Model.Entities;

namespace Proyecto2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogoController : Controller
    {
        private readonly CatalogoData catalogo;

        public CatalogoController(CatalogoData catalogo) {
            this.catalogo=catalogo;
        }
        [Authorize]
        [HttpGet("ObtenerRoles")]
        public IActionResult ObtenerRoles()
        {


           List <Rol> roles = catalogo.obtenerRoles(); 
           
                                         

            return Ok(new
            {
                roles = roles
            });
        }

        [Authorize]
        [HttpGet("ObtenerEstadosTarea")]
        public IActionResult ObtenerEstadosTarea()
        {


            List<Estado> estado = catalogo.obtenerEstados(true);



            return Ok(new
            {
                estado = estado
            });
        }
        [Authorize(Roles = "Administrador")]
        [HttpGet("ObtenerEstadosProyecto")]
        public IActionResult ObtenerEstadosProyecto()
        {


            List<Estado> estado = catalogo.obtenerEstados(false);



            return Ok(new
            {
                estado = estado
            });
        }
        [Authorize]
        [HttpGet("ObtenerPrioridades")]
        public IActionResult ObtenerPrioridades()
        {


            List<Prioridad> prioridades = catalogo.obtenerPrioridades();



            return Ok(new
            {
                prioridades = prioridades
            });
        }
    }


}
