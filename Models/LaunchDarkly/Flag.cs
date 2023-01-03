namespace LaunchLightly.Models.LaunchDarkly;

public class Flag {
	public string? name { get; set; }

	public string? key { get; set; }

	public Links? _links { get; set; }

	public Site? _site { get; set; }
}