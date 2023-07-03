using System;
using System.Windows.Input;

public class RelayCommand : ICommand
{
    private Action execute;
    private Func<bool> canExecute;

    public RelayCommand(Action execute)
    {
        this.execute = execute;
        this.canExecute = null;
    }

    public RelayCommand(Action execute, Func<bool> canExecute)
    {
        this.execute = execute;
        this.canExecute = canExecute;
    }

    public bool CanExecute(object parameter)
    {
        if (canExecute == null)
            return true;

        return canExecute();
    }

    public void Execute(object parameter)
    {
        execute?.Invoke();
    }

    public event EventHandler CanExecuteChanged;
}
