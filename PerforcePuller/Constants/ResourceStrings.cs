namespace PerforcePuller.Constants
{
    internal sealed class ResourceStrings
    {
        public const string Ellipse = "...";
        public const string ForwardSlash = "/";
        public const string ForwardSlashEllipse = "/...";
        public const string BackSlash = @"\";
        public const string PasswordKeyType = "Password";
        public const string RootFolderSearch = "{0}/*";

        public readonly static string ForwardSlashEllipseFormat = $"{{0}}{ForwardSlash}{Ellipse}";
        public readonly static string EllipseFormat = $"{{0}}{Ellipse}";
        public readonly static string BackSlashFormat = $"{{0}}{BackSlash}";
        public readonly static string SourceListJoiner = $"{System.Environment.NewLine}   ";

        public readonly static string ConnectingLogging = @"Connecting To Perforce Server: {0}
Username: {1}";
        public readonly static string SourceLogging = @"Copy To Location: {2}
Paths To Copy:
   {3}";
        public readonly static string CopyFileLogging = $"Copying perforce file from: {{0}}{System.Environment.NewLine}   to: {{1}}";
        public readonly static string UnableToLocateFile = $"Unable to locate the following path in the perforce server: {{0}}{System.Environment.NewLine}Please validate that the path above exists.";
    }
}
