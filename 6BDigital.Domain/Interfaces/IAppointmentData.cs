using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6BDigital.Domain.Interfaces
{
    public  interface IAppointmentData
    {
        void CreateAppointment(Appointment appointment);
        IEnumerable<Appointment> GetAppointments(bool awaitingApprovalOnly = false);
    }
}
