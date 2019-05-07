using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Controls;
using System.Windows.Media;

namespace ComicsMaster
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 
    static class FlagCheckSignUP
    {
        public static bool flag { get; set; }
    }
    static class NameSignIN
    {
        public static string flag { get; set; }
    }
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }
        bool flagSignUp = true;
        public SqlConnection sqlConect = null;
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
           sqlConect = new SqlConnection(connectionString);
            int sqlrezult;
                sqlConect.Open();
                string sq1 = string.Format("SELECT COUNT(*) from LOGINANDPASSWORD WHERE" + "( LOGINS=" + "@LOG" + " AND PASSWORDS= " + "@PAS)");
                using (SqlCommand cmd = new SqlCommand(sq1, this.sqlConect))
                {
                    cmd.Parameters.AddWithValue("@LOG", LoginBox.Text);
                    
                    cmd.Parameters.AddWithValue("@PAS", PasswordBox.Password.GetHashCode());
                    sqlrezult = (int)cmd.ExecuteScalar();
                }
                    sqlConect.Close();
            if (sqlrezult == 1)
            {

                NameSignIN.flag = LoginBox.Text;

                ComicMaster comicMaster = new ComicMaster();
                comicMaster.Show();
                this.Close();
            } 
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
                Shadow.Margin = new Thickness(0, 0, 0, 0);
            }
            else if(this.WindowState==WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                Shadow.Margin = new Thickness(10, 10, 10, 10);
            }
            
        }
        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            if (flagSignUp)
            {
                DoubleAnimation ShowSignUP = new DoubleAnimation();
                ShowSignUP.From = SignUp.ActualHeight;
                ShowSignUP.To = 460;
                ShowSignUP.Duration = TimeSpan.FromSeconds(0.15);
                SignUp.BeginAnimation(UserControl.HeightProperty, ShowSignUP);
                NavigationPanel.Background = Brushes.Black;
                DoubleAnimation ShowSignUPPanel = new DoubleAnimation();
                ShowSignUPPanel.From = NavigationPanel.Opacity;
                ShowSignUPPanel.To = 0.25;
                ShowSignUPPanel.Duration = TimeSpan.FromSeconds(0.5);
                NavigationPanel.BeginAnimation(Grid.OpacityProperty, ShowSignUPPanel);
            }
           
        }
        private void Grid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
