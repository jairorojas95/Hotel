using HotelBookingAPI.Domain.Interfaces;
using HotelManagement.Core.Entities;
using HotelManagement.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBookingAPI.Application.Services
{
    public class ReservationService : IReservationRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReservationRepository _reservationRepository;

        public ReservationService(IUnitOfWork unitOfWork, IReservationRepository reservationRepository)
        {
            _unitOfWork = unitOfWork;
            _reservationRepository = reservationRepository;
        }

        public async Task<int> CreatePassengerAsync(Passenger passenger)
        {
            try
            {
                await _unitOfWork.SaveChangesAsync(passenger);
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
                await _unitOfWork.SaveChangesAsync(reservation);
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
                return await _reservationRepository.GetReservationsAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al consultar las reservaciones", ex);
            }
        }
    }
}
