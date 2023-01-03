using System.Reactive;
using LaunchLightly.Views;
using ReactiveUI;

namespace LaunchLightly.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel() {
        OpenConfigWindow = ReactiveCommand.Create(OpenAWindow);
    }
    
    public string Greeting => "Welcome to Avalonia!";

    public ReactiveCommand<Unit, Unit> OpenConfigWindow { get; }
    
    private bool _alreadyOpenedConfigWindow = false;

    public void OpenAWindow() {
        if (!_alreadyOpenedConfigWindow) {
            ConfigWindow window = new();
            window.Show();
            _alreadyOpenedConfigWindow = true;
        }
    }
}

