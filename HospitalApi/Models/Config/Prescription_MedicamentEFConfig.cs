using HospitalApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalApi.Models.Config;

public class PrescriptionMedicamentEfConfig : IEntityTypeConfiguration<Prescription_Medicament>
{
    public void Configure(EntityTypeBuilder<Prescription_Medicament> builder)
    {
        builder.HasKey(e => new {e.IdPrescription, e.IdMedicament});

        builder.Property(e => e.IdPrescription).UseIdentityColumn();

        builder.Property(e => e.Details).IsRequired().HasMaxLength(100);

        builder.Property(e => e.IdPrescription).IsRequired();
        builder.Property(e => e.IdMedicament).IsRequired();


        builder.HasOne(e => e.Prescription)
            .WithMany(a => a.PrescriptionMedicaments)
            .HasConstraintName("Prescription_Medicament_Prescription")
            .HasForeignKey(e => e.IdPrescription)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Medicament)
            .WithMany(a => a.PrescriptionMedicaments)
            .HasConstraintName("Prescription_Medicament_Medicament")
            .HasForeignKey(e => e.IdMedicament)
            .OnDelete(DeleteBehavior.Restrict);


        builder.ToTable("Prescription_Medicament");
    }
}