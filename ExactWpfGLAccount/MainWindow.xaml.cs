using Exact;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace ExactWpfGLAccount
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            getAuthorization();
        }
        private void getAuthorization()
        {
            string request = Constants.BASE_URI + "/api/oauth2/auth" +
                            "?client_id={" + Constants.CLIENT_ID + "}" +
                            "&redirect_uri=" + Constants.CALLBACK_URL +
                            "&state=" + Constants.CLIENT_STATE +
                            "&response_type=code&force_login=0";


            webBrowser.Navigate(new Uri(request));
        }
        private void GetCode(string url)
        {
            if (url.IndexOf(Constants.BASE_URI) < 0)
            {
                int c = url.IndexOf("?code=");
                int s = url.IndexOf("&state=");
                OAuth.Code = url.Substring(c + 6, s - c - 6);
                OAuth.State = url.Substring(s + 7);
                GLAList glaList = new GLAList();
                glaList.Show();
                Close();
            }
        }

        private void webBrowser_Navigated(object sender, NavigationEventArgs e)
        {
            HideScriptErrors();
            webBrowser.RenderSize = new Size(600, 500);
            GetCode(e.Uri.ToString());
        }

        public void HideScriptErrors()  // zucht, zie  https://social.msdn.microsoft.com/Forums/vstudio/en-US/4f686de1-8884-4a8d-8ec5-ae4eff8ce6db/new-wpf-webbrowser-how-do-i-suppress-script-errors?forum=wpf
        {
            FieldInfo fiComWebBrowser = typeof(WebBrowser).GetField("_axIWebBrowser2", BindingFlags.Instance | BindingFlags.NonPublic);
            if (fiComWebBrowser == null) return;
            object objComWebBrowser = fiComWebBrowser.GetValue(webBrowser);
            if (objComWebBrowser == null) return;
            objComWebBrowser.GetType().InvokeMember("Silent", BindingFlags.SetProperty, null, objComWebBrowser, new object[] { true });
        }

        private void webBrowser_LoadCompleted(object sender, NavigationEventArgs e)
        {
            string script = "document.documentElement.style.overflow ='hidden';"
                + "document.documentElement.style.padding ='20'; "
                + "document.forms[0].style.backgroundColor ='#eeeeee';"
                + "document.getElementById('UserNameField').style.width='95%';"
                + "document.getElementById('PasswordField').style.width='95%';"
                ;
            try
            {
                WebBrowser wb = (WebBrowser)sender;
                wb.InvokeScript("execScript", new Object[] { script, "JavaScript" });
            }
            catch { }

        }
    }
}
