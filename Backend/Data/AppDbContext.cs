using Backend.Models;
using Backend.Models.Usuario;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Empresa> Empresas { get; set; }
    public DbSet<AtividadePrincipal> AtividadesPrincipais { get; set; }
    public DbSet<UsuarioEmpresa> UsuariosEmpresas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nome).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Senha).HasMaxLength(100);
            
            entity.HasIndex(e => e.Email).IsUnique();
        });

        
        modelBuilder.Entity<Empresa>(entity =>
        {
            entity.HasKey(e => e.Cnpj);
            entity.Property(e => e.Nome).HasMaxLength(100);
            entity.Property(e => e.Fantasia).HasMaxLength(100);
            entity.Property(e => e.Situacao).HasMaxLength(50);
            entity.Property(e => e.Abertura).HasMaxLength(20);
            entity.Property(e => e.Tipo).HasMaxLength(50);
            entity.Property(e => e.Logradouro).HasMaxLength(100);
            entity.Property(e => e.Numero).HasMaxLength(20);
            entity.Property(e => e.Complemento).HasMaxLength(100);
            entity.Property(e => e.Bairro).HasMaxLength(100);
            entity.Property(e => e.Municipio).HasMaxLength(100);
            entity.Property(e => e.Uf).HasMaxLength(2);
            entity.Property(e => e.Cep).HasMaxLength(10);
            entity.Property(e => e.NaturezaJuridica).HasMaxLength(100);
            entity.Property(e => e.Ativo).HasDefaultValue(true);
            
            entity.HasIndex(e => e.Cnpj).IsUnique();
        });

        
        modelBuilder.Entity<AtividadePrincipal>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasMaxLength(20);
            entity.Property(e => e.Descricao).HasMaxLength(200);

            entity.HasOne(e => e.Empresa)
                .WithMany(e => e.AtividadePrincipal)
                .HasForeignKey(e => e.EmpresaCnpj)
                .OnDelete(DeleteBehavior.Cascade);
            
        });
        
        
        modelBuilder.Entity<UsuarioEmpresa>()
            .HasKey(ue => new { ue.UsuarioId, ue.EmpresaCnpj  });

        modelBuilder.Entity<UsuarioEmpresa>()
            .HasOne(ue => ue.Usuario)
            .WithMany(u => u.UsuarioEmpresas)
            .HasForeignKey(ue => ue.UsuarioId);

        modelBuilder.Entity<UsuarioEmpresa>()
            .HasOne(ue => ue.Empresa)
            .WithMany(e => e.UsuarioEmpresas)
            .HasForeignKey(ue => ue.EmpresaCnpj);
    }

}