using System.Globalization;

namespace AnimeApp.Converters
{
    class FavoriteToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isFavorite = (bool)value;
            return isFavorite ? "favorite_filled.png" : "favorite_outline.png"; // или путь к твоим иконкам
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
