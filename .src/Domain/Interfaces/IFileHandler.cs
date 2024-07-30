namespace Domain.Interfaces;

public interface IFileHandler
{
    Task<string> GetText();

    Task SetText(string jsonString);
}