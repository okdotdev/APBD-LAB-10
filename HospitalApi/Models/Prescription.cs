using HospitalApi.Entities;

namespace HospitalApi.Models;

public class Prescription
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public int IdPatient { get; set; }
    public Patient Patient { get; set; }
    public int IdDoctor { get; set; }
    public Doctor Doctor { get; set; }

    public virtual ICollection<Prescription_Medicament> PrescriptionMedicaments { get; set; } =
        new List<Prescription_Medicament>();
}