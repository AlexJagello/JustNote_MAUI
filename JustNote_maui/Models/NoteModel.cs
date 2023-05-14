using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace JustNote_maui.Models
{

    [Table("SimpleNotes")]
    public class NoteModel : INotifyPropertyChanged, INoteModel
    {
        private string noteTitle = string.Empty;
        private string noteText = string.Empty;
        private DateTime? creationDataTime = null;
        private DateTime lastEditDataTime;


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

        public string NoteText 
        {
            get => noteText;
            set
            {
                noteText = value;
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
