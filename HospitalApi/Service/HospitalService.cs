using HospitalApi.Exceptions;
using HospitalApi.Models.DTOs;
using HospitalApi.Repository;

namespace HospitalApi.Service;

public class HospitalService : IHospitalService
{
    private readonly IHospitalRepository _hospitalRepository;

    public HospitalService(IHospitalRepository hospitalRepository)
    {
        _hospitalRepository = hospitalRepository;
    }

    public async Task<bool> AddPrescription(AddPrescriptionDTO newPrescription)
    {
        return await _hospitalRepository.AddPrescription(newPrescription);
    }

    public async Task<PatientDetailsDTO> GetPatientDetailsAsync(int patientId)
    {
        return await _hospitalRepository.GetPatientDetailsAsync(patientId);
    }
}