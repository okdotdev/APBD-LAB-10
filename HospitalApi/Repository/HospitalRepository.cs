using System.Collections;
using HospitalApi.Entities;
using HospitalApi.Exceptions;
using HospitalApi.Models;
using HospitalApi.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace HospitalApi.Repository;

public class HospitalRepository : IHospitalRepository
{
    private readonly AppDbContext _appDbContext;

    public HospitalRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<bool> AddPrescription(AddPrescriptionDTO newPrescription)
    {


        if (newPrescription.DueDate < newPrescription.Date)
        {
            throw new InvalidDateException("Due date must be greater than or equal to the prescription date");
        }

        Patient? patient = await _appDbContext.Patients
            .FirstOrDefaultAsync(m => m.IdPatient == newPrescription.patient.IdPatient);


        if (patient == null)
        {
            patient = new Patient()
            {
                IdPatient = newPrescription.patient.IdPatient,
                FirstName = newPrescription.patient.FirstName,
                LastName = newPrescription.patient.LastName,
                BirthDate = newPrescription.patient.BirthDate
            };

            await _appDbContext.AddAsync(patient);
            await _appDbContext.SaveChangesAsync();
        }

        Prescription prescription = new Prescription
        {
            Date = newPrescription.Date,
            DueDate = newPrescription.DueDate,
            Patient = patient,

        };

        List<Medicament> medicaments = new List<Medicament>();
        List<Prescription_Medicament> prescriptionMedicaments = new List<Prescription_Medicament>();

        foreach (AddMedicamentDTO medicamentDto in newPrescription.medicaments)
        {
            Medicament? medicament = await _appDbContext.Medicaments
                .FirstOrDefaultAsync(m => m.IdMedicament == medicamentDto.IdMedicament);

            if (medicament == null)
            {
                throw new Exception($"Medicament with ID {medicamentDto.IdMedicament} does not exist.");
            }

            medicaments.Add(medicament);
            prescriptionMedicaments.Add(new Prescription_Medicament()
            {
                IdMedicament = medicament.IdMedicament,
                IdPrescription = prescription.IdPrescription,
                Dose = medicamentDto.Dose,
                Details = medicamentDto.Details
            });
        }

        if (medicaments.Count > 10)
        {
            throw new ToManyMedicamentsException("There can't be more than 10 medicaments in Prescription");
        }


        await _appDbContext.AddAsync(prescription);
        await _appDbContext.AddAsync(prescriptionMedicaments);
        await _appDbContext.SaveChangesAsync();

        return true;


    }

    public async Task<PatientDetailsDTO> GetPatientDetailsAsync(int patientId)
    {
        var patient = await _appDbContext.Patients
            .Include(p => p.Prescriptions)
            .ThenInclude(pr => pr.PrescriptionMedicaments)
            .FirstOrDefaultAsync(p => p.IdPatient == patientId);

        if (patient == null)
        {
            throw new PatientNotFoundException("Patient Not Found");
        }

        PatientDetailsDTO result = new PatientDetailsDTO()
        {
            IdPatient = patient.IdPatient,
            FirstName = patient.FirstName,
            LastName =  patient.LastName,
            BirthDay = patient.BirthDate,
        };

        List<Prescription> prescriptions = await _appDbContext.Prescriptions
            .Where(p => p.IdPatient == patientId)
            .OrderBy(p => p.DueDate)
            .ToListAsync();

        List<PrescriptionDetailsDTO> prescriptionDetails = new List<PrescriptionDetailsDTO>();

        foreach (Prescription prescription in prescriptions)
        {
            List<Prescription_Medicament> prescriptionMedicaments = await _appDbContext.PrescriptionMedicaments
                .Where(p => p.IdPrescription == prescription.IdPrescription)
                .ToListAsync();

            Doctor? doctor = await _appDbContext.Doctors.FirstOrDefaultAsync(d => d.IdDoctor == prescription.IdDoctor);

            List<MedicamentDetailDTO> medicamentDetailDtos =  new List<MedicamentDetailDTO>();

            foreach (Prescription_Medicament prescriptionMedicament in prescriptionMedicaments)
            {
                var medicament = await _appDbContext.Medicaments
                    .FirstOrDefaultAsync(p => p.IdMedicament == prescriptionMedicament.IdMedicament);

                medicamentDetailDtos.Add(new MedicamentDetailDTO()
                {
                    IdMedicament = medicament.IdMedicament,
                    Name = medicament.Name,
                    Description = medicament.Description,
                    Dose = prescriptionMedicament.Dose
                });
            }


            prescriptionDetails.Add(new PrescriptionDetailsDTO()
            {
                IdPrescription = prescription.IdPrescription,
                Date = prescription.Date,
                Doctor = new DoctorDetailDTO()
                {
                    IdDoctor = doctor.IdDoctor,
                    FirstName = doctor.FirstName
                },
                Medicaments = medicamentDetailDtos
            });

        }

        return result;
    }




}