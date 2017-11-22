using KassaSystem.Classes;
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

namespace KassaSystem.Pages
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        bool A = true;
        public Home()
        {
            InitializeComponent();
        }

        private void num_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            TbInput.Text += btn.Content;
        }

        int i = 0;
        BrushConverter bc = new BrushConverter();
        private void BtPlus_Click(object sender, RoutedEventArgs e)
        {
            // Add columns
            var gridView = new GridView();
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Barcode",
                DisplayMemberBinding = new Binding("PBarcode")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Omschrijving",
                DisplayMemberBinding = new Binding("POmschrijving")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Prijs",
                DisplayMemberBinding = new Binding("PPrijs")
            });

            if (A == true)
            {
                // Populate list
                this.LvArtikelen.View = gridView;
                this.LvArtikelen.Items.Add(new Product(int.Parse(TbInput.Text), "artikel " + i, i));
            }
            else
            {
                // Populate list
                this.LvRetour.View = gridView;
                this.LvRetour.Items.Add(new Product(int.Parse(TbInput.Text), "artikel " + i, i));
            }
        }

        private void btRetour_Click(object sender, RoutedEventArgs e)
        {

            if(A == true)
            {
                btRetour.Background = Brushes.Green;
                A = false;
            }
            else
            {
                btRetour.Background = (Brush)bc.ConvertFrom("#FFDDDDDD");
                A = true;
            }
        }
    }
}
