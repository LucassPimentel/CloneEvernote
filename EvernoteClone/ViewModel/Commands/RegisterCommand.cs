using EvernoteClone.Model;
using System;
using System.Windows.Input;

namespace EvernoteClone.ViewModel.Commands
{
    public class RegisterCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public LoginViewModel LoginViewModel { get; set; }

        public RegisterCommand(LoginViewModel loginViewModel)
        {
            LoginViewModel = loginViewModel;
        }

        public bool CanExecute(object parameter)
        {
            var user = parameter as User;

            if (user == null)
                return false;

            if (string.IsNullOrEmpty(user.Name) ||
                string.IsNullOrEmpty(user.Lastname) ||
                string.IsNullOrEmpty(user.Password) ||
                string.IsNullOrEmpty(user.ConfirmPassword))
            {
                return false;
            }

            if (user.Password != user.ConfirmPassword)
                return false;

            return true;
        }

        public void Execute(object parameter)
        {
            LoginViewModel.Register();
        }
    }
}
