using System.IO;
using PerforcePuller.Constants;

namespace PerforcePuller.Helpers
{
    internal static class FileNameHelper
    {
        internal static string PerforcePathFormatter(string originalPath)
        {
            var finalizedPath = string.Empty;
            var loweredPath = originalPath.ToLowerInvariant();
            finalizedPath = loweredPath;

            //This is used to check if we are passing a file or a folder
            // if its a folder append ... or /... as needed
            if (string.IsNullOrWhiteSpace(Path.GetExtension(loweredPath)))
            {
                if (loweredPath.Length > 3)
                {
                    if (loweredPath.Substring(loweredPath.Length - 4, 4) != ResourceStrings.ForwardSlashEllipse)
                    {
                        if (loweredPath.Substring(loweredPath.Length - 1, 1) == ResourceStrings.ForwardSlash)
                        {
                            finalizedPath = string.Format(ResourceStrings.EllipseFormat, loweredPath);
                        }
                        else
                        {
                            finalizedPath = string.Format(ResourceStrings.ForwardSlashEllipseFormat, loweredPath);
                        }
                    }
                }
            }
            return finalizedPath;
        }

        internal static string PerforceRootFolder(string originalPath)
        {
            var childName = Path.GetFileName(originalPath);
            // Checking if its a folder
            // if it is the child name is the folder plus the /...
            if (string.IsNullOrWhiteSpace(Path.GetExtension(originalPath)))
            {
                var containingFolder = Path.GetPathRoot(originalPath).ToString().Replace(ResourceStrings.BackSlash, ResourceStrings.ForwardSlash);
                childName = originalPath.Replace(containingFolder, string.Empty).TrimStart('/');
            }
            var rootFolderSource = originalPath.Replace(childName, string.Empty);
            return rootFolderSource;
        }
    }
}
