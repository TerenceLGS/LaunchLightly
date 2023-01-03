using System.Collections.Generic;

namespace LaunchLightly.Models.LaunchDarkly.Semantic;
public class SemanticPatch<T>
    {
        public string? comment { get; set; } //optional
        public string? environmentKey { get; set; }
        public List<Instruction<T>> instructions { get; set; } = new List<Instruction<T>>();
    }