using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestApp.Common;
using TestApp.Common.Interfaces;
using TestApp.Model;
using TestApp.UI.Controllers;
using TestApp.UI.Infrastructure;
using TestApp.UI.Infrastructure.Dialogs;
using TestApp.UI.Views;

namespace TestApp.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;

            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    ConfigureServices(context.Configuration, services);
                })
                .Build();
        }

        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = (Exception) e.ExceptionObject;

            ErrorWindow errorWindow = new ErrorWindow
                (exception.Message + exception.InnerException?.Message, exception.StackTrace)
                {
                    WindowStartupLocation = WindowStartupLocation.CenterScreen
                };
            errorWindow.ShowDialog();
        }

        private void ConfigureServices(IConfiguration configuration, 
            IServiceCollection services)
        {
            string connectionString = configuration["ConnectionStrings:DefaultConnection"];
            services.AddDbContext<CompanyContext>(optionsBuilder =>
            {
                optionsBuilder.EnableSensitiveDataLogging();
                optionsBuilder.UseMySQL(connectionString);
            });
            services.AddSingleton<ICompanyRepository, CompanyRepository>();
            services.AddSingleton<ISeedRepository, SeedRepository>();
            services.AddSingleton<ICompanyController, CompanyController>();
            services.AddSingleton<CompanyEntitiesMapper>();
            services.AddSingleton<MainWindow>();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();

            var mainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow = mainWindow;
            mainWindow.Show();
            
            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using (_host)
            {
                await _host.StopAsync(TimeSpan.FromSeconds(5));
            }

            base.OnExit(e);
        }

    }
}
