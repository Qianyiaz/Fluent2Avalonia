using Fluent2Avalonia.Demo.ViewModels;
using Fluent2Avalonia.Demo.Views;
using Fluent2Avalonia.Icons.Models;
using Jab;

namespace Fluent2Avalonia.Demo.Services;

[ServiceProvider]
[Transient<MainWindow>]
[Transient<HomePage>]
[Transient<SettingsPage>]
[Transient<MainWindowViewModel>]
[Singleton<NavigationPageFactory>(Factory = nameof(CreateNavigationPageFactory))]
public sealed partial class MyServiceProvider
{
    private static NavigationPageFactory CreateNavigationPageFactory(IServiceProvider sp) => sp.GetService;
}