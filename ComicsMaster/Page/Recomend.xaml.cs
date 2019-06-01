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
using System.Data;
using System.Data.SqlClient;

namespace ComicsMaster.Page
{
    public partial class Recomend : UserControl
    {
        string starAvg;
        int ImagesMarginTop = 0,
            WidthGrid = 0,
            WidthGridForbutton = 0,
            LeftMarginCover = 0,
            TopMarginCover = 0;
        ComicMaster Comics;
        Grid GridComics = new Grid();
        bool firstbutton = true;
        string nameas;
        string name_kategory;
        string data;
        string data2;
        string name_Rest;
        string DiscriptionComics;
        public SqlConnection sqlConect = null;
        public SqlConnection sqlConect1 = null;
        public SqlConnection sqlConect2 = null;
        public SqlConnection sqlconect3 = null;
        public SqlConnection sqlConect4 = null;
        public SqlConnection sqlConect5 = null;
        bool ComicsItemsDo = false;
        string GlobalString = "",
            one, two, thr, four, five, six,
               catalog = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        double timeOpacity = 0.35;

        List<ComicsPreview> list = new List<ComicsPreview>();

        Button actionButton = new Button();
        Button adventureButton = new Button();
        Button horrorButton = new Button();
        Button comedyButton = new Button();
        Button demonsButton = new Button();
        Button MisteryButton = new Button();
        Button historicalButton = new Button();
        Button tragedyButton = new Button();
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        private void LoadDataRecomend(Grid Content, string Category)
        {
           
            sqlConect = new SqlConnection(connectionString);
            sqlConect.Open();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = $"SELECT * FROM COMICSCOVER WHERE IDPARENTCATEGORY ='{Category}'";
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader = command.ExecuteReader();
                int i = 0;
                
                    while (reader.Read())
                {
                    if (i < 5)
                    {
                        nameas = reader.GetValue(0).ToString();
                        name_kategory = reader.GetString(1);
                        data = reader.GetString(2);
                        data2 = reader.GetString(3);
                        DiscriptionComics = reader.GetString(4);
                        name_Rest = reader.GetString(5);
                        ComicsPreview comicsPreview = new ComicsPreview();
                        Iessie iessie = new Iessie(Convert.ToInt32(nameas), name_kategory, data, data2);

                        iessie.Margin = new Thickness(0, 0, 0, 0);
                        iessie.ComicsDiscription.Text = DiscriptionComics;
                        iessie.ComicsName.Text = name_Rest;
                        //iessie.Content.HorizontalAlignment = HorizontalAlignment.Center;
                        comicsPreview.ComicsWalpeper.Source = new BitmapImage(new Uri(catalog + data));
                        comicsPreview.NameComix.Text = name_Rest;
                        comicsPreview.NameGlavaComix.Text = name_kategory;
                        #region IESSIE_ADD_BUTTON
                        int buttonMarginLeft = 0,
                            buttonMarginTop = 0;
                        sqlConect1 = new SqlConnection(connectionString);
                        sqlConect1.Open();
                        var image = new Image { Source = new BitmapImage(new Uri(catalog + data2)) };
                        using (SqlConnection connection1 = new SqlConnection(connectionString))
                        {
                            connection1.Open();
                            string sql1 = "SELECT * FROM COMICSCOVERITEM";
                            SqlCommand command1 = new SqlCommand(sql1, connection1);
                            SqlDataReader reader1 = command1.ExecuteReader();
                            while (reader1.Read())
                            {
                                string nameqw = reader1.GetValue(1).ToString();
                                string name_Rest1 = reader1.GetString(2);
                                GlobalString = name_Rest1;
                                if (nameqw == nameas)
                                {
                                    Button button = new Button();

                                    button.Content = name_Rest1;
                                    button.Margin = new Thickness(10, 5, 10, 5);
                                    button.Height = 50;
                                    if (buttonMarginLeft < 4)
                                    {
                                        if (firstbutton)
                                        {
                                            Grid.SetRow(button, buttonMarginTop);
                                            Grid.SetColumn(button, buttonMarginLeft);
                                            WidthGridForbutton++;
                                            buttonMarginLeft++;
                                            button.Background = Brushes.DarkRed;
                                            button.Foreground = Brushes.White;
                                            firstbutton = false;
                                            button.Style = (Style)button.FindResource("ButtonComicsIssieStylefirst");
                                        }
                                        else
                                        {
                                            button.Style = (Style)button.FindResource("ButtonComicsIssieStyle");
                                            Grid.SetRow(button, buttonMarginTop);
                                            Grid.SetColumn(button, buttonMarginLeft);
                                            WidthGridForbutton++;
                                            buttonMarginLeft++;
                                        }

                                    }
                                    else
                                    {
                                        button.Style = (Style)button.FindResource("ButtonComicsIssieStyle");
                                        WidthGridForbutton = 0;
                                        iessie.ContentForButton.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                                        buttonMarginLeft = 0; buttonMarginTop++;
                                        Grid.SetRow(button, buttonMarginTop);
                                        Grid.SetColumn(button, buttonMarginLeft);
                                        buttonMarginLeft++;

                                    }
                                    button.Click += (sender, e) =>
                                    {
                                        Comics.Content.Children.Clear();
                                        Comics.ContentToIessie.Children.Clear();
                                        Comics.BackgroundIessie.Children.Clear();
                                        Comics.BackgroundIessie.Children.Remove(image);
                                        Comics.Content.Children.Add(Comics.GridAddChild(name_Rest1));
                                        ImagesMarginTop = 0;
                                    };
                                    iessie.ContentForButton.Children.Add(button);
                                }

                            };
                            connection1.Close();
                        }
                        buttonMarginTop = 0; buttonMarginLeft = 0; firstbutton = true;
                        #endregion
                        iessie.backImage.MouseDown += (sen, t) => { Comics.Content.Children.Clear(); Comics.ContentToIessie.Children.Clear(); Comics.BackgroundIessie.Children.Clear(); Comics.Content.Children.Add(new Recomend(Comics)); };
                        comicsPreview.ButtonGrid.MouseDown += (source, e) =>
                        {
                            Comics.Content.Children.Clear();
                            Comics.ContentToIessie.Children.Clear();
                            Comics.ContentToIessie.Children.Add(iessie);
                            Comics.BackgroundIessie.Children.Add(image);
                            try
                            {
                                var str = NameSignIN.flag;
                                var sqlConectHistory = new SqlConnection(connectionString);
                                int sqlrezult;
                                sqlConectHistory.Open();
                                string sq1 = string.Format("SELECT COUNT(*) from HISTORY WHERE" + "( IDHISTORYCOMICS=" + "@LOG)");
                                using (SqlCommand cmd = new SqlCommand(sq1, sqlConectHistory))
                                {
                                    cmd.Parameters.AddWithValue("@LOG", name_Rest);
                                    sqlrezult = (int)cmd.ExecuteScalar();
                                }
                                sqlConect.Close();
                                if (sqlrezult < 1)
                                {
                                    var connectHistory = new SqlConnection(connectionString);
                                    connectHistory.Open();
                                    string sqlQ = string.Format("Insert Into HISTORY(IDUSERLOGIN,IDHISTORYCOMICS,IDCOMICSCOVER,IDPARENTCATEGORY,COMICSCOVERWALPAPER,COMICSCOVERWALPAPERTOIESSIE) Values(@IDUSERLOGIN,@IDHISTORYCOMICS,@IDCOMICSCOVER,@IDPARENTCATEGORY,@COMICSCOVERWALPAPER,@COMICSCOVERWALPAPERTOIESSIE)");
                                    using (SqlCommand cmd = new SqlCommand(sqlQ, connectHistory))
                                    {
                                        cmd.Parameters.AddWithValue("@IDUSERLOGIN", str);
                                        cmd.Parameters.AddWithValue("@IDHISTORYCOMICS", name_Rest);
                                        cmd.Parameters.AddWithValue("@IDCOMICSCOVER", nameas);
                                        cmd.Parameters.AddWithValue("@IDPARENTCATEGORY", name_kategory);
                                        cmd.Parameters.AddWithValue("@COMICSCOVERWALPAPER", data);
                                        cmd.Parameters.AddWithValue("@COMICSCOVERWALPAPERTOIESSIE", data2);
                                        cmd.Parameters.AddWithValue("@COMICSDISCRIPTION", DiscriptionComics);
                                        cmd.ExecuteNonQuery();
                                    }
                                    connectHistory.Close();
                                }
                            }
                            catch
                            {
                                MessageBox.Show("Error");
                            }

                        };
                        if (WidthGrid < (int)Content.ActualWidth)
                        {
                            Content.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                            Grid.SetColumn(comicsPreview, LeftMarginCover);
                            LeftMarginCover++;
                            WidthGrid = LeftMarginCover * 160;
                        }
                        if (WidthGrid >= (int)Content.ActualWidth)
                        {
                            
                        }
                        Content.Children.Add(comicsPreview);
                        list.Add(comicsPreview);
                    }
                    i++;
                };
                connection.Close();
                LeftMarginCover = 0;
                TopMarginCover = 0;
                WidthGrid = 0;
            }
        }
        private void LoadDataPopular(Grid Content)
        {
            foreach(ComicsPreview c in list)
            {
                var sqlConect = new SqlConnection(connectionString);
                sqlConect.Open();
                string sq2 = $"Select AVG(STAR) from mark Where IDNAMECOMICS='{c.NameComix.Text}'";
                using (SqlCommand cmd = new SqlCommand(sq2, sqlConect))
                {
                    starAvg = cmd.ExecuteScalar().ToString();
                    cmd.ExecuteNonQuery();
                }
                c.avg.Text = starAvg;
            }
           

                var ordered = from j in list
                          orderby j.avg.Text descending
                          select j;
            List<ComicsPreview> OrderedList = new List<ComicsPreview>();
            foreach (ComicsPreview c in ordered)
            {
                OrderedList.Add(c);
            }
            int g = 0;
            LeftMarginCover = 0;
            foreach (ComicsPreview c in OrderedList)
            {
                if (g < 7)
                {
                    sqlConect = new SqlConnection(connectionString);
                    sqlConect.Open();
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string sql = $"SELECT * FROM COMICSCOVER WHERE COMICSNAME ='{c.NameComix.Text}'";
                        SqlCommand command = new SqlCommand(sql, connection);
                        SqlDataReader reader = command.ExecuteReader();
                        int i = 0;

                        while (reader.Read())
                        {
                            if (i < 3)
                            {
                                nameas = reader.GetValue(0).ToString();
                                name_kategory = reader.GetString(1);
                                data = reader.GetString(2);
                                data2 = reader.GetString(3);
                                DiscriptionComics = reader.GetString(4);
                                name_Rest = reader.GetString(5);
                                ComicsPreview comicsPreview = new ComicsPreview();
                                Iessie iessie = new Iessie(Convert.ToInt32(nameas), name_kategory, data, data2);

                                iessie.Margin = new Thickness(0, 0, 0, 0);
                                iessie.ComicsDiscription.Text = DiscriptionComics;
                                iessie.ComicsName.Text = name_Rest;
                                //iessie.Content.HorizontalAlignment = HorizontalAlignment.Center;
                                comicsPreview.ComicsWalpeper.Source = new BitmapImage(new Uri(catalog + data));
                                comicsPreview.NameComix.Text = name_Rest;
                                comicsPreview.NameGlavaComix.Text = name_kategory;
                                comicsPreview.Width = 320;
                                comicsPreview.Height = 260;
                                comicsPreview.ButtonGrid.Width = 320;
                                comicsPreview.ButtonGrid.Height = 260;
                                #region IESSIE_ADD_BUTTON
                                int buttonMarginLeft = 0,
                                    buttonMarginTop = 0;
                                sqlConect1 = new SqlConnection(connectionString);
                                sqlConect1.Open();
                                var image = new Image { Source = new BitmapImage(new Uri(catalog + data2)) };
                                using (SqlConnection connection1 = new SqlConnection(connectionString))
                                {
                                    connection1.Open();
                                    string sql1 = "SELECT * FROM COMICSCOVERITEM";
                                    SqlCommand command1 = new SqlCommand(sql1, connection1);
                                    SqlDataReader reader1 = command1.ExecuteReader();
                                    while (reader1.Read())
                                    {
                                        string nameqw = reader1.GetValue(1).ToString();
                                        string name_Rest1 = reader1.GetString(2);
                                        GlobalString = name_Rest1;
                                        if (nameqw == nameas)
                                        {
                                            Button button = new Button();

                                            button.Content = name_Rest1;
                                            button.Margin = new Thickness(10, 5, 10, 5);
                                            button.Height = 50;
                                            if (buttonMarginLeft < 4)
                                            {
                                                if (firstbutton)
                                                {
                                                    Grid.SetRow(button, buttonMarginTop);
                                                    Grid.SetColumn(button, buttonMarginLeft);
                                                    WidthGridForbutton++;
                                                    buttonMarginLeft++;
                                                    button.Background = Brushes.DarkRed;
                                                    button.Foreground = Brushes.White;
                                                    firstbutton = false;
                                                    button.Style = (Style)button.FindResource("ButtonComicsIssieStylefirst");
                                                }
                                                else
                                                {
                                                    button.Style = (Style)button.FindResource("ButtonComicsIssieStyle");
                                                    Grid.SetRow(button, buttonMarginTop);
                                                    Grid.SetColumn(button, buttonMarginLeft);
                                                    WidthGridForbutton++;
                                                    buttonMarginLeft++;
                                                }

                                            }
                                            else
                                            {
                                                button.Style = (Style)button.FindResource("ButtonComicsIssieStyle");
                                                WidthGridForbutton = 0;
                                                iessie.ContentForButton.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                                                buttonMarginLeft = 0; buttonMarginTop++;
                                                Grid.SetRow(button, buttonMarginTop);
                                                Grid.SetColumn(button, buttonMarginLeft);
                                                buttonMarginLeft++;

                                            }
                                            button.Click += (sender, e) =>
                                            {
                                                Comics.Content.Children.Clear();
                                                Comics.ContentToIessie.Children.Clear();
                                                Comics.BackgroundIessie.Children.Clear();
                                                Comics.BackgroundIessie.Children.Remove(image);
                                                Comics.Content.Children.Add(Comics.GridAddChild(name_Rest1));
                                                ImagesMarginTop = 0;
                                            };
                                            iessie.ContentForButton.Children.Add(button);
                                        }

                                    };
                                    connection1.Close();
                                }
                                buttonMarginTop = 0; buttonMarginLeft = 0; firstbutton = true;
                                #endregion
                                iessie.backImage.MouseDown += (sen, t) => { Comics.Content.Children.Clear(); Comics.ContentToIessie.Children.Clear(); Comics.BackgroundIessie.Children.Clear(); Comics.Content.Children.Add(new Recomend(Comics)); };
                                comicsPreview.ButtonGrid.MouseDown += (source, e) =>
                                {
                                    Comics.Content.Children.Clear();
                                    Comics.ContentToIessie.Children.Clear();
                                    Comics.ContentToIessie.Children.Add(iessie);
                                    Comics.BackgroundIessie.Children.Add(image);
                                    try
                                    {
                                        var str = NameSignIN.flag;
                                        var sqlConectHistory = new SqlConnection(connectionString);
                                        int sqlrezult;
                                        sqlConectHistory.Open();
                                        string sq1 = string.Format("SELECT COUNT(*) from HISTORY WHERE" + "( IDHISTORYCOMICS=" + "@LOG)");
                                        using (SqlCommand cmd = new SqlCommand(sq1, sqlConectHistory))
                                        {
                                            cmd.Parameters.AddWithValue("@LOG", name_Rest);
                                            sqlrezult = (int)cmd.ExecuteScalar();
                                        }
                                        sqlConect.Close();
                                        if (sqlrezult < 1)
                                        {
                                            var connectHistory = new SqlConnection(connectionString);
                                            connectHistory.Open();
                                            string sqlQ = string.Format("Insert Into HISTORY(IDUSERLOGIN,IDHISTORYCOMICS,IDCOMICSCOVER,IDPARENTCATEGORY,COMICSCOVERWALPAPER,COMICSCOVERWALPAPERTOIESSIE,COMICSDISCRIPTION) Values(@IDUSERLOGIN,@IDHISTORYCOMICS,@IDCOMICSCOVER,@IDPARENTCATEGORY,@COMICSCOVERWALPAPER,@COMICSCOVERWALPAPERTOIESSIE,@COMICSDISCRIPTION)");
                                            using (SqlCommand cmd = new SqlCommand(sqlQ, connectHistory))
                                            {
                                                cmd.Parameters.AddWithValue("@IDUSERLOGIN", str);
                                                cmd.Parameters.AddWithValue("@IDHISTORYCOMICS", name_Rest);
                                                cmd.Parameters.AddWithValue("@IDCOMICSCOVER", nameas);
                                                cmd.Parameters.AddWithValue("@IDPARENTCATEGORY", name_kategory);
                                                cmd.Parameters.AddWithValue("@COMICSCOVERWALPAPER", data);
                                                cmd.Parameters.AddWithValue("@COMICSCOVERWALPAPERTOIESSIE", data2);
                                                cmd.Parameters.AddWithValue("@COMICSDISCRIPTION", DiscriptionComics);
                                                cmd.ExecuteNonQuery();
                                            }
                                            connectHistory.Close();
                                        }
                                    }
                                    catch
                                    {
                                        MessageBox.Show("Error");
                                    }

                                };
                                if (WidthGrid < (int)Content.ActualWidth)
                                {
                                    Content.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                                    Grid.SetColumn(comicsPreview, LeftMarginCover);
                                    LeftMarginCover++;
                                    WidthGrid = LeftMarginCover * 160;
                                }
                                if (WidthGrid >= (int)Content.ActualWidth)
                                {

                                }
                                Content.Children.Add(comicsPreview);
                                
                            }
                            i++;
                        };
                        connection.Close();
                        
                        TopMarginCover = 0;
                        WidthGrid = 0;
                    }
                    g++;
                }
            }
               
        }
        public Recomend(ComicMaster Comic)
        {
            InitializeComponent();
            Comics = new ComicMaster();
            Comics = Comic;
            
        }
       
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDataRecomend(ContentManga, "MANGA");
            LoadDataRecomend(ContentAction, "ACTION");
            LoadDataRecomend(ContentAdventure, "ADVENTURE");
            LoadDataRecomend(ContentComedy, "COMEDY");
            LoadDataRecomend(ContentHoror, "HOROR");
            LoadDataRecomend(ContentMistery, "MISTERY");
            LoadDataRecomend(ContentTragedy, "TRAGEDY");
            LoadDataPopular(ContentPopular);
        }

    }
}
