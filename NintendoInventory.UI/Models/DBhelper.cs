﻿namespace NintendoInventory.UI.Models
{
    public class DBhelper
    {
        public static IConfiguration config;

        public static string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            config = builder.Build();
            return config.GetConnectionString("NintendoConnectionString");
        }
    }
}
