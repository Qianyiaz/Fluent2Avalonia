using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Fluent2Avalonia.Demo.Services;
using Fluent2Avalonia.Demo.Views;

namespace Fluent2Avalonia.Demo;

public class App : Application
{
    public override void Initialize() => AvaloniaXamlLoader.Load(this);

    public override void OnFrameworkInitializationCompleted()
    {
        using var sp = new MyServiceProvider();
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            desktop.MainWindow = sp.GetService<MainWindow>();
        base.OnFrameworkInitializationCompleted();
    }
}