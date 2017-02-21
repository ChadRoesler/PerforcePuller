using System;
using System.IO;
using System.Linq;
using PerforcePuller.Constants;

namespace PerforcePuller.Helpers
{
    public static class FileNameHelper
    {
        public static string PerforcePathFormatter(string originalPath)
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
                    // If the folder path does not end with /...
                    if (loweredPath.Substring(loweredPath.Length - 4, 4) != ResourceStrings.ForwardSlashEllipse)
                    {
                        // If the folder path does not end with / append /....
                        if (loweredPath.Substring(loweredPath.Length - 1, 1) == ResourceStrings.ForwardSlash)
                        {
                            finalizedPath = string.Format(ResourceStrings.EllipseFormat, loweredPath);
                        }
                        // If the folder path ends with / append ...
                        else
                        {
                            finalizedPath = string.Format(ResourceStrings.ForwardSlashEllipseFormat, loweredPath);
                        }
                    }
                }
                //This is more so if grabbing root for everything, lets hope this never gets passed because thats a lot of files to grab
                else
                {
                    // If the folder path does not end with / append /....
                    if (loweredPath.Substring(loweredPath.Length - 1, 1) == ResourceStrings.ForwardSlash)
                    {
                        finalizedPath = string.Format(ResourceStrings.EllipseFormat, loweredPath);
                    }
                    // If the folder path ends with / append ...
                    else
                    {
                        finalizedPath = string.Format(ResourceStrings.ForwardSlashEllipseFormat, loweredPath);
                    }
                }
            }
            return finalizedPath;
        }

        public static string PerforceGetParentFolder(string originalPath)
        {
            var childName = Path.GetFileName(originalPath);
            // Checking if its a folder
            // if it is the child name is the folder plus the /...
            // We fake a local path for gathering direct folders under depots:  //Folder/SubFolder/...
            if (string.IsNullOrWhiteSpace(Path.GetExtension(originalPath)))
            {
                var splitPath = originalPath.Split('/');
                var childItems = splitPath.Skip(Math.Max(0, splitPath.Count() - 2));
                childName = string.Join("/", childItems);
            }
            var folderSource = originalPath.Replace(childName, string.Empty);
            return folderSource;
        }
    }
}
