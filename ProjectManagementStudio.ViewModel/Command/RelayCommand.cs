using System.Windows.Input;

namespace ProjectManagementStudio.ViewModel.Command;

public class RelayCommand(Action execute) : ICommand
{
    private readonly Action _execute = execute;

    public event EventHandler? CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    public bool CanExecute(object? parameter) => true;

    public void Execute(object? parameter)
    {
        _execute.Invoke();
    }
}