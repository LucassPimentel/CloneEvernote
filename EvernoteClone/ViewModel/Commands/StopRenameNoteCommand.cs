using EvernoteClone.Model;
using System;
using System.Windows.Input;

namespace EvernoteClone.ViewModel.Commands
{
    public class StopRenameNoteCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly NotesViewModel _notesViewModel;

        public StopRenameNoteCommand(NotesViewModel notesViewModel)
        {
            _notesViewModel = notesViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var note = (Note)parameter;
            if (note != null) _notesViewModel.StopRenameNote(note);
        }
    }
}
