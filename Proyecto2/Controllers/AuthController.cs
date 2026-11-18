using Microsoft.AspNetCore.Mvc;
using Proyecto2;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly JwtService _jwtService;

    public AuthController(JwtService jwtService)
    {
        _jwtService = jwtService;
    }

    [HttpPost("AuthUser")]
    public IActionResult AuthUser()
    {
        // Valores de prueba para verificar JWT.
        // En producción deben venir de un usuario autenticado.
        int idUsuario = 1;
        string nombre = "UsuarioPrueba";
        string rol = "Administrador";

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