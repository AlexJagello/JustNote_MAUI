using JustNote_maui.Models;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace JustNote_maui.ViewModels
{
    public class NoteListViewModel : BaseNoteViewModel<NoteListModel>
    {
        private NoteListModel noteListModel;
        private ItemOfNoteList selectedItemListOfNote;
        private ICommand addListItem;
        private ICommand removeListItem;



        public override NoteListModel Note
        {
            get => noteListModel;
            set
            {
                noteListModel = value;
                OnPropertyChanged();
            }
        }

        public ItemOfNoteList SelectedItemListOfNote
        {
            get => selectedItemListOfNote;
            set
            {
                selectedItemListOfNote = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddListItem
        {
            get => addListItem;
        }

        public ICommand RemoveListItem
        {
            get => removeListItem;
        }

        public NoteListViewModel()
        {
            noteListModel = new NoteListModel();
            AddItemToList(new object());
           
            addListItem = new Command(AddItemToList);
            removeListItem = new Command(RemoveItemFromList);
        }

        public void AddItemToList(object parameter)
        {
           var list = Note.NoteList.ToList();
           var item = new ItemOfNoteList() { IsDone = false, ItemNote = "" };
           item.PropertyChanged += ItemListOfNotes_PropertyChangedAsync;
           list.Add(item);      

           Note.NoteList = list;

           SortNoteListByDoned();
        }

        public void RemoveItemFromList(object parameter)
        {
            var list = Note.NoteList.ToList();
            list.Remove(selectedItemListOfNote);
            Note.NoteList = list;
        }

        public override void Clear(object parameter)
        {
            Note.NoteList = new ObservableCollection<ItemOfNoteList>();
            AddItemToList(new object());
        }

        public override void SaveNoteItem(INoteModel noteModel)
        {
            Note.ListNoteStringInerpret = JsonSerializer.Serialize(Note.NoteList);
            AddNoteTitleIfItEmpty(Note.NoteList.First().ItemNote);
            App.RequestListNote.SaveItem(noteModel);
        }

        public override void RemoveNoteItem(int id)
        {
            App.RequestListNote.DeleteItem(id);
        }

        private async void ItemListOfNotes_PropertyChangedAsync(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ItemOfNoteList.IsDone))
            {
                await Task.Delay(400);
                SortNoteListByDoned();
            }
        }

        private void SortNoteListByDoned()
        {
            var sortedList = Note.NoteList.OrderBy(item => item.IsDone);
            Note.NoteList = sortedList;
        }
    }
}
