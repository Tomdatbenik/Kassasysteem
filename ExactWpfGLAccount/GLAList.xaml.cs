using Exact;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace ExactWpfGLAccount
{
    public partial class GLAList : Window
    {
        bool A = true;
        bool Korting = false;
        bool Wisselgeld = false;
        int iArtikelTotaal;
        int iRetourTotaal;
        double iTotaal;
        //int i = 0;
        ObservableCollection<GLAccount> GL = new ObservableCollection<GLAccount>();
        ObservableCollection<GLAccount> RL = new ObservableCollection<GLAccount>();
        List<GLAccount> Itemlist = new List<GLAccount>();
        BrushConverter bc = new BrushConverter();

        public GLAList()
        {
            InitializeComponent();

            ToonGLAccounts();
            //Gridview ein
            var gridView = new GridView();
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Barcode",
                DisplayMemberBinding = new Binding("ItemCode")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Description",
                DisplayMemberBinding = new Binding("ItemDescription")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Prijs",
                DisplayMemberBinding = new Binding("Price")
            });
            LvArtikelen.View = gridView;

            //Gridview zwei polizei
            var gridViewzwei = new GridView();
            gridViewzwei.Columns.Add(new GridViewColumn
            {
                Header = "Barcode",
                DisplayMemberBinding = new Binding("ItemCode")
            });
            gridViewzwei.Columns.Add(new GridViewColumn
            {
                Header = "Description",
                DisplayMemberBinding = new Binding("ItemDescription")
            });
            gridViewzwei.Columns.Add(new GridViewColumn
            {
                Header = "Prijs",
                DisplayMemberBinding = new Binding("Price")
            });
            LvRetour.View = gridViewzwei;
        }

        private async void ToonGLAccounts()
        {
            string f = "";
            Itemlist = await Rest.getGLAccounts(f);
            //list.ItemsSource = await Rest.getGLAccounts(f);
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            
        }

        private void Refresh_Click(object sender, System.Windows.Input.KeyEventArgs e)
        {
            
        }

        //private void list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (e.AddedItems.Count <= 0) return;
        //    GLAccount gla = e.AddedItems[0] as GLAccount;
        //    GLAEdit glaEdit = new GLAEdit();
        //    glaEdit.glaAccount = gla;
        //    glaEdit.Show();
        //}

        private void btnpin_Click(object sender, RoutedEventArgs e)
        {
            FActionscreen.Content = "Wacht tot de betaling gereed is";
            this.LvRetour.Items.Clear();
            this.LvArtikelen.Items.Clear();
            TbInput.Text = "";
        }

        

        private void btdoorgaan_Click(object sender, RoutedEventArgs e)
        {
            string input = TbInput.Text;

            if (Korting == true)
            {
                int iKorting = Int32.Parse(input);
                tbKorting.Text = "Korting: " + iKorting + "%";

                double iProcent = 100 - iKorting;
                double iPercentage = iProcent * 0.01;
                tbBetalen.Text = "Te betalen: €" + iTotaal * iPercentage;

                TbInput.Text = "";
            }
            else if (Korting == false)
            {
                if (A == true)
                {
                    GLAccount gl = checkstuff(Itemlist, input);
                    if (gl.ItemCode != "")
                    {
                        GL.Add(gl);
                    }
                    else
                    {
                        MessageBox.Show("Geen product gevonden");
                    }
                    LvArtikelen.ItemsSource = GL;
                    TbInput.Text = "";

                    iArtikelTotaal = 101;
                    tbArtikelen.Text = "Artikelen: €" + iArtikelTotaal;
                    //double nummer = 80*0.01;
                    //tbArtikelen.Text = "Artikelen: €" + nummer;
                }


                else if (A == false)
                {
                    GLAccount retour = checkstuff(Itemlist, input);
                    if (retour.ItemCode != "")
                    {
                        RL.Add(retour);
                    }
                    else
                    {
                        MessageBox.Show("Geen product gevonden");
                    }
                    LvRetour.ItemsSource = RL;
                    TbInput.Text = "";

                    iRetourTotaal = 10;
                    tbRetour.Text = "Retour: €" + iRetourTotaal;
                }

                iTotaal = iArtikelTotaal - iRetourTotaal;
                tbBetalen.Text = "Te betalen: €" + iTotaal;
            }

            else if (Wisselgeld == true)
            {

            }
            
            

            
            /* 
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
            */
            
            

        }

        private GLAccount checkstuff(List<GLAccount> Lijstje,string input)
        {
            GLAccount gl = new GLAccount("","","");
            foreach (GLAccount l in Lijstje)
            {
                if (l.ItemCode.ToString() == input)
                {
                    gl = l;
                }
            }
            return gl;
        }


        private void TbInput_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btdoorgaan_Click(null, null);
            }
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
                FActionscreen.Content = "Scan de retouren";
                A = false;

            }
            else
            {
                btRetour.Background = (Brush)bc.ConvertFrom("#FFDDDDDD");
                FActionscreen.Content = "Scan de artikelen";
                A = true;
            }
        }


        private void btKorting_Click(object sender, RoutedEventArgs e)
        {
            if (Korting == false)
            {
                btKorting.Background = Brushes.Green;
                FActionscreen.Content = "Scan de kortingspas";
                Korting = true;

            }
            else
            {
                btKorting.Background = (Brush)bc.ConvertFrom("#FFDDDDDD");
                FActionscreen.Content = "Scan de artikelen";
                Korting = false;
            }
        }

        private void btWisselgeld_Click(object sender, RoutedEventArgs e)
        {
            if (Wisselgeld == false)
            {
                btWisselgeld.Background = Brushes.Green;
                FActionscreen.Content = "Voer bedrag in wat klant heeft gegeven";
                Wisselgeld = true;

            }
            else
            {
                btWisselgeld.Background = (Brush)bc.ConvertFrom("#FFDDDDDD");
                FActionscreen.Content = "Scan de artikelen";
                Wisselgeld = false;
            }
        }
    }
}
