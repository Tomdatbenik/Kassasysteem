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
using KassaSystem.Classes;
using System.Windows.Shapes;
using System.Data;

namespace KassaSystem.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        MainWindow Main;
        Database db = new Database();
        public LoginPage(MainWindow main)
        {
            InitializeComponent();
            this.Main = main;
        }

        private void BtLogin_Click(object sender, RoutedEventArgs e)
        {
            //Register
            //string Salt = BCrypt.GenerateSalt();
            //string HashedPass = BCrypt.HashPassword(PbPass.Password, Salt);
            //string query = "INSERT INTO `kassasysteem`.`gebruikers` (`username`, `password`) VALUES ('" + TbNaam.Text + "', '" + HashedPass +"');";
            //db.ExecuteStringQuery(query);

            //Login
            DataTable dt = db.ExecuteStringQuery("SELECT password FROM gebruikers WHERE username LIKE '" + TbNaam.Text + "'");
            string sPassword = dt.Rows[0].Field<string>(0);

            if (PbPass.Password == sPassword) 
            {
                MessageBox.Show("U bent ingelogd");
                Main.Content = new Home();
            }
            else
            {
                MessageBox.Show("Verkeerd wachtwoord");
            }      
        }
    }
}
