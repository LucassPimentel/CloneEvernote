using System;
using System.Windows.Input;

namespace EvernoteClone.ViewModel.Commands
{
    public class NewNotebookCommand : ICommand
    {

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public NotesViewModel NoteViewModel { get; set; }

        public NewNotebookCommand(NotesViewModel noteViewModel)
        {
            NoteViewModel = noteViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            NoteViewModel.CreateNotebook();
        }
    }
}
