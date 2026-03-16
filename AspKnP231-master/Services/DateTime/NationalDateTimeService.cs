namespace AspKnP231.Services.DateTime;

public class NationalDateTimeService : IDateTimeService
{
    public string GetDate() => System.DateTime.Now.ToString("dd.MM.yyyy");
    public string GetTime() => System.DateTime.Now.ToString("HH:mm:ss");
}