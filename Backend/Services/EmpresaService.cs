using System.Net.Http.Json;
using Backend.Models;

namespace Backend.Services;

public class EmpresaService
{
    private readonly HttpClient _http;

    public EmpresaService(HttpClient http)
    {
        _http = http;
        _http.BaseAddress = new Uri("https://www.receitaws.com.br/v1/");
    }

    public async Task<EmpresaResponse?> ConsultarEmpresa(string cnpj)
    {
        return await _http.GetFromJsonAsync<EmpresaResponse>($"cnpj/{cnpj}");
    }
}