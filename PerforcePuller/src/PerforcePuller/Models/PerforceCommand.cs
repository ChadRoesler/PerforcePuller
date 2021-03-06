﻿using System.Collections.Generic;
using CommandLine;
using CommandLine.Text;
using PerforcePuller.Constants;
using PerforcePuller.Models.Interfaces;

namespace PerforcePuller.Models
{
    public class PerforceCommand : IPerforceSettings, IPathSettings, ICommonOptions
    {
        private string LocalMasterLocation;
        [Option ('c', "loadFromConfig",HelpText = HelpStrings.LoadFromConfigText, DefaultValue = false)]
        public bool LoadFromConfig { get; set; }

        [Option ('s',"server", HelpText = HelpStrings.ServerText)]
        public string Server { get; set; }

        [Option('u', "username", HelpText = HelpStrings.UsernameText)]
        public string Username { get; set; }

        [Option('p', "password", HelpText = HelpStrings.PasswordText)]
        public string Password { get; set; }

        [Option('t', "timeout", HelpText = HelpStrings.TimeoutText, DefaultValue = 15)]
        public int Timeout { get; set; }

        [OptionList('f',"sourceList", HelpText = HelpStrings.SourceListText)]
        public IList<string> SourcesList { get; set; }

        [Option('l', "location", HelpText = HelpStrings.LocationText, Required = true)]
        public string Location
        {
            get
            {
                return LocalMasterLocation;
            }
            set
            {
                if (value.Length > 1)
                {
                    LocalMasterLocation = value;
                    if (value.Substring(value.Length - 1, 1) != ResourceStrings.BackSlash)
                    {
                        LocalMasterLocation = string.Format(ResourceStrings.BackSlashFormat, value);
                    }
                }
            }
        }

        [Option('v', "verbose", HelpText = HelpStrings.VerboseText, DefaultValue = false)]
        public bool Verbose { get; set; }

        [ParserState]
        public IParserState LastParserState { get; set; }

        [HelpVerbOption("help", HelpText = HelpStrings.HelpText)]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this);
        }
    }
}
