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
using System.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Windows.Forms;

namespace KantorWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FillCurrency12ComboBox();
        }

        Dictionary<string, double> DictionaryExchangeRate = new Dictionary<string, double>()
        {
            { "ZłotyEuro", 0.23782 },
            {"ZłotyDolar", 0.28270 },
            {"ZłotyFunt", 0.21263 },
            {"EuroZłoty", 4.20491 },
            {"EuroDolar", 0.84118 },
            {"EuroFunt", 1.14040 },
            {"DolarZłoty", 3.53726 },
            {"DolarEuro", 1.18880 },
            {"DolarFunt", 1.32490 },
            {"FuntZłoty", 4.70299 },
            {"FuntDolar", 0.75477 },
            {"FuntEuro", 0.87689 }

        };

        private void FillCurrency12ComboBox()
        {
            string ConString = ConfigurationManager.ConnectionStrings["LocalHost"].ConnectionString;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                SqlCommand fillcomboboxwithcurriences = new SqlCommand("Select Name from Curriences", con);
                con.Open();
                SqlDataReader reader;
                reader = fillcomboboxwithcurriences.ExecuteReader();
                while (reader.Read())
                {
                    Currency1.Items.Add(reader[0]);
                
                }
            }
           

        }


        private void Amount_GotFocus(object sender, RoutedEventArgs e)
        {
            Amount.Text = string.Empty;

        }

        private void CountingButton_Click(object sender, RoutedEventArgs e)
        {
            double i = 0;
            bool j = double.TryParse(Amount.Text, out i);
            double rate = DictionaryExchangeRate[Currency1.Text + Currency2.Text];
            if (j)
            {
                Result.Text = (i * rate).ToString();
            }
        }

        private void Currency2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          

        }

        private void Currency1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Currency2.Items.Clear();
            foreach (var currency in Currency1.Items)
            {
                if (currency != Currency1.SelectedItem)
                {
                    Currency2.Items.Add(currency);
                }
            }
            
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
