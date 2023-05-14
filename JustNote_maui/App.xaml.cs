using JustNote_maui.DB;
using JustNote_maui.Models;
using JustNote_maui;
using SQLite;
using System;
using System.IO;


namespace JustNote_maui
{
    public partial class App : Application
    {
        public const string DATABASE_NAME = "notes.db";


        public static SQLiteConnection database;
        public static SQLiteConnection Database
        {
            get
            {
                if(database == null)
                {
                    database = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME));
                }
                return database;
            }
        }

        public static NoteRepository<NoteModel> requestSimpleNote;
        public static NoteRepository<NoteModel> RequestSimpleNote
        {
            get
            {
                if (requestSimpleNote == null)
                {
                    requestSimpleNote = new NoteRepository<NoteModel>(Database);
                }
                return requestSimpleNote;
            }
        }

        public static NoteRepository<NoteListModel> requestListNote;
        public static NoteRepository<NoteListModel> RequestListNote
        {
            get
            {
                if (requestListNote == null)
                {
                    requestListNote = new NoteRepository<NoteListModel>(Database);
                }
                return requestListNote;
            }
        }


        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();         
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
