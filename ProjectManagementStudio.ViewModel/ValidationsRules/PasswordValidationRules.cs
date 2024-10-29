using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace ProjectManagementStudio.ViewModel.ValidationsRules;

public class PasswordValidationRules : ValidationRule
{
    private readonly Regex _regex = new("^[a-zA-Z0-9@#$%&*()<>[\\]{}]{6,24}$");

    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        var input = value?.ToString();

        if (input is not null && _regex.IsMatch(input))
            return ValidationResult.ValidResult;

        return new ValidationResult(false, "");
    }
}
