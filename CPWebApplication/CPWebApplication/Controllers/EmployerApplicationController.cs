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
    }
}
