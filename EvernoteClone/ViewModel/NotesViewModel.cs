using EvernoteClone.Model;
using EvernoteClone.ViewModel.Commands;
using EvernoteClone.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EvernoteClone.ViewModel
{
    public class NotesViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Notebook> Notebooks { get; set; }
        public ObservableCollection<Note> Notes { get; set; }
        public NewNotebookCommand NewNotebookCommand { get; set; }
        public NewNoteCommand NewNoteCommand { get; set; }
        public StartRenameNotebookCommand StartRenameNotebookCommand { get; set; }
        public StopRenameNotebookCommand StopRenameNotebookCommand { get; set; }
        public CloseAppCommand CloseAppCommand { get; set; }
        public DeleteNoteCommand DeleteNoteCommand { get; set; }
        public DeleteNotebookCommand DeleteNotebookCommand { get; set; }
        public StartRenameNoteCommand StartRenameNoteCommand { get; set; }
        public StopRenameNoteCommand StopRenameNoteCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler SelectedNoteChanged;

        private Notebook selectedNotebook;

        public Notebook SelectedNotebook
        {
            get { return selectedNotebook; }
            set
            {
                selectedNotebook = value;
                OnPropertyChanged("SelectedNotebook");
                GetNotes();
            }
        }

        private Visibility isVisible;

        public Visibility IsVisible
        {
            get { return isVisible; }
            set
            {
                isVisible = value;
                OnPropertyChanged("IsVisible");
            }
        }

        private Visibility isNoteVisible;

        public Visibility IsNoteVisible
        {
            get { return isNoteVisible; }
            set
            {
                isNoteVisible = value;
                OnPropertyChanged("IsNoteVisible");
            }
        }

        private Note selectedNote;

        public Note SelectedNote
        {
            get { return selectedNote; }
            set
            {
                selectedNote = value;
                OnPropertyChanged("SelectedNote");
                SelectedNoteChanged?.Invoke(this, new EventArgs());
            }
        }

        public NotesViewModel()
        {
            Notebooks = new ObservableCollection<Notebook>();
            Notes = new ObservableCollection<Note>();

            NewNoteCommand = new NewNoteCommand(this);
            NewNotebookCommand = new NewNotebookCommand(this);
            StartRenameNotebookCommand = new StartRenameNotebookCommand(this);
            StopRenameNotebookCommand = new StopRenameNotebookCommand(this);
            CloseAppCommand = new CloseAppCommand(this);
            DeleteNotebookCommand = new DeleteNotebookCommand(this);
            DeleteNoteCommand = new DeleteNoteCommand(this);
            StartRenameNoteCommand = new StartRenameNoteCommand(this);
            StopRenameNoteCommand = new StopRenameNoteCommand(this);

            IsVisible = Visibility.Collapsed;
            IsNoteVisible = Visibility.Collapsed;

            GetNotebooks();
        }

        public void CreateNotebook()
        {
            var newNotebook = new Notebook()
            {
                Name = "Sem título",
                UserId = int.Parse(App.UserId),
                CreatedAt = DateTime.Now
            };

            DatabaseHelper.Insert(newNotebook);
            GetNotebooks();
        }

        public void CreateNote(int notebookId)
        {
            var newNote = new Note()
            {
                NotebookId = notebookId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Title = "Sem título"
            };

            DatabaseHelper.Insert(newNote);
            GetNotes();
        }

        public void GetNotebooks()
        {
            List<Notebook?> notebooks = (DatabaseHelper.Read<Notebook>())
                .Where(n => n.UserId.ToString() == App.UserId)
                .ToList();

            Notebooks.Clear();

            foreach (var notebook in notebooks)
            {
                Notebooks.Add(notebook);
            }
        }

        public void GetNotes()
        {

            Notes.Clear();

            if (SelectedNotebook is not null)
            {
                var notes = (DatabaseHelper.Read<Note>())
                    .Where(x => x.NotebookId == SelectedNotebook.Id);

                foreach (var note in notes)
                {
                    Notes.Add(note);
                }
            }

        }

        public void CloseApp()
        {
            Application.Current.Shutdown();
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void StartRenameNotebook()
        {
            IsVisible = Visibility.Visible;
        }

        public void StopRenameNotebook(Notebook notebook)
        {
            IsVisible = Visibility.Collapsed;

            DatabaseHelper.Update(notebook);
            GetNotebooks();
        }

        public void StartRenameNote()
        {
            IsNoteVisible = Visibility.Visible;
        }

        public void StopRenameNote(Note note)
        {
            IsNoteVisible = Visibility.Collapsed;
            note.UpdatedAt = DateTime.Now;

            DatabaseHelper.Update(note);
            GetNotes();
        }

        public void DeleteNotebook(Notebook notebook)
        {
            DatabaseHelper.Delete(notebook);
            GetNotebooks();
        }

        public void DeleteNote(Note note)
        {
            DatabaseHelper.Delete(note);
            GetNotes();
        }
    }
}
