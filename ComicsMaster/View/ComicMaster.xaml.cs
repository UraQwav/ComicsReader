using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
using System.Windows.Media.Animation;
using System.Threading;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Diagnostics;
using ComicsMaster.Page;

namespace ComicsMaster
{
    public partial class ComicMaster : Window
    {
        #region GlobalParameters
        int ImagesMarginTop = 0,
             WidthGrid = 0,
             WidthGridForbutton = 0,
             LeftMarginCover = 0, 
             TopMarginCover = 0;
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
            one, two,thr,four,five,six,
               catalog = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        double timeOpacity = 0.35;
        Button actionButton = new Button();
        Button adventureButton = new Button();
        Button horrorButton = new Button();
        Button comedyButton = new Button();
        Button demonsButton = new Button();
        Button MisteryButton = new Button();
        Button historicalButton = new Button();
        Button tragedyButton = new Button();
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        #endregion
        #region LoadDataFunction
        public void ButtonMenuCreateAnimation(Border rectangle)
        {
            if (rectangle == RecommendationBackground)
            {
                RecommendationBackground.BeginAnimation(OpacityProperty, new DoubleAnimation(RecommendationBackground.Opacity, 1, TimeSpan.FromSeconds(timeOpacity)));
                LatestUpdateBackground.BeginAnimation(OpacityProperty, new DoubleAnimation(LatestUpdateBackground.Opacity, 0, TimeSpan.FromSeconds(timeOpacity)));
                CategoryBackground.BeginAnimation(OpacityProperty, new DoubleAnimation(CategoryBackground.Opacity, 0, TimeSpan.FromSeconds(timeOpacity)));
                HistoryBackground.BeginAnimation(OpacityProperty, new DoubleAnimation(HistoryBackground.Opacity, 0, TimeSpan.FromSeconds(timeOpacity)));
                FavouritesBackground.BeginAnimation(OpacityProperty, new DoubleAnimation(FavouritesBackground.Opacity, 0, TimeSpan.FromSeconds(timeOpacity)));
                BrowserBackground.BeginAnimation(OpacityProperty, new DoubleAnimation(BrowserBackground.Opacity, 0, TimeSpan.FromSeconds(timeOpacity)));
                addBackground.BeginAnimation(OpacityProperty, new DoubleAnimation(addBackground.Opacity, 0, TimeSpan.FromSeconds(timeOpacity)));
            }
            if (rectangle == LatestUpdateBackground)
            {
                RecommendationBackground.BeginAnimation(OpacityProperty, new DoubleAnimation(RecommendationBackground.Opacity, 0, TimeSpan.FromSeconds(timeOpacity)));
                LatestUpdateBackground.BeginAnimation(OpacityProperty, new DoubleAnimation(LatestUpdateBackground.Opacity, 1, TimeSpan.FromSeconds(timeOpacity)));
                CategoryBackground.BeginAnimation(OpacityProperty, new DoubleAnimation(CategoryBackground.Opacity, 0, TimeSpan.FromSeconds(timeOpacity)));
                HistoryBackground.BeginAnimation(OpacityProperty, new DoubleAnimation(HistoryBackground.Opacity, 0, TimeSpan.FromSeconds(timeOpacity)));
                FavouritesBackground.BeginAnimation(OpacityProperty, new DoubleAnimation(FavouritesBackground.Opacity, 0, TimeSpan.FromSeconds(timeOpacity)));
                BrowserBackground.BeginAnimation(OpacityProperty, new DoubleAnimation(BrowserBackground.Opacity, 0, TimeSpan.FromSeconds(timeOpacity)));
                addBackground.BeginAnimation(OpacityProperty, new DoubleAnimation(addBackground.Opacity, 0, TimeSpan.FromSeconds(timeOpacity)));
            }
            if (rectangle == CategoryBackground)
            {
                RecommendationBackground.BeginAnimation(OpacityProperty, new DoubleAnimation(RecommendationBackground.Opacity, 0, TimeSpan.FromSeconds(timeOpacity)));
                LatestUpdateBackground.BeginAnimation(OpacityProperty, new DoubleAnimation(LatestUpdateBackground.Opacity, 0, TimeSpan.FromSeconds(timeOpacity)));
                CategoryBackground.BeginAnimation(OpacityProperty, new DoubleAnimation(CategoryBackground.Opacity, 1, TimeSpan.FromSeconds(timeOpacity)));
                HistoryBackground.BeginAnimation(OpacityProperty, new DoubleAnimation(HistoryBackground.Opacity, 0, TimeSpan.FromSeconds(timeOpacity)));
                FavouritesBackground.BeginAnimation(OpacityProperty, new DoubleAnimation(FavouritesBackground.Opacity, 0, TimeSpan.FromSeconds(timeOpacity)));
                BrowserBackground.BeginAnimation(OpacityProperty, new DoubleAnimation(BrowserBackground.Opacity, 0, TimeSpan.FromSeconds(timeOpacity)));
                addBackground.BeginAnimation(OpacityProperty, new DoubleAnimation(addBackground.Opacity, 0, TimeSpan.FromSeconds(timeOpacity)));
            }
            if (rectangle == HistoryBackground)
            {
                RecommendationBackground.BeginAnimation(OpacityProperty, new DoubleAnimation(RecommendationBackground.Opacity, 0, TimeSpan.FromSeconds(timeOpacity)));
                LatestUpdateBackground.BeginAnimation(OpacityProperty, new DoubleAnimation(LatestUpdateBackground.Opacity, 0, TimeSpan.FromSeconds(timeOpacity)));
                CategoryBackground.BeginAnimation(OpacityProperty, new DoubleAnimation(CategoryBackground.Opacity, 0, TimeSpan.FromSeconds(timeOpacity)));
                HistoryBackground.BeginAnimation(OpacityProperty, new DoubleAnimation(HistoryBackground.Opacity, 1, TimeSpan.FromSeconds(timeOpacity)));
                FavouritesBackground.BeginAnimation(OpacityProperty, new DoubleAnimation(FavouritesBackground.Opacity, 0, TimeSpan.FromSeconds(timeOpacity)));
                BrowserBackground.BeginAnimation(OpacityProperty, new DoubleAnimation(BrowserBackground.Opacity, 0, TimeSpan.FromSeconds(timeOpacity)));
                addBackground.BeginAnimation(OpacityProperty, new DoubleAnimation(addBackground.Opacity, 0, TimeSpan.FromSeconds(timeOpacity)));
            }
            if (rectangle == FavouritesBackground)
            {
                RecommendationBackground.BeginAnimation(OpacityProperty, new DoubleAnimation(RecommendationBackground.Opacity, 0, TimeSpan.FromSeconds(timeOpacity)));
                LatestUpdateBackground.BeginAnimation(OpacityProperty, new DoubleAnimation(LatestUpdateBackground.Opacity, 0, TimeSpan.FromSeconds(timeOpacity)));
                CategoryBackground.BeginAnimation(OpacityProperty, new DoubleAnimation(CategoryBackground.Opacity, 0, TimeSpan.FromSeconds(timeOpacity)));
                HistoryBackground.BeginAnimation(OpacityProperty, new DoubleAnimation(HistoryBackground.Opacity, 0, TimeSpan.FromSeconds(timeOpacity)));
                FavouritesBackground.BeginAnimation(OpacityProperty, new DoubleAnimation(HistoryBackground.Opacity, 1, TimeSpan.FromSeconds(timeOpacity)));
                BrowserBackground.BeginAnimation(OpacityProperty, new DoubleAnimation(BrowserBackground.Opacity, 0, TimeSpan.FromSeconds(timeOpacity)));
                addBackground.BeginAnimation(OpacityProperty, new DoubleAnimation(addBackground.Opacity, 0, TimeSpan.FromSeconds(timeOpacity)));
            }
            if (rectangle == BrowserBackground)
            {
                RecommendationBackground.BeginAnimation(OpacityProperty, new DoubleAnimation(RecommendationBackground.Opacity, 0, TimeSpan.FromSeconds(timeOpacity)));
                LatestUpdateBackground.BeginAnimation(OpacityProperty, new DoubleAnimation(LatestUpdateBackground.Opacity, 0, TimeSpan.FromSeconds(timeOpacity)));
                CategoryBackground.BeginAnimation(OpacityProperty, new DoubleAnimation(CategoryBackground.Opacity, 0, TimeSpan.FromSeconds(timeOpacity)));
                HistoryBackground.BeginAnimation(OpacityProperty, new DoubleAnimation(HistoryBackground.Opacity, 0, TimeSpan.FromSeconds(timeOpacity)));
                FavouritesBackground.BeginAnimation(OpacityProperty, new DoubleAnimation(FavouritesBackground.Opacity, 0, TimeSpan.FromSeconds(timeOpacity)));
                BrowserBackground.BeginAnimation(OpacityProperty, new DoubleAnimation(BrowserBackground.Opacity, 1, TimeSpan.FromSeconds(timeOpacity)));
                addBackground.BeginAnimation(OpacityProperty, new DoubleAnimation(addBackground.Opacity, 0, TimeSpan.FromSeconds(timeOpacity)));
            }
            if (rectangle == addBackground)
            {
                RecommendationBackground.BeginAnimation(OpacityProperty, new DoubleAnimation(RecommendationBackground.Opacity, 0, TimeSpan.FromSeconds(timeOpacity)));
                LatestUpdateBackground.BeginAnimation(OpacityProperty, new DoubleAnimation(LatestUpdateBackground.Opacity, 0, TimeSpan.FromSeconds(timeOpacity)));
                CategoryBackground.BeginAnimation(OpacityProperty, new DoubleAnimation(CategoryBackground.Opacity, 0, TimeSpan.FromSeconds(timeOpacity)));
                HistoryBackground.BeginAnimation(OpacityProperty, new DoubleAnimation(HistoryBackground.Opacity, 0, TimeSpan.FromSeconds(timeOpacity)));
                FavouritesBackground.BeginAnimation(OpacityProperty, new DoubleAnimation(FavouritesBackground.Opacity, 0, TimeSpan.FromSeconds(timeOpacity)));
                BrowserBackground.BeginAnimation(OpacityProperty, new DoubleAnimation(BrowserBackground.Opacity, 0, TimeSpan.FromSeconds(timeOpacity)));
                addBackground.BeginAnimation(OpacityProperty, new DoubleAnimation(addBackground.Opacity, 1, TimeSpan.FromSeconds(timeOpacity)));
            }
        }
        public void CreateButtonCategory(Button button, string buttonContent)
        {
            button.Style= (Style)button.FindResource("ButtonSignStyle");
            button.VerticalAlignment = VerticalAlignment.Top;
            button.HorizontalAlignment = HorizontalAlignment.Left;
            button.Width = 100;
            button.Height = 30;
            button.Content = buttonContent;
            button.Click += (source, t) => { Content.Children.Clear(); LoadDataCategory(button).GetAwaiter();};
        }
        public System.Windows.UIElement GridAddChild(string name_Rest1)
        {
            Grid ContentGridChildren = new Grid();
            ContentGridChildren.Margin = new Thickness(1, 1, 1, 1);
            sqlConect2 = new SqlConnection(connectionString);
            sqlConect2.Open();
            using (SqlConnection connection2 = new SqlConnection(connectionString))
            {
                connection2.Open();
                string sql2 = $"SELECT * FROM IMAGESITEMS WHERE IDPARENTCOMICSITEM = '{ name_Rest1 }'";
                SqlCommand command2 = new SqlCommand(sql2, connection2);
                SqlDataReader reader2 = command2.ExecuteReader();
                int Row = 0;
                ContentGridChildren.RowDefinitions.Clear();
                while (reader2.Read())
                {
                    try
                    {
                        ContentGridChildren.RowDefinitions.Add(new RowDefinition { Height =GridLength.Auto});
                        string data2 = reader2.GetString(2);
                        Image imageSS = new Image();
                        imageSS.Source = new BitmapImage(new Uri("pack://siteoforigin:,,," + data2, UriKind.RelativeOrAbsolute));
                        imageSS.VerticalAlignment = VerticalAlignment.Top;
                        imageSS.Margin = new Thickness(0, 0, 0, 20);
                        Grid.SetRow(imageSS,Row);
                        ContentGridChildren.Children.Add(imageSS);
                        Row++;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                };
                connection2.Close();
                ComicsItemsDo = true;
                return ContentGridChildren;
            }
        }
        #endregion
        #region CLASS
        public class InternetChecker
        {
            [System.Runtime.InteropServices.DllImport("wininet.dll")]
            private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);
            public static bool IsConnectedToInternet()
            {
                int Desc;
                return InternetGetConnectedState(out Desc, 0);
            }
        }
        #endregion
        #region LoadData
        private async Task LoadDataLatestUpdate()
        {
            Loader loader = new Loader();
            Canvas.SetZIndex(loader, (int)98);
            loader.Margin = new Thickness(0, 0, -40, 0);
            Content.Children.Add(loader);
            sqlConect = new SqlConnection(connectionString);
            await sqlConect.OpenAsync();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM COMICSCOVER";
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader = await command.ExecuteReaderAsync();
                while (reader.Read())
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
                    comicsPreview.ComicsWalpeper.Source = new BitmapImage(new Uri(catalog+data));
                    comicsPreview.NameComix.Text = name_Rest;
                    comicsPreview.NameGlavaComix.Text = name_kategory;
                    #region IESSIE_ADD_BUTTON
                    int buttonMarginLeft = 0,
                        buttonMarginTop = 0;
                    sqlConect1 = new SqlConnection(connectionString);
                    await sqlConect1.OpenAsync();
                    var image = new Image { Source = new BitmapImage(new Uri(catalog + data2)) };
                    using (SqlConnection connection1 = new SqlConnection(connectionString))
                    {
                        connection1.Open();
                        string sql1 = "SELECT * FROM COMICSCOVERITEM";
                        SqlCommand command1 = new SqlCommand(sql1, connection1);
                        SqlDataReader reader1 = await command1.ExecuteReaderAsync();
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
                                    if (firstbutton) {
                                        Grid.SetRow(button, buttonMarginTop);
                                        Grid.SetColumn(button, buttonMarginLeft);
                                        WidthGridForbutton++;
                                        buttonMarginLeft++;
                                        button.Background = Brushes.DarkRed;
                                        button.Foreground = Brushes.White;
                                        firstbutton = false;
                                        button.Style = (Style)button.FindResource("ButtonComicsIssieStylefirst");
                                    }
                                    else {
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
                                    Content.Children.Clear();
                                    ContentToIessie.Children.Clear();
                                    BackgroundIessie.Children.Clear();
                                    Loader loaderItems = new Loader();
                                    Canvas.SetZIndex(loaderItems, (int)98);
                                    Content.Children.Add(loaderItems);
                                    BackgroundIessie.Children.Remove(image);
                                    Content.Children.Add(GridAddChild(name_Rest1));
                                    Content.Children.Remove(loaderItems);
                                    ImagesMarginTop = 0;
                                };
                                iessie.ContentForButton.Children.Add(button);
                            }
                        };
                        connection1.Close();
                    }
                    buttonMarginTop = 0; buttonMarginLeft = 0; firstbutton = true;
                    #endregion
                    iessie.backImage.MouseDown += (sen, t) => { Content.Children.Clear(); ContentToIessie.Children.Clear(); BackgroundIessie.Children.Clear(); LoadDataLatestUpdate().GetAwaiter(); };
                    comicsPreview.ButtonGrid.MouseDown += (source, e) => {
                        Content.Children.Clear();
                        ContentToIessie.Children.Clear();
                        ContentToIessie.Children.Add(iessie);
                        BackgroundIessie.Children.Add(image);
         
                    };
                    if (WidthGrid < (int)Content.ActualWidth)
                    {
                        comicsPreview.Margin = new Thickness(LeftMarginCover * 184, 320 * TopMarginCover, 0, 0);
                        LeftMarginCover++;
                        WidthGrid = LeftMarginCover * 184;
                    }
                    if (WidthGrid >= (int)Content.ActualWidth)
                    {
                        LeftMarginCover = 0; TopMarginCover++;
                        comicsPreview.Margin = new Thickness(LeftMarginCover * 184, 320 * TopMarginCover, 0, 0);
                        LeftMarginCover++;
                        WidthGrid = 0;
                    }
                    Content.Children.Add(comicsPreview);
                };
                connection.Close();
                LeftMarginCover = 0;
                TopMarginCover = 0;
                WidthGrid = 0;
            }
            Content.Children.Remove(loader);
        }
        private async Task LoadDataHistory()
        {
            Loader loader = new Loader();
            Canvas.SetZIndex(loader, (int)98);
            loader.Margin = new Thickness(0, 0, -40, 0);
            Content.Children.Add(loader);
            sqlConect = new SqlConnection(connectionString);
            await sqlConect.OpenAsync();
            var strLogin = NameSignIN.flag;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = $"SELECT * FROM HISTORY WHERE IDUSERLOGIN='{strLogin}'";
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    string name_Rest = reader.GetString(2);
                    string nameas = reader.GetValue(3).ToString();
                    string name_kategory = reader.GetString(4);
                    string data = reader.GetString(5);
                    string data2 = reader.GetString(6);
                    try { string DiscriptionComics = reader.GetString(7); }
                    catch { string DiscriptionComics = "It's very funny commics and I recommend you read it"; }
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
                    await sqlConect1.OpenAsync();
                    var image = new Image { Source = new BitmapImage(new Uri(catalog + data2)) };
                    using (SqlConnection connection1 = new SqlConnection(connectionString))
                    {
                        connection1.Open();
                        string sql1 = "SELECT * FROM COMICSCOVERITEM";
                        SqlCommand command1 = new SqlCommand(sql1, connection1);
                        SqlDataReader reader1 = await command1.ExecuteReaderAsync();
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
                                    Content.Children.Clear();
                                    ContentToIessie.Children.Clear();
                                    BackgroundIessie.Children.Clear();
                                    Loader loaderItems = new Loader();
                                    Canvas.SetZIndex(loaderItems, (int)98);
                                    Content.Children.Add(loaderItems);
                                    BackgroundIessie.Children.Remove(image);
                                    Content.Children.Add(GridAddChild(name_Rest1));
                                    Content.Children.Remove(loaderItems);
                                    ImagesMarginTop = 0;
                                };
                                iessie.ContentForButton.Children.Add(button);
                            }
                        };
                        connection1.Close();
                    }
                    buttonMarginTop = 0; buttonMarginLeft = 0; firstbutton = true;
                    #endregion
                    iessie.backImage.MouseDown += (sen, t) => { Content.Children.Clear(); ContentToIessie.Children.Clear(); BackgroundIessie.Children.Clear(); LoadDataHistory().GetAwaiter(); };
                    comicsPreview.ButtonGrid.MouseDown += (source, e) => {
                        Content.Children.Clear();
                        ContentToIessie.Children.Clear();
                        ContentToIessie.Children.Add(iessie);
                        BackgroundIessie.Children.Add(image);
                        
                    };
                    if (WidthGrid < (int)Content.ActualWidth)
                    {
                        comicsPreview.Margin = new Thickness(LeftMarginCover * 184, 320 * TopMarginCover, 0, 0);
                        LeftMarginCover++;
                        WidthGrid = LeftMarginCover * 184;
                    }
                    if (WidthGrid >= (int)Content.ActualWidth)
                    {
                        LeftMarginCover = 0; TopMarginCover++;
                        comicsPreview.Margin = new Thickness(LeftMarginCover * 184, 320 * TopMarginCover, 0, 0);
                        LeftMarginCover++;
                        WidthGrid = 0;
                    }
                    Content.Children.Add(comicsPreview);
                };
                connection.Close();
                LeftMarginCover = 0;
                TopMarginCover = 0;
                WidthGrid = 0;
            }
            Content.Children.Remove(loader);
        }
        private async Task LoadDataCategory(Button buttonCategory)
        {
            Loader loader = new Loader();
            Canvas.SetZIndex(loader, (int)98);
            loader.Margin = new Thickness(0, 0, -40, 0);
            Content.Children.Add(loader);
            sqlConect = new SqlConnection(connectionString);
            await sqlConect.OpenAsync();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = $"SELECT * FROM COMICSCOVER WHERE IDPARENTCATEGORY='{buttonCategory.Content.ToString()}'";
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader = await command.ExecuteReaderAsync();
                while (reader.Read())
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
                    await sqlConect1.OpenAsync();
                    var image = new Image { Source = new BitmapImage(new Uri(catalog + data2)) };
                    using (SqlConnection connection1 = new SqlConnection(connectionString))
                    {
                        connection1.Open();
                        string sql1 = "SELECT * FROM COMICSCOVERITEM";
                        SqlCommand command1 = new SqlCommand(sql1, connection1);
                        SqlDataReader reader1 = await command1.ExecuteReaderAsync();
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
                                    Content.Children.Clear();
                                    ContentToIessie.Children.Clear();
                                    BackgroundIessie.Children.Clear();
                                    Loader loaderItems = new Loader();
                                    Canvas.SetZIndex(loaderItems, (int)98);
                                    Content.Children.Add(loaderItems);
                                    BackgroundIessie.Children.Remove(image);
                                    Content.Children.Add(GridAddChild(name_Rest1));
                                    Content.Children.Remove(loaderItems);
                                    ImagesMarginTop = 0;
                                };
                                iessie.ContentForButton.Children.Add(button);
                            }
                        };
                        connection1.Close();
                    }
                    buttonMarginTop = 0; buttonMarginLeft = 0; firstbutton = true;
                    #endregion
                    iessie.backImage.MouseDown += (sen, t) => { Content.Children.Clear();
                                                                ContentToIessie.Children.Clear();
                                                                BackgroundIessie.Children.Clear();
                                                                #region ACTIONBUTTON
                                                                CreateButtonCategory(actionButton, "ACTION");
                                                                actionButton.Margin = new Thickness(250, 50, 0, 0);
                                                                Content.Children.Add(actionButton);
                                                                #endregion
                                                                #region HORRORBUTTON
                                                                CreateButtonCategory(horrorButton, "HOROR");
                                                                horrorButton.Margin = new Thickness(500, 50, 0, 0);
                                                                Content.Children.Add(horrorButton);
                                                                #endregion
                                                                #region DEMONSBUTTON
                                                                CreateButtonCategory(demonsButton, "DEMONS");
                                                                demonsButton.Margin = new Thickness(250, 100, 0, 0);
                                                                Content.Children.Add(demonsButton);
                                                                tragedyButton.Style = (Style)tragedyButton.FindResource("ButtonSignStyle");
                                                                #endregion
                                                                #region TRAGEDYBUTTON
                                                                CreateButtonCategory(tragedyButton, "TRAGEDY");
                                                                tragedyButton.Margin = new Thickness(500, 100, 0, 0);
                                                                Content.Children.Add(tragedyButton);
                                                                #endregion
                                                                #region MISTERYBUTTON
                                                                CreateButtonCategory(MisteryButton, "MISTERY");
                                                                MisteryButton.Margin = new Thickness(250, 150, 0, 0);
                                                                Content.Children.Add(MisteryButton);
                                                                #endregion
                                                                #region ADVENTUREBUTTON
                                                                CreateButtonCategory(adventureButton, "ADVENTURE");
                                                                adventureButton.Margin = new Thickness(500, 150, 0, 0);
                                                                Content.Children.Add(adventureButton);
                                                                #endregion
                                                                #region HISTORICALBUTTON
                                                                CreateButtonCategory(historicalButton, "HISTORY");
                                                                historicalButton.Margin = new Thickness(250, 200, 0, 0);
                                                                Content.Children.Add(historicalButton);
                                                                #endregion
                                                                #region COMEDYBUTTON
                                                                CreateButtonCategory(comedyButton, "COMEDY");
                                                                comedyButton.Margin = new Thickness(500, 200, 0, 0);
                                                                Content.Children.Add(comedyButton);
                                                                #endregion
                                                               };
                    comicsPreview.ButtonGrid.MouseDown += (source, e) => {
                    Content.Children.Clear();
                    ContentToIessie.Children.Clear();
                    ContentToIessie.Children.Add(iessie);
                    BackgroundIessie.Children.Add(image);
                    

                    };
                    if (WidthGrid < (int)Content.ActualWidth)
                    {
                        comicsPreview.Margin = new Thickness(LeftMarginCover * 184, 320 * TopMarginCover, 0, 0);
                        LeftMarginCover++;
                        WidthGrid = LeftMarginCover * 184;
                    }
                    if (WidthGrid >= (int)Content.ActualWidth)
                    {
                        LeftMarginCover = 0; TopMarginCover++;
                        comicsPreview.Margin = new Thickness(LeftMarginCover * 184, 320 * TopMarginCover, 0, 0);
                        LeftMarginCover++;
                        WidthGrid = 0;
                    }
                    Content.Children.Add(comicsPreview);
                };
                connection.Close();
                LeftMarginCover = 0;
                TopMarginCover = 0;
                WidthGrid = 0;
            }
            Content.Children.Remove(loader);
        }
        private async Task LoadDataFavourite()
        {
            Loader loader = new Loader();
            Canvas.SetZIndex(loader, (int)98);
            loader.Margin = new Thickness(0, 0, -40, 0);
            Content.Children.Add(loader);
            sqlConect = new SqlConnection(connectionString);
            await sqlConect.OpenAsync();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = $"SELECT * FROM FAVOURITES WHERE IDUSERLOGIN='{NameSignIN.flag}'";
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                   
                    nameas = reader.GetValue(3).ToString();
                    name_kategory = reader.GetString(4);
                    data = reader.GetString(5);
                    data2 = reader.GetString(6);
                    DiscriptionComics = reader.GetString(7);
                    name_Rest = reader.GetString(2);
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
                    await sqlConect1.OpenAsync();
                    var image = new Image { Source = new BitmapImage(new Uri(catalog + data2)) };
                    using (SqlConnection connection1 = new SqlConnection(connectionString))
                    {
                        connection1.Open();
                        string sql1 = "SELECT * FROM COMICSCOVERITEM";
                        SqlCommand command1 = new SqlCommand(sql1, connection1);
                        SqlDataReader reader1 = await command1.ExecuteReaderAsync();
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
                                    Content.Children.Clear();
                                    ContentToIessie.Children.Clear();
                                    BackgroundIessie.Children.Clear();
                                    Loader loaderItems = new Loader();
                                    Canvas.SetZIndex(loaderItems, (int)98);
                                    Content.Children.Add(loaderItems);
                                    BackgroundIessie.Children.Remove(image);
                                    Content.Children.Add(GridAddChild(name_Rest1));
                                    Content.Children.Remove(loaderItems);
                                    ImagesMarginTop = 0;
                                };
                                iessie.ContentForButton.Children.Add(button);
                            }
                        };
                        connection1.Close();
                    }
                    buttonMarginTop = 0; buttonMarginLeft = 0; firstbutton = true;
                    #endregion
                    iessie.backImage.MouseDown += (sen, t) => { Content.Children.Clear(); ContentToIessie.Children.Clear(); BackgroundIessie.Children.Clear(); LoadDataFavourite().GetAwaiter(); };
                    comicsPreview.ButtonGrid.MouseDown += (source, e) => {
                        Content.Children.Clear();
                        ContentToIessie.Children.Clear();
                        ContentToIessie.Children.Add(iessie);
                        BackgroundIessie.Children.Add(image);
                        

                    };
                    if (WidthGrid < (int)Content.ActualWidth)
                    {
                        comicsPreview.Margin = new Thickness(LeftMarginCover * 184, 320 * TopMarginCover, 0, 0);
                        LeftMarginCover++;
                        WidthGrid = LeftMarginCover * 184;
                    }
                    if (WidthGrid >= (int)Content.ActualWidth)
                    {
                        LeftMarginCover = 0; TopMarginCover++;
                        comicsPreview.Margin = new Thickness(LeftMarginCover * 184, 320 * TopMarginCover, 0, 0);
                        LeftMarginCover++;
                        WidthGrid = 0;
                    }
                    Content.Children.Add(comicsPreview);
                };
                connection.Close();
                LeftMarginCover = 0;
                TopMarginCover = 0;
                WidthGrid = 0;
            }
            Content.Children.Remove(loader);
        }
        #endregion

        public ComicMaster()
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Content.Children.Add(new Recomend(this));
            var str = NameSignIN.flag;
            if (str == "ADMIN")
            {
                addButton.Height = 40;
            }
            BurgerRecomendR.BeginAnimation(Rectangle.WidthProperty, new DoubleAnimation(BurgerRecomendR.ActualWidth, BurgerRecomend.ActualWidth-10, TimeSpan.FromSeconds(timeOpacity)));
            try { ImageN1.Source = new BitmapImage(new Uri(catalog+@"\Images\icons8-gas-48 (1).png")); } catch { };
        }
        private void Burger_MouseEnter(object sender, MouseEventArgs e)
        {

        }
        private void BurgerRecomend_MouseEnter(object sender, MouseEventArgs e)=> BurgerRecomend.Background = new SolidColorBrush(Color.FromRgb(220,220,220));
        private void BurgerUpdate_MouseEnter(object sender, MouseEventArgs e) => BurgerUpdate.Background = new SolidColorBrush(Color.FromRgb(220, 220, 220));
        private void BurgerFavourite_MouseEnter(object sender, MouseEventArgs e) => BurgerFavourite.Background = new SolidColorBrush(Color.FromRgb(220, 220, 220));
        private void BurgerCategory_MouseEnter(object sender, MouseEventArgs e) => BurgerCategory.Background = new SolidColorBrush(Color.FromRgb(220, 220, 220));
        private void BurgerRecomend_MouseDown(object sender, MouseButtonEventArgs e)
        {
            #region animation
            BurgerRecomendR.BeginAnimation(WidthProperty, new DoubleAnimation(BurgerRecomendR.ActualWidth, BurgerRecomend.ActualWidth-10, TimeSpan.FromSeconds(timeOpacity)));
            BurgerUpdateR.BeginAnimation(WidthProperty, new DoubleAnimation(BurgerUpdateR.ActualWidth, 0, TimeSpan.FromSeconds(timeOpacity)));
            BurgerFavouriteR.BeginAnimation(WidthProperty, new DoubleAnimation(BurgerFavouriteR.ActualWidth, 0, TimeSpan.FromSeconds(timeOpacity)));
            BurgerCategoryR.BeginAnimation(WidthProperty, new DoubleAnimation(BurgerCategoryR.ActualWidth, 0, TimeSpan.FromSeconds(timeOpacity)));
            try
            {
                ImageN1.Source = new BitmapImage(new Uri(catalog+@"\Images\icons8-gas-48 (1).png"));
                ImageN2.Source = new BitmapImage(new Uri(catalog + @"\Images\icons8-update-file-48.png"));
                ImageN4.Source = new BitmapImage(new Uri(catalog+@"\Images\icons8-sorting-52 (1).png"));
                ImageN3.Source = new BitmapImage(new Uri(catalog+@"\Images\icons8-favorite-folder-filled-50 (1).png"));
            }
            catch { }
            #endregion
            Content.Children.Clear();
            ContentToIessie.Children.Clear();
            BackgroundIessie.Children.Clear();
            //LoadDataRecomend().GetAwaiter();
            Content.Children.Add(new Recomend(this));
        }
        private void BurgerUpdate_MouseDown(object sender, MouseButtonEventArgs e)
        {
            #region animation
            BurgerRecomendR.BeginAnimation(WidthProperty, new DoubleAnimation(BurgerRecomendR.ActualWidth, 0, TimeSpan.FromSeconds(timeOpacity)));
            BurgerUpdateR.BeginAnimation(WidthProperty, new DoubleAnimation(BurgerUpdateR.ActualWidth, BurgerUpdate.ActualWidth-10, TimeSpan.FromSeconds(timeOpacity)));
            BurgerFavouriteR.BeginAnimation(WidthProperty, new DoubleAnimation(BurgerFavouriteR.ActualWidth, 0, TimeSpan.FromSeconds(timeOpacity)));
            BurgerCategoryR.BeginAnimation(WidthProperty, new DoubleAnimation(BurgerCategoryR.ActualWidth, 0, TimeSpan.FromSeconds(timeOpacity)));
            try
            {
                ImageN1.Source = new BitmapImage(new Uri(catalog + @"\Images\icons8-gas-48.png"));
                ImageN4.Source = new BitmapImage(new Uri(catalog+@"\Images\icons8-sorting-52 (1).png"));
                ImageN3.Source = new BitmapImage(new Uri(catalog+@"\Images\icons8-favorite-folder-filled-50 (1).png"));
                ImageN2.Source = new BitmapImage(new Uri(catalog+@"\Images\icons8-update-file-48 (1).png"));
            }
            catch { }
            #endregion
            Content.Children.Clear();
            ContentToIessie.Children.Clear();
            BackgroundIessie.Children.Clear();
            LoadDataLatestUpdate().GetAwaiter();
        }
        private void BurgerFavourite_MouseDown(object sender, MouseButtonEventArgs e)
        {
            #region animation
            BurgerRecomendR.BeginAnimation(WidthProperty, new DoubleAnimation(BurgerRecomendR.ActualWidth, 0, TimeSpan.FromSeconds(timeOpacity)));
            BurgerUpdateR.BeginAnimation(WidthProperty, new DoubleAnimation(BurgerUpdateR.ActualWidth, 0, TimeSpan.FromSeconds(timeOpacity)));
            BurgerFavouriteR.BeginAnimation(WidthProperty, new DoubleAnimation(BurgerFavouriteR.ActualWidth, BurgerFavourite.ActualWidth-10, TimeSpan.FromSeconds(timeOpacity)));
            BurgerCategoryR.BeginAnimation(WidthProperty, new DoubleAnimation(BurgerCategoryR.ActualWidth, 0, TimeSpan.FromSeconds(timeOpacity)));
            try
            {
                ImageN2.Source = new BitmapImage(new Uri(catalog+@"\Images\icons8-update-file-48.png"));
                ImageN1.Source = new BitmapImage(new Uri(catalog+@"\Images\icons8-gas-48.png"));
                ImageN4.Source = new BitmapImage(new Uri(catalog+@"\Images\icons8-sorting-52 (1).png"));
                ImageN3.Source = new BitmapImage(new Uri(catalog + @"\Images\icons8-favorite-folder-filled-50 (2).png"));
            }
            catch { }
            #endregion
            Content.Children.Clear();
            ContentToIessie.Children.Clear();
            BackgroundIessie.Children.Clear();
            LoadDataFavourite().GetAwaiter();
        }
        private void BurgerCategory_MouseDown(object sender, MouseButtonEventArgs e)
        {
            #region animation
            DoubleAnimation recollor1 = new DoubleAnimation();
            recollor1.From = BurgerRecomendR.ActualWidth;
            recollor1.To = 0;
            recollor1.Duration = TimeSpan.FromSeconds(timeOpacity);
            BurgerRecomendR.BeginAnimation(Rectangle.WidthProperty, recollor1);
            DoubleAnimation recollor2 = new DoubleAnimation();
            recollor2.From = BurgerUpdateR.ActualWidth;
            recollor2.To = 0;
            recollor2.Duration = TimeSpan.FromSeconds(timeOpacity);
            BurgerUpdateR.BeginAnimation(Rectangle.WidthProperty, recollor2);
            DoubleAnimation recollor3 = new DoubleAnimation();
            recollor3.From = BurgerFavouriteR.ActualWidth;
            recollor3.To = 0;
            recollor3.Duration = TimeSpan.FromSeconds(timeOpacity);
            BurgerFavouriteR.BeginAnimation(Rectangle.WidthProperty, recollor3);
            DoubleAnimation recollor4 = new DoubleAnimation();
            recollor4.From = BurgerCategoryR.ActualWidth;
            recollor4.To = 70;
            recollor4.Duration = TimeSpan.FromSeconds(timeOpacity);
            BurgerCategoryR.BeginAnimation(Rectangle.WidthProperty, recollor4);
            ImageN2.Source = new BitmapImage(new Uri(catalog + @"\Images\icons8-update-file-48.png"));
            ImageN1.Source = new BitmapImage(new Uri(catalog+@"\Images\icons8-gas-48.png"));
            ImageN3.Source = new BitmapImage(new Uri(catalog+@"\Images\icons8-favorite-folder-filled-50 (1).png"));

            ImageN4.Source = new BitmapImage(new Uri(catalog+@"\Images\icons8-sorting-52 (2).png"));
            #endregion
        }
        private void Burger_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DoubleAnimation recollor4 = new DoubleAnimation();
            recollor4.From = FullMenu.ActualWidth;
            recollor4.To = ParentGrid.ActualWidth;
            recollor4.Duration = TimeSpan.FromSeconds(timeOpacity+0.4);
            FullMenu.BeginAnimation(Grid.WidthProperty, recollor4);
            BlackFill.Visibility = Visibility.Visible;
            DoubleAnimation recollor1 = new DoubleAnimation();
            recollor1.From = BlackFill.Opacity;
            recollor1.To = 1;
            recollor1.Duration = TimeSpan.FromSeconds(timeOpacity+0.4);
            BlackFill.BeginAnimation(Border.OpacityProperty, recollor1);
        }
        private void BurgerRecomend_MouseLeave(object sender, MouseEventArgs e)
        {
            BurgerCategory.Background = null;
            BurgerFavourite.Background = null;
            BurgerUpdate.Background = null;
            BurgerRecomend.Background = null;
        }
        private void BlackFill_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
            DoubleAnimation recollor1 = new DoubleAnimation();
            recollor1.From = BlackFill.Opacity;
            recollor1.To = 0;
            recollor1.Duration = TimeSpan.FromSeconds(timeOpacity+0.4);
            BlackFill.BeginAnimation(Border.OpacityProperty, recollor1);
            BlackFill.Visibility = Visibility.Hidden;
            DoubleAnimation recollor4 = new DoubleAnimation();
            recollor4.From = FullMenu.ActualWidth;
            recollor4.To = 0;
            recollor4.Duration = TimeSpan.FromSeconds(timeOpacity+0.4);
            FullMenu.BeginAnimation(Grid.WidthProperty, recollor4);

        }
        private void PlayAudio_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (InternetChecker.IsConnectedToInternet())
            {
                try
                {

                    var document = WebPlayer.Document as mshtml.HTMLDocument;
                    var inputs = document.getElementsByTagName("button");
                    foreach (mshtml.IHTMLElement element in inputs)
                    {
                        if (element.getAttribute("className") == "ytp-play-button ytp-button")
                        {
                            element.click();
                        }
                    }
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.ToString());
                }
            }
            else
            {
                MessageBox.Show("You are not connet to Internet");
            }
        }
        private void WebPlayer_LoadCompleted(object sender, NavigationEventArgs e)
        {
          
        }
        private void VolumAudio_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var document = WebPlayer.Document as mshtml.HTMLDocument;
                var inputs = document.getElementsByTagName("button");
                foreach (mshtml.IHTMLElement element in inputs)
                {
                    if (element.getAttribute("className") == "ytp-mute-button ytp-button")
                    {
                        element.click();
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }
        }
        private void NextAudio_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var document = WebPlayer.Document as mshtml.HTMLDocument;
                var inputs = document.getElementsByTagName("a");
                foreach (mshtml.IHTMLElement element in inputs)
                {
                    if (element.getAttribute("className") == "ytp-next-button ytp-button")
                    {
                        element.click();
                    }
                }
            }
            catch(Exception exp)
            {
                MessageBox.Show(exp.ToString());
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
        private void Button_Click(object sender, RoutedEventArgs e)=>System.Windows.Application.Current.Shutdown();
        private void Button_Click_1(object sender, RoutedEventArgs e)=>this.WindowState = WindowState.Minimized;
        private void Grid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)=>this.DragMove();
        private void ChangeUserImage_Click(object sender, RoutedEventArgs e)
        {

        }
        #region ChooseButtonMenu
        private void RecommendationButton2_Click(object sender, RoutedEventArgs e)
        {
            #region recommendationBackground
            ButtonMenuCreateAnimation(RecommendationBackground);
            #endregion
            Content.Children.Clear();
            ContentToIessie.Children.Clear();
            BackgroundIessie.Children.Clear();
            Content.Children.Add(new Recomend(this));
        }
        private void LatestUpdateButton2_Click(object sender, RoutedEventArgs e)
        {
            #region latestUpdateBackground
            ButtonMenuCreateAnimation(LatestUpdateBackground);
            #endregion
            Content.Children.Clear();
            ContentToIessie.Children.Clear();
            BackgroundIessie.Children.Clear();
            LoadDataLatestUpdate().GetAwaiter();
        }
        private void CategoryButton2_Click(object sender, RoutedEventArgs e)
        {
            Content.Children.Clear();
            ContentToIessie.Children.Clear();
            BackgroundIessie.Children.Clear();
            #region categoryBackground
            ButtonMenuCreateAnimation(CategoryBackground);
            #endregion
            #region ACTIONBUTTON
            CreateButtonCategory(actionButton, "ACTION");
            actionButton.Margin = new Thickness(250, 50,0,0);
            Content.Children.Add(actionButton);
            #endregion
            #region HORRORBUTTON
            CreateButtonCategory(horrorButton, "HOROR");
            horrorButton.Margin = new Thickness(500, 50, 0, 0);
            Content.Children.Add(horrorButton);
            #endregion
            #region DEMONSBUTTON
            CreateButtonCategory(demonsButton, "DEMONS");
            demonsButton.Margin = new Thickness(250, 100, 0, 0);
            Content.Children.Add(demonsButton);
            tragedyButton.Style = (Style)tragedyButton.FindResource("ButtonSignStyle");
            #endregion
            #region TRAGEDYBUTTON
            CreateButtonCategory(tragedyButton, "TRAGEDY");
            tragedyButton.Margin = new Thickness(500, 100, 0, 0);
            Content.Children.Add(tragedyButton);
            #endregion
            #region MISTERYBUTTON
            CreateButtonCategory(MisteryButton, "MISTERY");
            MisteryButton.Margin = new Thickness(250, 150, 0, 0);
            Content.Children.Add(MisteryButton);
            #endregion
            #region ADVENTUREBUTTON
            CreateButtonCategory(adventureButton, "ADVENTURE");
            adventureButton.Margin = new Thickness(500, 150, 0, 0);
            Content.Children.Add(adventureButton);
            #endregion
            #region HISTORICALBUTTON
            CreateButtonCategory(historicalButton, "HISTORY");
            historicalButton.Margin = new Thickness(250, 200, 0, 0);
            Content.Children.Add(historicalButton);
            #endregion
            #region COMEDYBUTTON
            CreateButtonCategory(comedyButton, "COMEDY");
            comedyButton.Margin = new Thickness(500, 200, 0, 0);
            Content.Children.Add(comedyButton);
            #endregion
        }
        private void HistoryButton2_Click(object sender, RoutedEventArgs e)
        {
            #region historyBackground
            ButtonMenuCreateAnimation(HistoryBackground);
            #endregion
            Content.Children.Clear();
            ContentToIessie.Children.Clear();
            BackgroundIessie.Children.Clear();
            LoadDataHistory().GetAwaiter();
        }
        private void FavouritesButton2_Click(object sender, RoutedEventArgs e)
        {
            #region favouritesBackground
            ButtonMenuCreateAnimation(FavouritesBackground);
            #endregion

            Content.Children.Clear();
            ContentToIessie.Children.Clear();
            BackgroundIessie.Children.Clear();
            LoadDataFavourite().GetAwaiter();
        }
        private void BrowserButton2_Click(object sender, RoutedEventArgs e)
        {
            #region browserBackground
            ButtonMenuCreateAnimation(BrowserBackground);
            #endregion

            Content.Children.Clear();
            ContentToIessie.Children.Clear();
            BackgroundIessie.Children.Clear();
            try
            {
                Content.Children.Add(new WebBrowser { Source = new Uri("https://vk.com/id_sergei_qw") });
            }
            catch { }
        }
        #endregion
        private void addButton2_Click(object sender, RoutedEventArgs e)
        {
            #region addBackground
            ButtonMenuCreateAnimation(addBackground);
            #endregion
            Content.Children.Clear();
            Content.Children.Add(new ADMINPAGE { Width = 800, Height = 450 });
        }
    }
}
