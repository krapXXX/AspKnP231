namespace AspKnP231.Services.DateTime;

public class SqlDateTimeService : IDateTimeService
{
    public string GetDate() => System.DateTime.Now.ToString("yyyy-MM-dd");
    public string GetTime() => System.DateTime.Now.ToString("HH:mm:ss.fff");
}