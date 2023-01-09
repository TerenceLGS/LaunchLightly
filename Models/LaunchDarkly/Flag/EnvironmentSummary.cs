using System.Collections.Generic;

namespace LaunchLightly.Models.LaunchDarkly;

public class EnvironmentSummary {
	public long prerequisites { get; set; }

	public Dictionary<string, EnvironmentVariation> variations { get; set; }
}