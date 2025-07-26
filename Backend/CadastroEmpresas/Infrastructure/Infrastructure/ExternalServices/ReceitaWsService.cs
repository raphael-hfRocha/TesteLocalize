using CadastroEmpresas.Application.DTOs;
using System.Text.Json;

namespace CadastroEmpresas.src.Infrastructure.ExternalServices
{
    public class ReceitaWsService
    {
        private readonly HttpClient _httpClient;

        public ReceitaWsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<EmpresaDTO> ObterDadosCnpjAsync(string cnpj)
        {
            var url = $"https://www.receitaws.com.br/v1/cnpj/{cnpj}";
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
