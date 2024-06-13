namespace HospitalApi.Models.DTOs;

public class AddMedicamentDTO
{
    public int IdMedicament { get; set; }
    public int Dose { get; set; }
    public string Description { get; set; }
    public string Details { get; set; }
}