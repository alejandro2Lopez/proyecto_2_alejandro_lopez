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
       
        [HttpGet("ObtenerRoles")]
        public IActionResult ObtenerRoles()
        {


           List <Rol> roles = catalogo.obtenerRoles(); 
           
                                         

            return Ok(new
            {
                roles = roles
            });
        }
    }

}
