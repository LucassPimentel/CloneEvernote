using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

namespace EvernoteClone.ViewModel.Commands
{
    public class CloseAppCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public NotesViewModel NotesViewModel { get; set; }

        public CloseAppCommand(NotesViewModel notesViewModel)
        {
            NotesViewModel = notesViewModel;
        }

        public bool CanExecute(object parameter)
        {
            var processName = Process.GetCurrentProcess().ProcessName;
            var process = Process.GetProcessesByName(processName).FirstOrDefault();

            return process != null ? true : false;
        }

        public void Execute(object parameter)
        {
            NotesViewModel.CloseApp();
        }
    }
}
