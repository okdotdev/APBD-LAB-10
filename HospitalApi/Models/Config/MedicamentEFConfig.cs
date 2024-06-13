using HospitalApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalApi.Models.Config;

public class MedicamentEFConfig : IEntityTypeConfiguration<Medicament>
{
    public void Configure(EntityTypeBuilder<Medicament> builder)
    {
        builder.HasKey(e => e.IdMedicament);
        builder.Property(e => e.IdMedicament).UseIdentityColumn();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Description).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Type).IsRequired().HasMaxLength(100);
        builder.ToTable("Medicament");
    }
}