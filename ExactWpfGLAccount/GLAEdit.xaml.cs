using Exact;
using System.Windows;




namespace ExactWpfGLAccount
{
    public partial class GLAEdit : Window
    {
        private string originalCode = null;
        public GLAccount glaAccount = null;

        public GLAEdit()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Code.Text = glaAccount.Code;
            Description.Text = glaAccount.Description;
            originalCode = glaAccount.Code;
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            glaAccount.Code = Code.Text;
            glaAccount.Description = Description.Text; 
            if (glaAccount.Code == originalCode) await Rest.updateGLAccount(glaAccount); else await Rest.insertGLAccount(glaAccount);
            Close();
        }
        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            await Rest.deleteGLAccount(glaAccount);
            Close();
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            Code.Text = "";
            Description.Text = "";
            Code.IsReadOnly = false;
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
