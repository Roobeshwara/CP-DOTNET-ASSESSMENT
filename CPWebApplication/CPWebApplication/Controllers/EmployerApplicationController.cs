using CPWebApplication.Interfaces;
using CPWebApplication.Models;
using Microsoft.AspNetCore.Mvc;

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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("GetQuestionsByType")]
        public async Task<IActionResult> GetQuestionsByType(string applicationId, string programTitle,string questionType)
        {
            try
            {
                var result = await _employerApplicationService.GetQuestionsByTypeAsync(applicationId, programTitle, questionType);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
