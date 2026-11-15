using Avalonia;
using Avalonia.Controls;

namespace Fluent2Avalonia.Controls;

public partial class NavigationViewItem : RadioButton
{
    [GeneratedDirectProperty] public partial Type? TargetPageType { get; set; }

    [GeneratedStyledProperty] public partial object? Icon { get; set; }
}