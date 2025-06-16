using ControleDeCinema.Domínio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleDeCinema.Infra.Módulo_Assento;
public class MapeamentoAssento : IEntityTypeConfiguration<Assento>
{
    public void Configure(EntityTypeBuilder<Assento> builder)
    {
        builder.ToTable("TB_ASSENTOS");

        builder.Property(x => x.Id)
            .IsRequired()
            .HasColumnType("int")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Numero)
            .HasColumnType("varchar(10)")
            .IsRequired();

        builder.Property(x => x.Disponivel)
            .HasColumnType("bit")
            .IsRequired();

        builder.Property(x => x.SessaoId)
            .HasColumnType("int")
            .IsRequired();

        builder.HasOne(x => x.Sessao)
            .WithMany(x => x.Assentos)
            .HasForeignKey(x => x.SessaoId);

        builder.HasOne(x => x.Usuario)
            .WithMany()
            .HasForeignKey("Usuario_Id")
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }
}
