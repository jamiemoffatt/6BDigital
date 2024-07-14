using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6BDigital.Domain
{
    public class Appointment
    {
        public int? AppointmentId { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [Required, Display(Name = "Date & Time")]
        public DateTime DateTime { get; set; }

        [MaxLength(2000), Required]
        public string Issue { get; set; }

        [MaxLength(20), Required, Phone, Display(Name = "Phone Number")]
        public string ContactNumber { get; set; }

        [MaxLength(50), EmailAddress, Required, Display(Name = "Email")]
        public string ContactEmail { get; set; }

        public int? ApprovedBy { get; set; }

        public DateTime? ApprovedDateTime { get; set; }
    }
}
