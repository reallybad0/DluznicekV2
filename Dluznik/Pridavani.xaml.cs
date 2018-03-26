using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Diagnostics;
using LiveCharts;
using LiveCharts.Wpf;
namespace Dluznik
{
    /// <summary>
    /// Interakční logika pro Pridavani.xaml
    /// </summary>
    public partial class Pridavani : Page
    {
        public Pridavani()
        {
            InitializeComponent();
            FillComboBoxes();
            FillLV();
            FillCBSeznamy();
            
        }

        public void FillComboBoxes()
        {
            //Months
            for (int i = 1; i < 11; i++)
            {
                Pocet.Items.Add(i);
            }

            string[] Months = new string[] { "", "Leden", "Únor", "Březen", "Duben", "Květen", "Červen", "Červenec", "Srpen", "Září", "Říjen", "Listopad", "Prosinec" };
            MTH.ItemsSource = Months;
            MTH.SelectedIndex = DateTime.Now.Month;


        }
        public void FillLV()
        {
            //Itemy ke koupi
            Itemy.Items.Clear();
            var itemsFromDb = new ObservableCollection<Item>(App.Database.GetItems<Item>().Result);
            foreach (Item i in itemsFromDb) { }
            for (int r = 0; r < itemsFromDb.Count(); r++)
            {
                Itemy.Items.Add(itemsFromDb[r]);
            }

        }

        public void FillCBSeznamy()
        {
            Seznamy.Items.Clear();
            Seznamy.Items.Clear();
            var itemsFromDb = new ObservableCollection<Seznam>(App.Database.GetItems<Seznam>().Result);
            foreach (Seznam i in itemsFromDb) { }
            for (int r = 0; r < itemsFromDb.Count(); r++)
            {
                Seznamy.Items.Add(itemsFromDb[r]);

            }
        }

        private void AddTransaction_Click(object sender, RoutedEventArgs e)
        {
            int month = MTH.SelectedIndex;
            int year = Int32.Parse(YR.Text);
            int count = Pocet.SelectedIndex + 1;
            int item = Itemy.SelectedIndex + 1;
            bool paid = Convert.ToBoolean(Paid.IsChecked);
            Seznam ids = (Seznam)Seznamy.SelectedItem;
            int idseznamu = ids.ID;

            try
            {
                Transakce t = new Transakce();
                t.IDPredmet = item;
                t.Mnozstvi = count;
                t.Rok = year;
                t.Mesic = month;
                t.Zaplaceno = paid;
                t.IDSeznamu = idseznamu;

                App.Database.SaveItems<Transakce>(t);
                MessageBox.Show("Výdaj byl přidán");
            }
            catch
            {
                MessageBox.Show("Něco se pokazilo");
            }



        }
        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                Item v = new Item();
                v.Nazev = Vydaj.Text;
                v.Cena = Int32.Parse(Cena.Text);
                App.Database.SaveItems<Item>(v);
                MessageBox.Show("Předmět byl zapsán do databáze");
                Vydaj.Clear();
                Cena.Clear();
            }
            catch
            {
                MessageBox.Show("Vyskytla se chyba.");
            }


        }
        private void AddSeznam_Click(object sender, RoutedEventArgs e)
        {

            string Nazev = NewSeznam.Text;
            Seznam s = new Seznam();
            s.NazevSeznamu = Nazev;
            App.Database.SaveItems<Seznam>(s);
            MessageBox.Show("Seznam byl vytvořen");
        }
    }
}

