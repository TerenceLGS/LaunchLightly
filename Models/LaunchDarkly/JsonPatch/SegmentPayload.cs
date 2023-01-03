using System.Collections.Generic;

namespace LaunchLightly.Models.LaunchDarkly.JsonPatch;
public class SegmentPayload
{
    public List<Operation> patch { get; set; } = new List<Operation>();
}
