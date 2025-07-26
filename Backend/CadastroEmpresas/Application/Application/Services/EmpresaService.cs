using CadastroEmpresas.Application.DTOs;
using CadastroEmpresas.Domain.Entities;
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

        public EmpresaService(HttpClient httpClient) 
        {
            _httpClient = httpClient;
        }

        public async Task<EmpresaDTO> ObterDadosCnpj(Empresa vo)
        {
            var url = $"https://www.receitaws.com.br/v1/cnpj/{vo.Cnpj}";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Erro ao consultar CNPJ: {response.ReasonPhrase}");
            }

            var json = await response.Content.ReadAsStringAsync();
            var empresa = JsonSerializer.Deserialize<EmpresaDTO>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return empresa;
        }
    }
}
