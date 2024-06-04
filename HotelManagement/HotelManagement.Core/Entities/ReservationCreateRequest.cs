using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.Entities
{
    public class ReservationCreateRequest
    {
        public int RoomId { get; set; }

        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumPersons { get; set; }
        public string DestinationCity { get; set; }
        public string ReservationStatus { get; set; } // Consider using an enum for status
        public List<Passenger> Passenger { get; set; }
       
    }
}
