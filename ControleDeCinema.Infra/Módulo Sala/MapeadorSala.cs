using ControleDeCinema.Domínio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleDeCinema.Infra.Compartilhado;
public class MapeadorSala : IEntityTypeConfiguration<Sala>
{
    public void Configure(EntityTypeBuilder<Sala> builder)
    {
        builder.ToTable("TB_SALAS");

        builder.Property(s => s.Id)
            .IsRequired()
            .HasColumnType("int")
            .ValueGeneratedOnAdd();

        builder.Property(s => s.Capacidade)
            .IsRequired()
            .HasColumnType("int");

        builder.Property(s => s.Nome)
            .IsRequired()
            .HasColumnType("varchar(50)");

        builder.Property(s => s.Disponivel)
            .IsRequired()
            .HasColumnType("bit");

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