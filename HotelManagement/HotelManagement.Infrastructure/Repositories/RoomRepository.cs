using Microsoft.EntityFrameworkCore;
using HotelBookingAPI.Domain.Interfaces;
using HotelManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBookingAPI.Infrastructure.Data.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly DataContext _context;

        public RoomRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Room>> GetRoomsByHotelIdAsync(int hotelId)
        {
            try
            {
                return await _context.Room
                    .Where(r => r.HotelId == hotelId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al consultar las habitaciones por ID de hotel", ex);
            }
        }
        public async Task<List<Room>> GetAllRoomsAsync()
        {
            try
            {
                return await _context.Room.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al consultar todas las habitaciones", ex);
            }
        }

        public async Task<Room> GetRoomByIdAsync(int id)
        {
            try
            {
                return await _context.Room.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al consultar la habitación por ID", ex);
            }
        }

        public async Task AddRoomAsync(Room room)
        {
            try
            {
                _context.Room.Add(room);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear la habitación", ex);
            }
        }

        public async Task UpdateRoomAsync(Room room)
        {
            try
            {
                _context.Entry(room).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la habitación", ex);
            }
        }


        public async Task<bool> InactivateRoomAsync(int id)
        {
            try
            {
                var room = await _context.Room.FindAsync(id);
                if (room == null)
                {
                    return false;
                }

                room.active = !room.active;
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                throw new Exception("Error al cambiar el esatdo de la habitación", ex);
            }
        }
    }
}
