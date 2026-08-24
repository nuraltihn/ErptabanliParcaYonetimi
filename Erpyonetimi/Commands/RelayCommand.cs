using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Windows;
namespace Erpyonetimi.Commands
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object?>? _execute;
        private readonly Func<object?, Task>? _executeAsync;
        private readonly Func<object?, bool>? _canExecute;

        public RelayCommand(
          Action execute,
          Func<bool>? canExecute = null)
        {
            _execute = _ => execute();

            _canExecute = canExecute == null
                ? null
                : _ => canExecute();
        }

        public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }
        public RelayCommand(Func<Task> executeAsync, Func<bool>? canExecute = null)
        {
            _executeAsync=_=> executeAsync();
            _canExecute = canExecute == null ?
                null : _ => canExecute();
        }
        public RelayCommand(Func<object?, Task> executeAsync,
            Func<object?, bool>? canExecute = null)
        {
            _executeAsync = executeAsync;
            _canExecute = canExecute;
        }
        public event EventHandler? CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(object? parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public async void Execute(object? parameter)
        {
            try { 
            if (_executeAsync != null)
            {
                await _executeAsync(parameter);
                return;
            }
            _execute?.Invoke(parameter);}
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "hata");
            }
        }
        
    }
}
