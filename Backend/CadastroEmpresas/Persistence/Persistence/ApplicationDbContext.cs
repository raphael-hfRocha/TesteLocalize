using CadastroEmpresas.Domain.Entities;
using CadastroEmpresas.src.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
    {
    }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Empresa> Empresas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Empresa>(entity =>
        {
            entity.ToTable("EMPRESA");
            entity.HasKey(e => e.IdEmpresa);
            entity.Property(e => e.IdEmpresa).HasColumnName("EMPRESA_ID");
            entity.Property(e => e.NomeEmpresarial).HasColumnName("EMPRESA_NOME_EMPRESARIAL").HasMaxLength(40);
            entity.Property(e => e.NomeFantasia).HasColumnName("EMPRESA_NOME_FANTASIA").HasMaxLength(40);
            entity.Property(e => e.Cnpj).HasColumnName("EMPRESA_CNPJ").HasMaxLength(18);
            entity.Property(e => e.Situacao).HasColumnName("EMPRESA_SITUACAO").HasMaxLength(40);
            entity.Property(e => e.Abertura).HasColumnName("EMPRESA_ABERTURA").HasMaxLength(8);
            entity.Property(e => e.Tipo).HasColumnName("EMPRESA_TIPO").HasMaxLength(20);
            entity.Property(e => e.NaturezaJuridica).HasColumnName("EMPRESA_NATUREZA_JURIDICA").HasMaxLength(255);
            entity.Property(e => e.AtividadePrincipal).HasColumnName("EMPRESA_ATIVIDADE_PRINCIPAL").HasMaxLength(100);
            entity.Property(e => e.Logradouro).HasColumnName("EMPRESA_LOGRADOURO").HasMaxLength(100);
            entity.Property(e => e.Numero).HasColumnName("EMPRESA_NUMERO");
            entity.Property(e => e.Complemento).HasColumnName("EMPRESA_COMPLEMENTO").HasMaxLength(250);
            entity.Property(e => e.Bairro).HasColumnName("EMPRESA_BAIRRO").HasMaxLength(40);
            entity.Property(e => e.Municipio).HasColumnName("EMPRESA_MUNICIPIO").HasMaxLength(40);
            entity.Property(e => e.Uf).HasColumnName("EMPRESA_UF").HasMaxLength(2);
            entity.Property(e => e.Cep).HasColumnName("EMPRESA_CEP").HasMaxLength(8);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("USUARIO");
            entity.HasKey(u => u.IdUsuario);
            entity.Property(u => u.IdUsuario).HasColumnName("USUARIO_ID");
            entity.Property(u => u.Nome).HasColumnName("USUARIO_NOME").HasMaxLength(100);
            entity.Property(u => u.Email).HasColumnName("USUARIO_EMAIL").HasMaxLength(50);
            entity.Property(u => u.Senha).HasColumnName("USUARIO_SENHA").HasMaxLength(20);
        });

        base.OnModelCreating(modelBuilder);
    }
}