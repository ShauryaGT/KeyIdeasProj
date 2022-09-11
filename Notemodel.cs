using SQLite;
namespace XamlMvvm
{
    public class NoteModel
    {
        [PrimaryKey, AutoIncrement]
        public string Text { get; set; }
    }
}