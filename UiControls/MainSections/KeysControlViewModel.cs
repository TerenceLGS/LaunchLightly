using System;
using LaunchLightly.ViewModels;

namespace LaunchLightly.UiControls.MainSections;

public class KeysControlViewModel : ViewModelBase {
	
	public string SdkKey { get; set; } = "String.Empty";
	public string ApiKey { get; set; } = String.Empty;
	public string ProjectId { get; set; } = String.Empty;
	public string EnvironmentKey { get; set; } = String.Empty;
	public string FlagId { get; set; } = String.Empty;
	public bool? RevealKeys { get; set; } = true;

	public string? PasswordChar => RevealKeys.GetValueOrDefault() ? null : "~";
}