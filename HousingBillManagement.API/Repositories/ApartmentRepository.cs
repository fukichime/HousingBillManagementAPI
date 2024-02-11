using HousingBillManagement.API.Data;
using HousingBillManagement.API.Models;
using HousingBillManagement.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HousingBillManagement.API.Repositories
{
    public class ApartmentRepository : IApartmentRepository
    {
        private readonly AppDbContext _context;

        public ApartmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Apartment>> GetAllApartments()
        {
            return await _context.Apartments.ToListAsync();
        }

        public async Task<Apartment> GetApartmentById(int id)
        {
            return await _context.Apartments.FindAsync(id);
        }

        public async Task<Apartment> GetApartmentByBlockAndNumber(string blockInfo, int number)
        {
            return await _context.Apartments
                .Where(a => a.BlockInfo == blockInfo && a.Number == number)
                .FirstOrDefaultAsync();
        }

        public async Task AddApartment(Apartment apartment)
        {
            _context.Apartments.Add(apartment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateApartment(Apartment updatedApartment)
        {
            var existingApartment = await _context.Apartments.FindAsync(updatedApartment.Id);
            if (existingApartment != null)
            {
                // Update properties of the existingApartment with updatedApartment values.
                existingApartment.BlockInfo = updatedApartment.BlockInfo;
                existingApartment.IsOccupied = updatedApartment.IsOccupied;
                existingApartment.ApartmentType = updatedApartment.ApartmentType;
                existingApartment.Floor = updatedApartment.Floor;
                existingApartment.Number = updatedApartment.Number;
                existingApartment.areTheyOwner = updatedApartment.areTheyOwner;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteApartment(int id)
        {
            var apartment = await _context.Apartments.FindAsync(id);
            if (apartment != null)
            {
                _context.Apartments.Remove(apartment);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AssignUserToApartment(int apartmentId, User user)
        {
            var apartment = await _context.Apartments.FindAsync(apartmentId);
            if (apartment != null)
            {
                var AssignedUserTCNo = user.TCNo;
                apartment.AssignedUser = user;
                await _context.SaveChangesAsync();
            }

        }
    }
}
