using HospitalApi.Models.DTOs;

namespace HospitalApi.Service;

public interface IHospitalService
{
    Task<bool> AddPrescription(AddPrescriptionDTO newPrescription);
    Task<PatientDetailsDTO> GetPatientDetailsAsync(int patientId);
}