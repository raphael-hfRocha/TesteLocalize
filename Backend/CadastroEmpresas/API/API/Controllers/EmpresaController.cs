using Application.Services;
using CadastroEmpresas.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
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
                var result = await _empresaService.ObterDadosCnpjBanco(vo);
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
        public ActionResult<Empresa> Post(Empresa vo)
        {
            var empresa = new Empresa
            {
                NomeEmpresarial = "Nome Empresarial Exemplo",
                NomeFantasia = "Nome Fantasia Exemplo",
                Cnpj = vo.Cnpj,
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
                Usuario = {
                     IdUsuario = vo.Usuario.IdUsuario
                },
                IdUsuario = vo.Usuario.IdUsuario
                };

            try
            {
                var result = _empresaService.CadastrarEmpresa(empresa);
                return Created($"/api/Empresa/{result.IdEmpresa}", result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao cadastrar empresa: {ex.Message}");
            }
        }

        [HttpGet("SearchEmpresasUsuario")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> ObterMinhasEmpresas(Int32 idUsuario)
        {
            try
            {
                var empresas = await _empresaService.ObterEmpresasPorUsuario(idUsuario);
                return Ok(empresas);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao cadastrar empresa: {ex.Message}");
            }
        }
    }
}