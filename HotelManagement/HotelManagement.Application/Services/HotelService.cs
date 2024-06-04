using HotelBookingAPI.Domain.Interfaces;
using HotelManagement.Core.Entities;


namespace HotelBookingAPI.Application.Services
{
    public class HotelService
    {
        private readonly IHotelRepository _hotelRepository;

        public HotelService(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public async Task<List<Hotel>> GetAllHotelsAsync()
        {
            try
            {
                return await _hotelRepository.GetAllHotelsAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al recuperar todos los hoteles en el servicio", ex);
            }
        }

        public async Task<Hotel> GetHotelByIdAsync(int id)
        {
            try
            {
                return await _hotelRepository.GetHotelByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al recuperar el hotel por ID en el servicio", ex);
            }
        }

        public async Task AddHotelAsync(Hotel hotel)
        {
            try
            {
                await _hotelRepository.AddHotelAsync(hotel);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar el hotel en el servicio", ex);
            }
        }

        public async Task UpdateHotelAsync(Hotel hotel)
        {
            try
            {
                await _hotelRepository.UpdateHotelAsync(hotel);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el hotel en el servicio", ex);
            }
        }

        public async Task InactivateHotelAsync(int id)
        {
            try
            {
                await _hotelRepository.InactivateHotelAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al inactivar el hotel en el servicio", ex);
            }
        }
    }
}
