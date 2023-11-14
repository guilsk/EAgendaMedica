using EAgendaMedica.Dominio.ModuloMedico;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EAgendaMedica.Infra.Orm.ModuloMedico {
    public class MapeadorMedicoOrm : IEntityTypeConfiguration<Medico> {
        public void Configure(EntityTypeBuilder<Medico> builder) {

            builder.ToTable("TBMedico");

            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.Nome).IsRequired();
            builder.Property(x => x.Crm).IsRequired();
            builder.HasMany(x => x.Atividades);

        }
    }
}
