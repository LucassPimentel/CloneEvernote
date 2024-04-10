using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvernoteClone.Model
{
    // TODO: Ajustar método de salvar o richTextBox

    //https://stackoverflow.com/questions/15983278/storing-data-of-rich-text-box-to-database-with-formatting

    public class Note
    {
        [PrimaryKey]
        [Indexed]
        [AutoIncrement]
        public int Id { get; set; }
        [Indexed]
        public int NotebookId { get; set; }
        public string Title { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string FileLocation { get; set; }
    }
}
