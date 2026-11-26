using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto2.Data;
using Proyecto2.Model.Entities;

namespace Proyecto2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProyectoController : Controller
    {

        private ProyectoData _proyecto;
        public IActionResult Index()
        {
            return View();
        }
        public ProyectoController(ProyectoData proyecto)
        {
            _proyecto = proyecto;

        }

        [Authorize]
        [HttpGet("ObtenerProyectos")]
        public IActionResult ObtenerProyectos()
        {
            List<Proyecto> proyectos = _proyecto.obtenerProyectos();

            return Ok(new
            {
                proyectos = proyectos
            });
        }

        [Authorize]
        [HttpPost("InsertarProyecto")]
        public IActionResult InsertarProyecto([FromBody] Proyecto proyecto)
        {
            try
            {
                bool insertado = _proyecto.InsertarProyecto(proyecto);

                if (!insertado)
                {
                    return BadRequest(new
                    {
                        mensaje = "No se pudo insertar el proyecto."
                    });
                }

                return Ok(new
                {
                    mensaje = "Proyecto registrado correctamente."
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    mensaje = "Ocurrió un error al registrar el proyecto."
                });
            }
        }
        [Authorize]
        [HttpPost("ActualizarProyecto")]
        public IActionResult ActualizarProyecto([FromBody] Proyecto proyecto)
        {
            try
            {
                if (proyecto.id_proyecto <= 0)
                {
                    return BadRequest(new
                    {
                        mensaje = "El identificador del proyecto no es válido."
                    });
                }

                bool actualizado = _proyecto.ActualizarProyecto(proyecto);

                if (!actualizado)
                {
                    return BadRequest(new
                    {
                        mensaje = "No se pudo actualizar el proyecto."
                    });
                }

                return Ok(new
                {
                    mensaje = "Proyecto actualizado correctamente."
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    mensaje = "Ocurrió un error al actualizar el proyecto."
                });
            }
        }
        [Authorize]
        [HttpGet("EliminarProyecto/{id_proyecto}")]
        public IActionResult EliminarProyecto(int id_proyecto)
        {
            try
            {
                if (id_proyecto <= 0)
                {
                    return BadRequest(new
                    {
                        mensaje = "El identificador del proyecto no es válido."
                    });
                }

                bool eliminado = _proyecto.EliminarProyecto(id_proyecto);

                if (!eliminado)
                {
                    return BadRequest(new
                    {
                        mensaje = "No se pudo eliminar el proyecto."
                    });
                }

                return Ok(new
                {
                    mensaje = "Proyecto eliminado correctamente."
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    mensaje = "Ocurrió un error al eliminar el proyecto."
                });
            }
        }
    }
   
    }
