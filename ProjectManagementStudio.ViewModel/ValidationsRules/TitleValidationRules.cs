using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace ProjectManagementStudio.ViewModel.ValidationsRules;

public class TitleValidationRules : ValidationRule
{
    private readonly Regex _regex = new("^[a-zA-Z0-9\\s]{3,50}$");

    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        var input = value?.ToString();

        if (string.IsNullOrWhiteSpace(input))
        {
            return new ValidationResult(false, "The title cannot be empty.");
        }

        if (input.Length < 3 || input.Length > 50)
        {
            return new ValidationResult(false, "The title must be between 3 and 50 characters long.");
        }

        if (!_regex.IsMatch(input))
        {
            return new ValidationResult(false, "The title contains invalid characters.");
        }

        return ValidationResult.ValidResult;
    }
}

