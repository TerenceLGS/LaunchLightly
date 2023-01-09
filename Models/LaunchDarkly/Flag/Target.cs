using System.Collections.Generic;

namespace LaunchLightly.Models.LaunchDarkly;

public class Target {
	public List<string>? values { get; set; }

	public long? variation { get; set; }
}