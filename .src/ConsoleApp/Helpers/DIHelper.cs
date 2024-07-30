namespace ConsoleApp.Helpers;

public static class DIHelper
{
    private static readonly ServiceCollection Services = new();

    private static readonly ConfigurationBuilder ConfigBuilder = new();

    private static readonly ServiceProvider ServiceProvider;
    
    static DIHelper()
    {
        ConfigBuilder.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
        var configuration = ConfigBuilder.Build();

        Services.AddSingleton<IConfiguration>(configuration);
        Services.AddScoped<IFileHandler, FileHandler>();
        Services.AddScoped<IRepository<Employee>, EmployeeRepository>();
        Services.AddScoped<IUnitOfWork, UnitOfWork>();
        Services.AddScoped<IEmployeeService, EmployeeService>();

        ServiceProvider = Services.BuildServiceProvider();
    }
    
    public static T GetService<T>() => ServiceProvider.GetService<T>() 
                                       ?? throw new ArgumentException($"Service of type {typeof(T)} not found");
}