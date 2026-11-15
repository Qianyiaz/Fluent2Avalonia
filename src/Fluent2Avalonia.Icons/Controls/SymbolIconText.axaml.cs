using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using Avalonia.Media;

namespace Fluent2Avalonia.Icons.Controls;

[TemplatePart("PART_Icon", typeof(SymbolIcon))]
[TemplatePart("PART_TextBlock", typeof(TextBlock))]
[TemplatePart("PART_SelectableTextBlock", typeof(SelectableTextBlock))]
public partial class SymbolIconText : TemplatedControl
{
    private SymbolIcon? _icon;

    [GeneratedStyledProperty] public partial object? Symbol { get; set; }

    [GeneratedStyledProperty] public partial string? Text { get; set; }

    [GeneratedStyledProperty(DefaultValue = 16d)]
    public partial double IconSize { get; set; }

    [GeneratedStyledProperty(DefaultValue = 8d)]
    public partial double Spacing { get; set; }

    [GeneratedStyledProperty(DefaultValue = Dock.Left)]
    public partial Dock IconPlacement { get; set; }

    [GeneratedStyledProperty(DefaultValue = TextAlignment.Left)]
    public partial TextAlignment TextAlignment { get; set; }

    [GeneratedStyledProperty(DefaultValue = double.NaN)]
    public partial double IconSlotWidth { get; set; }

    [GeneratedStyledProperty] public partial bool IsTextSelectable { get; set; }

    [GeneratedStyledProperty] public partial IBrush? IconForeground { get; set; }

    [GeneratedStyledProperty(DefaultValue = HorizontalAlignment.Left)]
    public partial HorizontalAlignment HorizontalContentAlignment { get; set; }

    [GeneratedStyledProperty(DefaultValue = VerticalAlignment.Center)]
    public partial VerticalAlignment VerticalContentAlignment { get; set; }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        _icon = e.NameScope.Find<SymbolIcon>("PART_Icon");
        ApplyIconForeground();
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == IconForegroundProperty)
            ApplyIconForeground();
    }

    private void ApplyIconForeground()
    {
        if (_icon is null) return;

        if (IconForeground is { } fg)
            _icon.Foreground = fg;
        else
            _icon.ClearValue(ForegroundProperty);
    }
}