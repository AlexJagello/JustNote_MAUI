using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace JustNote_maui.Models
{
    [Table("ListNotes")]
    public class NoteListModel : INoteModel, INotifyPropertyChanged
    {
        private string noteTitle = string.Empty;
        private IEnumerable<ItemOfNoteList> listNote;
        private DateTime? creationDataTime = null;
        private DateTime lastEditDataTime;

        private string listNoteStringInerpret;

        public NoteListModel()
        {
            listNote = new ObservableCollection<ItemOfNoteList>();
        }


        public Type Type{ get => GetType(); }

        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }

        public string NoteTitle
        {
            get => noteTitle;
            set
            {
                noteTitle = value;
                OnPropertyChanged();
            }
        }

        [Ignore]
        public IEnumerable<ItemOfNoteList> NoteList
        {
            get => listNote;
            set
            {
                listNote = value;
                OnPropertyChanged();
            }
        }

        public string ListNoteStringInerpret
        {
            get
            {
                return listNoteStringInerpret;
            }
            set
            {

                listNoteStringInerpret = value;
                OnPropertyChanged();

            }
        }

        public DateTime? CreationDataTime
        {
            get => creationDataTime;
            set
            {
                creationDataTime = value;
                OnPropertyChanged();
            }
        }

        public DateTime LastEditDataTime
        {
            get => lastEditDataTime;
            set
            {
                lastEditDataTime = value;
                OnPropertyChanged();
            }
        }



        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
