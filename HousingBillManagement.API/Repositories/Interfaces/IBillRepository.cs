using HousingBillManagement.API.Models;

namespace HousingBillManagement.API.Repositories.Interfaces
{
    public interface IBillRepository
    {
        Task<IEnumerable<Bill>> GetAllBills();
        Task<IEnumerable<Bill>> GetBillsByBuilding(string blockInfo);
        Task AddBill(Bill bill);
        Task UpdateBill(Bill updatedBill);
        Task DeleteBill(int id);
        Task AssignBillToApartment(int billId, int apartmentId);
    }
}
