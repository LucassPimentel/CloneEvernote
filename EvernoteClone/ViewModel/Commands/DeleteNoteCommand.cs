using EvernoteClone.Model;
using System;
using System.Windows.Input;

namespace EvernoteClone.ViewModel.Commands
{
    public class DeleteNoteCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private NotesViewModel _notesViewModel { get; set; }

        public DeleteNoteCommand(NotesViewModel notesViewModel)
        {
            _notesViewModel = notesViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var notebook = parameter as Note;

            _notesViewModel.DeleteNote(notebook);
        }
    }
}
