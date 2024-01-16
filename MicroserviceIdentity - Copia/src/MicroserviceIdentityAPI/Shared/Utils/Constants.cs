using System.Globalization;

namespace MicroserviceIdentityAPI.Shared.Utils
{
    public static class Constants
    {
        private static IConfigurationBuilder GetConfigurationBuilder()
        {
            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            return new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile($"appsettings.{envName}.json");
        }

        public static string GetConnectionString()
        {
            var builder = GetConfigurationBuilder();
            var config = builder.Build();

            return config[$"ConnectionStrings:Connection"] ?? string.Empty;
        }

        public static string GetConnectionString(string conn)
        {
            var builder = GetConfigurationBuilder();
            var config = builder.Build();

            return config[$"ConnectionStrings:{conn}"] ?? string.Empty;
        }

        public static string GetConnectionStringMongoDb()
        {
            return GetParameterById("ConnectionMongoDb") ?? string.Empty;
        }

        public static string GetParameterById(string key)
        {
            var builder = GetConfigurationBuilder();
            var config = builder.Build();

            return  config[$"Parameters:{key}"] ?? string.Empty;
        }

        public static string GetNameDataBaseMongoDb()
        {
            return GetParameterById("NameDataBaseMongo") ?? string.Empty;
        }


        public static string WriteDirectory(string path, string directory = "")
        {
            CultureInfo culture = new("pt-BR");
            DateTimeFormatInfo dtFi = culture.DateTimeFormat;

            string pathYear = path + DateTime.Now.Year;
            string pathMonth = pathYear + "\\" + culture.TextInfo.ToTitleCase(dtFi.GetMonthName(DateTime.Now.Month));
            string pathDay = pathMonth + "\\" + DateTime.Now.Day + "\\";

            string pathDirectory = string.Empty;
            if(string.IsNullOrEmpty(directory))
                pathDirectory = pathDay;
            else
                pathDirectory = pathDay + "\\" + directory + "\\";
            
            ValidateDirectory(pathYear);
            ValidateDirectory(pathMonth);
            ValidateDirectory(pathDay);
            ValidateDirectory(pathDirectory);

            return pathDirectory;            
        }

        private static void ValidateDirectory(string path)
        {
            if(!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception)
                {                    
                }
            }
        }
    }
}