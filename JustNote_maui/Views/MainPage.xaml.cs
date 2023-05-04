using JustNote_maui.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JustNote_maui
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();            
        }

        protected override void OnAppearing()
        {          
            notesList.ItemsSource = GetListFromDB();

            base.OnAppearing();
        }

        private List<INoteModel> GetListFromDB()
        {
            var tabl1 = App.RequestSimpleNote.GetItems();
            var tabl2 = App.RequestListNote.GetItems();
            var allTables = new List<INoteModel>();

            allTables.AddRange(tabl1);
            allTables.AddRange(tabl2);

            return allTables;
        }


        private void ButtonCreatedFilter_Clicked(object sender, EventArgs e)
        {
            List<INoteModel> list = (notesList.ItemsSource.Cast<INoteModel>()).ToList();

            var sortedList = list.OrderBy(t => t.CreationDataTime);

            notesList.ItemsSource = sortedList;

            base.OnAppearing();
        }

        private void ButtonEditedFilter_Clicked(object sender, EventArgs e)
        {
            List<INoteModel> list = (notesList.ItemsSource.Cast<INoteModel>()).ToList();

            var sortedList = list.OrderBy(t => t.LastEditDataTime);

            notesList.ItemsSource = sortedList;

            base.OnAppearing();
        }

        private void ButtonAlphabetFilter_Clicked(object sender, EventArgs e)
        {
            List<INoteModel> list = (notesList.ItemsSource.Cast<INoteModel>()).ToList();

            var sortedList = list.OrderBy(t => t.NoteTitle);

            notesList.ItemsSource = sortedList;

            base.OnAppearing();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            List<INoteModel> list = (notesList.ItemsSource.Cast<INoteModel>()).ToList();
            list.Reverse();
            notesList.ItemsSource = list;
        }
    }
}
