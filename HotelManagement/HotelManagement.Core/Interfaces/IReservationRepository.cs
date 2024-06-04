
using HotelManagement.Core.Entities;

namespace HotelBookingAPI.Domain.Interfaces
{
    public interface IReservationRepository
    {
        
        Task<int> CreatePassengerAsync(Passenger passenger);
        Task<int> CreateReservationAsync(Reservations reservation);
        Task<List<Reservations>> GetReservationsAsync();

    }
}