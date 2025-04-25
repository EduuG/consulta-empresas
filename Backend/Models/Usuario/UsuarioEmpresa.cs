using Backend.Models;
using Backend.Models.Usuario;

public class UsuarioEmpresa
{
    public Guid UsuarioId { get; set; }
    public Usuario Usuario { get; set; }
    
    public string EmpresaCnpj { get; set; }
    public Empresa Empresa { get; set; }
}