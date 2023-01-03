using System;

namespace LaunchLightly.ViewModels; 

public class SecondWindowViewModel : ViewModelBase {
	
}

public class ConfigWindowViewModel : ViewModelBase {
	
	public string SdkKey { get; set; } = String.Empty;
	public string ApiKey { get; set; } = String.Empty;
	public string ProjectId { get; set; } = String.Empty;
	public string EnvironmentKey { get; set; } = String.Empty;
	public string FlagId { get; set; } = String.Empty;
	public bool RevealKeys { get; set; } = true;
}

public class ResultsWindowViewModel : ViewModelBase {
	public string ResultsJson { get; set; }
}