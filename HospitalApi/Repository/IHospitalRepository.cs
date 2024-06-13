using HospitalApi.Models.DTOs;

namespace HospitalApi.Repository;

public interface IHospitalRepository
{
    Task<bool> AddPrescription(AddPrescriptionDTO newPrescription);
    Task<PatientDetailsDTO> GetPatientDetailsAsync(int patientId);
}