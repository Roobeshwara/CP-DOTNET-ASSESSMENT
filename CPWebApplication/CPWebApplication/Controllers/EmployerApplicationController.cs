using CPWebApplication.Interfaces;
using CPWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using System.Net;

namespace CPWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployerApplicationController : ControllerBase
    {
        private readonly IEmployerApplicationService _employerApplicationService;
        public EmployerApplicationController(IEmployerApplicationService employerApplicationService)
        {
            _employerApplicationService = employerApplicationService;
        }
        [HttpPost]
        [Route("AddEmployerApplication")]
        public async Task<IActionResult> AddEmployerApplication(EmployerApplication application)
        {
            try
            {
                await _employerApplicationService.AddEmployerApplicationAsync(application);
                return Ok("Record Inserted");
            }
            catch (CosmosException cosmosEx) when (cosmosEx.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                return Conflict("Duplicate record. Record already exists.");
            }
            catch (CosmosException cosmosEx) when (cosmosEx.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound("Resource not found.");
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to insert record: " + ex.Message);
            }
        }
        [HttpPut]
        [Route("UpdateEmployerApplication")]
        public async Task<IActionResult> UpdateEmployerApplication(EmployerApplication application)
        {
            try
            {
                var result = await _employerApplicationService.UpadteEmployerApplicationAsync(application);
                return Ok(result);
            }
            catch (CosmosException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    return NotFound("Employer application not found.");
                }
                else if (ex.StatusCode == HttpStatusCode.PreconditionFailed)
                {
                    return Conflict("Update conflict occurred. Please try again.");
                }
                else
                {
                    return StatusCode((int)ex.StatusCode, "An error occurred while updating the employer application.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("GetQuestionsByType")]
        public async Task<IActionResult> GetQuestionsByType(string applicationId,string questionType)
        {
            try
            {
                var result = await _employerApplicationService.GetQuestionsByTypeAsync(applicationId, questionType);
                return Ok(result);
            }
            catch (CosmosException ex)
            {
                if (ex.StatusCode == HttpStatusCode.NotFound)
                {
                    return NotFound("Employer application not found.");
                }
                else if (ex.StatusCode == HttpStatusCode.PreconditionFailed)
                {
                    return Conflict("Update conflict occurred. Please try again.");
                }
                else
                {
                    return StatusCode((int)ex.StatusCode, "An error occurred while updating the employer application.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
