using Application.Services;
using CadastroEmpresas.Domain.Entities;
using CadastroEmpresas.Persistence;
using CadastroEmpresas.src.Infrastructure.ExternalServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CadastroEmpresas.src.API.Controllers
{
    [ApiController]
    [Route("api/controller")]
    [Authorize]
    public class EmpresaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private ReceitaWsService _receitaWsService;
        private IServiceProvider _serviceProvider;

        public EmpresaController()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddApplicationServices();
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        /*[HttpGet("ReturnClass")]
        public ActionResult<Empresa> ReturnClass()
        {
            Empresa empresa = new Empresa();

            InicializeObject i = new InicializeObject();
            var retorno = (Empresa)i.Inicialize(empresa);
            return Ok(retorno);
        }*/

        [HttpGet("SearchEmpresaByCnpj")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Empresa> PesquisarEmpresa(String cnpj)
        {
            var vo = new Empresa
            {
                Cnpj = cnpj
            };

            try
            {
                var empresa = _serviceProvider.GetService<EmpresaService>();
                var resultado = empresa.ObterDadosCnpj(vo);
                return Ok(empresa);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro interno: {ex.Message}");
            }
        }
    }
}