namespace PerforcePuller.Constants
{
    internal sealed class ErrorStrings
    {
        public readonly static string UnableToConvertAppKey = "Unable to convert app key [{0}] value: {1} to type {2}.";
        public readonly static string UnableToLocateAppKey = "Unable to locate app key: {0}";
        public readonly static string UnableToLocateFile = $"Unable to locate the following path in the perforce server: {{0}}{System.Environment.NewLine}Please validate that the path above exists.";
        public readonly static string DictionaryErrors = $"Error found in path: {{0}} {System.Environment.NewLine}   {{1}}";
    }
}
