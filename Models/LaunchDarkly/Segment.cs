using System.Collections.Generic;

namespace LaunchLightly.Models.LaunchDarkly;

public class Segment {
	public string? name { get; set; }

	public string? description { get; set; }

	public List<string?> tags { get; set; } = new();

	public long? creationDate { get; set; }

	public string? key { get; set; }

	public List<string> included { get; set; } = new();

	public List<string> excluded { get; set; } = new();

	public Links? _links { get; set; }

	public List<Rule> rules { get; set; } = new ();

	public int? version { get; set; }

	public bool deleted { get; set; }

	public List<FlagLink> _flags { get; set; } = new ();

	public int? generation { get; set; }
}