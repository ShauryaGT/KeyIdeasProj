using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Threading.Tasks;

namespace XamlMvvm
{
    public class SQLiteHelper
    {
        private readonly SQLiteAsyncConnection db;
        public SQLiteHelper(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<NoteModel>();
        }
        public Task<int> CreateNote(NoteModel note)
        {
            return db.InsertAsync(note);
        }
        public Task<List<NoteModel>> ReadNote()
        {
            return db.Table<NoteModel>().ToListAsync();
        }
        public Task<int> UpdateNote(NoteModel note)
        {
            return db.UpdateAsync(note);
        }
        public Task<int> DeleteNote()
        {
            return db.DeleteAllAsync<NoteModel>();
        }
    }
}

