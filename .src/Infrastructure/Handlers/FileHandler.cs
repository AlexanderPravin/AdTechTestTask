namespace Infrastructure.Handlers;

public class FileHandler: IFileHandler
{
    private readonly string _filePath;

    public FileHandler(IConfiguration configuration)
    {
        _filePath = configuration.GetSection("FilePath").Value ??
                    throw new ArgumentException("FilePath not found in appsettings.json");

        if (File.Exists(_filePath)) return;
        
        using var stream = File.Create(_filePath);
    }

    public async Task<string> GetText()
    {
        using var reader = new StreamReader(_filePath);
        
        return await reader.ReadToEndAsync();
    }

    public async Task SetText(string jsonString)
    {
        await using var writer = new StreamWriter(_filePath);
        
        await writer.WriteAsync(jsonString);
    }
}