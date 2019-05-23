using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Controls;
using System.Windows.Media;
using ComicsMaster.Models;

namespace ComicsMaster
{
    #region GlobalClass
    static class FlagCheckSignUP
    {
        public static bool flag { get; set; }
    }
    static class NameSignIN
    {
        public static string flag { get; set; }
    }
    #endregion
    public partial class LoginWindow : Window
    {
        #region GlobalParametrs
        bool flagSignUp = true;
        public SqlConnection sqlConect = null;
        Repository repository;
        #endregion
        public LoginWindow()
        {
            InitializeComponent();
            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            repository = new Repository();
        }
        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            if (repository.FindUser(LoginBox.Text, PasswordBox.Password) == 1)
            {
                NameSignIN.flag = LoginBox.Text;
                ComicMaster comicMaster = new ComicMaster();
                comicMaster.Show();
                this.Close();
            } 
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
                Shadow.Margin = new Thickness(0, 0, 0, 0);
            }
            else if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                Shadow.Margin = new Thickness(10, 10, 10, 10);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)=>this.Close();
        private void Button_Click_1(object sender, RoutedEventArgs e)=>this.WindowState = WindowState.Minimized;
        private void Grid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) => this.DragMove();
        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            if (flagSignUp)
            {
                DoubleAnimation ShowSignUP = new DoubleAnimation();
                ShowSignUP.From = SignUp.ActualHeight;
                ShowSignUP.To = Shadow.ActualHeight;
                ShowSignUP.Duration = TimeSpan.FromSeconds(0.05);
                SignUp.BeginAnimation(UserControl.HeightProperty, ShowSignUP);
                DoubleAnimation ShowSignUPOpacity = new DoubleAnimation();
                ShowSignUPOpacity.From = SignUp.Opacity;
                ShowSignUPOpacity.To = 1;
                ShowSignUPOpacity.Duration = TimeSpan.FromSeconds(0.75);
                SignUp.BeginAnimation(UserControl.OpacityProperty, ShowSignUPOpacity);
                NavigationPanel.Background = Brushes.Black;
                DoubleAnimation ShowSignUPPanel = new DoubleAnimation();
                ShowSignUPPanel.From = NavigationPanel.Opacity;
                ShowSignUPPanel.To = 0.25;
                ShowSignUPPanel.Duration = TimeSpan.FromSeconds(0.5);
                NavigationPanel.BeginAnimation(Grid.OpacityProperty, ShowSignUPPanel);
            }
           
        }
    }
}
