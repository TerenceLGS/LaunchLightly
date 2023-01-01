namespace LaunchLightly.ViewModels; 

public class SecondWindowViewModel : ViewModelBase {
	
}

public class ConfigWindowViewModel : ViewModelBase {
	
	public string SdkKey { get; set; }
	public string ApiKey { get; set; }
	public string ProjectId { get; set; }
	public string FlagId { get; set; }
}

public class ResultsWindowViewModel : ViewModelBase {
	public string ResultsJson { get; set; }
}