using System.Collections.Generic;

namespace LaunchLightly.Models.LaunchDarkly.JsonPatch;

public class ClauseOperation
{
    //public string? _id { get; set; }
    public string? attribute { get; set; }
    public string? op { get; set; }
    public List<string> values { get; set; } = new List<string>();
    public bool negate { get; set; }
}