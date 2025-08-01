using API.DTOs;
using Application.Services;
using CadastroEmpresas.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Empresa ReturnClass()
        {
            return new Empresa
            {
                IdEmpresa = 0,
                NomeEmpresarial = "",
                NomeFantasia = "",
                Cnpj = "",
                Situacao = "",
                Abertura = "",
                Tipo = "",
                NaturezaJuridica = "",
                AtividadePrincipal = "",
                Logradouro = "",
                Numero = "",
                Complemento = "",
                Bairro = "",
                Municipio = "",
                Uf = "",
                Cep = ""
            };
        }

        [HttpGet("{cnpj}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Empresa>> PesquisarEmpresaReceitaWs(string cnpj)
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Empresa>> PesquisarEmpresaBanco(string cnpj)
        {
            var vo = new Empresa
            {
                Cnpj = cnpj
            };
            try
            {
                var result = await _empresaService.ObterDadosCnpjBanco(vo);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro interno: {ex.Message}");
            }
        }
    }
}
