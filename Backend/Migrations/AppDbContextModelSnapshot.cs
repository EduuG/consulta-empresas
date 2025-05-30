﻿// <auto-generated />
using System;
using Backend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Backend.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Backend.Models.AtividadePrincipal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20)
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descricao")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasAnnotation("Relational:JsonPropertyName", "text");

                    b.Property<string>("EmpresaCnpj")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("EmpresaCnpj");

                    b.ToTable("AtividadesPrincipais");
                });

            modelBuilder.Entity("Backend.Models.Empresa", b =>
                {
                    b.Property<string>("Cnpj")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Abertura")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<bool>("Ativo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<string>("Bairro")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Cep")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Complemento")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Fantasia")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Logradouro")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Municipio")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("NaturezaJuridica")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Nome")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Numero")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Situacao")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Tipo")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Uf")
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.HasKey("Cnpj");

                    b.HasIndex("Cnpj")
                        .IsUnique();

                    b.ToTable("Empresas");
                });

            modelBuilder.Entity("Backend.Models.Usuario.Usuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("UsuarioEmpresa", b =>
                {
                    b.Property<Guid>("UsuarioId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EmpresaCnpj")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UsuarioId", "EmpresaCnpj");

                    b.HasIndex("EmpresaCnpj");

                    b.ToTable("UsuariosEmpresas");
                });

            modelBuilder.Entity("Backend.Models.AtividadePrincipal", b =>
                {
                    b.HasOne("Backend.Models.Empresa", "Empresa")
                        .WithMany("AtividadePrincipal")
                        .HasForeignKey("EmpresaCnpj")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Empresa");
                });

            modelBuilder.Entity("UsuarioEmpresa", b =>
                {
                    b.HasOne("Backend.Models.Empresa", "Empresa")
                        .WithMany("UsuarioEmpresas")
                        .HasForeignKey("EmpresaCnpj")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Backend.Models.Usuario.Usuario", "Usuario")
                        .WithMany("UsuarioEmpresas")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Empresa");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Backend.Models.Empresa", b =>
                {
                    b.Navigation("AtividadePrincipal");

                    b.Navigation("UsuarioEmpresas");
                });

            modelBuilder.Entity("Backend.Models.Usuario.Usuario", b =>
                {
                    b.Navigation("UsuarioEmpresas");
                });
#pragma warning restore 612, 618
        }
    }
}
