using DbUp;
using DbUp.Engine;
using DbUp.Support;
using System;
using System.Configuration;
using DbUp.Builder;

namespace _6BDigital.DbUp
{
    public class program
    {
        public static void Main(string[] args)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            EnsureDatabase.For.SqlDatabase(connectionString);

            var upgrader =
                DeployChanges.To                
                    .SqlDatabase(connectionString)
                    .WithScriptsFromFileSystem($"{AppDomain.CurrentDomain.BaseDirectory}/Scripts", new SqlScriptOptions { ScriptType = ScriptType.RunOnce, RunGroupOrder = 1 })
                    .LogToConsole()
                    .LogScriptOutput()
                    .WithTransaction()
                    .WithExecutionTimeout(TimeSpan.FromSeconds(180))
                    .Build();


            upgrader.PerformUpgrade();
        }
    }
}