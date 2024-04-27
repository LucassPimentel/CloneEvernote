using EvernoteClone.Model;
using System;
using System.Windows.Input;

namespace EvernoteClone.ViewModel.Commands
{
    public class StopRenameNotebookCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public NotesViewModel NotesViewModel { get; set; }

        public StopRenameNotebookCommand(NotesViewModel notesViewModel)
        {
            NotesViewModel = notesViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var notebook = parameter as Notebook;

            if (notebook != null)
                NotesViewModel.StopRenameNotebook(notebook);
        }
    }
}


