using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class LocalityController : ControllerBase
    {
        private readonly ILocalityService _localityService;

        public LocalityController(ILocalityService localityService)
        {
            _localityService = localityService;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var localities = await _localityService.GetAllLocalitiesAsync();
            return Ok(localities);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var locality = await _localityService.GetLocalityByIdAsync(id);
            if (locality == null) return NotFound();
            return Ok(locality);
        }

        [HttpGet("actives")]
        public async Task<IActionResult> GetActives()
        {
            var localities = await _localityService.GetActiveLocalitiesAsync();
            return Ok(localities);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LocalityCreateDto dto)
        {
            var locality = await _localityService.CreateLocalityAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = locality.Id }, locality);
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] LocalityUpdateDto dto)
        {
            var locality = await _localityService.UpdateLocalityAsync(id, dto);
            if (locality == null) return NotFound();
            return Ok(locality);
        }
        [Authorize(Roles = "Admin")]
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> ChangeStatus(int id, [FromBody] ChangeStatusDto dto)
        {
            var result = await _localityService.ChangeStatusAsync(id, dto.Status);
            if (!result) return NotFound();
            return Ok(new { message = "Estado actualizado correctamente" });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _localityService.DeleteLocalityAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

    }
}