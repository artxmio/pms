using ProjectManagementStudio.Model.Enums;
using System.Globalization;
using System.Windows.Data;

namespace ProjectManagementStudio.View.Converters;

public class ProjectStatusConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo cultureInfo)
    {
        if (value is ProjectStatus status)
        {
            return status switch
            {
                ProjectStatus.Opened => "Открыт",
                ProjectStatus.Closed => "Закрыт",
                ProjectStatus.Stoped => "Остановлен",
                _ => "Неизвестно"
            };
        }
        return "Неизвестно";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo cultureInfo)
    {
        throw new NotImplementedException();
    }
}
