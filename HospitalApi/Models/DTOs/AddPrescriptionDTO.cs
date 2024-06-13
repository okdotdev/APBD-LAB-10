namespace HospitalApi.Models.DTOs;

public class AddPrescriptionDTO
{
    public AddPatientDTO patient  { get; set; }
    public virtual ICollection<AddMedicamentDTO> medicaments { get; set; } = new List<AddMedicamentDTO>();
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
}