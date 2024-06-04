using Microsoft.EntityFrameworkCore;
using HotelBookingAPI.Domain.Interfaces;
using HotelManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBookingAPI.Infrastructure.Data.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        private readonly DataContext _context;

        public HotelRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Hotel>> GetAllHotelsAsync()
        {
            try
            {
                return await _context.Hotel
                                .Include(h => h.rooms) // Incluye las habitaciones relacionadas
                                .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al recuperar todos los hoteles", ex);
            }
        }

        public async Task<Hotel> GetHotelByIdAsync(int id)
        {
            try
            {
                return await _context.Hotel
                           .Include(h => h.rooms)
                           .FirstOrDefaultAsync(h => h.hotelId == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al Consultar el hotel por ID", ex);
            }
        }

        public async Task AddHotelAsync(Hotel hotel)
        {
            try
            {
                _context.Hotel.Add(hotel);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al Crear el hotel", ex);
            }
        }

        public async Task UpdateHotelAsync(Hotel hotel)
        {
            try
            {
                _context.Entry(hotel).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el hotel", ex);
            }
        }

        public async Task<bool> InactivateHotelAsync(int id)
        {
            try
            {
                var hotel = await _context.Hotel.FindAsync(id);
                if (hotel == null)
                {
                    return false;
                }

                hotel.active = !hotel.active;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cambiar el esatdo del hotel", ex);
            }
        }
    }
}
