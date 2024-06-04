using HotelBookingAPI.Application.Services;
using HotelManagement.Core.Entities;
using HotelManagement.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySolution.Infrastructure.Data;
using System.Net.Mail;
using System.Xml.Linq;


namespace HotelManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase 
    {
        private readonly ReservationService _reservationService;
        private readonly IEmailService _emailService;
        private readonly IUnitOfWork _unitOfWork;

        public ReservationsController(ReservationService reservationService, IEmailService emailController, IUnitOfWork unitOfWork)
        {
            _reservationService = reservationService;
            _emailService = emailController;
            _unitOfWork = unitOfWork;
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateReservationAsync(ReservationCreateRequest request)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                // Crear y guardar pasajeros
                var passengerIds = new List<int>();
                var emails = new List<string>();

                foreach (var item in request.Passenger)
                {
                    var passenger = new Passenger
                    {
                        firstName = item.firstName,
                        lastName = item.lastName,
                        birthDate = item.birthDate,
                        gender = item.gender,
                        documentType = item.documentType,
                        documentNumber = item.documentNumber,
                        email = item.email,
                        phoneNumber = item.phoneNumber,
                        fullNameEmergency = item.fullNameEmergency,
                        phoneNumberEmergency = item.phoneNumberEmergency
                    };

                    int passengerId = await _reservationService.CreatePassengerAsync(passenger);
                    passengerIds.Add(passengerId);
                    emails.Add(item.email);
                }

                // Crear y guardar las reservas
                var reservationIds = new List<int>();

                foreach (var passengerId in passengerIds)
                {
                    var reservation = new Reservations
                    {
                        roomId = request.RoomId,
                        CheckInDate = request.CheckInDate,
                        CheckOutDate = request.CheckOutDate,
                        numPersons = request.NumPersons,
                        destinationCity = request.DestinationCity,
                        passengerId = passengerId
                    };

                    int reservationId = await _reservationService.CreateReservationAsync(reservation);
                    reservationIds.Add(reservationId);
                }

                await _unitOfWork.CommitAsync();

                // Enviar correos electrónicos
                var emailTasks = emails.Select(email =>
                    _emailService.SendEmailAsync(email, "Reserva", "La reserva fue realizada con éxito")
                ).ToList();

                await Task.WhenAll(emailTasks);

                return Ok(new
                {
                    ReservationId = reservationIds,
                    PassengerIds = passengerIds,
                    Message = "Reserva creada exitosamente."
                });
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return StatusCode(500, new { Message = $"Ocurrió un error al crear la reserva: {ex.Message}" });
            }
        }

        [HttpGet("getReservations")]
        public async Task<IActionResult> GetReservationsAsync()
        {
            try
            {
                var reservations = await _reservationService.GetReservationsAsync();

                return Ok(reservations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Ocurrió un error al obtener las reservas: {ex.Message}" });
            }
        }


    }
}
