using HousingBillManagement.API.Models;

namespace HousingBillManagement.API.Repositories.Interfaces
{
    public interface IApartmentRepository
    {
        Task<IEnumerable<Apartment>> GetAllApartments();
        Task<Apartment> GetApartmentById(int id);
        Task<Apartment> GetApartmentByBlockAndNumber(string blockInfo, int number);
        Task AddApartment(Apartment apartment);
        Task UpdateApartment(Apartment updatedApartment);
        Task DeleteApartment(int id);
        Task AssignUserToApartment(int apartmentId, User user);
    }
}
