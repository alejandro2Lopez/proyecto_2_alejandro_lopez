using Microsoft.AspNetCore.Mvc;
using Proyecto2;
using Proyecto2.Data;
using Proyecto2.Model.Entities;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly JwtService _jwtService;
    private readonly UsuarioData _suarioData;
    public AuthController(JwtService jwtService, UsuarioData usuarioData)
    {
        _jwtService = jwtService;
        _suarioData = usuarioData;
    }

    [HttpPost("AuthUser")]
    public IActionResult AuthUser([FromBody] LoginDTO usuario)
    {


        Usuario? usuario1 = _suarioData.ValidarUsuario(usuario.Email, usuario.Contrasennia);
        if (usuario1 == null) {
            return Unauthorized(new
            {
                mensaje = "Credenciales incorrectas"
            });
        }

    
        int idUsuario = usuario1.id_usuario;
        string nombre = usuario1.nombre_usuario;
        string rol = usuario1.rol.nombre_rol;

        string token = _jwtService.GenerarToken(
            idUsuario,
            nombre,
            rol
        );

        return Ok(new
        {
            access_token = token,
            token_type = "Bearer"
        });
    }
}