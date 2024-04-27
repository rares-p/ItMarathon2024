using System.Globalization;

namespace ItMarathonFrontend.Helpers;

//[Localizability(LocalizationCategory.NeverLocalize)]
public sealed class BooleanToVisibilityConverterReversed : System.Windows.Data.IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (bool)value ? Visibility.Collapsed : Visibility.Visible;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}