using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Backend.Data;
using Backend.Models.Usuario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _config;

    public UsuarioController(AppDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    [HttpPost("cadastro")]
    public async Task<IActionResult> Cadastrar([FromBody] UsuarioRequest request)
    {
        
        // Verificação de campos vazios
        if (string.IsNullOrWhiteSpace(request.Nome) ||
            string.IsNullOrWhiteSpace(request.Email) ||
            string.IsNullOrWhiteSpace(request.Senha))
        {
            return BadRequest("Todos os campos são obrigatórios.");
        }
        
        // Verifica se o e-mail já existe
        var existe = await _context.Usuarios
            .AnyAsync(u => u.Email.ToLower() == request.Email.ToLower());

        if (existe)
        {
            return Conflict("Não foi possível cadastrar com esse e-mail.");
        }
        
        var usuario = new Usuario(request.Nome, request.Email, BCrypt.Net.BCrypt.HashPassword(request.Senha));
        
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, new {
            usuario.Id,
            usuario.Nome,
            usuario.Email
        });
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == request.Email);
        
        if (usuario == null || !BCrypt.Net.BCrypt.Verify(request.Senha, usuario.Senha))
            return Unauthorized("Email ou senha inválidos.");

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Name, usuario.Nome),
            new Claim(ClaimTypes.Email, usuario.Email)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return Ok(new { token = tokenString });
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUsuario(Guid id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario is null) return NotFound();
        
        return Ok(usuario);
    }
    
    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetUsuarioInfo()
    {
        string? usuarioId = User.Claims.FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(usuarioId))
        {
            return Unauthorized("Usuário não autenticado.");
        }

        var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == Guid.Parse(usuarioId));

        if (usuario == null)
        {
            return NotFound("Usuário não encontrado.");
        }

        return Ok(new
        {
            nome = usuario.Nome,
            email = usuario.Email
        });
    }
}