using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLitePCL;
namespace Dluznik
{
   public class Conn
    {
        private SQLiteAsyncConnection database;

        public Conn(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Item>().Wait();
            database.CreateTableAsync<Transakce>().Wait();
            database.CreateTableAsync<Seznam>().Wait();
            database.CreateTableAsync<Dluh>().Wait();
            database.CreateTableAsync<Osoba>().Wait();
        }

        public Task<List<T>> GetItems<T>() where T : ATable, new()
        {
            return database.Table<T>().ToListAsync();
        }
        public Task<int> SaveItems<T>(T item) where T : ATable, new()
        {
            if (item.ID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<T> GetItemWhereID<T>(int id) where T : ATable, new()
        {
            return database.Table<T>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }
        /*
        public List<Transakce> GetItemsByMonth(int month)
        {
            var neco = database.Table<Transakce>().Where(i => i.Mesic == month).ToListAsync();
            deb
        }
        */

        public Task<Transakce> GetItemWhere(int month)
        {
            return database.Table<Transakce>().Where(i => i.Mesic == month).FirstOrDefaultAsync(); // LINQ syntaxe
        }
        public Task<int> DeleteItem<T>(T item) where T : ATable, new()
        {
            return database.DeleteAsync(item);
        }
        public void RT<T>() where T : ATable, new()
        {
            database.DropTableAsync<T>().Wait();
            database.CreateTableAsync<T>().Wait();
        }




    }
}
