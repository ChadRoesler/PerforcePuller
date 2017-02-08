using System.Collections.Generic;

namespace PerforcePuller.Models.Interfaces
{
    public interface IPathSettings
    {
        string Location { get; set; }
        IList<string> SourcesList { get; set; }
    }
}
