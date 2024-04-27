using System;
using System.Windows.Input;

namespace EvernoteClone.ViewModel.Commands
{
    public class ShowRegisterCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public LoginViewModel LoginViewModel { get; set; }

        public ShowRegisterCommand(LoginViewModel loginViewModel)
        {
            LoginViewModel = loginViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            LoginViewModel.SwitchViews();
        }
    }
}
