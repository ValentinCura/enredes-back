using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var plans = await _planService.GetAllPlansAsync();
            return Ok(plans);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var plan = await _planService.GetPlanByIdAsync(id);
            if (plan == null) return NotFound();
            return Ok(plan);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("locality/{localityId}")]
        public async Task<IActionResult> GetByLocality(int localityId)
        {
            var plans = await _planService.GetPlansByLocalityIdAsync(localityId);
            return Ok(plans);
        }

        [HttpGet("actives")]
        public async Task<IActionResult> GetActives()
        {
            var plans = await _planService.GetActivePlansAsync();
            return Ok(plans);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PlanCreateDto dto)
        {
            var plan = await _planService.CreatePlanAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = plan.Id }, plan);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PlanUpdateDto dto)
        {
            var plan = await _planService.UpdatePlanAsync(id, dto);
            if (plan == null) return NotFound();
            return Ok(plan);
        }
        [Authorize(Roles = "Admin")]
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> ChangeStatus(int id, [FromBody] ChangeStatusDto dto)
        {
            var result = await _planService.ChangeStatusAsync(id, dto.Status);
            if (!result) return NotFound();
            return Ok(new { message = "Estado actualizado correctamente" });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _planService.DeletePlanAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

    }
}