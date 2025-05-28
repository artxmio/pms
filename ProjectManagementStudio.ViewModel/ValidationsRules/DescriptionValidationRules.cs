using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace ProjectManagementStudio.ViewModel.ValidationsRules;

public class DescriptionValidationRules : ValidationRule
{
    private readonly Regex _regex = new("^[a-zA-Z0-9.,!?\\s]{10,500}$");

    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        var input = value?.ToString();

        if (string.IsNullOrWhiteSpace(input))
        {
            return new ValidationResult(false, "The description cannot be empty.");
        }

        if (input.Length < 10 || input.Length > 500)
        {
            return new ValidationResult(false, "The description must be between 10 and 500 characters long.");
        }

        if (!_regex.IsMatch(input))
        {
            return new ValidationResult(false, "The description contains invalid characters.");
        }

        return ValidationResult.ValidResult;
    }
}