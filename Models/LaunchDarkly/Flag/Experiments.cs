using System.Collections.Generic;

namespace LaunchLightly.Models.LaunchDarkly;

public class Experiments {
	public long? baselineIdx { get; set; }

	public List<string>? items { get; set; }
}