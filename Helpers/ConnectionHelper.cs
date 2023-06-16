using Microsoft.Extensions.Configuration;
using Npgsql;
using System;

namespace ContactPro.Helpers
{
    public static class ConnectionHelper
    {
        public static string GetConnectionString(IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("pgSettings")["pgConnection"];

            var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

            return string.IsNullOrEmpty(databaseUrl) ? connectionString : BuildConnectionString(databaseUrl);
        }

        private static string BuildConnectionString(string databaseurl)
        {
            var databaseuri = new Uri(databaseurl);
            var userInfo = databaseuri.UserInfo.Split(':');

            return new NpgsqlConnectionStringBuilder()
            {
                Host = databaseuri.Host,
                Port = databaseuri.Port,
                Username = userInfo[0],
                Password = userInfo[1],
                Database = databaseuri.LocalPath.TrimStart('/'),
                SslMode = SslMode.Prefer,
                TrustServerCertificate = true
            }.ToString();
        }
    }
}
