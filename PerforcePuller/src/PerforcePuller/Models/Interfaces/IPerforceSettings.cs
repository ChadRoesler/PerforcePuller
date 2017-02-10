namespace PerforcePuller.Models.Interfaces
{
    public interface IPerforceSettings
    {
        string Server { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        int Timeout { get; set; }
    }
}
