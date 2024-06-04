using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HotelManagement.Core.Entities
{
    public class Passenger
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int passengerId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }

        public DateTime birthDate { get; set; }
        public char gender { get; set; }

        public string documentType { get; set; }
        public string documentNumber { get; set; }

        public string email { get; set; }
        public string phoneNumber { get; set; }

        public string fullNameEmergency { get; set; }

        public string phoneNumberEmergency { get; set; }
    
     
    }
}
