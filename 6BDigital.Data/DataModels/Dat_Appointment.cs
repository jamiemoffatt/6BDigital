using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6BDigital.Data.DataModels
{
    internal class Dat_Appointment
    {
        public int AppointmentId { get; set; }
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
        public string Issue { get; set; }
        public string ContactNumber { get; set; }
        public string ContactEmail { get; set; }
        public int? ApprovedBy { get; set; }
        public DateTime? ApprovedDateTime { get; set; }
    }
}
