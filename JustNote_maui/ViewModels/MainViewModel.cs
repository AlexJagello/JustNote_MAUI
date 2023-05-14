using JustNote_maui.Models;
using JustNote_maui;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using System.Text.Json;
using System.Collections;

namespace JustNote_maui.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private ObservableCollection<INoteModel> noteModels;
        private INoteModel selectedItem;
        private bool isSortReverseCheked;

        private ICommand addCommand;
        private ICommand addNoteListCommand;
        private ICommand sortCreateCommand;
        private ICommand sortEditCommand;
        private ICommand sortABCCommand;


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

        public ObservableCollection<INoteModel> NoteModels
        {
            get => noteModels;
            set
            {
                noteModels = value;
                OnPropertyChanged();
            }
        }

        public bool IsSortReverseCheked
        {
            get => isSortReverseCheked;
            set
            {
                isSortReverseCheked = value;
                ReverseSortingFunc();
                OnPropertyChanged();
            }
        }


        public ICommand AddCommand { get => addCommand; }
        public ICommand AddNoteListCommand { get => addNoteListCommand; }
        public ICommand SortCreateCommand { get => sortCreateCommand; }
        public ICommand SortEditCommand { get => sortEditCommand; }
        public ICommand SortABCCommand { get => sortABCCommand; }

        public MainViewModel() 
        {
            Title = "My Notes";

            addCommand = new Command(AddNoteItem);
            addNoteListCommand = new Command(AddNoteListItem);
            sortCreateCommand = new Command(SortCreateFunc);
            sortEditCommand = new Command(SortEditFunc);
            sortABCCommand = new Command(SortABCFunc);
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

        public void SortCreateFunc()
        {
            IEnumerable<INoteModel> sortedList = NoteModels.OrderByDescending(t => t.CreationDataTime);
            if (isSortReverseCheked) sortedList = sortedList.Reverse();
            NoteModels = new ObservableCollection<INoteModel>(sortedList);
        }
        public void SortEditFunc()
        {
            IEnumerable<INoteModel> sortedList = NoteModels.OrderByDescending(t => t.LastEditDataTime);
            if (isSortReverseCheked) sortedList = sortedList.Reverse();
            NoteModels = new ObservableCollection<INoteModel>(sortedList);
        }
        public void SortABCFunc()
        {
            IEnumerable<INoteModel> sortedList = NoteModels.OrderBy(t => t.NoteTitle);
            if (isSortReverseCheked) sortedList = sortedList.Reverse();
            NoteModels = new ObservableCollection<INoteModel>(sortedList);
        }

        public void ReverseSortingFunc()
        {
            var sortedList = NoteModels.Reverse();
            NoteModels = new ObservableCollection<INoteModel>(sortedList);
        }

    }
}
