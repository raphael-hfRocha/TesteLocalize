using CadastroEmpresas.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace CadastroEmpresas.src.API.Controllers
{
    [ApiController]
    [Route("api/controller")]
    public class UsuarioController
    {
        private readonly ApplicationDbContext _context;
        /*private readonly IUsuarioService _usuarioService;*/

        public UsuarioController(ApplicationDbContext context)
        {
            _context = context;
        }

        //[HttpPost("Login")]
        //public async Task<IActionResult> Login(LoginDTO dto)
        //{
        //    var token = await _usuarioService.LoginAsync(dto);
        //    return Ok(new { token });
        //}
    }
}
