using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.TextFormatting;
using Fluent2Avalonia.Icons.Models;

namespace Fluent2Avalonia.Icons.Controls;

public partial class SymbolIcon : IconElement
{
    private TextLayout? _textLayout;

    static SymbolIcon()
    {
        AffectsMeasure<SymbolIcon>(SymbolProperty, FontSizeProperty);
        AffectsRender<SymbolIcon>(SymbolProperty, FontSizeProperty, ForegroundProperty);
    }

    [GeneratedStyledProperty(DefaultValue = 16d)]
    public partial double IconSize { get; set; }

    [GeneratedStyledProperty] public partial object? Symbol { get; set; }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == SymbolProperty || change.Property == FontSizeProperty ||
            change.Property == ForegroundProperty)
            GenerateText();
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);

        if (_textLayout == null)
            GenerateText();
    }

    protected override Size MeasureOverride(Size availableSize)
    {
        if (_textLayout == null)
            GenerateText();

        return new Size(_textLayout!.Width, _textLayout.Height);
    }

    public override void Render(DrawingContext context)
    {
        if (_textLayout == null) GenerateText();

        var dstRect = new Rect(Bounds.Size);
        using (context.PushClip(dstRect))
        {
            var pt = new Point(
                dstRect.Center.X - _textLayout!.Width * 0.5,
                dstRect.Center.Y - _textLayout.Height * 0.5);
            _textLayout.Draw(context, pt);
        }
    }

    private void GenerateText()
    {
        _textLayout?.Dispose();
        _textLayout = null;

        if (Symbol == null) return;
        var glyph = Symbol switch
        {
            Symbols s => char.ConvertFromUtf32((int)s),
            string str => str,
            _ => throw new ArgumentException($"Invalid Symbol type: {Symbol?.GetType().FullName}")
        };

        if (this.TryFindResource("WinSymbolsFont", out var res) && res is FontFamily ff)
            _textLayout = new TextLayout(glyph, new Typeface(ff), IconSize, Foreground);
    }
}