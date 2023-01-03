using System.Collections.Generic;

namespace LaunchLightly.Models.LaunchDarkly;

public class Segment {
    public string? name { get; set; }

    public string? description { get; set; }

    public List<string?> tags { get; set; }

    public long? creationDate { get; set; }

    public string? key { get; set; }

    public List<string> included { get; set; }

    public List<string> excluded { get; set; }

    public Links? _links { get; set; }

    public List<Rule> rules { get; set; } = new List<Rule>();

    public int? version { get; set; }

    public bool deleted { get; set; }

    public List<Flag> _flags { get; set; } = new List<Flag>();

    public int? generation { get; set; }
}