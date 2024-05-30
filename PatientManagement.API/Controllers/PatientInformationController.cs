using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Licenses;
using PatinetManagement.DataAccess.Domain;
using PatinetManagement.DataAccess.Enums;
using PatinetManagement.Infrastructure.Repositories;
using PatinetManagement.Utility.ViewModels;

namespace PatientManagement.API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class PatientInformationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public PatientInformationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> GetDiseaseList()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = await _unitOfWork.DiseaseInformation.GetAllAsync();
                    return Ok(data);
                }
                return NotFound("Disease List Not Found");
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return BadRequest($"There is Something Wrong ! Please Try Again. {ex.Message}");
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllergiesList()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = await _unitOfWork.Allergies.GetAllAsync();
                    return Ok(data);
                }
                return NotFound("Allergies List Not Found");
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return BadRequest($"There is Something Wrong ! Please Try Again. {ex.Message}");
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetNDCsList()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = await _unitOfWork.NCDs.GetAllAsync();
                    return Ok(data);
                }
                return NotFound("NDCs List Not Found");
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return BadRequest($"There is Something Wrong ! Please Try Again. {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> SavePatient([FromBody] PatientViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var patient = model.Patient;
                    var allergies = model.AllergiesDetails;
                    var ncds = model.NCD_Details;

                    string allergiesIds = string.Join(",", allergies.Select(a => a.AllergiesId));
                    string ncdsIds = string.Join(",", ncds.Select(n => n.NCDId));

                    var patientData = new Patientinformation
                    {
                        PatientName = patient.PatientName,
                        DiseaseInformationId = patient.DiseaseInformationId,
                        EpilepsyId = patient.EpilepsyId,
                        AllergiesDetailsId = allergiesIds,
                        NCDDetailsId = ncdsIds
                    };
                    await _unitOfWork.Patient.AddAsync(patientData);
                    await _unitOfWork.CommitAsync();

                    var MasterId = patientData.Id;

                    foreach (var ale in allergies)
                    {
                        var allergiesData = new Allergies_Details
                        {
                            PatientId = MasterId,
                            AllergiesId = ale.AllergiesId
                        };
                        await _unitOfWork.AllergiesDetails.AddAsync(allergiesData);
                    }
                    foreach (var ncd in ncds)
                    {
                        var ncdsData = new NCD_Details
                        {
                            PatientId = MasterId,
                            NCDId = ncd.NCDId
                        };
                        await _unitOfWork.NCDsDetails.AddAsync(ncdsData);
                    }
                    await _unitOfWork.CommitAsync();
                    return Ok(new
                    {
                        Message = "Patient Information Save Successfully."
                    });
                }
                return BadRequest(new
                {
                    Message = "There is Something Wrong ! Please Try Again."
                });
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return BadRequest($"There is Something Wrong ! Please Try Again. {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> PatientInformationList()
        {
            try
            {
                var patients = await _unitOfWork.Patient.GetAllAsync();
                var diseases = await _unitOfWork.DiseaseInformation.GetAllAsync();
                var ncdList = await _unitOfWork.NCDs.GetAllAsync();
                var allergiesList = await _unitOfWork.Allergies.GetAllAsync();

                var result = patients.Select(patient =>
                {
                    var disease = diseases.FirstOrDefault(d => d.Id == patient.DiseaseInformationId);
                    var epilepsy = ((Epilepsy)patient.EpilepsyId).ToString();
                    var ncdDetails = string.Join(", ", ncdList
                        .Where(ncd => patient.NCDDetailsId?.Split(',').Select(int.Parse).Contains(ncd.Id) ?? false)
                        .Select(ncd => ncd.NCDName));
                    var allergiesDetails = string.Join(", ", allergiesList
                        .Where(allergy => patient.AllergiesDetailsId?.Split(',').Select(int.Parse).Contains(allergy.Id) ?? false)
                        .Select(allergy => allergy.AllergiesName));

                    return new PatientViewModel
                    {
                        Id = patient.Id,
                        PatientName = patient.PatientName,
                        DiseaseName = disease?.DiseaseName,
                        EpilepsyName = epilepsy,
                        NCDDetails = ncdDetails,
                        AllergiesDetailses = allergiesDetails
                    };
                }).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return BadRequest($"There is Something Wrong ! Please Try Again. {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var getPatient = await _unitOfWork.Patient.GetAsync(x => x.Id == id);
                    if (getPatient != null)
                    {
                        _unitOfWork.Patient.Remove(getPatient);
                        await _unitOfWork.CommitAsync();
                        return Ok(new
                        {
                            Message = "Patient Information Update Successfully."
                        });
                    }
                    else
                    {
                        return NotFound($"Patient with ID {id} not found.");
                    }
                }

                return BadRequest("Invalid model state. Please check your input.");
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return BadRequest($"There is Something Wrong ! Please Try Again. {ex.Message}");
            }
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> UpdatePatient([FromBody] PatientViewModel model,int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var patient = model.Patient;
                    var allergies = model.AllergiesDetails;
                    var ncds = model.NCD_Details;

                    string allergiesIds = string.Join(",", allergies.Select(a => a.AllergiesId));
                    string ncdsIds = string.Join(",", ncds.Select(n => n.NCDId));

                    var existingPatient = await _unitOfWork.Patient.GetAsync(x => x.Id == id);
                    if (existingPatient == null)
                    {
                        return NotFound(new { Message = "Patient not found." });
                    }

                    existingPatient.PatientName = patient.PatientName;
                    existingPatient.DiseaseInformationId = patient.DiseaseInformationId;
                    existingPatient.EpilepsyId = patient.EpilepsyId;
                    existingPatient.NCDDetailsId = ncdsIds;
                    existingPatient.AllergiesDetailsId = allergiesIds;



                    var existingAllergies = await _unitOfWork.AllergiesDetails.GetAllAsync(x => x.PatientId == id);
                    var existingNCDs = await _unitOfWork.NCDsDetails.GetAllAsync(x => x.PatientId == id);

                    _unitOfWork.AllergiesDetails.RemoveRange(existingAllergies);
                    _unitOfWork.NCDsDetails.RemoveRange(existingNCDs);
                    
                    await _unitOfWork.CommitAsync();

                    foreach (var ale in allergies)
                    {
                        var allergiesData = new Allergies_Details
                        {
                            PatientId = id,
                            AllergiesId = ale.AllergiesId
                        };
                        await _unitOfWork.AllergiesDetails.AddAsync(allergiesData);
                    }
                    foreach (var ncd in ncds)
                    {
                        var ncdsData = new NCD_Details
                        {
                            PatientId = id,
                            NCDId = ncd.NCDId
                        };
                        await _unitOfWork.NCDsDetails.AddAsync(ncdsData);
                    }

                    await _unitOfWork.CommitAsync();

                    return Ok(new
                    {
                        Message = "Patient Information Updated Successfully."
                    });
                }
                return BadRequest(new
                {
                    Message = "There is Something Wrong! Please Try Again."
                });
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return BadRequest($"There is Something Wrong! Please Try Again. {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientInformation(int id)
        {
            try
            {
                var patient = await _unitOfWork.Patient.GetAsync(p => p.Id == id);
                if (patient == null)
                {
                    return NotFound(new { Message = "Patient not found." });
                }

                var disease = await _unitOfWork.DiseaseInformation.GetAsync(d => d.Id == patient.DiseaseInformationId);
                var ncdList = await _unitOfWork.NCDs.GetAllAsync();
                var allergiesList = await _unitOfWork.Allergies.GetAllAsync();

                var ncdDetails = ncdList
                    .Where(ncd => patient.NCDDetailsId?.Split(',').Select(int.Parse).Contains(ncd.Id) ?? false)
                    .Select(ncd => new NCDDetailViewModel { Id = ncd.Id, NCDName = ncd.NCDName })
                    .ToList();

                var allergiesDetails = allergiesList
                    .Where(allergy => patient.AllergiesDetailsId?.Split(',').Select(int.Parse).Contains(allergy.Id) ?? false)
                    .Select(allergy => new AllergyDetailViewModel { Id = allergy.Id, AllergiesName = allergy.AllergiesName })
                    .ToList();

                var patientViewModel = new PatientViewModel
                {
                    Id = patient.Id,
                    PatientName = patient.PatientName,
                    DiseaseName = disease?.DiseaseName,
                    EpilepsyName = ((Epilepsy)patient.EpilepsyId).ToString(),
                    NCDDetails = string.Join(", ", ncdDetails.Select(ncd => ncd.NCDName)),
                    AllergiesDetailses = string.Join(", ", allergiesDetails.Select(allergy => allergy.AllergiesName)),
                    NCDDetailList = ncdDetails,
                    AllergyDetailList = allergiesDetails
                };

                return Ok(patientViewModel);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return BadRequest($"There is Something Wrong! Please Try Again. {ex.Message}");
            }
        }

    }
}
