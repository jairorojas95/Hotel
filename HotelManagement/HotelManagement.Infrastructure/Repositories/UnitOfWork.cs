using HotelBookingAPI.Infrastructure.Data;
using HotelManagement.Core.Entities;
using HotelManagement.Core.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace MySolution.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private IDbContextTransaction _transaction;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync(Passenger passenger)
        {
            try
            {
                _context.Passenger.Add(passenger);
                await _context.SaveChangesAsync();
                return passenger.passengerId;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar los cambios para el pasajero", ex);
            }
        }

        public async Task<int> SaveChangesAsync(Reservations reservations)
        {
            try
            {
                _context.Reservations.Add(reservations);
                await _context.SaveChangesAsync();
                return reservations.reservationId;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar los cambios para la reservación", ex);
            }
        }

        public async Task BeginTransactionAsync()
        {
            try
            {
                _transaction = await _context.Database.BeginTransactionAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al comenzar la transacción", ex);
            }
        }

        public async Task CommitAsync()
        {
            try
            {
                await _transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al confirmar la transacción", ex);
            }
        }

        public async Task RollbackAsync()
        {
            try
            {
                await _transaction.RollbackAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al deshacer la transacción", ex);
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }
    }
}
