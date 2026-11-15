using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;

namespace Fluent2Avalonia.Converters;

public class RadioButtonCheckedConverter : IValueConverter
{
    public static readonly RadioButtonCheckedConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        value is Type selected && parameter is Type item && selected == item;

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        value is true && parameter is Type item ? item : BindingOperations.DoNothing;
}