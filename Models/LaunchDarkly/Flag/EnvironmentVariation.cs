namespace LaunchLightly.Models.LaunchDarkly;

public class EnvironmentVariation {
	public bool? isFallthrough { get; set; }

	public bool? isOff { get; set; }

	public long? nullRules { get; set; }

	public long? rules { get; set; }

	public long? targets { get; set; }
}