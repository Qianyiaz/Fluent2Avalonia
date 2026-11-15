using System.Collections;
using System.Diagnostics.CodeAnalysis;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Fluent2Avalonia.Converters;
using Fluent2Avalonia.Icons.Models;

namespace Fluent2Avalonia.Controls;

[TemplatePart(TogglePaneButtonPart, typeof(Button))]
[TemplatePart(BackButtonPart, typeof(Button))]
[TemplatePart(SplitViewPart, typeof(SplitView))]
public partial class NavigationView : ContentControl
{
    #region Constants

    private const string TogglePaneButtonPart = "PART_TogglePaneButton";
    private const string BackButtonPart = "PART_BackButton";
    private const string SplitViewPart = "PART_SplitView";

    #endregion

    #region Fields

    private readonly Stack<Type> _backStack = new();
    private readonly Dictionary<Type, object> _pageCache = new();
    private Button? _backButton;
    private SplitView? _splitView;
    private Button? _toggleButton;
    private bool _isNavigatingBack;
    private bool _isInitialized;

    #endregion

    #region Overrides

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        _toggleButton = e.NameScope.Find<Button>(TogglePaneButtonPart)!;
        _backButton = e.NameScope.Find<Button>(BackButtonPart)!;
        _splitView = e.NameScope.Find<SplitView>(SplitViewPart)!;

        _toggleButton.Click += (_, _) => IsPaneOpen = !IsPaneOpen;
        _backButton.Click += BackRequested ?? ((_, _) => Back());

        _backButton.IsVisible = IsBackButtonVisible;
        _backButton.IsEnabled = IsBackEnabled;
        _toggleButton.IsVisible = IsPaneToggleButtonVisible;
        _splitView.IsPaneOpen = IsPaneOpen;

        if (MenuItemsSource != null)
            MenuItems = [.. MenuItemsSource.Cast<object>()];
        if (FooterMenuItemsSource != null)
            FooterMenuItems = [.. FooterMenuItemsSource.Cast<object>()];

        AttachItems(MenuItems);
        AttachItems(FooterMenuItems);

        if (TargetPageType != null)
            UpdateContent(TargetPageType);

        _isInitialized = true;
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (!_isInitialized) return;
        if (change.Property == TargetPageTypeProperty && change.NewValue is Type newType)
        {
            if (!_isNavigatingBack && change.OldValue is Type oldType && oldType != newType)
                _backStack.Push(oldType);

            UpdateContent(newType);
            CanBack = _backStack.Count > 0;
        }
        else if (change.Property == IsPaneOpenProperty)
        {
            _splitView!.IsPaneOpen = IsPaneOpen;
            if (_toggleButton != null)
                ToolTip.SetTip(_toggleButton, IsPaneOpen ? "Close Navigation" : "Open Navigation");
            UpdateItemToolTips(MenuItems);
            UpdateItemToolTips(FooterMenuItems);
        }
        else if (change.Property == IsBackButtonVisibleProperty)
        {
            _backButton!.IsVisible = IsBackButtonVisible;
        }
        else if (change.Property == IsBackEnabledProperty)
        {
            _backButton!.IsEnabled = IsBackEnabled;
        }
        else if (change.Property == IsPaneToggleButtonVisibleProperty)
        {
            _toggleButton!.IsVisible = IsPaneToggleButtonVisible;
        }
        else if (change.Property == MenuItemsSourceProperty)
        {
            MenuItems = [.. MenuItemsSource!.Cast<object>()];
            AttachItems(MenuItems);
        }
        else if (change.Property == FooterMenuItemsSourceProperty)
        {
            FooterMenuItems = [.. FooterMenuItemsSource!.Cast<object>()];
            AttachItems(FooterMenuItems);
        }
    }

    protected override void OnSizeChanged(SizeChangedEventArgs e)
    {
        base.OnSizeChanged(e);
        var shouldOpen = e.NewSize.Width > 1000;
        if (shouldOpen != IsPaneOpen)
            SetCurrentValue(IsPaneOpenProperty, shouldOpen);
    }

    #endregion

    #region Private Methods

    private void UpdateItemToolTips(IList<object>? items)
    {
        if (items == null) return;
        foreach (var obj in items)
            if (obj is NavigationViewItem item)
                ToolTip.SetTip(item, IsPaneOpen ? null : item.Content);
    }

    private void AttachItems(IList<object>? items)
    {
        if (items == null) return;
        foreach (var obj in items)
            if (obj is NavigationViewItem { TargetPageType: { } pageType } item)
                item.Bind(ToggleButton.IsCheckedProperty, CompiledBinding.Create<NavigationView, Type?>(
                    nv => nv.TargetPageType, this,
                    RadioButtonCheckedConverter.Instance, BindingMode.TwoWay, converterParameter: pageType));
    }

    #endregion

    #region Public Methods

    public void Back()
    {
        if (_backStack.Count == 0) return;

        _isNavigatingBack = true;
        try
        {
            TargetPageType = _backStack.Pop();
        }
        finally
        {
            _isNavigatingBack = false;
        }
    }

    private void UpdateContent(Type pageType)
    {
        if (IsPageCacheEnabled && _pageCache.TryGetValue(pageType, out var cached))
        {
            Content = cached;
            return;
        }

        var page = NavigationPageFactory?.Invoke(pageType);
        if (page is null) return;

        if (IsPageCacheEnabled)
            _pageCache[pageType] = page;

        Content = page;
    }

    public void ClearPageCache() => _pageCache.Clear();

    #endregion

    #region Properties

    [GeneratedStyledProperty(DefaultValue = true)]
    public partial bool IsPaneOpen { get; set; }

    [GeneratedDirectProperty]
    [SuppressMessage("Trimming",
        "IL2111:Method 'System.Activator.CreateInstance(Type)' with parameters or return value with 'DynamicallyAccessedMembersAttribute' is accessed via reflection. Trimmer can't guarantee availability of the requirements of the method.",
        Justification =
            "This fallback is only used for design-time preview and is not relied upon in trimmed/AOT published builds.")]
    public partial NavigationPageFactory? NavigationPageFactory { get; set; } = Activator.CreateInstance;

    [GeneratedStyledProperty(DefaultValue = true)]
    public partial bool IsPageCacheEnabled { get; set; }

    [GeneratedStyledProperty] public partial Type? TargetPageType { get; set; }

    [GeneratedStyledProperty(DefaultValue = true)]
    public partial bool IsBackButtonVisible { get; set; }

    [GeneratedStyledProperty(DefaultValue = false)]
    public partial bool IsBackEnabled { get; set; }

    [GeneratedStyledProperty(DefaultValue = true)]
    public partial bool IsPaneToggleButtonVisible { get; set; }

    [GeneratedDirectProperty] public partial IList<object>? MenuItems { get; set; } = [];

    [GeneratedDirectProperty] public partial IList<object>? FooterMenuItems { get; set; } = [];

    [GeneratedStyledProperty] public partial IEnumerable? MenuItemsSource { get; set; }

    [GeneratedStyledProperty] public partial IEnumerable? FooterMenuItemsSource { get; set; }

    [GeneratedDirectProperty] public partial bool CanBack { get; private set; }

    public event EventHandler<EventArgs>? BackRequested;

    #endregion
}