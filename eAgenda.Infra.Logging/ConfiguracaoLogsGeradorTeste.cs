﻿using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;

namespace eAgenda.Infra.Logging
{
    public interface IConfiguracaoLogseAgenda
    {
        void ConfigurarEscritaLogs();
    }

    public class ConfiguracaoLogsLocal : IConfiguracaoLogseAgenda
    {
        private string diretorioSaida;

        public ConfiguracaoLogsLocal(IConfiguration configuration)
        {
            diretorioSaida = configuration.GetSection("diretorioSaida").Value;
        }

        public void ConfigurarEscritaLogs()
        {                        
            Log.Logger = new LoggerConfiguration()
                   .MinimumLevel.Override("Microsoft", LogEventLevel.Information).Enrich.FromLogContext()
                   .MinimumLevel.Debug()
                   .WriteTo.File(diretorioSaida + "/log.txt", rollingInterval: RollingInterval.Day,
                            outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                   .CreateLogger();            
        }
    }

    public class ConfiguracaoLogsAzure : IConfiguracaoLogseAgenda
    {
        public void ConfigurarEscritaLogs()
        {
            var connectionString =
                "DefaultEndpointsProtocol=https;AccountName=eagendabloblogging;AccountKey=YDCnFhxJ21ffjUSR7OBYR48dIpPthU/N4di1IQjvDcEkqxqgPzCSI0XDv5SXuBVEJgp1IhKdrCVC+AStqyjIsw==;EndpointSuffix=core.windows.net";

            var x = new BlobServiceClient(connectionString);          

            Log.Logger = new LoggerConfiguration()
                   .MinimumLevel.Override("Microsoft", LogEventLevel.Information).Enrich.FromLogContext()
                   .MinimumLevel.Debug()
                   .WriteTo.AzureBlobStorage(x, storageFileName: "{yyyy}/{MM}/{dd}/log.txt")
                   .CreateLogger();           
        }
    }
}
