using NLog;
using NLog.Web;

namespace BackgroundService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

            logger.Debug("init main");

            try
            {
                var builder = WebApplication.CreateBuilder(args);

                var startup = new Startup(builder.Configuration);

                startup.ConfigureServices(builder.Services);

                var app = builder.Build();

                startup.Configure(app, builder.Environment);

                app.Run();
            }
            catch (Exception exception)
            {
                // NLog: catch setup errors
                logger.Error(exception, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }
        }
    }
}