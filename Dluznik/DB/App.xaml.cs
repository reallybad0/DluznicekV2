using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using SQLite;
using SQLitePCL;
namespace Dluznik
{
    /// <summary>
    /// Interakční logika pro App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static Conn _database;
        public static Conn Database
        {
            get
            {
                if (_database == null)
                {
                    var fileHelper = new FileHelper();
                    _database = new Conn(fileHelper.GetLocalFilePath("Databejs.db3"));
                }
                return _database;
            }
        }
    }
}
