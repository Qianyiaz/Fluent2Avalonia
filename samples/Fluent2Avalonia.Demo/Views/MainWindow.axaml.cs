using Avalonia.Controls;
using Fluent2Avalonia.Demo.ViewModels;

namespace Fluent2Avalonia.Demo.Views;

public partial class MainWindow : Window
{
    public MainWindow() => InitializeComponent();

    public MainWindow(MainWindowViewModel vm) : this() => DataContext = vm;
}