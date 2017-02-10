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
        public const string PerforceServerKey = "PerforceServer";
        public const string PerforceUsernameKey = "PerforceUsername";
        public const string PerforcePasswordKey = "PerforcePassword";
        public const string PerforceTimeoutKey = "PerforceTimeout";

        public readonly static string ForwardSlashEllipseFormat = $"{{0}}{ForwardSlash}{Ellipse}";
        public readonly static string EllipseFormat = $"{{0}}{Ellipse}";
        public readonly static string BackSlashFormat = $"{{0}}{BackSlash}";
        public readonly static string SourceListJoiner = $"{System.Environment.NewLine}   ";
    }
}
