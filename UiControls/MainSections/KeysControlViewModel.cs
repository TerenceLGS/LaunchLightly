using System;
using System.ComponentModel;
using LaunchLightly.ViewModels;
using ReactiveUI;

namespace LaunchLightly.UiControls.MainSections;

public class KeysControlViewModel : ViewModelBase {
	private bool? _revealKeys = true;
	private string _sdkKey = String.Empty;
	private string _apiKey = String.Empty;
	private string _projectId = String.Empty;
	private string _environmentKey = String.Empty;
	private string _flagId = String.Empty;

	public string SdkKey {
		get => _sdkKey;
		set {
			_sdkKey = value;
			this.RaisePropertyChanged();
		}
	}

	public string ApiKey {
		get => _apiKey;
		set {
			_apiKey = value;
			this.RaisePropertyChanged();
		}
}

	public string ProjectId {
		get => _projectId;
		set {
			_projectId = value;
			this.RaisePropertyChanged();
		}
}

	public string EnvironmentKey {
		get => _environmentKey;
		set {
			_environmentKey = value;
			this.RaisePropertyChanged();
		}
	}

	public string FlagId {
		get => _flagId;
		set {
			_flagId = value;
			this.RaisePropertyChanged();
		}
}

	public bool? RevealKeys {
		get => _revealKeys;
		set {
			_revealKeys = value;
			this.RaisePropertyChanged();
		}
	}
}