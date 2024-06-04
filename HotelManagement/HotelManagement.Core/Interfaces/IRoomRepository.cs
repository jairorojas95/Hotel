
using HotelManagement.Core.Entities;

namespace HotelBookingAPI.Domain.Interfaces
{
    public interface IRoomRepository
    {
        Task<List<Room>> GetRoomsByHotelIdAsync(int hotelId);
        Task<Room> GetRoomByIdAsync(int id);
        Task AddRoomAsync(Room room);
        Task UpdateRoomAsync(Room room);
        Task<bool> InactivateRoomAsync (int id);
        Task<List<Room>> GetAllRoomsAsync();


    }
}