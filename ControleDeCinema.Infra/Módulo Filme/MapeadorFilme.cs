
using ControleDeCinema.Domínio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleDeCinema.Infra.Compartilhado;
public class MapeadorFilme : IEntityTypeConfiguration<Filme>
{
    public void Configure(EntityTypeBuilder<Filme> builder)
    {
        builder.ToTable("TB_FILME");

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(x => x.Nome)
            .HasColumnType("varchar(150)")
            .IsRequired();

        builder.Property(x => x.Duracao)
            .HasColumnType("int")
            .IsRequired();

        builder.Property(x => x.CategoriaId)
            .HasColumnType("int")
            .IsRequired();

        builder.Property(x => x.CategoriaId)
            .HasColumnType("int")
            .IsRequired();
    }
}
