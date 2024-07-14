using _6BDigital.Data.DataModels;
using _6BDigital.Domain;
using _6BDigital.Domain.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6BDigital.Data
{
    public class UserData : IUserData
    {
        private string _ConnectionString;
        public UserData(string connectionString)
        {
            _ConnectionString = connectionString;
        }

        public User AuthenticateUser(string userName, string password)
        {
            using (var connection = new SqlConnection(_ConnectionString))
            {
                string sql =
                    $@"SELECT * 
                       FROM {nameof(Dat_User)} da
                       WHERE da.{nameof(Dat_User.Username)} = @{nameof(userName)} AND da.{nameof(Dat_User.Password)} = @{nameof(password)} ";

                var parameters = new { userName, password };

                Dat_User databaseUser = connection.QuerySingleOrDefault<Dat_User>(sql, parameters);

                if (databaseUser == null)
                {
                    return null;
                }

                return new User { Name = databaseUser.Name, UserName = databaseUser.Username};
            }
        }
    }
}
