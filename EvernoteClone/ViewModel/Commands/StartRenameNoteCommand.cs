using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EvernoteClone.ViewModel.Commands
{
    public class StartRenameNoteCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly NotesViewModel _viewModel;

        public StartRenameNoteCommand(NotesViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _viewModel.StartRenameNote();
        }
    }
}
