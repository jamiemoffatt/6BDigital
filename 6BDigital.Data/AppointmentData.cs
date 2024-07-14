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

        public IEnumerable<Appointment> GetAppointments(bool awaitingApprovalOnly = false)
        {
            using (var connection = new SqlConnection(_ConnectionString))
            {
                string sql =
                    $@"SELECT 
                            da.{nameof(Dat_Appointment.AppointmentId)}, 
                            da.{nameof(Dat_Appointment.Name)},
                            da.{nameof(Dat_Appointment.DateTime)},
                            da.{nameof(Dat_Appointment.Issue)},
                            da.{nameof(Dat_Appointment.ContactNumber)},
                            da.{nameof(Dat_Appointment.ContactEmail)},
                            du.{nameof(Dat_User.Name)} AS ApprovedBy,
                            da.{nameof(Dat_Appointment.ApprovedDateTime)}
                       FROM {nameof(Dat_Appointment)} da
                       LEFT JOIN {nameof(Dat_User)} du ON da.{nameof(Dat_Appointment.ApprovedBy)} = du.{nameof(Dat_User.UserId)}
                       WHERE da.{nameof(Dat_Appointment.ApprovedBy)} is null OR @{nameof(awaitingApprovalOnly)} = 0 ";

                var parameters = new { awaitingApprovalOnly };

                var result = connection.Query<Appointment>(sql, parameters);

                return result;
            }
        }
    }
}
