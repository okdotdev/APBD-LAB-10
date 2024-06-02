using HospitalApi.Entities;
using HospitalApi.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PrescriptionsController : ControllerBase
{
    private readonly HospitalDbContext _context;

    public PrescriptionsController(HospitalDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> AddPrescription([FromBody] AddPrescriptionRequest request)
    {
        if (request.Medicaments.Count > 10)
        {
            return BadRequest("A prescription cannot include more than 10 medicaments.");
        }

        Patient patient = await _context.Patients.FindAsync(request.Patient.IdPatient) ??
                          new Patient
                          {
                              FirstName = request.Patient.FirstName,
                              LastName = request.Patient.LastName,
                              BirthDate = request.Patient.DateOfBirth
                          };

        foreach (MedicamentDto med in request.Medicaments)
        {
            if (!await _context.Medicaments.AnyAsync(m => m.IdMedicament == med.IdMedicament))
            {
                return BadRequest($"Medicament with Id {med.IdMedicament} does not exist.");
            }
        }

        if (request.DueDate < request.Date)
        {
            return BadRequest("DueDate must be greater than or equal to Date.");
        }

        Prescription prescription = new Prescription
        {
            Date = request.Date,
            DueDate = request.DueDate,
            Patient = patient,
            IdDoctor = request.Doctor.IdDoctor,
            PrescriptionMedicaments = request.Medicaments.Select(m => new PrescriptionMedicament
            {
                IdMedicament = m.IdMedicament,
                Dose = m.Dose,
                Details = m.Description
            }).ToList()
        };

        _context.Prescriptions.Add(prescription);
        await _context.SaveChangesAsync();

        return Ok(prescription);
    }
}