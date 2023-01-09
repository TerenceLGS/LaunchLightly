using System.Collections.Generic;

namespace LaunchLightly.Models.LaunchDarkly;

public class EnviroSummary {
	public long? prerequisites { get; set; }

	public Dictionary<string, EnvironmentVariation>? variations { get; set; }
}