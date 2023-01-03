using LaunchLightly.ViewModels;
using ReactiveUI;

namespace LaunchLightly.UiControls.MainSections;

public class ResultsControlViewModel : ViewModelBase {
	private string _resultsJson;

	public string ResultsJson {
		get => _resultsJson;
		set {
			_resultsJson = value;
			this.RaisePropertyChanged();
		}
	}
}