using System.Text.Json.Serialization;

namespace Backend.Models;

public class AtividadePrincipal
{
    public Guid Id { get; set; }
    
    [JsonPropertyName("text")]
    public string? Descricao  {get; set;}
    
    public string EmpresaCnpj {get; set;} = String.Empty;
    
    [JsonIgnore]
    public Empresa Empresa { get; set; } = new();

    public AtividadePrincipal()
    {
        Id = Guid.NewGuid();
    }
}