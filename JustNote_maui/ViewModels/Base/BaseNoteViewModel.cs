using JustNote_maui.Models;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace JustNote_maui.ViewModels
{
    public abstract class BaseNoteViewModel<T> : BaseViewModel where T : INoteModel, new()
    {
        private ICommand saveCommand;
        private ICommand clearCommand;
        private ICommand removeCommand;


        public ICommand SaveCommand
        {
            get => saveCommand;
        }

        public ICommand ClearCommand
        {
            get => clearCommand;
        }

        public ICommand RemoveCommand
        {
            get => removeCommand;
        }

        public abstract T Note { get; set; }



        public BaseNoteViewModel()
        {
            saveCommand = new Command(SaveNote);
            clearCommand = new Command(Clear);
            removeCommand = new Command(RemoveNote);
        }

        internal async void SaveNote(object parameter)
        {

            if (Note.CreationDataTime == null)
                Note.CreationDataTime = DateTime.Now;
            Note.LastEditDataTime = DateTime.Now;

            SaveNoteItem(Note);

            await Shell.Current.GoToAsync("..");
        }

        public async void RemoveNote(object obj)
        {
            if (obj is int)
                RemoveNoteItem((int)obj);
            await Shell.Current.GoToAsync("..");
        }


        protected void AddNoteTitleIfItEmpty(string noteText)
        {
            if (Note.NoteTitle.Trim(' ') == string.Empty)
            {
                int border = 40;
                string firstLine = noteText.Split(new char[] { '\n', '\r' })[0];
                if (firstLine.Length >= border)
                {

                    var splitedstr = firstLine.Substring(0, border).Split(" ");

                    if (splitedstr.Length <= 1)
                        Note.NoteTitle = splitedstr[0];
                    else
                        for (int i = 0; i < splitedstr.Length - 1; i++)
                            Note.NoteTitle += splitedstr[i] + " ";
                }
                else
                {
                    Note.NoteTitle = noteText;
                }
            }
        }


       
        public abstract void SaveNoteItem(INoteModel noteModel);

        public abstract void Clear(object parameter);

        public abstract void RemoveNoteItem(int id);

    }
}
