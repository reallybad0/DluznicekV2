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
    /// Interakční logika pro PrvniVerze.xaml
    /// </summary>
    public partial class PrvniVerze : Page
    {
        public PrvniVerze()
        {
            InitializeComponent();

            #region

            FillTR();
            FillCB();
            FillCBSeznamy();
            FillComboBoxes();
            #endregion
        }

        public ObservableCollection<Item> GetItems()
        {
            var itemsFromDb = new ObservableCollection<Item>(App.Database.GetItems<Item>().Result);
            return itemsFromDb;
        }
        public ObservableCollection<Osoba> GetOsoby()
        {
            var itemsFromDb = new ObservableCollection<Osoba>(App.Database.GetItems<Osoba>().Result);
            return itemsFromDb;
        }
        //Functions
        #region

        public void FillComboBoxes()
        {
            string[] Months = new string[] { "", "Leden", "Únor", "Březen", "Duben", "Květen", "Červen", "Červenec", "Srpen", "Září", "Říjen", "Listopad", "Prosinec" };
          //  MTH.ItemsSource = Months;
            MTHTR.ItemsSource = Months;
          //  MTH.SelectedIndex = DateTime.Now.Month;


        }
       
        public void FillCB()
        {
            yearcb.Items.Clear();

            var itemsFromDb = new ObservableCollection<Transakce>(App.Database.GetItems<Transakce>().Result);
            foreach (Transakce i in itemsFromDb) { }
            List<int> values = new List<int>();

            for (int r = 0; r < itemsFromDb.Count(); r++)
            {
                int p = itemsFromDb[r].Rok;
                if (!values.Contains(p))
                {
                    values.Add(p);
                }
            }
            yearcb.ItemsSource = values;


        }
        public void FillCBSeznamy()
        {
      
            SeznamyCB.Items.Clear();
            var itemsFromDb = new ObservableCollection<Seznam>(App.Database.GetItems<Seznam>().Result);
            foreach (Seznam i in itemsFromDb) { }
            for (int r = 0; r < itemsFromDb.Count(); r++)
            {

                SeznamyCB.Items.Add(itemsFromDb[r]);
            }
        }
        public void FillTR()
        {
            TransakceLV.Items.Clear();
            var itemsFromDb = new ObservableCollection<Transakce>(App.Database.GetItems<Transakce>().Result);
            foreach (Transakce i in itemsFromDb) { }
            var itemylist = new ObservableCollection<Item>(App.Database.GetItems<Item>().Result);
            foreach (Item y in itemylist) { }

            for (int r = 0; r < itemsFromDb.Count(); r++)
            {
                int IDP = itemsFromDb[r].IDPredmet - 1;
                int Pocet = itemsFromDb[r].Mnozstvi;
                int IDT = itemsFromDb[r].ID;
                int trprice = itemylist[IDP].Cena * Pocet;

                string itemname = itemylist[IDP].Nazev.ToString();
                TransakceLV.Items.Add(IDT + ". " + Pocet + "x " + itemname + "       " + trprice + ",-");
            }
        }

        public void FillTRWhere(int month)
        {
            TransakceLV.Items.Clear();
            var itemsFromDb = new ObservableCollection<Transakce>(App.Database.GetItems<Transakce>().Result);
            foreach (Transakce i in itemsFromDb) { }
            var itemylist = new ObservableCollection<Item>(App.Database.GetItems<Item>().Result);
            foreach (Item y in itemylist) { }

            FillTheList(itemsFromDb, itemylist, month);

        }

        public void FillTheList(ObservableCollection<Transakce> itemsFromDb, ObservableCollection<Item> itemylist, int month)
        {
            int monthprice = 0;
            for (int r = 0; r < itemsFromDb.Count(); r++)
            {

                int IDP = itemsFromDb[r].IDPredmet - 1;
                int Pocet = itemsFromDb[r].Mnozstvi;


                int trprice = itemylist[IDP].Cena * Pocet;
                int mth = itemsFromDb[r].Mesic;
                string itemname = itemylist[IDP].Nazev.ToString();
                if (mth == month)
                {
                    TransakceLV.Items.Add(Pocet + "x " + itemname + "       " + trprice + ",-");
                    monthprice += trprice;
                }
            }
            mthlbl.Content = MTHTR.SelectedValue.ToString();
            price.Content = monthprice;
        }

        public void FillTheListYear(ObservableCollection<Transakce> itemsFromDb, ObservableCollection<Item> itemylist, int year)
        {
            int monthprice = 0;
            for (int r = 0; r < itemsFromDb.Count(); r++)
            {

                int IDP = itemsFromDb[r].IDPredmet - 1;
                int Pocet = itemsFromDb[r].Mnozstvi;

                int trprice = itemylist[IDP].Cena * Pocet;
                int mth = itemsFromDb[r].Rok;
                string itemname = itemylist[IDP].Nazev.ToString();
                if (mth == year)
                {
                    TransakceLV.Items.Add(Pocet + "x " + itemname + "       " + trprice + ",-");
                    monthprice += trprice;
                }
                //mthlbl.Content = MTHTR.SelectedValue.ToString();
            }
            price.Content = monthprice;
        }


        public void FillTheListSeznam(ObservableCollection<Transakce> itemsFromDb, ObservableCollection<Item> itemylist, int SeznamID)
        {
            // Použít GetItemsWhere
            TransakceLV.Items.Clear();
            int monthprice = 0;
            for (int r = 0; r < itemsFromDb.Count(); r++)
            {
                int IDP = itemsFromDb[r].IDPredmet - 1;
                int Pocet = itemsFromDb[r].Mnozstvi;

                int trprice = itemylist[IDP].Cena * Pocet;
                int mth = itemsFromDb[r].IDSeznamu;
                string itemname = itemylist[IDP].Nazev.ToString();
                if (mth == SeznamID)
                {
                    TransakceLV.Items.Add(Pocet + "x " + itemname + "       " + trprice + ",-");
                    monthprice += trprice;
                }
                //mthlbl.Content = MTHTR.SelectedValue.ToString();
            }

            price.Content = monthprice;
        }
        #endregion
        //OnClicks , Selection Changed
        #region
    

        private void yearcb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            mthlbl.Content = yearcb.SelectedValue.ToString();
            int selectedyear = Int32.Parse(yearcb.SelectedValue.ToString());
            TransakceLV.Items.Clear();
            var itemsFromDb = new ObservableCollection<Transakce>(App.Database.GetItems<Transakce>().Result);
            foreach (Transakce i in itemsFromDb) { }
            var itemylist = new ObservableCollection<Item>(App.Database.GetItems<Item>().Result);
            foreach (Item y in itemylist) { }

            FillTheListYear(itemsFromDb, itemylist, selectedyear);


            //FillTRWhere(yearcb.SelectedIndex);


        }
        private void MTHTR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FillTRWhere(MTHTR.SelectedIndex);
        }


      

        private void TransakceLV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            Deletebutton.Visibility = Visibility.Visible;
            
        }


        private void Seznamy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var itemsFromDb = new ObservableCollection<Transakce>(App.Database.GetItems<Transakce>().Result);
            var itemylist = new ObservableCollection<Item>(App.Database.GetItems<Item>().Result);

            Seznam s = (Seznam)SeznamyCB.SelectedItem;
            mthlbl.Content = s.NazevSeznamu;
            FillTheListSeznam(itemsFromDb, itemylist, s.ID);
            //FillTRWhere(s.ID);
            //ID => proměnná
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            string y = (string)TransakceLV.SelectedItem;
            string v = y.Split('.')[0];
            int r = Int32.Parse(v);
            Transakce l = new Transakce();
            l.ID = r;
            App.Database.DeleteItem<Transakce>(l);
            FillTR();
        }

    


        #endregion
    }
}
