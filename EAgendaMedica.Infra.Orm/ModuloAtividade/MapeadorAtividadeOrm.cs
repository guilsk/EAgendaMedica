using EAgendaMedica.Dominio.ModuloAtividade;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EAgendaMedica.Infra.Orm.ModuloAtividade
{
    public class MapeadorAtividadeOrm : IEntityTypeConfiguration<Atividade>
    {
        public void Configure(EntityTypeBuilder<Atividade> builder) {

            builder.ToTable("TBAtividade");

            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Descricao).IsRequired();
            builder.Property(x => x.Data).IsRequired();
            builder.Property(x => x.HoraInicio).IsRequired();
            builder.Property(x => x.HoraFim).IsRequired();
            builder.Property(x => x.TipoAtividade).HasConversion<int>().IsRequired();
            builder.HasMany(x => x.Medicos);
        }
    }
}
