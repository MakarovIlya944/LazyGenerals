using System;
using System.IO;
using System.Windows;
using LazyGenerals.Client.Services;
using LazyGenerals.Client.View;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LazyGenerals.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public IConfiguration Configuration { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory());

            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (environment != null && File.Exists(Path.Combine(Directory.GetCurrentDirectory(), $"appsettings.{environment}.json")))
            {
                builder.AddJsonFile(path: $"appsettings.{environment}.json",
                                    optional: false,
                                    reloadOnChange: true);
            }

            Configuration = builder.Build();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient<Broker>()
                .ConfigureHttpClient(x => x.BaseAddress = new Uri("http://localhost:5000/"));

            services.AddTransient<MainWindow>();
            services.AddTransient<LobbyWindow>();
        }
    }
}