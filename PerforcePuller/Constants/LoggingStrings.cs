namespace PerforcePuller.Constants
{
    internal sealed class LoggingStrings
    {
        public readonly static string LoadingConfigFile = "Loading unspecified variables from Config File.";
        public readonly static string ConnectingLogging = @"Connecting to Perforce server: {0}
Username: {1}";
        public readonly static string SourceLogging = @"Copy To Location: {2}
Paths To Copy:
   {3}";
        public readonly static string CopyFileLogging = $"Copying perforce file from: {{0}}{System.Environment.NewLine}   to: {{1}}";
        public readonly static string LoggingOut = "Logging out of Perforce Server.";
    }
}
