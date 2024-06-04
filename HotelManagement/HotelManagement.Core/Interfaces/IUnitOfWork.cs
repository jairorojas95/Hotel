using HotelManagement.Core.Entities;

namespace HotelManagement.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync(Passenger passenger);
        Task<int> SaveChangesAsync(Reservations Reservation);
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
