using System.Globalization;

namespace ItMarathonFrontend.Helpers;

//[Localizability(LocalizationCategory.NeverLocalize)]
public sealed class BooleanToVisibilityConverter : System.Windows.Data.IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (bool)value ? Visibility.Visible : Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}