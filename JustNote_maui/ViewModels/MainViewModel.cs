using JustNote_maui.Models;
using JustNote_maui;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using System.Text.Json;

namespace JustNote_maui.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private ObservableCollection<NoteModel> nodeModels = new ObservableCollection<NoteModel>();
        private bool toggled = false;
        private INoteModel selectedItem;

        private ICommand addCommand;
        private ICommand addNoteListCommand;

        public INoteModel SelectedItem
        {
            get => selectedItem;
            set
            {
                if (value == null) return;
                selectedItem = value;
                UpdateNote(value);
                OnPropertyChanged();
            }
        }

        public ObservableCollection<NoteModel> NodeModels
        {
            get => nodeModels;
            set
            {
                nodeModels = value;
                OnPropertyChanged();
            }
        }

        public bool Toggled
        {
            get => toggled;
            set
            {
                if (toggled == value) return;
                    toggled = value;
                OnPropertyChanged();
            }
        }


        public ICommand AddCommand
        {
            get => addCommand;
        }

        public ICommand AddNoteListCommand
        {
            get => addNoteListCommand;
        }

        public MainViewModel() 
        {
            Title = "My Notes";
            addCommand = new Command(AddNoteItem);
            addNoteListCommand = new Command(AddNoteListItem);
        }

        private async void UpdateNote(INoteModel noteModel)
        {
            if (noteModel is NoteModel)
            {
                Note notePage = new Note();
                notePage.BindingContext = new NoteViewModel() { Note = (NoteModel)noteModel };
                await Shell.Current.Navigation.PushAsync(notePage);
            }
            if (noteModel is NoteListModel noteListModel)
            { 
                NoteList noteList = new NoteList();

                noteListModel.NoteList = JsonSerializer.Deserialize<ItemOfNoteList[]>(noteListModel.ListNoteStringInerpret);
                noteList.BindingContext = new NoteListViewModel() { Note = noteListModel };
                await Shell.Current.Navigation.PushAsync(noteList);
            }    
        }

        public async void AddNoteItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(Note));
        }

        public async void AddNoteListItem(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NoteList));
        }
    }
}
