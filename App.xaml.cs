using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Praktika.Views;
using System.IO;
using System.Windows;
using Application = System.Windows.Application;

namespace Praktika;

public partial class App : Application
{
    public IServiceProvider ServiceProvider { get; private set; } = null!;

    public IConfiguration Configuration { get; private set; }

    public App()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        Configuration = builder.Build();

        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);

        ServiceProvider = serviceCollection.BuildServiceProvider();
    }

    private void OnStartup(object sender, StartupEventArgs e)
    {
        var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();

        mainWindow.Show();
    }

    private void ConfigureServices(ServiceCollection services)
    {
        services.AddDbContext<PraktikaContext>(options =>
        {
            options.UseSqlite("Data Source=\"D:\\Projects\\Сторонние\\PraktikaPhilimonov\\Praktika\\bin\\Debug\\net8.0-windows\\praktika.db\";Foreign Keys = true;");
        });

        services.AddSingleton<MainWindow>();
        services.AddSingleton<EditClientWindow>();
    }
}
