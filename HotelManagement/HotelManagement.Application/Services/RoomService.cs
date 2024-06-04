using HotelBookingAPI.Domain.Interfaces;
using HotelBookingAPI.Infrastructure.Data.Repositories;
using HotelManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelBookingAPI.Application.Services
{
    public class RoomService
    {
        private readonly IRoomRepository _roomRepository;
        

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<List<Room>> GetRoomsByHotelIdAsync(int hotelId)
        {
            try
            {
                return await _roomRepository.GetRoomsByHotelIdAsync(hotelId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las habitaciones por ID de hotel", ex);
            }
        }

        public async  Task<List<Room>>  GetAllRoomAsync()
        {
            try
            {
                return await _roomRepository.GetAllRoomsAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la habitación por ID", ex);
            }
        }


        public async Task<Room> GetRoomByIdAsync(int id)
        {
            try
            {
                return await _roomRepository.GetRoomByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la habitación por ID", ex);
            }
        }

        public async Task AddRoomAsync(Room room)
        {
            try
            {
               
                await _roomRepository.AddRoomAsync(room);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar la habitación", ex);
            }
        }

        public async Task UpdateRoomAsync(Room room)
        {
            try
            {
                await _roomRepository.UpdateRoomAsync(room);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la habitación", ex);
            }
        }



        public async Task InactivateRoomAsync(int id)
        {
            try
            {
                await _roomRepository.InactivateRoomAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cambiar el esatdo de la habitación", ex);
            }
        }
    }
}
