using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Core.Entities
{
    public class Hotel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int hotelId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public bool active { get; set; }
        public ICollection<Room>? rooms { get; set; }
   
    }
}