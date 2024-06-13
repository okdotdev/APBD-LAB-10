using HospitalApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalApi.Entities.Config;

public class PrescriptionEFConfig : IEntityTypeConfiguration<Prescription>
{
    public void Configure(EntityTypeBuilder<Prescription> builder)
    {
        builder.HasKey(e => e.IdPrescription);

        builder.Property(e => e.IdPrescription).UseIdentityColumn();
        builder.Property(e => e.Date).IsRequired();
        builder.Property(e => e.DueDate).IsRequired();

        builder.Property(e => e.IdDoctor).IsRequired();
        builder.Property(e => e.IdPatient).IsRequired();


        builder.HasOne(e => e.Doctor)
            .WithMany(a => a.Prescriptions)
            .HasConstraintName("Prescription_Doctor")
            .HasForeignKey(e => e.IdDoctor)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Patient)
            .WithMany(a => a.Prescriptions)
            .HasConstraintName("Prescription_Patient")
            .HasForeignKey(e => e.IdPatient)
            .OnDelete(DeleteBehavior.Restrict);


        builder.ToTable("Prescription");
    }
}