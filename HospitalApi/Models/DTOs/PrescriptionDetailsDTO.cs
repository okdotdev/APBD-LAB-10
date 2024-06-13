namespace HospitalApi.Models.DTOs;

public class PrescriptionDetailsDTO
{
    public int IdPrescription{ get; set; }
    public DateTime Date { get; set; }
    public virtual ICollection<MedicamentDetailDTO> Medicaments { get; set; } = new List<MedicamentDetailDTO>();
    public DoctorDetailDTO Doctor { get; set; }
}