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
    /// Interakční logika pro DruhaVerze.xaml
    /// </summary>
    public partial class DruhaVerze : Page
    {
        public DruhaVerze()
        {
            InitializeComponent();
            var itemsFromDb = new ObservableCollection<Dluh>(App.Database.GetItems<Dluh>().Result);

            SortBy.Items.Add("Dlužník");
            SortBy.Items.Add("Částka");
            SortBy.Items.Add("Datum");
            CelkemDluhyBox.Content = CelkemDluhy();
            FillPeopleCB();
            FillDebtList(itemsFromDb);
            //App.Database.RT<Dluh>();
            //App.Database.RT<Osoba>();
        }
        public ObservableCollection<Osoba> GetOsoby()
        {
            var itemsFromDb = new ObservableCollection<Osoba>(App.Database.GetItems<Osoba>().Result);
            return itemsFromDb;
        }
        public ObservableCollection<Dluh> GetDluhy()
        {
            var items = new ObservableCollection<Dluh>(App.Database.GetItems<Dluh>().Result);
            return items;
        }
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ListBoxItem uu = (ListBoxItem)LBDluhy.SelectedItem;
           /* try
            {
                string y = uu.Content.ToString();
                string v = y.Split('.')[0];
                int r = Int32.Parse(v);
                Dluh selected = App.Database.GetItemWhereID<Dluh>(r).Result;
                var Osoby = GetOsoby();
                JmenoDluznika.Content = Osoby[selected.IDOsoby-1];

                var dluhy = GetDluhy();
                
                int celkemdluzi = 0;
                for(int i = 0; i< dluhy.Count(); i++)
                {
                    if (dluhy[i].IDOsoby == selected.IDOsoby)
                    {
                        celkemdluzi = celkemdluzi + NovaCena(dluhy[i]);

                    }

                }
                
            } catch
            {

            }            
            //CurrentDebt.Content = NovaCena(selected);            

        */
        }

        public int NovaCena(Dluh d)
        {
            int puvodni = d.Castka;
            int sazba = d.Sazba;

            DateTime DluhCas = new DateTime(d.Rok, d.Mesic, d.Den);
            DateTime TedCas = DateTime.Now;

            TimeSpan Diff = DluhCas.Subtract(TedCas);
            int rozdilek = Diff.Days;
            int tomonths = (int)(rozdilek / 30.436875);

            if (tomonths < 0)
            {
                tomonths = tomonths * -1;
            }

            int plusurok = (puvodni * sazba) / 100;
            int finalcena = (tomonths * plusurok) + puvodni;
            return finalcena;
        }
        private void AddDebt(object sender, RoutedEventArgs e)
        {
            try
            {
                Dluh d = new Dluh();
                d.Castka = Int32.Parse(Castka.Text);

                d.Den = Int32.Parse(Den.Text);
                d.Rok = Int32.Parse(Rok.Text);
                d.Mesic = Int32.Parse(Mesic.Text);

                d.Sazba = Int32.Parse(Sazba.Text);
                d.Zaplaceno = false;

                Osoba ii = (Osoba)CBP.SelectedItem;
                d.IDOsoby = ii.ID;
                App.Database.SaveItems<Dluh>(d);
                MessageBox.Show("Dluh byl přidán");
                var itemsFromDb = new ObservableCollection<Dluh>(App.Database.GetItems<Dluh>().Result);
                FillDebtList(itemsFromDb);

            }
            catch
            {
                MessageBox.Show("Něco se nepovedlo :( ");
            }


        }
        public void FillPeopleCB()
        {
            CBP.Items.Clear();
            var itemsFromDb = new ObservableCollection<Osoba>(App.Database.GetItems<Osoba>().Result);
            for (int r = 0; r < itemsFromDb.Count(); r++)
            {
                CBP.Items.Add(itemsFromDb[r]);
            }
        }
        public void FillDebtList(ObservableCollection<Dluh> itemsFromDb)
        {
            LBDluhy.Items.Clear();
            var itemylist = new ObservableCollection<Osoba>(App.Database.GetItems<Osoba>().Result);


            for (int r = 0; r < itemsFromDb.Count(); r++)
            {
                ListBoxItem ItemToAdd = new ListBoxItem();
                int IDOsoby = itemsFromDb[r].IDOsoby - 1;
                int IDDluhu = itemsFromDb[r].ID;
                int Castka = itemsFromDb[r].Castka;

                string jmenoosoby = itemylist[IDOsoby].Name.ToString();
                string KPrecteniDatum = itemsFromDb[r].Den.ToString() + ". " + itemsFromDb[r].Mesic.ToString() + ". " + itemsFromDb[r].Rok.ToString();

                DateTime DluhDateTime = new DateTime(itemsFromDb[r].Rok, itemsFromDb[r].Mesic, itemsFromDb[r].Den);



                if (DluhDateTime < DateTime.Now)
                {
                    //už to bylo! 
                    int SUroky = NovaCena(itemsFromDb[r]);
                    if (itemsFromDb[r].Zaplaceno)
                    {
                        Brush DarkGray = Brushes.DarkGray;
                        ItemToAdd.Foreground = DarkGray;
                        //je to zaplacený
                    }
                    else
                    {
                        Brush Red = Brushes.Red;
                        ItemToAdd.Foreground = Red;
                        //není to zaplacený
                    }
                    ItemToAdd.Content = IDDluhu + ". " + jmenoosoby + " ~ " + SUroky + " ~ " + KPrecteniDatum;
                }
                else
                {
                    //ještě je čas
                    Brush Green = Brushes.Green;
                    ItemToAdd.Foreground = Green;
                    ItemToAdd.Content = IDDluhu + ". " + jmenoosoby + " ~ " + Castka + " ~ " + KPrecteniDatum;
                }

                LBDluhy.Items.Add(ItemToAdd);

            }

        }

        public int CelkemDluhy()
        {
            int celkem = 0;
            var vsechnydluhy = GetDluhy();
            for(int l = 0; l < vsechnydluhy.Count(); l++)
            {
                if (!vsechnydluhy[l].Zaplaceno)
                {

                    celkem = celkem + NovaCena(vsechnydluhy[l]);
                }
            }
            return celkem;
        }
        private void Add_Person(object sender, RoutedEventArgs e)
        {
            try
            {//checkovat
                Osoba o = new Osoba();
                o.Name = PersonName.Text;
                App.Database.SaveItems<Osoba>(o);
                MessageBox.Show("Dlužník byl přidán!");
                PersonName.Text = "";
                FillPeopleCB();
            }
            catch
            {
                MessageBox.Show("Něco se nepovedlo");
            }

        }




        private void SortBy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var itemsFromDb = new ObservableCollection<Dluh>(App.Database.GetItems<Dluh>().Result);
            if (SortBy.SelectedIndex == 0)
            {
                //Dluznik
                ObservableCollection<Dluh> Sorted = new ObservableCollection<Dluh>(itemsFromDb.OrderByDescending(x => x.IDOsoby).ThenByDescending(y => y.Castka));
                FillDebtList(Sorted);
            }
            else if (SortBy.SelectedIndex == 1)
            {
                //Castka
                ObservableCollection<Dluh> Sorted = new ObservableCollection<Dluh>(itemsFromDb.OrderByDescending(x => x.Castka));
                FillDebtList(Sorted);
            }
            else if (SortBy.SelectedIndex == 2)
            {
                //Datum
                ObservableCollection<Dluh> Sorted = new ObservableCollection<Dluh>(itemsFromDb.OrderBy(x => x.Rok));
                FillDebtList(Sorted);
            }

        }

        private void DeleteDl_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ListBoxItem uu = (ListBoxItem)LBDluhy.SelectedItem;
                string y = uu.Content.ToString();
                string v = y.Split('.')[0];
                int r = Int32.Parse(v);
                Dluh selected = App.Database.GetItemWhereID<Dluh>(r).Result;
                selected.Zaplaceno = true;
                App.Database.SaveItems<Dluh>(selected);
                MessageBox.Show("Dluh byl zplacen!");
                FillDebtList(GetDluhy());
            }catch{

            }


        }

  
    }
}
