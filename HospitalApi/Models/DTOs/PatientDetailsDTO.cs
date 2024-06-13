namespace HospitalApi.Models.DTOs;

public class PatientDetailsDTO
{
    public int IdPatient  { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDay { get; set; }
    public virtual ICollection<PrescriptionDetailsDTO> Prescription { get; set; } = new List<PrescriptionDetailsDTO>();

}