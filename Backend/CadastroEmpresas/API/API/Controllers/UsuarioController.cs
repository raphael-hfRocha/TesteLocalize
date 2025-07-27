using Application.Services;
using CadastroEmpresas.Domain.Entities;
using CadastroEmpresas.src.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace CadastroEmpresas.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet("ReturnClass")]
        public ActionResult<Usuario> ReturnClass()
        {
            Usuario usuario = new Usuario();
            return Ok(usuario);
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Usuario>> PesquisarEmpresaBanco(Int32 idUsuario)
        {
            var vo = new Usuario
            {
                IdUsuario = idUsuario
            };
            try
            {
                var result = await _usuarioService.ObterDadosUsuario(vo);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro interno: {ex.Message}");
            }
        }

        [HttpGet("Login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Usuario>> Login(String email, String senha)
        {
            var vo = new Usuario
            {
                Email = email,
                Senha = senha
            };
            try
            {
                var result = await _usuarioService.Login(vo);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro interno: {ex.Message}");
            }
        }


        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Usuario> Post(String nome, String email, String senha)
        {
            var usuario = new Usuario
            {
                Nome = nome,
                Email = email,
                Senha = senha
            };
            try
            {
                var result = _usuarioService.CadastrarUsuario(usuario);
                return Created($"/api/Usuario/{result.IdUsuario}", result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao cadastrar empresa: {ex.Message}");
            }
        }
    }
}
