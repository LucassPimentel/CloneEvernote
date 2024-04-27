using System;
using System.Windows.Input;

namespace EvernoteClone.ViewModel.Commands
{
    public class StartRenameNotebookCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public NotesViewModel NotesViewModel { get; set; }

        public StartRenameNotebookCommand(NotesViewModel notesViewModel)
        {
            NotesViewModel = notesViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            NotesViewModel.StartRenameNotebook();
        }
    }
}
