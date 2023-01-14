using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using LaunchLightly.Services;
using LaunchLightly.ViewModels;
using LaunchLightly.Views;

namespace LaunchLightly;

public partial class App : Application
{
	public override void Initialize()
	{
		AvaloniaXamlLoader.Load(this);
	}

	public override void OnFrameworkInitializationCompleted()
	{
		if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop) {
			ILaunchDarklyApi ld = new LaunchDarklyApi();
			desktop.MainWindow = new MainWindow
			{
				DataContext = new MainWindowViewModel(ld),
			};
		}

		base.OnFrameworkInitializationCompleted();
	}
}