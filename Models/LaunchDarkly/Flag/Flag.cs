using System;
using System.Collections.Generic;

namespace LaunchLightly.Models.LaunchDarkly;

public class Flag {
    public Links? _links { get; set; }

    public Maintainer? _maintainer { get; set; }

    public long? _version { get; set; }

    public bool archived { get; set; }

    public ClientSideAvailability? clientSideAvailability { get; set; }

    public long? creationDate { get; set; }

    public List<string>? customProperties { get; set; }

    public Defaults? defaults { get; set; }

    public string description { get; set; }

    public Dictionary<string, Environment> environments { get; set; }

    public Experiments experiments { get; set; }

    public List<string> goalIds { get; set; }

    public bool includeInSnippet { get; set; }

    public string key { get; set; }

    public string kind { get; set; }

    public string maintainerId { get; set; }

    public string name { get; set; }

    public List<string> tags { get; set; }

    public bool temporary { get; set; }

    public object variationJsonSchema { get; set; }

    public List<VariationElement> variations { get; set; }
}