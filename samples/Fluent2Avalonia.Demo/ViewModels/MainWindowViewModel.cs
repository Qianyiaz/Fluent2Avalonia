using Fluent2Avalonia.Icons.Models;

namespace Fluent2Avalonia.Demo.ViewModels;

public class MainWindowViewModel(NavigationPageFactory pageFactory)
{
    public NavigationPageFactory PageFactory => pageFactory;
}