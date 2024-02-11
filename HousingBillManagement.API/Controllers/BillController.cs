using HousingBillManagement.API.Models;
using HousingBillManagement.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HousingBillManagement.API.Controllers
{

    [Authorize(Roles = "admin")]
    [ApiController]
    [Route("api/[controller]")]
    public class BillController :ControllerBase
    {
        private readonly IBillRepository _billRepository;

        public BillController(IBillRepository billRepository)
        {
            _billRepository = billRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bill>>> GetAllBills()
        {
            var bills = await _billRepository.GetAllBills();
            return Ok(bills);
        }

        [HttpGet("building/{blockInfo}")]
        public async Task<ActionResult<IEnumerable<Bill>>> GetBillsByBuilding(string blockInfo)
        {
            var bills = await _billRepository.GetBillsByBuilding(blockInfo);
            return Ok(bills);
        }

        [HttpPost]
        public async Task<ActionResult> CreateBill(Bill bill)
        {
            try
            {
                await _billRepository.AddBill(bill);
                return Ok(new { Message = "Bill created successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Failed to create bill. Error: {ex.Message}" });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBill(int id, Bill updatedBill)
        {
            try
            {
                await _billRepository.UpdateBill(updatedBill);
                return Ok(new { Message = "Bill updated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Failed to update bill. Error: {ex.Message}" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBill(int id)
        {
            try
            {
                await _billRepository.DeleteBill(id);
                return Ok(new { Message = "Bill deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Failed to delete bill. Error: {ex.Message}" });
            }
        }

        [HttpPost("assign/{billId}/{apartmentId}")]
        public async Task<ActionResult> AssignBillToApartment(int billId, int apartmentId)
        {
            try
            {
                await _billRepository.AssignBillToApartment(billId, apartmentId);
                return Ok(new { Message = "Bill assigned to apartment successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = $"Failed to assign bill to apartment. Error: {ex.Message}" });
            }
        }
    }
}
