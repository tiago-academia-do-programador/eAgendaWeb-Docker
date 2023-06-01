using eAgenda.Infra.Orm;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace GeradorTestes.Infra.Orm
{
    public class eAgendaDbContextFactory : IDesignTimeDbContextFactory<eAgendaDbContext>
    {
        public eAgendaDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<eAgendaDbContext>();

            string dbHost = Environment.GetEnvironmentVariable("DB_HOST");
            string dbName = Environment.GetEnvironmentVariable("DB_NAME");
            string dbUserId = Environment.GetEnvironmentVariable("DB_USER_ID");
            string dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");

            builder.UseSqlServer($"Data Source={dbHost};Initial Catalog={dbName}; User ID={dbUserId}; Password={dbPassword}; Integrated Security=True");

            return new eAgendaDbContext(builder.Options);
        }
    }
}
