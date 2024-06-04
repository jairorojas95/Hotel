using HotelManagement.Core.Entities;

namespace HotelBookingAPI.Domain.Interfaces
{
    public interface IHotelRepository
    {
        Task<List<Hotel>> GetAllHotelsAsync();
        Task<Hotel> GetHotelByIdAsync(int id);
        Task AddHotelAsync(Hotel hotel);
        Task UpdateHotelAsync(Hotel hotel);
        Task<bool> InactivateHotelAsync(int id);

    }
}