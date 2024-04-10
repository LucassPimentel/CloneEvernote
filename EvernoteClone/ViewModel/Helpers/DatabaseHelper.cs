using EvernoteClone.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ObjectiveC;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace EvernoteClone.ViewModel.Helpers
{
    public class DatabaseHelper
    {
        private static string dbFile = Path.Combine(Environment.CurrentDirectory, "evernoteCloneDb.db");

        public static bool Insert<T>(T item)
        {
            var result = false;

            using (var connection = new SQLiteConnection(dbFile))
            {
                connection.CreateTable<T>();
                var insertedRows = connection.Insert(item);

                if (insertedRows > 0)
                    result = true;
            }
            return result;
        }

        public static bool Update<T>(T item)
        {
            var result = false;

            using (var connection = new SQLiteConnection(dbFile))
            {
                connection.CreateTable<T>();
                var insertedRows = connection.Update(item);

                if (insertedRows > 0)
                    result = true;
            }
            return result;
        }

        public static bool Delete<T>(T item)
        {
            var result = false;

            using (var connection = new SQLiteConnection(dbFile))
            {
                connection.CreateTable<T>();
                var insertedRows = connection.Delete(item);

                if (insertedRows > 0)
                    result = true;
            }
            return result;
        }

        // where T : new() => O connection.Table<T>() precisa que o item seja um tipo não abstrato
        // com um construtor sem parâmetros, colocando esses parâmetros no metodo, conseguimos utilizar o T mesmo assim.
        public static List<T> Read<T>() where T : new()
        {
            List<T> items;

            using (var connection = new SQLiteConnection(dbFile))
            {
                connection.CreateTable<T>();
                items = connection.Table<T>().ToList();
            }

            return items;
        }
    }
}
