using Application.Interfaces;
using Application.Models;
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var localities = await _localityService.GetAllLocalitiesAsync();
            return Ok(localities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var locality = await _localityService.GetLocalityByIdAsync(id);
            if (locality == null) return NotFound();
            return Ok(locality);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LocalityCreateDto dto)
        {
            var locality = await _localityService.CreateLocalityAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = locality.Id }, locality);
        }
    }
}