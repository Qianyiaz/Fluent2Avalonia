using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace Fluent2Avalonia.Icons.Controls;

public class SymbolIconExt : MarkupExtension
{
    public SymbolIconExt()
    {
    }

    public SymbolIconExt(object? symbol) => Symbol = symbol;

    public object? Symbol { get; set; }

    public double IconSize { get; set; }

    public IBrush? Foreground { get; set; }

    public override object ProvideValue(IServiceProvider _)
    {
        var icon = new SymbolIcon { Symbol = Symbol };

        if (Foreground != null)
            icon.Foreground = Foreground;

        if (IconSize > 0)
            icon.IconSize = IconSize;

        return icon;
    }
}