using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace ProjectManagementStudio.ViewModel.ValidationsRules;

public class LoginValidationRules : ValidationRule
{
    private readonly Regex _regex = new Regex("^[a-zA-Z0-9_]{6,20}$");

    public override ValidationResult Validate(object? value, CultureInfo cultureInfo)
    {
        var input = value?.ToString();

        if (input is not null && _regex.IsMatch(input))
            return ValidationResult.ValidResult;

        return new ValidationResult(false, "");
    }
}