using HousingBillManagement.API.Data;
using HousingBillManagement.API.Models;
using HousingBillManagement.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HousingBillManagement.API.Repositories
{
    public class BillRepository : IBillRepository
    {
        private readonly AppDbContext _context;

        public BillRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Bill>> GetAllBills()
        {
            return await _context.Bills.Include(b => b.Apartment).Include(b => b.User).ToListAsync();
        }

        public async Task<IEnumerable<Bill>> GetBillsByBuilding(string blockInfo)
        {
            return await _context.Bills
                .Where(b => b.Apartment.BlockInfo == blockInfo)
                .Include(b => b.Apartment)
                .Include(b => b.User)
                .ToListAsync();
        }

        public async Task AddBill(Bill bill)
        {
            await _context.Bills.AddAsync(bill);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBill(Bill updatedBill)
        {
            var existingBill = await _context.Bills.FindAsync(updatedBill.Id);
            if (existingBill == null)
            {
                throw new ArgumentException($"Bill not found with ID: {updatedBill.Id}");
            }

            existingBill.Type = updatedBill.Type;
            existingBill.Amount = updatedBill.Amount;
            existingBill.Year = updatedBill.Year;
            existingBill.Month = updatedBill.Month;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteBill(int id)
        {
            var bill = await _context.Bills.FindAsync(id);
            _context.Bills.Remove(bill);
            await _context.SaveChangesAsync();
        }

        public async Task AssignBillToApartment(int billId, int apartmentId)
        {
            var bill = await _context.Bills.FindAsync(billId);
            var apartment = await _context.Apartments.FindAsync(apartmentId);

            if (bill != null && apartment != null)
            {
                bill.Apartment = apartment;
                await _context.SaveChangesAsync();
            }
        }

    }
}
