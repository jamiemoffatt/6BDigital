using System.Data.SqlClient;
using _6BDigital.Domain;
using _6BDigital.Data.DataModels;
using Dapper;
using _6BDigital.Domain.Interfaces;


namespace _6BDigital.Data
{
    public class AppointmentData : IAppointmentData
    {
        private string _ConnectionString;
        public AppointmentData(string connectionString)
        {
            _ConnectionString = connectionString;
        }

        public void CreateAppointment(Appointment appointment)
        {
            using (var connection = new SqlConnection(_ConnectionString))
            {
                string sql =
                    $@"INSERT INTO {nameof(Dat_Appointment)} ({nameof(Dat_Appointment.Name)}, {nameof(Dat_Appointment.DateTime)}, {nameof(Dat_Appointment.Issue)}, {nameof(Dat_Appointment.ContactNumber)}, {nameof(Dat_Appointment.ContactEmail)})
                       VALUES (@{nameof(appointment.Name)}, @{nameof(appointment.DateTime)}, @{nameof(appointment.Issue)}, @{nameof(appointment.ContactNumber)},
                              @{nameof(appointment.ContactEmail)})";

                var parameters = new { appointment.Name, appointment.DateTime, appointment.Issue, appointment.ContactNumber, appointment.ContactEmail };

                var result = connection.Execute(sql, parameters);

            }
        }
    }
}
