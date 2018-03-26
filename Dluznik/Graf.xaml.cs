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
using LiveCharts;
using LiveCharts.Wpf;
using System.Collections.ObjectModel;
namespace Dluznik
{
    /// <summary>
    /// Interakční logika pro Graf.xaml
    /// </summary>
    public partial class Graf : UserControl
    {
        public Graf()
        {
            InitializeComponent();
            List<int> roky = GetYears();
            var itemsFromDb = new ObservableCollection<Transakce>(App.Database.GetItems<Transakce>().Result);
            //int celkemr = GetYearly(roky[0]);
            List<double> mm = new List<double>();
            for(int p = 0; p < roky.Count(); p++)
            {
                int celkemr = GetYearly(roky[p]);
                mm.Add(celkemr);
            }

            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Roční Výdaje",
                    //Výdaje roční :: 
                    Values = new ChartValues<double>(mm), 
                    
                }
            };
            //Názvy let
            Labels = new[] { "","","","",""};
            for (int o = 0; o < roky.Count(); o++)
            {
                Labels[o] = roky[o].ToString();
            }
            Formatter = value => value.ToString("N");

            DataContext = this;
        }
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        public List<int> GetYears()
        {
            List<int> Roky = new List<int>();
            var itemsFromDb = new ObservableCollection<Transakce>(App.Database.GetItems<Transakce>().Result);
            for (int r = 0; r < itemsFromDb.Count(); r++)
            {
                if (!Roky.Contains(itemsFromDb[r].Rok))
                {
                    Roky.Add(itemsFromDb[r].Rok);
                }
            }            
            return Roky;
        }


        public int GetYearly(int year)
        {
            var itemsFromDb = new ObservableCollection<Transakce>(App.Database.GetItems<Transakce>().Result);
            var itemylist = new ObservableCollection<Item>(App.Database.GetItems<Item>().Result);
            int yearprice = 0;
            for (int r = 0; r < itemsFromDb.Count(); r++)
            {

                int IDP = itemsFromDb[r].IDPredmet - 1;
                int Pocet = itemsFromDb[r].Mnozstvi;

                int trprice = itemylist[IDP].Cena * Pocet;
                int mth = itemsFromDb[r].Rok;

                if (mth == year)
                {
                    yearprice += trprice;
                }
                //mthlbl.Content = MTHTR.SelectedValue.ToString();
            }
            return yearprice;

        }

    }
}
