using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Core.Entities { 

 public class Room
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int roomId { get; set; }
    public string roomType { get; set; }
    public decimal baseCost { get; set; }
    public decimal taxes { get; set; }
    public string location { get; set; }
    public bool active { get; set; }
    public int HotelId { get; set; } 


    }

}