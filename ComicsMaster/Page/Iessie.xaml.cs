using System;
using System.Data.SqlClient;
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
using ComicsMaster.Page;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ComicsMaster
{
    public partial class Iessie : UserControl
    {
        #region GlobalParameters
        bool MousedownOn = false;
        double timeOpacity = 0.25;
        string nameComics; string idUser;
        double starAvg;
        int IDCOMICSCOVER;
        string IDPARENTCATEGORY, COMICSCOVERWALPAPER, COMICSCOVERWALPAPERTOIESSIE;
        public string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString,
                     catalog = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        #endregion

        public Iessie( int IDCOMICSCOVER1, string IDPARENTCATEGORY1,string COMICSCOVERWALPAPER1, string COMICSCOVERWALPAPERTOIESSIE1)
        {
            InitializeComponent();
            IDCOMICSCOVER = IDCOMICSCOVER1;  IDPARENTCATEGORY = IDPARENTCATEGORY1; COMICSCOVERWALPAPER = COMICSCOVERWALPAPER1; COMICSCOVERWALPAPERTOIESSIE= COMICSCOVERWALPAPERTOIESSIE1;
            
        }
        private void Fafourite_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (MousedownOn == true)
            {
                Fafourite.Height = 35;
                Fafourite.Source = new BitmapImage(new Uri(catalog + @"\Images\icons8-сложение-50.png"));
                var connect = new SqlConnection(connectionString);
                connect.Open();
                string sql = string.Format($"Delete  FAVOURITES Where IDFAFOURITECOMICS = '{ComicsName.Text}'");
                using (SqlCommand cmd = new SqlCommand(sql, connect))
                {
                    cmd.ExecuteNonQuery();
                }
                MousedownOn = false;
            }
            else
            {
                var connect = new SqlConnection(connectionString);
                connect.Open();
                string sql = string.Format("Insert Into FAVOURITES" +
                "(IDUSERLOGIN,IDFAFOURITECOMICS,IDCOMICSCOVER,IDPARENTCATEGORY,COMICSCOVERWALPAPER,COMICSCOVERWALPAPERTOIESSIE,COMICSDISCRIPTION) Values(@IDUSERLOGIN,@IDFAFOURITECOMICS,@IDCOMICSCOVER,@IDPARENTCATEGORY,@COMICSCOVERWALPAPER,@COMICSCOVERWALPAPERTOIESSIE,@COMICSDISCRIPTION)");
                using (SqlCommand cmd = new SqlCommand(sql, connect))
                {
                    cmd.Parameters.AddWithValue("@IDUSERLOGIN", NameSignIN.flag);
                    cmd.Parameters.AddWithValue("@IDFAFOURITECOMICS", ComicsName.Text);
                    cmd.Parameters.AddWithValue("@IDCOMICSCOVER", IDCOMICSCOVER);
                    cmd.Parameters.AddWithValue("@IDPARENTCATEGORY", IDPARENTCATEGORY);
                    cmd.Parameters.AddWithValue("@COMICSCOVERWALPAPER", COMICSCOVERWALPAPER);
                    cmd.Parameters.AddWithValue("@COMICSCOVERWALPAPERTOIESSIE", COMICSCOVERWALPAPERTOIESSIE);
                    cmd.Parameters.AddWithValue("@COMICSDISCRIPTION", ComicsDiscription.Text);
                    cmd.ExecuteNonQuery();
                }
                connect.Close();
                Fafourite.Height = 30;
                Fafourite.Source = new BitmapImage(new Uri(catalog + @"\Images\icons8-мусор-90.png"));
                    MousedownOn = true;
            }
        }
        private void BurgerRecomend_MouseDown(object sender, MouseButtonEventArgs e)
        {
            #region animation
            Chapters.Foreground = Brushes.Black;
            Videos.Foreground = Brushes.Gray;
            BurgerRecomendR.BeginAnimation(WidthProperty, new DoubleAnimation(BurgerRecomendR.ActualWidth, BurgerRecomend.ActualWidth, TimeSpan.FromSeconds(timeOpacity)));
            BurgerUpdateR.BeginAnimation(WidthProperty, new DoubleAnimation(BurgerUpdateR.ActualWidth, 0, TimeSpan.FromSeconds(timeOpacity)));
            #endregion
        }
        private void BurgerUpdate_MouseDown(object sender, MouseButtonEventArgs e)
        {
            #region animation
            Chapters.Foreground = Brushes.Gray;
            Videos.Foreground = Brushes.Black;
            BurgerRecomendR.BeginAnimation(WidthProperty, new DoubleAnimation(BurgerRecomendR.ActualWidth, 0, TimeSpan.FromSeconds(timeOpacity)));
            BurgerUpdateR.BeginAnimation(WidthProperty, new DoubleAnimation(BurgerUpdateR.ActualWidth, BurgerUpdate.ActualWidth, TimeSpan.FromSeconds(timeOpacity)));
            #endregion
        }
        private void BurgerRecomend_MouseLeave(object sender, MouseEventArgs e)
        {
            BurgerUpdate.Background = null;
            BurgerRecomend.Background = null;
        }
        private void BurgerRecomend_MouseEnter(object sender, MouseEventArgs e) => BurgerRecomend.Background = new SolidColorBrush(Color.FromRgb(220, 220, 220));
        private void BurgerUpdate_MouseEnter(object sender, MouseEventArgs e) => BurgerUpdate.Background = new SolidColorBrush(Color.FromRgb(220, 220, 220));

        private void Star1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int oneStar = 1;
            var connect = new SqlConnection(connectionString);
            connect.Open();
            string sql = string.Format("Insert Into MARK" +
            "(IDPARENTCOMICS,IDNAMECOMICS,IDUSERNAME,STAR) Values(@IDPARENTCOMICS,@IDNAMECOMICS,@IDUSERNAME,@STAR)");
            using (SqlCommand cmd = new SqlCommand(sql, connect))
            {
                cmd.Parameters.AddWithValue("@IDPARENTCOMICS", IDCOMICSCOVER);
                cmd.Parameters.AddWithValue("@IDNAMECOMICS", ComicsName.Text);
                cmd.Parameters.AddWithValue("@IDUSERNAME", NameSignIN.flag);
                cmd.Parameters.AddWithValue("@STAR", oneStar.ToString());
                cmd.ExecuteNonQuery();
            }
            connect.Close();
            Star1.Source = new BitmapImage(new Uri(catalog + @"\Images\icons8-звезда-64.png"));
            Star2.Source = new BitmapImage(new Uri(catalog+@"\Images\icons8-звезда-64 (2).png"));
            Star3.Source = new BitmapImage(new Uri(catalog+@"\Images\icons8-звезда-64 (2).png"));
            Star1.MouseEnter -= Star1_MouseEnter;
            Star2.MouseEnter -= Star2_MouseEnter;
            Star3.MouseEnter -= Star3_MouseEnter;
            Star1.MouseLeave -= Star1_MouseLeave;
            Star2.MouseLeave -= Star1_MouseLeave;
            Star3.MouseLeave -= Star1_MouseLeave;
            Star1.MouseDown -= Star1_MouseDown;
            Star2.MouseDown -= Star2_MouseDown;
            Star3.MouseDown -= Star3_MouseDown;
        }

        private void Star2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int twoStar = 2;
            var connect = new SqlConnection(connectionString);
            connect.Open();
            string sql = string.Format("Insert Into MARK" +
            "(IDPARENTCOMICS,IDNAMECOMICS,IDUSERNAME,STAR) Values(@IDPARENTCOMICS,@IDNAMECOMICS,@IDUSERNAME,@STAR)");
            using (SqlCommand cmd = new SqlCommand(sql, connect))
            {
                cmd.Parameters.AddWithValue("@IDPARENTCOMICS", IDCOMICSCOVER);
                cmd.Parameters.AddWithValue("@IDNAMECOMICS", ComicsName.Text);
                cmd.Parameters.AddWithValue("@IDUSERNAME", NameSignIN.flag);
                cmd.Parameters.AddWithValue("@STAR", twoStar.ToString());
                cmd.ExecuteNonQuery();
            }
            connect.Close();
            Star1.Source = new BitmapImage(new Uri(catalog + @"\Images\icons8-звезда-64.png"));
            Star2.Source = new BitmapImage(new Uri(catalog+@"\Images\icons8-звезда-64.png"));
            Star3.Source = new BitmapImage(new Uri(catalog+@"\Images\icons8-звезда-64 (2).png"));

            Star1.MouseEnter -= Star1_MouseEnter;
            Star2.MouseEnter -= Star2_MouseEnter;
            Star3.MouseEnter -= Star3_MouseEnter;
            Star1.MouseLeave -= Star1_MouseLeave;
            Star2.MouseLeave -= Star1_MouseLeave;
            Star3.MouseLeave -= Star1_MouseLeave;
            Star1.MouseDown -= Star1_MouseDown;
            Star2.MouseDown -= Star2_MouseDown;
            Star3.MouseDown -= Star3_MouseDown;
        }

        private void Star3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int threeStar = 3;
            var connect = new SqlConnection(connectionString);
            connect.Open();
            string sql = string.Format("Insert Into MARK" +
            "(IDPARENTCOMICS,IDNAMECOMICS,IDUSERNAME,STAR) Values(@IDPARENTCOMICS,@IDNAMECOMICS,@IDUSERNAME,@STAR)");
            using (SqlCommand cmd = new SqlCommand(sql, connect))
            {
                cmd.Parameters.AddWithValue("@IDPARENTCOMICS", IDCOMICSCOVER);
                cmd.Parameters.AddWithValue("@IDNAMECOMICS", ComicsName.Text);
                cmd.Parameters.AddWithValue("@IDUSERNAME", NameSignIN.flag);
                cmd.Parameters.AddWithValue("@STAR", threeStar.ToString());
                cmd.ExecuteNonQuery();
            }
            connect.Close();
            Star1.Source = new BitmapImage(new Uri(catalog + @"\Images\icons8-звезда-64.png"));
            Star2.Source = new BitmapImage(new Uri(catalog+@"\Images\icons8-звезда-64.png"));
            Star3.Source = new BitmapImage(new Uri(catalog+@"\Images\icons8-звезда-64.png"));

            Star1.MouseEnter -= Star1_MouseEnter;
            Star2.MouseEnter -= Star2_MouseEnter;
            Star3.MouseEnter -= Star3_MouseEnter;
            Star1.MouseLeave -= Star1_MouseLeave;
            Star2.MouseLeave -= Star1_MouseLeave;
            Star3.MouseLeave -= Star1_MouseLeave;
            Star1.MouseDown -= Star1_MouseDown;
            Star2.MouseDown -= Star2_MouseDown;
            Star3.MouseDown -= Star3_MouseDown;
        }

        private void Star1_MouseEnter(object sender, MouseEventArgs e)
        {
            Star1.Source = new BitmapImage(new Uri(catalog + @"\Images\icons8-звезда-64.png"));
            Star2.Source = new BitmapImage(new Uri(catalog+@"\Images\icons8-звезда-64 (2).png"));
            Star3.Source = new BitmapImage(new Uri(catalog+@"\Images\icons8-звезда-64 (2).png"));
        }

        private void Star2_MouseEnter(object sender, MouseEventArgs e)
        {
            Star1.Source = new BitmapImage(new Uri(catalog + @"\Images\icons8-звезда-64.png"));
            Star2.Source = new BitmapImage(new Uri(catalog+@"\Images\icons8-звезда-64.png"));
            Star3.Source = new BitmapImage(new Uri(catalog+@"\Images\icons8-звезда-64 (2).png"));
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var sqlConect = new SqlConnection(connectionString);
            sqlConect.Open();
            int sqlrezult, sqlrezult2;
            string sqlrezult3;
            string sq1 = string.Format("SELECT COUNT(*) from FAVOURITES WHERE" + "( IDUSERLOGIN=" + "@LOG) and ( IDFAFOURITECOMICS=" + "@Name)");
            using (SqlCommand cmd = new SqlCommand(sq1, sqlConect))
            {
                cmd.Parameters.AddWithValue("@LOG", NameSignIN.flag);
                cmd.Parameters.AddWithValue("@Name", ComicsName.Text);
                sqlrezult = (int)cmd.ExecuteScalar();
            }
            string sq2 = string.Format("SELECT COUNT(*) from Mark WHERE" + "( IDUSERNAME=" + "@LOG) and ( IDNAMECOMICS=" + "@Name)");
            using (
                SqlCommand cmd = new SqlCommand(sq2, sqlConect))
            {
                cmd.Parameters.AddWithValue("@LOG", NameSignIN.flag);
                cmd.Parameters.AddWithValue("@Name", ComicsName.Text);
                sqlrezult2 = (int)cmd.ExecuteScalar();
            }
            sqlConect.Close();
            if (sqlrezult >= 1)
            {
                Fafourite.Height = 30;
                Fafourite.Source = new BitmapImage(new Uri(@"E:\OOP\ComicsMaster\ComicsMaster\images\icons8-мусор-90.png"));
                MousedownOn = true;
            }
            if (sqlrezult2 >= 1)
            {
                var sqlConect2 = new SqlConnection(connectionString);
                sqlConect.Open();
                string sq3 = string.Format("SELECT * from Mark WHERE" + "( IDUSERNAME=" + "@LOG) and ( IDNAMECOMICS=" + "@Name)");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = $"SELECT Star from MARK WHERE ( IDUSERNAME='{NameSignIN.flag}') and ( IDNAMECOMICS='{ComicsName.Text}')";
                    SqlCommand command = new SqlCommand(sql, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        try
                        {
                            sqlrezult3 = reader.GetString(4);

                            connection.Close();

                            if (sqlrezult3 == "1")
                            {
                                Star1.Source = new BitmapImage(new Uri(catalog + @"\Images\icons8-звезда-64.png"));
                                Star2.Source = new BitmapImage(new Uri(catalog + @"\Images\icons8-звезда-64 (2).png"));
                                Star3.Source = new BitmapImage(new Uri(catalog + @"\Images\icons8-звезда-64 (2).png"));
                            }
                            if (sqlrezult3 == "2")
                            {
                                Star1.Source = new BitmapImage(new Uri(catalog + @"\Images\icons8-звезда-64.png"));
                                Star2.Source = new BitmapImage(new Uri(catalog + @"\Images\icons8-звезда-64.png"));
                                Star3.Source = new BitmapImage(new Uri(catalog + @"\Images\icons8-звезда-64 (2).png"));
                            }
                            if (sqlrezult3 == "3")
                            {
                                Star1.Source = new BitmapImage(new Uri(catalog + @"\Images\icons8-звезда-64.png"));
                                Star2.Source = new BitmapImage(new Uri(catalog + @"\Images\icons8-звезда-64.png"));
                                Star3.Source = new BitmapImage(new Uri(catalog + @"\Images\icons8-звезда-64.png"));
                            }
                        }
                        catch { }
                    };
                }
                    Star1.MouseEnter -= Star1_MouseEnter;
                    Star2.MouseEnter -= Star2_MouseEnter;
                    Star3.MouseEnter -= Star3_MouseEnter;
                    Star1.MouseLeave -= Star1_MouseLeave;
                    Star2.MouseLeave -= Star1_MouseLeave;
                    Star3.MouseLeave -= Star1_MouseLeave;
                    Star1.MouseDown -= Star1_MouseDown;
                    Star2.MouseDown -= Star2_MouseDown;
                    Star3.MouseDown -= Star3_MouseDown;
                
                sqlConect2.Close();
            }

            try
            {
                var str = NameSignIN.flag;
                var sqlConectHistory = new SqlConnection(connectionString);
                int sqlrezult4;
                sqlConectHistory.Open();
                string sq4 = string.Format("SELECT COUNT(*) from HISTORY WHERE" + "( IDHISTORYCOMICS=" + "@LOG)");
                using (SqlCommand cmd = new SqlCommand(sq4, sqlConectHistory))
                {
                    cmd.Parameters.AddWithValue("@LOG", ComicsName.Text);
                    sqlrezult4 = (int)cmd.ExecuteScalar();
                }
                sqlConect.Close();
                if (sqlrezult < 1)
                {
                    var connectHistory = new SqlConnection(connectionString);
                    connectHistory.Open();
                    string sqlQ = string.Format("Insert Into HISTORY(IDUSERLOGIN,IDHISTORYCOMICS,IDCOMICSCOVER,IDPARENTCATEGORY,COMICSCOVERWALPAPER,COMICSCOVERWALPAPERTOIESSIE,COMICSDISCRIPTION) Values(@IDUSERLOGIN,@IDHISTORYCOMICS,@IDCOMICSCOVER,@IDPARENTCATEGORY,@COMICSCOVERWALPAPER,@COMICSCOVERWALPAPERTOIESSIE,@COMICSDISCRIPTION)");
                    using (SqlCommand cmd = new SqlCommand(sqlQ, connectHistory))
                    {
                        cmd.Parameters.AddWithValue("@IDUSERLOGIN", NameSignIN.flag);
                        cmd.Parameters.AddWithValue("@IDHISTORYCOMICS", ComicsName.Text);
                        cmd.Parameters.AddWithValue("@IDCOMICSCOVER", IDCOMICSCOVER);
                        cmd.Parameters.AddWithValue("@IDPARENTCATEGORY", IDPARENTCATEGORY);
                        cmd.Parameters.AddWithValue("@COMICSCOVERWALPAPER", COMICSCOVERWALPAPER);
                        cmd.Parameters.AddWithValue("@COMICSCOVERWALPAPERTOIESSIE", COMICSCOVERWALPAPERTOIESSIE);
                        cmd.Parameters.AddWithValue("@COMICSDISCRIPTION", ComicsDiscription.Text);
                        cmd.ExecuteNonQuery();
                    }
                    connectHistory.Close();
                }
            }
            catch
            {
                MessageBox.Show("Error");
            }


        }

        private void Star3_MouseEnter(object sender, MouseEventArgs e)
        {
            Star1.Source = new BitmapImage(new Uri(catalog + @"\Images\icons8-звезда-64.png"));
            Star2.Source = new BitmapImage(new Uri(catalog+@"\Images\icons8-звезда-64.png"));
            Star3.Source = new BitmapImage(new Uri(catalog+@"\Images\icons8-звезда-64.png"));
        }

        private void Star1_MouseLeave(object sender, MouseEventArgs e)
        {
            Star1.Source = new BitmapImage(new Uri(catalog + @"\Images\icons8-звезда-64 (2).png"));
            Star2.Source = new BitmapImage(new Uri(catalog+@"\Images\icons8-звезда-64 (2).png"));
            Star3.Source = new BitmapImage(new Uri(catalog+@"\Images\icons8-звезда-64 (2).png"));
        }

        private void Comment_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Comments CommentPanel = new Comments(ComicsName.Text, NameSignIN.flag);
            CommentPanel.Width = 0; CommentPanel.HorizontalAlignment = HorizontalAlignment.Right;
            Parent.Children.Add(CommentPanel);
            CommentPanel.BeginAnimation(WidthProperty, new DoubleAnimation(CommentPanel.ActualWidth, Header.ActualWidth, TimeSpan.FromSeconds(timeOpacity)));
        }
    }
}
