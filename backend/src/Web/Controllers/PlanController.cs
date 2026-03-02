using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlanController : ControllerBase
    {
        private readonly IPlanService _planService;

        public PlanController(IPlanService planService)
        {
            _planService = planService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var plans = await _planService.GetAllPlansAsync();
            return Ok(plans);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var plan = await _planService.GetPlanByIdAsync(id);
            if (plan == null) return NotFound();
            return Ok(plan);
        }

        [HttpGet("locality/{localityId}")]
        public async Task<IActionResult> GetByLocality(int localityId)
        {
            var plans = await _planService.GetPlansByLocalityIdAsync(localityId);
            return Ok(plans);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PlanCreateDto dto)
        {
            var plan = await _planService.CreatePlanAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = plan.Id }, plan);
        }
    }
}