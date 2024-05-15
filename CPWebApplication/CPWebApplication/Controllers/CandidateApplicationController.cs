using CPWebApplication.Interfaces;
using CPWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace CPWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateApplicationController : ControllerBase
    {
        private readonly ICandidateApplicationService _candidateApplicationService;
        public CandidateApplicationController(ICandidateApplicationService candidateApplicationService)
        {
            _candidateApplicationService = candidateApplicationService;
        }
        [HttpPost]
        [Route("AddCandidateApplication")]
        public async Task<IActionResult> AddCandidateApplication(CandidateApplication application)
        {
            try
            {
                await _candidateApplicationService.AddCandidateApplicationAsync(application);
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
    }
}
