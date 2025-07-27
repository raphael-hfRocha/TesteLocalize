using Application.Services;
using CadastroEmpresas.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace CadastroEmpresas.src.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EmpresaController : ControllerBase
    {
        private readonly EmpresaService _empresaService;

        public EmpresaController(EmpresaService empresaService)
        {
            _empresaService = empresaService;
        }

        [HttpGet("ReturnClass")]
        public ActionResult<Empresa> ReturnClass()
        {
            Empresa empresa = new Empresa();
            return Ok(empresa);
        }

        [HttpGet("{cnpj}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Empresa>> PesquisarEmpresaReceitaWs(String cnpj)
        {
            var vo = new Empresa
            {
                Cnpj = cnpj
            };

            try
            {
                var result = await _empresaService.ObterDadosCnpjReceitaWs(vo);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro interno: {ex.Message}");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Empresa>> PesquisarEmpresaBanco(String cnpj)
        {
            var vo = new Empresa 
            { 
                Cnpj = cnpj
            };
            try
            {
                var resultado = await _empresaService.ObterDadosCnpjBanco(vo);
                return Ok(resultado);
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
        public ActionResult<Empresa> Post(String cnpj)
        {
            var empresa = new Empresa
            {
                NomeEmpresarial = "Nome Empresarial Exemplo",
                NomeFantasia = "Nome Fantasia Exemplo",
                Cnpj = cnpj,
                Situacao = "Ativa",
                Abertura = "2023-01-01",
                Tipo = "Sociedade Limitada",
                NaturezaJuridica = "Empresa Individual",
                AtividadePrincipal = "Comércio Varejista",
                Logradouro = "Rua Exemplo",
                Numero = "123",
                Complemento = "Apto 1",
                Bairro = "Bairro Exemplo",
                Municipio = "Cidade Exemplo",
                Uf = "SP",
                Cep = "12345-678",
            };

            try
            {
                var resultado = _empresaService.CadastrarEmpresa(empresa);
                return Created($"/api/Empresa/{resultado.IdEmpresa}", resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao cadastrar empresa: {ex.Message}");
            }
        }
    }
}