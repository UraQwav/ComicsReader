using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Windows.Media.Animation;
using System.Windows.Input;
using System.Windows.Media;
using System.Text.RegularExpressions;
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
                cmd.Parameters.AddWithValue("@GENDER", "INCOGNITO");
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

        private void PasswordSignUpBox_PasswordChanged(object sender, RoutedEventArgs e)
        {

            if (PasswordSignUpBox.Password.Length <= 4)
            {
                PasswordSignUpBox.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                BlockPas.Text = "PASSWORD IS SMALL";
                WarningPassword.Visibility = Visibility.Visible;
                SignUpButton.Click -= new System.Windows.RoutedEventHandler(this.SignUpButton_Click);
            }
            else
            {
                PasswordSignUpBox.BorderBrush = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                BlockPas.Text = "";
                WarningPassword.Visibility = Visibility.Hidden;
                SignUpButton.Click -= new System.Windows.RoutedEventHandler(this.SignUpButton_Click);
            }
        }

        private void LoginSignUpBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            sqlConect = new SqlConnection(connectionString);
            int sqlrezult;
            sqlConect.Open();
            string sq1 = string.Format("SELECT COUNT(*) from LOGINANDPASSWORD WHERE" + "( LOGINS=" + "@LOG)");
            using (SqlCommand cmd = new SqlCommand(sq1, this.sqlConect))
            {
                cmd.Parameters.AddWithValue("@LOG", LoginSignUpBox.Text);
                sqlrezult = (int)cmd.ExecuteScalar();
            }
            sqlConect.Close();
            

                if (!Regex.IsMatch(LoginSignUpBox.Text, "[A-zА-я]"))
                {
                    LoginSignUpBox.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                    ShortLogin.Text = "ADD ALPHABETIC CHARACTERS";
                    WarningLogin.Visibility = Visibility.Visible;
                    SignUpButton.Click -= new System.Windows.RoutedEventHandler(this.SignUpButton_Click);
                }
                else if (LoginSignUpBox.Text.Length < 5)
                {
                    LoginSignUpBox.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                    ShortLogin.Text = "LOGIN IS SMALL";
                    WarningLogin.Visibility = Visibility.Visible;
                    SignUpButton.Click -= new System.Windows.RoutedEventHandler(this.SignUpButton_Click);
                }
                else if (sqlrezult==1)
                {
                    LoginSignUpBox.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                    ShortLogin.Text = "LOGIN IS BUSY";
                    WarningLogin.Visibility = Visibility.Visible;
                    SignUpButton.Click -= new System.Windows.RoutedEventHandler(this.SignUpButton_Click);
                }
                else if (LoginSignUpBox.Text.Length > 95)
                {
                    LoginSignUpBox.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                    ShortLogin.Text = "LOGIN IS SO BIG";
                    WarningLogin.Visibility = Visibility.Visible;
                    SignUpButton.Click -= new System.Windows.RoutedEventHandler(this.SignUpButton_Click);
                }
                else if (LoginSignUpBox.Text.Length ==0)
                {
                    LoginSignUpBox.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                    ShortLogin.Text = "LOGIN IS EMPTY";
                    WarningLogin.Visibility = Visibility.Visible;
                    SignUpButton.Click -= new System.Windows.RoutedEventHandler(this.SignUpButton_Click);
                }
                else
                {
                    LoginSignUpBox.BorderBrush = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                    ShortLogin.Text = "";
                    WarningLogin.Visibility = Visibility.Hidden;
                    SignUpButton.Click -= new System.Windows.RoutedEventHandler(this.SignUpButton_Click);
                }
            

        }

        private void AgeBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (AgeBox.Text.Length > 95)
            {
                AgeBox.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                BlockAge.Text = "AGE IS SO BIG";
                WarningAge.Visibility = Visibility.Visible;
                SignUpButton.Click -= new System.Windows.RoutedEventHandler(this.SignUpButton_Click);
            }
            else if (AgeBox.Text.Length == 0)
            {
                AgeBox.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                BlockAge.Text = "AGE IS EMPTY";
                WarningAge.Visibility = Visibility.Visible;
                SignUpButton.Click -= new System.Windows.RoutedEventHandler(this.SignUpButton_Click);
            }
            else
            {
                AgeBox.BorderBrush = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                BlockAge.Text = "";
                WarningAge.Visibility = Visibility.Hidden;
                SignUpButton.Click -= new System.Windows.RoutedEventHandler(this.SignUpButton_Click);
            }

        }
        private void SurNameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SurNameBox.Text.Length > 95)
            {
                SurNameBox.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                BlockSurname.Text = "SURNAME IS SO BIG";
                WarningSurname.Visibility = Visibility.Visible;
                SignUpButton.Click -= new System.Windows.RoutedEventHandler(this.SignUpButton_Click);
            }
            else if (SurNameBox.Text.Length == 0)
            {
                SurNameBox.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                BlockSurname.Text = "SURNAME IS EMPTY";
                WarningSurname.Visibility = Visibility.Visible;
                SignUpButton.Click -= new System.Windows.RoutedEventHandler(this.SignUpButton_Click);
            }
            else
            {
                SurNameBox.BorderBrush = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                BlockSurname.Text = "";
                WarningSurname.Visibility = Visibility.Hidden;
                SignUpButton.Click -= new System.Windows.RoutedEventHandler(this.SignUpButton_Click);
            }
        }

        private void NameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (NameBox.Text.Length > 95)
            {
                NameBox.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                BlockName.Text = "NAME IS SO BIG";
                WarningName.Visibility = Visibility.Visible;
                SignUpButton.Click -= new System.Windows.RoutedEventHandler(this.SignUpButton_Click);
            }
            else if (NameBox.Text.Length == 0)
            {
                NameBox.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                BlockName.Text = "NAME IS EMPTY";
                WarningName.Visibility = Visibility.Visible;
                SignUpButton.Click -= new System.Windows.RoutedEventHandler(this.SignUpButton_Click);
            }
            else
            {
                NameBox.BorderBrush = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                BlockName.Text = "";
                WarningName.Visibility = Visibility.Hidden;
                SignUpButton.Click -= new System.Windows.RoutedEventHandler(this.SignUpButton_Click);
            }

        }

        private void PasswordSignUpRepeatBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PasswordSignUpBox.Password != PasswordSignUpRepeatBox.Password)
            {
                PasswordSignUpBox.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                PasswordSignUpRepeatBox.BorderBrush = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                BlockRepeat.Text = "PASSWORD DO NOT MATCH";
                WarningReapetPas.Visibility = Visibility.Visible;
                SignUpButton.Click -= new System.Windows.RoutedEventHandler(this.SignUpButton_Click);
            }
            else
            {
                PasswordSignUpBox.BorderBrush = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                PasswordSignUpRepeatBox.BorderBrush = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                BlockRepeat.Text = "";
                WarningReapetPas.Visibility = Visibility.Hidden;
                SignUpButton.Click += new System.Windows.RoutedEventHandler(this.SignUpButton_Click);
            }

        }

        private void LoginSignUpBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789qwertyuiopasdfghjklzxcvbnm.QWERTYUIOPASDFGHJKLZXCVBNMйцукенгшщзхъфывапролджэячсмитьбю@ЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮ_".IndexOf(e.Text) < 0;
        }

        private void LoginSignUpBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
            if (LoginSignUpBox.Text.Length > 95)
            {
                e.Handled = true;
            }
            if (e.Key == Key.Back)
            {
                e.Handled = false;
            }
        }

        private void SurNameBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
            if (LoginSignUpBox.Text.Length > 95)
            {
                e.Handled = true;
            }
            if (e.Key == Key.Back)
            {
                e.Handled = false;
            }
        }

        private void SurNameBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "qwertyuiopasdfghjklzxcvbnm-QWERTYUIOPASDFGHJKLZXCVBNMйцукенгшщзхъфывапролджэячсмитьбюЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮ".IndexOf(e.Text) < 0;
        }
        private void NameBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
            if (LoginSignUpBox.Text.Length > 95)
            {
                e.Handled = true;
            }
            if (e.Key == Key.Back)
            {
                e.Handled = false;
            }
        }

        private void NameBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "qwertyuiopasdfghjklzxcvbnm-QWERTYUIOPASDFGHJKLZXCVBNMйцукенгшщзхъфывапролджэячсмитьбюЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮ".IndexOf(e.Text) < 0;
        }

        private void AgeBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "1234567890".IndexOf(e.Text) < 0;
            AgeBox.MaxLength = 2;
        }

        private void AgeBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
            if (e.Key == Key.Back)
            {
                e.Handled = false;
            }
        }
    }
}
