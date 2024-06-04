using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HotelManagement.Core.Entities
{
    public class Reservations
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int reservationId { get; set; }

        public int roomId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int numPersons { get; set; }
        public string destinationCity { get; set; }
        public int passengerId{ get; set; }
        public Passenger passenger { get; set; }

    }
}