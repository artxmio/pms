using Newtonsoft.Json.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ProjectManagementStudio.View.UserControls.TextPasswordBox;

public class TextPasswordBox : TextBox
{
    public static readonly DependencyProperty PasswordProperty =
        DependencyProperty.Register("Password", typeof(string), typeof(TextPasswordBox), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnPasswordPropertyChanged));

    //public static readonly DependencyProperty IsPasswordVisibleProperty =
    //    DependencyProperty.Register("IsPasswordVisible", typeof(bool), typeof(TextPasswordBox), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIsPasswordVisiblePropertyChanged));


    private const char CHAR = '●';

    public TextPasswordBox()
    {
        Text = Password;
    }

    public string Password
    {
        get
        {
            return (string)GetValue(PasswordProperty);
        }
        set
        {
            SetValue(PasswordProperty, value);
        }
    }

    //public bool IsPasswordVisible
    //{
    //    get
    //    {
    //        return (bool)GetValue(IsPasswordVisibleProperty);
    //    }
    //    set
    //    {
    //        SetValue(IsPasswordVisibleProperty, value);

            
    //    }
    //}

    private static void OnPasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        
    }

    private static void OnIsPasswordVisiblePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        
    }

    protected override void OnTextChanged(TextChangedEventArgs e)
    {
        var diff = Text.Length - Password.Length;

        if (diff == 0)
        {
            return;
        }

        if (diff < 0)
        {
            var charsToRemove = Password.Length - Text.Length;

            var startIndex = CaretIndex;

            Password = Password.Remove(startIndex, charsToRemove);

            this.Text = new string(CHAR, Password.Length);

            CaretIndex = Text.Length;

            return;
        }
        else
        {
            var newPasswordChars = new string((from ch in Text where ch != CHAR select ch).ToArray());
            if (Password.Length != Text.Length)
            {
                Password = new string(Password + newPasswordChars);
            }

            this.Text = new string(CHAR, Password.Length);
            CaretIndex = Text.Length;
            return;
        }
    }
}
