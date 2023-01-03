using Avalonia.Controls;
using LaunchLightly.ViewModels;

namespace LaunchLightly.Views;

public partial class ConfigWindow : Window
{
    public ConfigWindow() {
        DataContext = new ConfigWindowViewModel();
        InitializeComponent();
    }
}