using BCrypt.Net;

namespace Backend.Models.Usuario;

public class Usuario
{
    public Guid Id { get; init; }
    
    public string Nome { get; private set; }
    public string Email { get; private set; }
    public string Senha { get; private set; }
    
    
    public List<UsuarioEmpresa> UsuarioEmpresas { get; set; } = new();
    
    public Usuario(string nome, string email, string senha)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Email = email;
        Senha = senha;
    }
}