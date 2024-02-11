using HousingBillManagement.API.Models;
using HousingBillManagement.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HousingBillManagement.API.Controllers
{
    [Authorize(Roles = "admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class ApartmentController : ControllerBase
    {
        private readonly IApartmentRepository _apartmentRepository;

        public ApartmentController(IApartmentRepository apartmentRepository)
        {
            _apartmentRepository = apartmentRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Apartment>>> GetApartments()
        {
            var apartments = await _apartmentRepository.GetAllApartments();
            return Ok(apartments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Apartment>> GetApartmentById(int id)
        {
            var apartment = await _apartmentRepository.GetApartmentById(id);

            if (apartment == null)
            {
                return NotFound();
            }

            return Ok(apartment);
        }

        [HttpGet("{blockInfo}/{number}")]
        public async Task<ActionResult<Apartment>> GetApartmentByBlockAndNumber(string blockInfo, int number)
        {
            var apartment = await _apartmentRepository.GetApartmentByBlockAndNumber(blockInfo, number);

            if (apartment == null)
            {
                return NotFound();
            }

            return Ok(apartment);
        }

        [HttpPost]
        public async Task<ActionResult<Apartment>> CreateApartment([FromBody] Apartment apartment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _apartmentRepository.AddApartment(apartment);

            return CreatedAtAction(nameof(GetApartmentById), new { id = apartment.Id }, apartment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateApartment(int id, [FromBody] Apartment updatedApartment)
        {
            if (id != updatedApartment.Id)
            {
                return BadRequest("Mismatched apartment ID in the request.");
            }

            try
            {
                await _apartmentRepository.UpdateApartment(updatedApartment);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating apartment: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApartment(int id)
        {
            var apartment = await _apartmentRepository.GetApartmentById(id);
            if (apartment == null)
            {
                return NotFound();
            }

            await _apartmentRepository.DeleteApartment(id);
            return NoContent();
        }

        [HttpPost("{id}/AssignUser")]
        public async Task<IActionResult> AssignUserToApartment(int id, [FromBody] User user)
        {
            try
            {
                await _apartmentRepository.AssignUserToApartment(id, user);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error assigning user to apartment: {ex.Message}");
            }
        }
    }
}
