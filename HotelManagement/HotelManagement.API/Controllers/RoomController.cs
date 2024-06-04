using HotelBookingAPI.Application.Services;
using HotelBookingAPI.Domain.Interfaces;
using HotelManagement.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;


namespace HotelManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly RoomService _roomService;
        private readonly HotelService _hotelService;

        public RoomController(RoomService roomService, HotelService hotelService)
        {
            _roomService = roomService;
            _hotelService = hotelService;
        }

        [HttpGet("GetRoom")]
        public async Task<ActionResult<List<Room>>> GetRooms()
        {
            try
            {
                var rooms = await _roomService.GetAllRoomAsync();
                return Ok(rooms);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al consultar las habitaciones: {ex.Message}");
            }
        }

        [HttpGet("GetRoom/{id}")]
        public async Task<ActionResult<IEnumerable<Room>>> GetRooms(int id)
        {
            try
            {
                var rooms = await _roomService.GetRoomByIdAsync(id);
                return Ok(rooms);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al consultar las habitaciones: {ex.Message}");
            }
        }

        [HttpPost("CreateRoom")]
        public async Task<ActionResult<Room>> CreateRoom(Room room)
        {
            try
            {
                var hotel = await _hotelService.GetHotelByIdAsync(room.HotelId);
                if (hotel == null)
                {
                    return NotFound("El Hotel no existe.");
                }
               
                await _roomService.AddRoomAsync(room);
                
                return Ok(room);
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear la habitación: {ex.Message}");
            }
        }

        [HttpPut("State/{id}")]
        public async Task<ActionResult<Room>> UpdateRoom(int id)
        {
            try
            {
                await _roomService.InactivateRoomAsync(id);
                return Ok(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar el estado de la habitación: {ex.Message}");
            }
        }


        [HttpPut("UpdateRoom")]
        public async Task<IActionResult> UpdateHotel(Room room)
        {
            try
            {
                await _roomService.UpdateRoomAsync(room);
                return Ok(room);
            }
            catch (Exception ex)
            {
                // Log the exception (not shown here)
                return StatusCode(500, "Ocurrió un error al actualizar los datos de la habitacion.");
            }
        }
    }
}
