using System.Collections.Generic;

namespace LaunchLightly.Models.LaunchDarkly.Semantic;

public class Instruction<T> {
	public string? kind { get; set; }
	public string? ruleId { get; set; }
	public string? clauseId { get; set; }
	public List<T> values { get; set; } = new List<T>();
}