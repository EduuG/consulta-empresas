using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Backend.Models;

public class Empresa
{
    public string Cnpj { get; private set; }
    
    public string? Nome { get; private set; }
    public string? Fantasia { get; private set; }
    public string? Situacao { get; private set; }
    public string? Abertura { get; private set; }
    public string? Tipo { get; private set; }
    public string? Logradouro { get; private set; }
    public string? Numero { get; private set; }
    public string? Complemento { get; private set; }
    public string? Bairro { get; private set; }
    public string? Municipio { get; private set; }
    public string? Uf { get; private set; }
    public string? Cep { get; private set; }
    public string? NaturezaJuridica { get; private set; }
    public bool Ativo { get; private set; }
    
    [JsonIgnore]
    public List<AtividadePrincipal>? AtividadePrincipal { get; private set; }
    
    [JsonIgnore]
    public List<UsuarioEmpresa> UsuarioEmpresas { get; set; } = new();
    
    public Empresa() 
    {
        Cnpj = string.Empty;
        AtividadePrincipal = new List<AtividadePrincipal>();
    }

    public Empresa(string cnpj, string nome, string fantasia, string situacao, string abertura, string tipo, string logradouro, 
        string numero, string complemento, string bairro, string municipio, string uf, string cep, string naturezaJuridica, 
        List<AtividadePrincipal> atividadePrincipal)
    {
        Cnpj = cnpj;
        Nome = nome;
        Fantasia = fantasia;
        Situacao = situacao;
        Abertura = abertura;
        Tipo = tipo;
        Logradouro = logradouro;
        Numero = numero;
        Complemento = complemento;
        Bairro = bairro;
        Municipio = municipio;
        Uf = uf;
        Cep = cep;
        NaturezaJuridica = naturezaJuridica;
        AtividadePrincipal = atividadePrincipal;
        Ativo = true;
    }
}