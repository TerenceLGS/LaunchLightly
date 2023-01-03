namespace LaunchLightly.Models; 

public class LdApiKeys {
	public LdApiKeys(string? api, string? sdk, string? project, string? environment, string? key) {
		ApiKey = api;
		SdkKey = sdk;
		Project = project;
		Environment = environment;
		Key = key;
	}

	public LdApiKeys() { }
	public string? ApiKey { get; set; }
	public string? SdkKey { get; set; }
	public string? Project { get; set; }
	public string? Environment { get; set; }
	/// <summary> Segment or Flag key </summary>
	public string? Key { get; set; }
}