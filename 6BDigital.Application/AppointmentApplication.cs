using _6BDigital.Domain;
using _6BDigital.Domain.Interfaces;

namespace _6BDigital.Application
{
    public class AppointmentApplication : IAppointmentApplication
    {

        private IAppointmentData _data;

        public AppointmentApplication(IAppointmentData data)
        {
            _data = data;
        }

        public void CreateAppointment(Appointment appointment)
        {
            _data.CreateAppointment(appointment);
        }
    }
}
