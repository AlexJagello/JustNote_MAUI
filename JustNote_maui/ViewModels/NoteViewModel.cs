using JustNote_maui.Models;
using System;
using System.Windows.Input;

namespace JustNote_maui.ViewModels
{
    public class NoteViewModel : BaseNoteViewModel<NoteModel>
    {
     
        private NoteModel note;

        public override NoteModel Note
        {
            get => note;
            set
            {
                note = value;
                OnPropertyChanged();
            }
        }

        public NoteViewModel() {

            Note = new NoteModel();     
        }

      

        public override void SaveNoteItem(INoteModel noteModel)
        {
            AddNoteTitleIfItEmpty(Note.NoteText);
            App.RequestSimpleNote.SaveItem(Note);
        }

        public override void Clear(object parameter)
        {
            Note.NoteText = string.Empty;
        }

        public override void RemoveNoteItem(int id)
        {
            App.RequestSimpleNote.DeleteItem(id);
        }
    }
}
