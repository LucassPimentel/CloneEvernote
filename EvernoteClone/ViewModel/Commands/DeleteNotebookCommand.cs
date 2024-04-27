using EvernoteClone.Model;
using System;
using System.Windows.Input;

namespace EvernoteClone.ViewModel.Commands
{
    public class DeleteNotebookCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private NotesViewModel _notesViewModel { get; set; }

        public DeleteNotebookCommand(NotesViewModel notesViewModel)
        {
            _notesViewModel = notesViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var notebook = parameter as Notebook;

            _notesViewModel.DeleteNotebook(notebook);
        }
    }
}
