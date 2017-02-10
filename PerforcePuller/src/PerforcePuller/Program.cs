using System;
using System.Linq;
using System.Reflection;
using CommandLine;
using log4net;
using PerforcePuller.Constants;
using PerforcePuller.Helpers;
using PerforcePuller.Models;

namespace PerforcePuller
{
    class Program
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        static void Main(string[] args)
        {
#if DEBUG
            System.Diagnostics.Debugger.Launch();
#endif
            var p4Model = new PerforceCommand();
            var parsingStatus = Parser.Default.ParseArguments(args, p4Model);

            try
            {
                if (!parsingStatus)
                {
                    Log.Info(p4Model.GetUsage());
                    return;
                }
                else
                {
                    if (p4Model.LoadFromConfig)
                    {
                        if (p4Model.Verbose)
                        {
                            Log.Info(LoggingStrings.LoadingConfigFile);
                        }
                        try
                        {
                            if (string.IsNullOrWhiteSpace(p4Model.Server))
                            {
                                p4Model.Server = AppSettingsHelper.Get<string>(ResourceStrings.PerforceServerKey);
                            }
                            if (string.IsNullOrWhiteSpace(p4Model.Username))
                            {
                                p4Model.Username = AppSettingsHelper.Get<string>(ResourceStrings.PerforceUsernameKey);
                            }
                            if (string.IsNullOrWhiteSpace(p4Model.Password))
                            {
                                p4Model.Password = AppSettingsHelper.Get<string>(ResourceStrings.PerforcePasswordKey);
                            }
                            if (string.IsNullOrWhiteSpace(p4Model.Timeout.ToString()))
                            {
                                p4Model.Timeout = AppSettingsHelper.Get<int>(ResourceStrings.PerforceTimeoutKey);
                            }
                        }
                        catch(Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }
                    }
                    if (p4Model.Verbose)
                    {
                        Log.Info(string.Format(LoggingStrings.ConnectingLogging, p4Model.Server, p4Model.Username, p4Model.Location, string.Join(ResourceStrings.SourceListJoiner, p4Model.SourcesList)));
                    }
                    var p4Repo = PerforceHelpers.GetPerforceRepo(p4Model.Server, p4Model.Username, p4Model.Password, p4Model.Timeout);
                    var dictionaryToUse = PerforceHelpers.CreateSourceDictionary(p4Repo, p4Model.Location, p4Model.SourcesList.ToList());
                    foreach (var entry in dictionaryToUse)
                    {
                        if (p4Model.Verbose)
                        {
                            Log.Info(string.Format(LoggingStrings.CopyFileLogging, entry.Value, entry.Key));
                        }
                        PerforceHelpers.CopyFile(p4Repo, entry.Key, entry.Value);
                    }
                    if (p4Model.Verbose)
                    {
                        Log.Info(LoggingStrings.LoggingOut);
                    }
                    PerforceHelpers.PerforceLogout(p4Repo);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
    }
}
