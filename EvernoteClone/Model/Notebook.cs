
using SQLite;
using System;

namespace EvernoteClone.Model
{
    public class Notebook
    {
        [PrimaryKey]
        [Indexed]
        [AutoIncrement]
        public int Id { get; set; }
        [Indexed]
        public int UserId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
