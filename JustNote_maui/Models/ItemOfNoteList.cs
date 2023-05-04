using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace JustNote_maui.Models
{
    public class ItemOfNoteList : INotifyPropertyChanged
    {

        private string itemNote = "";
        private bool isDone = false;
        private bool isFocused = true;


        public string ItemNote
        {
            get => itemNote;
            set
            {
                if (itemNote == value) return;
                itemNote = value;
                OnPropertyChanged();
            }
        }

        public bool IsDone
        {
            get => isDone;
            set
            {
                if(isDone == value) return;
                isDone = value;

                ItemNote = MadeTextStrikethrough(itemNote);

                OnPropertyChanged();
            }
        }

        public bool IsFocused
        {
            get => isFocused;
            set
            {
                if (isFocused == value) return;
                isFocused = value;
                OnPropertyChanged();
            }
        }

        private string MadeTextStrikethrough(string sourceText)
        {
            var newString = "";

            if (isDone)
            {           
                foreach (var character in sourceText)
                {
                    newString += $"{character}\u0336";
                }
            }
            else
            {
                foreach (var character in sourceText)
                {
                    if (!character.Equals('\u0336'))
                        newString += character;
                }
            }

            return newString;
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
