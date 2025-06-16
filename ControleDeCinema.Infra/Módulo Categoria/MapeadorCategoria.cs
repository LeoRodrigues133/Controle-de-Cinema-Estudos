using ControleDeCinema.Domínio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleDeCinema.Infra.Compartilhado;
internal class MapeadorCategoria : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.ToTable("TB_CATEGORIA");

        builder.Property(x => x.Id)
            .HasColumnType("int")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Nome)
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.Property(s => s.EmpresaId)
            .HasColumnType("int")
            .HasColumnName("Empresa_Id")
            .IsRequired();

        builder.HasOne(g => g.Empresa)
            .WithMany()
            .HasForeignKey(s => s.EmpresaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}