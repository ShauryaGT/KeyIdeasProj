using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Net.Http;
using Xamarin.Forms;
using System.IO;
using Xamarin.Forms.Xaml;

namespace XamlMvvm
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public MainPageViewModel()
        {
            Notes = new ObservableCollection<NoteModel>();

            SaveNoteCommand = new Command(() =>
            {

                string strurl = string.Format(NoteText);
                WebRequest requestObjGet = WebRequest.Create(strurl);
                requestObjGet.Method = "GET";
                HttpWebResponse responseObjGet = null;
                responseObjGet = (HttpWebResponse)requestObjGet.GetResponse();
                string strurltest = null;
                using (Stream stream = responseObjGet.GetResponseStream())
                {
                    StreamReader sr = new StreamReader(stream);
                    strurltest = sr.ReadToEnd();
                    sr.Close();
                    Notes.Add(new NoteModel { Text = strurltest });
                    App.MyDatabase.CreateNote(new NoteModel { Text = strurltest });
                }
                NoteText = string.Empty;
            },
            () => !string.IsNullOrEmpty(NoteText));

            EraseNotesCommand = new Command(() =>
            {
                Notes.Clear();
                App.MyDatabase.DeleteNote();
                }
                );
            
        
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        string noteText;
        public string NoteText
        {
            get => noteText;
            set
            {
                noteText = value;
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(nameof(NoteText)));

                SaveNoteCommand.ChangeCanExecute();
            }
        }

        public ObservableCollection<NoteModel> Notes { get; }

        public Command SaveNoteCommand { get; }
        public Command EraseNotesCommand { get; }
        

    }
}