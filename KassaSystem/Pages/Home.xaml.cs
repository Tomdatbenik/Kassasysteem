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
        int i = 0;
        List<Product> PL = new List<Product>();
        BrushConverter bc = new BrushConverter();
        //werkt
        public Home()
        {
            InitializeComponent();
        }

        private void num_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            TbInput.Text += btn.Content;
        }

        private void btRetour_Click(object sender, RoutedEventArgs e)
        {

            if (A == true)
            {
                btRetour.Background = Brushes.Green;
                FActionscreen.Content = "Scan producten om te retouneren";
                A = false;

            }
            else
            {
                btRetour.Background = (Brush)bc.ConvertFrom("#FFDDDDDD");
                FActionscreen.Content = "Scan producten";
                A = true;
            }
        }

        private void btnpin_Click(object sender, RoutedEventArgs e)
        {
            this.LvRetour.Items.Clear();
            this.LvArtikelen.Items.Clear();
            TbInput.Text = "";
        }

        public void GetZeMoneys()
        {

        }

        private void btdoorgaan_Click(object sender, RoutedEventArgs e)
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

            Product p = new Product(TbInput.Text, "artikel " + i, i);
            PL.Add(p);

            i++;
            int artikeltotaal = 0;
            int retourtotaal = 0;
            if (A == true)
            {
                // Populate list
                this.LvArtikelen.View = gridView;
                if (TbInput.Text != "")
                {

                    this.LvArtikelen.Items.Add(p);
                    foreach (Product q in PL)
                    {
                        artikeltotaal += q.PPrijs;
                    }
                    tbArtikelen.Text = "Artikelen: €" + artikeltotaal;

                }
            }
            else
            {
                // Populate list
                this.LvRetour.View = gridView;
                if (TbInput.Text != "")
                {
                    this.LvRetour.Items.Add(p);
                    foreach (Product q in PL)
                    {
                        retourtotaal += q.PPrijs;
                    }
                    tbRetour.Text = "Retour: €" + retourtotaal;
                }
            }
            TbInput.Text = "";
            int totaal = artikeltotaal - retourtotaal;
            tbBetalen.Text = "Te betalen: €" + totaal;
        }

        private void TbInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btdoorgaan_Click(null, null);
            }
        }
    }
}