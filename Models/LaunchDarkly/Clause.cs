using System.Collections.Generic;

namespace LaunchLightly.Models.LaunchDarkly;

public class Clause {
	public string? _id { get; set; }

	public string? attribute { get; set; }

	public string? op { get; set; }

	public List<string> values { get; set; } = new ();

	public bool negate { get; set; }
}