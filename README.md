# PerforcePuller

A Handly Little Tool for pulling files and directories from Perfroce

Allows loading from the config file if desired.
If -c is passed and values that are in the config file are passed as arguments the values passed will supersede the config file values.

-f is a string list that allows for multiple paths to be specified.  It will also auto append /... or ... in the case of folder paths passed.

Below is the generated help.


  -c, --loadFromConfig    (Default: False) Load Perforce settings from Config
                          File.

  -s, --server            Address of the P4 Server you are attempting to reach.

  -u, --username          Username to log into P4 with.

  -p, --password          Password to log into P4 with.

  -t, --timeout           (Default: 15) Timeout of Command in minutes.

  -f, --sourceList        List of Sources in P4 to pull files from, can be
                          Files or Folders.

  -l, --location          Required. Location to place files pulled from P4.

  -v, --verbose           (Default: False) Print info as Tasks are run.
