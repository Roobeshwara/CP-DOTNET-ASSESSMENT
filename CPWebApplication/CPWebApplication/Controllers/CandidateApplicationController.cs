using CPWebApplication.Interfaces;
using CPWebApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
