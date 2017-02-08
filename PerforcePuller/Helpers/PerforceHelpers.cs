using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Perforce.P4;
using PerforcePuller.Constants;

namespace PerforcePuller.Helpers
{
    internal static class PerforceHelpers
    {
        internal static void CopyFile(Repository p4Repo, string copyToLocation,  string sourceFile)
        {
            GetFileContentsCmdOptions fileContentsOptions = new GetFileContentsCmdOptions(GetFileContentsCmdFlags.Suppress, copyToLocation);
            var depotObj = new DepotPath(sourceFile);
            p4Repo.GetFileContents(fileContentsOptions, new FileSpec(depotObj, null));
        }

        internal static Dictionary<string, string> CreateSourceDictionary(Repository p4Repo, string copyToLocation,  List<string> sourcePathList)
        {
            var errorList = new Dictionary<string, Exception>();
            var folderSourceDictionary = new Dictionary<string, string>();
            var parentFolder = string.Empty;
            foreach (var sourcePath in sourcePathList)
            {
                try
                {
                    var getDepotFilesCommand = new GetDepotFilesCmdOptions(GetDepotFilesCmdFlags.NotDeleted, 0);
                    var formattedSource = FileNameHelper.PerforcePathFormatter(sourcePath);
                    var trimmedSource = FileNameHelper.PerforceRootFolder(formattedSource);

                    var fileSpec = new FileSpec(new DepotPath(formattedSource), null);
                    var fileList = p4Repo.GetFiles(getDepotFilesCommand, fileSpec);

                    if (fileList == null)
                    {
                        throw new Exception(string.Format(ResourceStrings.UnableToLocateFile, formattedSource));
                    }
                    else
                    {
                        var fileListPaths = fileList.Select(x => x.DepotPath.Path.ToLowerInvariant()).ToList();
                        foreach (var file in fileListPaths)
                        {
                            var strippedPerforceFilePath = file.Replace(trimmedSource, string.Empty);
                            var transformedPath = Path.Combine(copyToLocation, strippedPerforceFilePath).Replace(ResourceStrings.ForwardSlash, ResourceStrings.BackSlash);
                            if (!folderSourceDictionary.ContainsKey(transformedPath))
                            {
                                folderSourceDictionary.Add(transformedPath, file);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    errorList.Add(sourcePath, ex);
                }
            }
            if (errorList.Count > 0)
            {
                throw new Exception(string.Join(Environment.NewLine, errorList.Select(x => "Error with Path: " + x.Key + " : " + x.Value.Message)));
            }
            return folderSourceDictionary;
        }

        internal static Repository GetPerforceRepo(string serverAddress, string username, string password, int timeout)
        {
            var p4Repo = new Repository(new Server(new ServerAddress(serverAddress)));
            var p4Client = new Client();
            var p4Options = new Options();
            var p4TimeSpan = new TimeSpan(0, 0, timeout, 0);
            p4Options[ResourceStrings.PasswordKeyType] = password;
            p4Client.Options = ClientOption.AllWrite;
            p4Repo.Connection.Client = p4Client;
            p4Repo.Connection.UserName = username;
            p4Repo.Connection.CommandTimeout = p4TimeSpan;
            p4Repo.Connection.Connect(p4Options);
            p4Repo.Connection.Login(password);
            return p4Repo;
        }

        internal static void PerforceLogout(Repository p4Repo)
        {
            p4Repo.Connection.Logout(new LogoutCmdOptions(LogoutCmdFlags.None, null));
        }
    }
}
