using EvernoteClone.ViewModel;
using EvernoteClone.ViewModel.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Xps;

namespace EvernoteClone.View
{
    /// <summary>
    /// Lógica interna para NotesWindow.xaml
    /// </summary>
    public partial class NotesWindow : Window
    {
        NotesViewModel notesViewModel;

        public NotesWindow()
        {
            InitializeComponent();

            notesViewModel = FindResource("viewModel") as NotesViewModel;
            notesViewModel.SelectedNoteChanged += NotesViewModel_SelectedNoteChanged;

            var fontFamilies = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            fontFamilyComboBox.ItemsSource = fontFamilies;

            var fontSizes = new List<double>()
            {
                8, 9, 10, 11, 12, 14, 16, 18, 20, 24, 28, 32, 48, 72
            };

            fontSizeComboBox.ItemsSource = fontSizes;
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            if (string.IsNullOrEmpty(App.UserId))
            {
                var loginWindow = new LoginWindow();
                loginWindow.Owner = this;
                loginWindow.ShowDialog();

                notesViewModel.GetNotebooks();
            }
        }

        private void NotesViewModel_SelectedNoteChanged(object sender, EventArgs e)
        {
            contentRichTextBox.Document.Blocks.Clear();

            if (notesViewModel.SelectedNote != null)
            {
                saveButton.IsEnabled = true;

                if (!string.IsNullOrEmpty(notesViewModel.SelectedNote.FileLocation))
                {
                    var fileStream = new FileStream(notesViewModel.SelectedNote.FileLocation, FileMode.Open);
                    var content = new TextRange(contentRichTextBox.Document.ContentStart, contentRichTextBox.Document.ContentEnd);
                    content.Load(fileStream, DataFormats.Rtf);
                    fileStream.Close();
                }
            }
            else
            {
                saveButton.IsEnabled = false;
            }
        }

        private void contentRichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // TextRange pega o valor do richText
            var currentText = new TextRange(contentRichTextBox.Document.ContentStart, contentRichTextBox.Document.ContentEnd)
                .Text
                .Trim();

            var lengthText = currentText.Length;

            statusTextBlock.Text = $"Tamanho do documento: {lengthText} {(lengthText > 1 ? "Caracteres" : "Caractere")}";
        }

        private void boldButton_Click(object sender, RoutedEventArgs e)
        {
            // se o sender como ToggleButton isChecked for nulo retorna falso
            var isButtonChecked = (sender as ToggleButton).IsChecked ?? false;

            if (isButtonChecked)
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Bold);
            else
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Normal);

        }

        private void contentRichTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var selectedWeight = contentRichTextBox.Selection.GetPropertyValue(Inline.FontWeightProperty);
            boldButton.IsChecked = (selectedWeight != DependencyProperty.UnsetValue) && (selectedWeight.Equals(FontWeights.Bold));

            var selectedStyle = contentRichTextBox.Selection.GetPropertyValue(Inline.FontStyleProperty);
            italicButton.IsChecked = (selectedStyle != DependencyProperty.UnsetValue) && (selectedStyle.Equals(FontStyles.Italic));

            var selecteDecoration = contentRichTextBox.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            underlineButton.IsChecked = (selecteDecoration != DependencyProperty.UnsetValue) && (selecteDecoration.Equals(TextDecorations.Underline));

            fontFamilyComboBox.SelectedItem = contentRichTextBox.Selection.GetPropertyValue(Inline.FontFamilyProperty);
            fontSizeComboBox.Text = (contentRichTextBox.Selection.GetPropertyValue(Inline.FontSizeProperty)).ToString();
        }


        private void italicButton_Click(object sender, RoutedEventArgs e)
        {
            var isButtonChecked = (sender as ToggleButton).IsChecked ?? false;

            if (isButtonChecked)
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Italic);
            else
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Normal);
        }

        private void underlineButton_Click(object sender, RoutedEventArgs e)
        {
            var isButtonChecked = (sender as ToggleButton).IsChecked ?? false;

            if (isButtonChecked)
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
            else
            {
                TextDecorationCollection textDecorations;
                (contentRichTextBox.Selection.GetPropertyValue(Inline.TextDecorationsProperty) as TextDecorationCollection)
                    .TryRemove(TextDecorations.Underline, out textDecorations);

                contentRichTextBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, textDecorations);
            }
        }

        //TODO: Arrumar o botão de salvar, pois o botão está ativo mesmo sem nota selecionada

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            var rtfFile = System.IO.Path.Combine(Environment.CurrentDirectory, $"{notesViewModel.SelectedNote.Id}.rtf");
            var currentNote = notesViewModel.SelectedNote;

            notesViewModel.SelectedNote.FileLocation = rtfFile;
            notesViewModel.SelectedNote.UpdatedAt = DateTime.Now;
            DatabaseHelper.Update(notesViewModel.SelectedNote);


            var fileStream = new FileStream(rtfFile, FileMode.Create);
            var content = new TextRange(contentRichTextBox.Document.ContentStart, contentRichTextBox.Document.ContentEnd);
            content.Save(fileStream, DataFormats.Rtf);
            fileStream.Close();

            notesViewModel.GetNotes();
            notesViewModel.SelectedNote = currentNote;
        }


        private void fontFamilyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (fontFamilyComboBox.SelectedItem != null)
            {
                contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, fontFamilyComboBox.SelectedItem);
                contentRichTextBox.Focus();
            }
        }

        private void fontSizeComboBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            contentRichTextBox.Selection.ApplyPropertyValue(Inline.FontSizeProperty, fontSizeComboBox.SelectedItem);

            TextRange r = new TextRange(contentRichTextBox.Selection.Start, contentRichTextBox.Selection.End);
            r.ApplyPropertyValue(TextElement.FontSizeProperty, fontSizeComboBox.SelectedItem);

            contentRichTextBox.Focus();
        }
    }
}
