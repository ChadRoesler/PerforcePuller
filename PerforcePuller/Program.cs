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
                    if (p4Model.Verbose)
                    {
                        Log.Info(string.Format(ResourceStrings.ConnectingLogging, p4Model.Server, p4Model.Username, p4Model.Location, string.Join(ResourceStrings.SourceListJoiner,p4Model.SourcesList)));
                    }
                    var p4Repo = PerforceHelpers.GetPerforceRepo(p4Model.Server, p4Model.Username, p4Model.Password, p4Model.Timeout);
                    var dictionaryToUse = PerforceHelpers.CreateSourceDictionary(p4Repo, p4Model.Location, p4Model.SourcesList.ToList());
                    foreach (var entry in dictionaryToUse)
                    {
                        if (p4Model.Verbose)
                        {
                            Log.Info(string.Format(ResourceStrings.CopyFileLogging, entry.Value, entry.Key));
                        }
                        PerforceHelpers.CopyFile(p4Repo, entry.Key, entry.Value);
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
