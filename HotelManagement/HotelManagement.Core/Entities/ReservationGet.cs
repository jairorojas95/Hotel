using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.Entities
{
    public class ReservationsGet
    {
        [Key]
        public int reservationId { get; set; }
        public int roomId { get; set; }
        public DateTime checkInDate { get; set; }
        public DateTime checkOutDate { get; set; }
        public int numPersons { get; set; }
        public string destinationCity { get; set; }
        public int passengerId { get; set; }
        public ICollection<Passenger> passenger { get; set; }

    }
}
