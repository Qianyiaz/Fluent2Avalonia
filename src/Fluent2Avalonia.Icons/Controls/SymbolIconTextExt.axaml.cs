using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace Fluent2Avalonia.Icons.Controls;

public class SymbolIconTextExt : MarkupExtension
{
    public SymbolIconTextExt()
    {
    }

    public SymbolIconTextExt(object? symbol, string? text)
    {
        Symbol = symbol;
        Text = text;
    }

    public object? Symbol { get; set; }
    public string? Text { get; set; }
    public double? IconSize { get; set; }
    public double? Spacing { get; set; }
    public Dock? IconPlacement { get; set; }
    public double? IconSlotWidth { get; set; }
    public TextAlignment? TextAlignment { get; set; }
    public bool? IsTextSelectable { get; set; }
    public IBrush? IconForeground { get; set; }
    public HorizontalAlignment? HorizontalContentAlignment { get; set; }
    public VerticalAlignment? VerticalContentAlignment { get; set; }
    public double? FontSize { get; set; }
    public IBrush? Foreground { get; set; }

    public override object ProvideValue(IServiceProvider _)
    {
        var icon = new SymbolIconText
        {
            Symbol = Symbol,
            Text = Text
        };

        if (FontSize != null) icon.FontSize = FontSize.Value;
        if (Foreground != null) icon.Foreground = Foreground;
        if (IconSize != null) icon.IconSize = IconSize.Value;
        if (Spacing != null) icon.Spacing = Spacing.Value;
        if (IconPlacement != null) icon.IconPlacement = IconPlacement.Value;
        if (TextAlignment != null) icon.TextAlignment = TextAlignment.Value;
        if (IsTextSelectable != null) icon.IsTextSelectable = IsTextSelectable.Value;
        if (IconForeground != null) icon.IconForeground = IconForeground;
        if (IconSlotWidth != null) icon.IconSlotWidth = IconSlotWidth.Value;
        if (HorizontalContentAlignment != null) icon.HorizontalContentAlignment = HorizontalContentAlignment.Value;
        if (VerticalContentAlignment != null) icon.VerticalContentAlignment = VerticalContentAlignment.Value;

        return icon;
    }
}