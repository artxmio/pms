using ProjectManagementStudio.Model.Enums;
using System.Globalization;
using System.Windows.Data;

namespace ProjectManagementStudio.View.Converters;

public class SprintStatusConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo cultureInfo)
    {
        if (value is SprintStatus status)
        {
            return status switch
            {
                SprintStatus.Opened => "Открыт",
                SprintStatus.Closed => "Закрыт",
                SprintStatus.Unknown => "Неизвестно",
                _ => "Неопределенный статус"
            };
        }
        return "Ошибка статуса";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo cultureInfo)
    {
        throw new NotImplementedException();
    }
}