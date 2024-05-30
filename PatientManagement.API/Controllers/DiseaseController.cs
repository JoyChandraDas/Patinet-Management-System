using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatinetManagement.DataAccess.Domain;
using PatinetManagement.Infrastructure.Repositories;

namespace PatientManagement.API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class DiseaseController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public DiseaseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllDisease()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var diseaseList = await _unitOfWork.DiseaseInformation.GetAllAsync();
                    if (diseaseList != null && diseaseList.Any())
                    {
                        var list = new List<DiseaseInformation>();

                        foreach (var des in diseaseList)
                        {
                            var diseaseResponse = new DiseaseInformation
                            {
                                Id = des.Id,
                                DiseaseName = des.DiseaseName
                            };
                            list.Add(diseaseResponse);
                        }
                        return Ok(list);
                    }
                    else
                    {
                        return BadRequest("Disease List List Not Found");
                    }
                }
                else
                {
                    return UnprocessableEntity("Invalid model state. Please check your input.");
                }
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return BadRequest($"There is Something Wrong ! Please Try Again. {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDisease(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var disease = await _unitOfWork.DiseaseInformation.GetAsync(x => x.Id == id);
                    if (disease != null)
                    {
                        var diseaseResponse = new DiseaseInformation
                        {
                            Id = disease.Id,
                            DiseaseName = disease.DiseaseName
                        };
                        return Ok(diseaseResponse);
                    }
                    else
                    {
                        NotFound($"Disease with this {id} not Found.");
                    }
                }
                return BadRequest("There is Something Wrong ! Please Try Again.");
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return BadRequest($"There is Something Wrong ! Please Try Again. {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveDisease(DiseaseInformation disease)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var des = new DiseaseInformation
                    {
                        DiseaseName = disease.DiseaseName
                    };
                    await _unitOfWork.DiseaseInformation.AddAsync(des);
                    await _unitOfWork.CommitAsync();
                    return Ok(new
                    {
                        Message = "Disease Information Save Successfully."
                    });
                }

                return BadRequest(new
                {
                    Message = "There is Something Wrong ! Please Try Again"
                });
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return BadRequest($"There is Something Wrong ! Please Try Again. {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDisease(int id, DiseaseInformation disease)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingDisease = await _unitOfWork.DiseaseInformation.GetAsync(x => x.Id == id);

                    if (existingDisease != null)
                    {
                        existingDisease.DiseaseName = disease.DiseaseName;
                        _unitOfWork.DiseaseInformation.Update(existingDisease);
                        await _unitOfWork.CommitAsync();
                        return Ok(new
                        {
                            Message = "Disease Information Update Successfully."
                        });
                    }
                    else
                    {
                        return NotFound($"Disease with ID {id} not found.");
                    }
                }

                return BadRequest("Invalid model state. Please check your input.");
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return BadRequest($"There is Something Wrong! Please Try Again. {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDisease(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var disease = await _unitOfWork.DiseaseInformation.GetAsync(x => x.Id == id);
                    if (disease != null)
                    {
                        _unitOfWork.DiseaseInformation.Remove(disease);
                        await _unitOfWork.CommitAsync();
                        return Ok("Disease Delete Successfully.");
                    }
                    else
                    {
                        NotFound($"Disease with this {id} not Found.");
                    }
                }
                return BadRequest("There is Something Wrong ! Please Try Again.");
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return BadRequest($"There is Something Wrong ! Please Try Again. {ex.Message}");
            }
        }


    }
}
