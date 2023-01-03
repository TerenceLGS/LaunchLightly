using System;
using System.Reactive;
using ReactiveUI;

namespace LaunchLightly.ViewModels;

public class MainWindowViewModel : ViewModelBase {
	public MainWindowViewModel() {
		OpenConfigWindow = ReactiveCommand.Create(OpenAWindow);
	}

	public ReactiveCommand<Unit, Unit> OpenConfigWindow { get; }

	private bool _alreadyOpenedConfigWindow = false;

	public void OpenAWindow() {
		if (!_alreadyOpenedConfigWindow) {
			//ConfigWindow window = new();
			//window.Show();
			_alreadyOpenedConfigWindow = true;
		}
	}

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

	private string _resultsJson;

	public string ResultsJson {
		get => _resultsJson;
		set {
			_resultsJson = value;
			this.RaisePropertyChanged();
		}
	}
}