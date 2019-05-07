using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Windows.Media.Animation;
using System.Windows.Input;
using System.Windows.Media;
namespace ComicsMaster
{
    /// <summary>
    /// Логика взаимодействия для SignUp.xaml
    /// </summary>
    /// 
    static class User
    {
        public static string Login { get; set; }
        public static string Password { get; set; }
    }
    public partial class SignUp : UserControl
    {
        public SignUp()
        {
            InitializeComponent();
        }
        public SqlConnection sqlConect = null;
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            FlagCheckSignUP.flag = false;
            sqlConect = new SqlConnection(connectionString);
            sqlConect.Open();
            string sq1 = string.Format("Insert Into USERSDATA" + "(NAME,SURNAME,GENDER,AGE) Values (@NAME,@SURNAME,@GENDER,@AGE); Insert Into LOGINANDPASSWORD" + "(LOGINS,PASSWORDS) Values (@LOGINS,@PASSWORDS)");
            using (SqlCommand cmd = new SqlCommand(sq1, this.sqlConect))
            {
                cmd.Parameters.AddWithValue("@NAME", NameBox.Text);
                cmd.Parameters.AddWithValue("@SURNAME", SurNameBox.Text);
                cmd.Parameters.AddWithValue("@GENDER", GenderBox.Text);
                cmd.Parameters.AddWithValue("AGE", AgeBox.Text);
                cmd.Parameters.AddWithValue("@LOGINS", LoginSignUpBox.Text);
                cmd.Parameters.AddWithValue("@PASSWORDS", PasswordSignUpBox.Password.GetHashCode());
                cmd.ExecuteNonQuery();
                FlagCheckSignUP.flag = true;
                User.Login = LoginSignUpBox.Text;
            }
            sqlConect.Close();
            if (FlagCheckSignUP.flag == true)
            {
                DoubleAnimation ShowSignUP = new DoubleAnimation();
                ShowSignUP.From = this.ActualHeight;
                ShowSignUP.To = 0;
                ShowSignUP.Duration = TimeSpan.FromSeconds(0.15);
                this.BeginAnimation(UserControl.HeightProperty, ShowSignUP);
            }
        }
    }
}
