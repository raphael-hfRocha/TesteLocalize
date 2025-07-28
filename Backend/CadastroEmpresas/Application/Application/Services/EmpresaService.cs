using CadastroEmpresas.Domain.Entities;
using CadastroEmpresas.src.Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Services
{
    public class EmpresaService
    {
        private readonly HttpClient _httpClient;
        private readonly EmpresaRepository _empresaRepository;

        public EmpresaService(HttpClient httpClient, EmpresaRepository empresaRepository)
        {
            _httpClient = httpClient;
            _empresaRepository = empresaRepository;
        }

        public async Task<Empresa> ObterDadosCnpjReceitaWs(Empresa vo)
        {
            var url = $"https://www.receitaws.com.br/v1/cnpj/{vo.Cnpj}";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Erro ao consultar CNPJ: {response.ReasonPhrase}");
            }

            var json = await response.Content.ReadAsStringAsync();
            var empresa = JsonSerializer.Deserialize<Empresa>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            
            if (empresa == null)
            {
                throw new Exception("Não foi possível deserializar os dados da empresa da API.");
            }
            
            return empresa;
        }

        public Empresa CadastrarEmpresa(Empresa empresa)
        {
            if (empresa == null)
            {
                throw new ArgumentNullException(nameof(empresa), "Dados da empresa inválidos.");
            }

            var retorno = _empresaRepository.CadastrarEmpresa(empresa);

            return retorno;
        }

        public async Task<Empresa?> ObterDadosCnpjBanco(Empresa empresa)
        {
            if (empresa.Cnpj == null)
            {
                throw new ArgumentNullException(nameof(empresa.Cnpj), "CNPJ não pode ser nulo.");
            }

            var retorno = await _empresaRepository.ObterDadosCnpjBanco(empresa);

            return retorno;
        }

        public async Task<List<Empresa>> ObterEmpresasPorUsuario(Int32 idUsuario)
        {
            if (idUsuario <= 0)
            {
                throw new ArgumentException("ID do usuário deve ser maior que zero.", nameof(idUsuario));
            }

            return await _empresaRepository.ObterEmpresasPorUsuario(idUsuario);
        }

        public async Task<int> ContarEmpresasTotal()
        {
            return await _empresaRepository.ContarEmpresasTotal();
        }

        public async Task<List<Empresa>> ObterTodasEmpresas()
        {
            return await _empresaRepository.ObterTodasEmpresas();
        }
    }
}
