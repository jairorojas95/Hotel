using Microsoft.AspNetCore.Mvc;
using HotelBookingAPI.Application.Services;
using HotelManagement.Core.Entities;



namespace HotelManagement.API.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class HotelsController : ControllerBase
    {
        private readonly HotelService _hotelService;
        private readonly RoomService _roomService;
        private readonly ReservationService _reservationService;

        public HotelsController(HotelService hotelService, RoomService roomService, ReservationService reservationService)
        {
            _hotelService = hotelService;
            _roomService = roomService;
            _reservationService = reservationService;
        }

        [HttpGet("GetHotel")]
        public async Task<ActionResult<List<Hotel>>> GetHotels()
        {
            try
            {
                var hotels = await _hotelService.GetAllHotelsAsync();
                return Ok(hotels);
            }
            catch (Exception ex)
            {
                // Log the exception (not shown here)
                return StatusCode(500, "Ocurrió un error al obtener los hoteles.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Hotel>> GetHotel(int id)
        {
            try
            {
                var hotel = await _hotelService.GetHotelByIdAsync(id);

                if (hotel == null)
                {
                    return NotFound("Por favor valide que el hotel exista.");
                }

                return Ok(hotel);
            }
            catch (Exception ex)
            {
                // Log the exception (not shown here)
                return StatusCode(500, "Ocurrió un error al obtener el hotel.");
            }
        }

        [HttpPost("CreateHotel")]
        public async Task<ActionResult<Hotel>> CreateHotel(Hotel hotel)
        {
            try
            {
                await _hotelService.AddHotelAsync(hotel);
                return CreatedAtAction("GetHotel", new { id = hotel.hotelId }, hotel);
            }
            catch (Exception ex)
            {
                // Log the exception (not shown here)
                return StatusCode(500, "Ocurrió un error al crear el hotel.");
            }
        }

        [HttpPut("state{id}")]
        public async Task<ActionResult<Hotel>> StateUpdateRoom(int id)
        {
            try
            {
                await _hotelService.InactivateHotelAsync(id);
                return Ok(id);
            }
            catch (Exception ex)
            {
                // Log the exception (not shown here)
                return StatusCode(500, "Ocurrió un error al actualizar el estado del hotel.");
            }
        }

        [HttpPut("UpdateHotel")]
        public async Task<IActionResult> UpdateHotel(Hotel hotel)
        {
            try
            {
                await _hotelService.UpdateHotelAsync(hotel);
                return Ok(hotel);
            }
            catch (Exception ex)
            {
                // Log the exception (not shown here)
                return StatusCode(500, "Ocurrió un error al actualizar el hotel.");
            }
        }

    }
}

