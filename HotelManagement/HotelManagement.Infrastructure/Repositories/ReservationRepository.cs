using Microsoft.EntityFrameworkCore;
using HotelBookingAPI.Domain.Interfaces;
using HotelManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBookingAPI.Infrastructure.Data.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly DataContext _context;

        public ReservationRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<int> CreatePassengerAsync(Passenger passenger)
        {
            try
            {
                _context.Passenger.Add(passenger);
                await _context.SaveChangesAsync();
                return passenger.passengerId;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear el pasajero", ex);
            }
        }

        public async Task<int> CreateReservationAsync(Reservations reservation)
        {
            try
            {
                _context.Reservations.Add(reservation);
                await _context.SaveChangesAsync();
                return reservation.reservationId;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear la reservación", ex);
            }
        }

        public async Task<List<Reservations>> GetReservationsAsync()
        {
            try
            {
                var reservations = await _context.Reservations
                                    .Include(r => r.passenger)
                                    .ToListAsync();

                return reservations;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al consultar las reservaciones", ex);
            }
        }
    }
}
