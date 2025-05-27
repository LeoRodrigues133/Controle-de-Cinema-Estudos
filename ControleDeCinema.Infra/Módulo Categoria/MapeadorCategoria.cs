using ControleDeCinema.Domínio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleDeCinema.Infra.Compartilhado;
internal class MapeadorCategoria : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.ToTable("TB_CATEGORIA");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnType("int")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Nome)
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.Property(x => x.FilmeId)
            .HasColumnType("int");

        builder.HasMany(x => x.Filmes)
            .WithMany(f => f.Categorias)
            .UsingEntity(y =>
                y.ToTable("TB_CATEGORIA_FILME"));
    }
}