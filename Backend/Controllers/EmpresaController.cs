using System.Security.Claims;
using System.Text.RegularExpressions;
using Backend.Data;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmpresaController : ControllerBase
{
    private readonly EmpresaService _empresaService;
    private readonly AppDbContext _context;

    public EmpresaController(EmpresaService empresaService, AppDbContext context)
    {
        _empresaService = empresaService;
        _context = context;
    }

    private string? ObterIdUsuario() =>
        User.Claims.FirstOrDefault(i => i.Type == ClaimTypes.NameIdentifier)?.Value;

    private async Task<bool> UsuarioJaAssociado(Guid usuarioId, string cnpj) =>
        await _context.UsuariosEmpresas
            .AnyAsync(ue => ue.UsuarioId == usuarioId && ue.EmpresaCnpj == cnpj);

    [Authorize]
    [HttpGet("{cnpj}")]
    public async Task<IActionResult> Cadastrar(string cnpj)
    {
        string cnpjLimpo = Regex.Replace(cnpj, @"\D", "");
        
        var empresaExistente = await _context.Empresas
            .Include(e => e.AtividadePrincipal)
            .FirstOrDefaultAsync(e => e.Cnpj == cnpjLimpo);

        if (empresaExistente != null)
        {
            var usuarioId = ObterIdUsuario();

            if (string.IsNullOrEmpty(usuarioId))
            {
                return Unauthorized("Usuário não autenticado.");
            }

            if (await UsuarioJaAssociado(Guid.Parse(usuarioId), empresaExistente.Cnpj))
            {
                return Conflict("Empresa já cadastrada.");
            }

            var usuarioEmpresa = new UsuarioEmpresa
            {
                UsuarioId = Guid.Parse(usuarioId),
                EmpresaCnpj = empresaExistente.Cnpj
            };

            _context.UsuariosEmpresas.Add(usuarioEmpresa);
            await _context.SaveChangesAsync();

            return Ok(empresaExistente);
        }

        var empresaResponse = await _empresaService.ConsultarEmpresa(cnpjLimpo);
        if (empresaResponse == null)
        {
            return BadRequest("Erro ao consultar empresa.");
        }

        var novaEmpresa = new Empresa(
            cnpjLimpo,
            empresaResponse.Nome,
            empresaResponse.Fantasia,
            empresaResponse.Situacao,
            empresaResponse.Abertura,
            empresaResponse.Tipo,
            empresaResponse.Logradouro,
            empresaResponse.Numero,
            empresaResponse.Complemento,
            empresaResponse.Bairro,
            empresaResponse.Municipio,
            empresaResponse.Uf,
            empresaResponse.Cep,
            empresaResponse.NaturezaJuridica,
            empresaResponse.AtividadePrincipal
        );

        var usuarioIdNovo = ObterIdUsuario();
        if (!string.IsNullOrEmpty(usuarioIdNovo))
        {
            var usuarioEmpresaNovo = new UsuarioEmpresa
            {
                UsuarioId = Guid.Parse(usuarioIdNovo),
                EmpresaCnpj = novaEmpresa.Cnpj
            };

            _context.UsuariosEmpresas.Add(usuarioEmpresaNovo);
        }

        _context.Empresas.Add(novaEmpresa);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(Cadastrar), new { cnpj = novaEmpresa.Cnpj }, novaEmpresa);
    }

    [Authorize]
    [HttpGet("listarEmpresas")]
    public async Task<IActionResult> Listar()
    {
        Guid usuarioId = Guid.Parse(User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value);

        var listaEmpresas = await _context.UsuariosEmpresas
            .Where(u => u.UsuarioId == usuarioId)
            .Include(u => u.Empresa)
            .ThenInclude(e => e.AtividadePrincipal)
            .Select(u => u.Empresa)
            .ToListAsync();

        var empresasResponse = listaEmpresas.Select(e => new 
        {
            e.Cnpj,
            e.Nome,
            e.Fantasia,
            e.Situacao,
            e.Abertura,
            e.NaturezaJuridica,
            e.Bairro,
            e.Cep,
            e.Complemento,
            e.Logradouro,
            e.Municipio,
            e.Numero,
            e.Tipo,
            e.Uf,
            AtividadePrincipal = e.AtividadePrincipal?.Select(a => new { descricao = a.Descricao }).ToList()
        }).ToList();

        return Ok(empresasResponse);
    }
}