using EvernoteClone.Model;
using System;
using System.Windows.Input;

namespace EvernoteClone.ViewModel.Commands
{
    public class NewNoteCommand : ICommand
    {
        // notifica ao mecanismo de binding que o valor do CanExecute foi alterado, é disparado toda vez que o valor de CanExecute muda
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public NotesViewModel NotesViewModel { get; set; }

        public NewNoteCommand(NotesViewModel notesViewModel)
        {
            NotesViewModel = notesViewModel;
        }

        public bool CanExecute(object parameter)
        {
            Notebook selectedNotebook = parameter as Notebook;

            if (selectedNotebook != null)
            {
                return true;
            }
            return false;
        }

        public void Execute(object parameter)
        {
            Notebook selectedNotebook = parameter as Notebook;
            NotesViewModel.CreateNote(selectedNotebook.Id);
        }
    }
}
