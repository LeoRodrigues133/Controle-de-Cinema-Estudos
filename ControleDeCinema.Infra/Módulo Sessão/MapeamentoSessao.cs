using ControleDeCinema.Domínio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleDeCinema.Infra.Módulo_Sessão;
public class MapeamentoSessao : IEntityTypeConfiguration<Sessão>
{
    public void Configure(EntityTypeBuilder<Sessão> builder)
    {
        builder.ToTable("TB_SESSAO");

        builder.Property(x => x.Id)
            .IsRequired()
            .HasColumnType("int")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Finalizada)
            .HasColumnType("bit")
            .IsRequired();

        builder.Property(x => x.FilmeId)
            .HasColumnType("int")
            .IsRequired();

        builder.HasOne(x => x.Filme)
            .WithMany()
            .HasForeignKey(x => x.FilmeId);

        builder.HasOne(x => x.Sala)
            .WithMany()
            .HasForeignKey(x => x.SalaId);

        builder.Property(x => x.SalaId)
            .HasColumnType("int")
            .IsRequired();

        builder.Property(x => x.DataDeExibicao)
            .HasColumnType("datetime2")
            .IsRequired();

        builder.Property(x => x.HorarioDaSessao)
            .HasColumnType("time")
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
