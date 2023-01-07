namespace LaunchLightly.Models.LaunchDarkly.JsonPatch;
public class Operation
{
    /// <summary>
    /// The operation type requested by this section of payload: add, replace, ?remove
    /// </summary>
    public string? op { get; set; }

    public string? path { get; set; }
    public string? value { get; set; }
}

