namespace LaunchLightly.Models.LaunchDarkly;

public class VariationValue {
	public long nullRules { get; set; }
	public long rules { get; set; }
	public long targets { get; set; }
	public bool? isFallthrough { get; set; }
	public bool? isOff { get; set; }
}