﻿using PersonalHelper.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PersonalHelper {
    public static class Constants {
        public const string DatabaseFilename = "TodoList.db3";
        public const SQLiteOpenFlags Flags =
            SQLiteOpenFlags.ReadWrite |
            SQLiteOpenFlags.Create |
            SQLiteOpenFlags.SharedCache;
        public static string DatabasePath {
            get {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFilename);
            }
        }
    }
    public class TodoItemDatabase {
        static SQLiteAsyncConnection Database;
        public static readonly AsyncLazy<TodoItemDatabase> Instance = new AsyncLazy<TodoItemDatabase>(async () => {
            var instance = new TodoItemDatabase();
            CreateTableResult result = await Database.CreateTableAsync<TodoItem>();
            return instance;
        });
        public TodoItemDatabase() {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }

        public async Task<IEnumerable<TodoItem>> GetItemsTodayAsync() =>
               await Database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Id] = 1");
        //.Where(x => x.DateRemember.Day == DateTime.Now.Day && x.DateRemember.Year == DateTime.Now.Year)
        //public Task<List<TodoItem>> GetItemsNotDoneAsync() {
        //    // SQL queries are also possible
        //    return Database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Id] = 0");
        //}
        //public Task<TodoItem> GetItemAsync(int id) {
        //    return Database.Table<TodoItem>().Where(i => i.Id == id).FirstOrDefaultAsync();
        //}
        public Task<int> SaveItemAsync(TodoItem item) {
            return Database.InsertAsync(item);
            //}
            //public Task<int> DeleteItemAsync(TodoItem item) {
            //    return Database.DeleteAsync(item);
            //}
        }
        public class AsyncLazy<T> : Lazy<Task<T>> {
            readonly Lazy<Task<T>> instance;

            public AsyncLazy(Func<T> factory) {
                instance = new Lazy<Task<T>>(() => Task.Run(factory));
            }

            public AsyncLazy(Func<Task<T>> factory) {
                instance = new Lazy<Task<T>>(() => Task.Run(factory));
            }

            public TaskAwaiter<T> GetAwaiter() {
                return instance.Value.GetAwaiter();
            }
        }
    }
}