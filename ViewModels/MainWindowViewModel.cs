using System.Reactive;
using LaunchLightly.UiControls.MainSections;
using ReactiveUI;

namespace LaunchLightly.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel() {
        OpenConfigWindow = ReactiveCommand.Create(OpenAWindow);
        KeysContent = new KeysControlViewModel();
        ResultsContent = new ResultsControlViewModel();
        BulkUpdateContent = new BulkUpdateControlViewModel();
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
    
    public KeysControlViewModel KeysContent { get; set; }
    public ResultsControlViewModel ResultsContent { get; set; }
    public BulkUpdateControlViewModel BulkUpdateContent { get; set; }
}
