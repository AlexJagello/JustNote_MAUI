using JustNote_maui.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace JustNote_maui.DB
{
    public class NoteRepository<T> where T: INoteModel, new()
    {
        SQLiteConnection database;
        static object locker = new object();
        public NoteRepository(SQLiteConnection database)
        {
            this.database = database;
            database.CreateTable<T>();
        }
        public IEnumerable<T> GetItems()
        {
            lock (locker)
            {
                return database.Table<T>().ToList();
            }
        }
        public T GetItem(int id)
        {
            lock (locker)
            {
                return database.Get<T>(id);
            }
        }
        public int DeleteItem(int id)
        {
            lock (locker)
            {
                return database.Delete<T>(id);
            }
        }

        public void Clear()
        {
            lock (locker)
            {
                database.DeleteAll<T>();
            }
        }

        public int SaveItem(INoteModel item)
        {
            lock (locker)
            {
                if (item.Id != 0)
                {
                    database.Update(item);
                    return item.Id;
                }
                else
                {
                    return database.Insert(item);
                }
            }
        }
    }
}
