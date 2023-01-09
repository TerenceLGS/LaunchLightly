using System;
using System.Collections.Generic;

namespace LaunchLightly.Models.LaunchDarkly;

public class Rule {
    public string? _id { get; set; }

    public List<Clause> clauses { get; set; } = new();

    public Guid? @ref { get; set; }

    public bool? trackEvents { get; set; }

    public long? variation { get; set; }
}
