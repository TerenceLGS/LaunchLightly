namespace LaunchLightly.Models.LaunchDarkly.JsonPatch;
public class Operation
{
    /// <summary>
    /// The operation type requested by this section of payload
    /// </summary>
    public string op { get; set; }

    public string path { get; set; }
}

