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

        if (input is not null)
        {
            if (input.Length < 5)
            {
                return new ValidationResult(false, "The email length must be more than 5 and less than 20 characters.");
            }

            if (_regex.IsMatch(input))
            {
                return ValidationResult.ValidResult;
            }
        }

        return new ValidationResult(false, "Please, enter the valid password!");
    }
}