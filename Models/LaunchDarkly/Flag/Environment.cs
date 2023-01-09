using System.Collections.Generic;

namespace LaunchLightly.Models.LaunchDarkly;

public class Environment {
	public string? _environmentName { get; set; }

	public Site? _site { get; set; }

	public EnvironmentSummary? _summary { get; set; }

	public bool? archived { get; set; }

	public FallThrough? fallthrough { get; set; }

	public long? lastModified { get; set; }

	public long? offVariation { get; set; }

	public bool? on { get; set; }

	public List<string>? prerequisites { get; set; }

	public List<Rule>? rules { get; set; }

	public string? salt { get; set; }

	public string? sel { get; set; }

	public List<Target>? targets { get; set; }

	public bool? trackEvents { get; set; }

	public bool? trackEventsFallthrough { get; set; }

	public long? version { get; set; }
}