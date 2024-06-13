using HospitalApi.Exceptions;
using HospitalApi.Models;
using HospitalApi.Models.DTOs;
using HospitalApi.Service;
using Microsoft.AspNetCore.Mvc;

namespace HospitalApi.Controllers;

[Route("api/hospital")]
[ApiController]
public class HospitalController : ControllerBase
{
    private readonly IHospitalService _hospitalService;

    public HospitalController(IHospitalService hospitalService)
    {
        _hospitalService = hospitalService;
    }


    [HttpPost]
    public async Task<IActionResult> AddPrescription( AddPrescriptionDTO newPrescription)
    {
        try
        {
            bool result = await _hospitalService.AddPrescription(newPrescription);
            if (result)
            {
                return Ok("Prescription added successfully.");
            }
            return BadRequest("Failed to add prescription.");
        }
        catch (InvalidDateException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (ToManyMedicamentsException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }


    }

    [HttpGet("patients/{patientId}")]
    public async Task<IActionResult> GetPatientDetails(int patientId)
    {
        try
        {
            var patient = await _hospitalService.GetPatientDetailsAsync(patientId);
            return Ok(patient);
        }
        catch (PatientNotFoundException ex)
        {
            return NotFound("Patient not found");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }



}