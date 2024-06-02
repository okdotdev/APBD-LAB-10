namespace HospitalApi.Entities.DTOs;

public class GetPatientResponse
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<PrescriptionDto> Prescriptions { get; set; }
}