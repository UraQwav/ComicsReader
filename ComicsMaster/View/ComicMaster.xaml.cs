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
using ComicsMaster.View;
using System.ComponentModel;

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
        public SqlConnection sqlConect = null;
        public SqlConnection sqlConect1 = null;
        public SqlConnection sqlConect2 = null;
        public SqlConnection sqlconect3 = null;
        public SqlConnection sqlConect4 = null;
        public SqlConnection sqlConect5 = null;
        bool act = false,
             adv = false,
             hor = false,
             com = false,
             dem = false,
             mis = false,
             his = false,
             tra = false,
             ComicsItemsDo = false;
        string GlobalString = "",
               catalog = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        double timeOpacity = 0.1;
        Button action = new Button();
        Button adventure = new Button();
        Button horror = new Button();
        Button comedy = new Button();
        Button demons = new Button();
        Button Mistery = new Button();
        Button historical = new Button();
        Button tragedy = new Button();
        UserControl2 History = new UserControl2();
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        #endregion
        #region LoadDataFunction
        public System.Windows.UIElement GridAddChild(string name_Rest1)
        {
            Grid ContentGridChildren = new Grid();
            ContentGridChildren.Margin = new Thickness(1, 1, 1, 1);
            sqlConect2 = new SqlConnection(connectionString);
            sqlConect2.Open();
            using (SqlConnection connection2 = new SqlConnection(connectionString))
            {
                connection2.Open();
                string sql2 = "SELECT * FROM IMAGESITEMS";
                SqlCommand command2 = new SqlCommand(sql2, connection2);
                SqlDataReader reader2 = command2.ExecuteReader();
                while (reader2.Read())
                {
                    try
                    {
                        string name_Rest2 = reader2.GetString(1);
                        if (name_Rest2 == name_Rest1)
                        {
                            string data2 = reader2.GetString(2);
                            Image imageSS = new Image();
                            imageSS.Source = new BitmapImage(new Uri("pack://siteoforigin:,,," + data2, UriKind.RelativeOrAbsolute));
                            imageSS.Width = 812.5;
                            imageSS.VerticalAlignment = VerticalAlignment.Top;
                            imageSS.Margin = new Thickness(0, 1260 * ImagesMarginTop, 0, 0);
                            ImagesMarginTop++;
                            ContentGridChildren.Children.Add(imageSS);
                        }
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
                    string nameas = reader.GetValue(0).ToString();
                    string name_kategory = reader.GetString(1);
                    string data = reader.GetString(2);
                    string data2 = reader.GetString(3);
                    string name_Rest = reader.GetString(4);
                    ComicsPreview comicsPreview = new ComicsPreview();
                    Iessie iessie = new Iessie();
                    iessie.Margin = new Thickness(10, 10, 10, 10);
                    iessie.Content.HorizontalAlignment = HorizontalAlignment.Center;
                    comicsPreview.ComicsWalpeper.Source = new BitmapImage(new Uri(catalog + data));
                    comicsPreview.NameComix.Text = name_Rest;
                    comicsPreview.NameGlavaComix.Text = name_kategory;
                    #region IESSIE_ADD_BUTTON
                    int buttonMarginLeft = 0,
                        buttonMarginTop = 1;
                    sqlConect1 = new SqlConnection(connectionString);
                    await sqlConect1.OpenAsync();
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
                                button.Style = (Style)button.FindResource("ButtonSignStyle");
                                button.Width = 150;
                                button.Height = 40;
                                button.HorizontalAlignment = HorizontalAlignment.Left;
                                button.Content = name_Rest1;
                                if (WidthGridForbutton < 800)
                                {
                                    button.Margin = new Thickness(buttonMarginLeft * 160, 150 * buttonMarginTop, 0, 0);
                                    buttonMarginLeft++;
                                    WidthGridForbutton = buttonMarginLeft * 160;
                                }
                                if (WidthGridForbutton >= 800)
                                {
                                    buttonMarginLeft = 0; buttonMarginTop++;
                                    button.Margin = new Thickness(buttonMarginLeft * 160, 150 * buttonMarginTop, 0, 0);
                                    buttonMarginLeft++;
                                    WidthGridForbutton = 0;
                                }
                                button.Click += (sender, e) =>
                                {
                                    Content.Children.Clear();
                                    Loader loaderItems = new Loader();
                                    Canvas.SetZIndex(loaderItems, (int)98);
                                    Content.Children.Add(loaderItems);
                                    Content.Children.Add(GridAddChild(name_Rest1));
                                    Content.Children.Remove(loaderItems);
                                    ImagesMarginTop = 0;
                                    #region READER_IMAGESITEMS
                                    //sqlConect2 = new SqlConnection(connectionString);
                                    //await sqlConect2.OpenAsync();
                                    //using (SqlConnection connection2 = new SqlConnection(connectionString))
                                    //{
                                    //    connection2.Open();
                                    //    string sql2 = "SELECT * FROM IMAGESITEMS";
                                    //    SqlCommand command2 = new SqlCommand(sql2, connection2);
                                    //    SqlDataReader reader2 = await command2.ExecuteReaderAsync();
                                    //    while (reader2.Read())
                                    //    {
                                    //        try
                                    //        {
                                    //            string name_Rest2 = reader2.GetString(1);
                                    //            if (name_Rest2 == name_Rest1)
                                    //            {
                                    //                byte[] data2 = (byte[])reader2.GetValue(2);
                                    //                MemoryStream ms_2 = new MemoryStream(data2);
                                    //                System.Drawing.Image newImage_2 = System.Drawing.Image.FromStream(ms_2);
                                    //                Image imageS = new Image();
                                    //                BitmapImage bi_2 = new BitmapImage();
                                    //                bi_2.BeginInit();
                                    //                MemoryStream ms2_2 = new MemoryStream();
                                    //                newImage_2.Save(ms2_2, System.Drawing.Imaging.ImageFormat.Jpeg);
                                    //                ms2_2.Seek(0, SeekOrigin.Begin);
                                    //                bi_2.StreamSource = ms2_2;
                                    //                bi_2.EndInit();
                                    //                imageS.Source = bi_2;
                                    //                imageS.Width = 812.5;
                                    //                imageS.VerticalAlignment = VerticalAlignment.Top;
                                    //                imageS.Margin = new Thickness(0, 1260 * ImagesMarginTop, 0, 0);
                                    //                ImagesMarginTop++;
                                    //                Content.Children.Add(imageS);
                                    //            }
                                    //        }
                                    //        catch
                                    //        {
                                    //            MessageBox.Show("Erorr");
                                    //        }
                                    //    };
                                    //    connection.Close();
                                    //}
                                    //ImagesMarginTop = 0;
                                    #endregion
                                };
                                iessie.Content.Children.Add(button);
                            }
                        };
                        connection1.Close();
                    }
                    buttonMarginTop = 1; buttonMarginLeft = 0;
                    #endregion
                    iessie.TitleImages.Source = new BitmapImage(new Uri(catalog + data2));
                    comicsPreview.ButtonGrid.MouseDown += (source, e) => { Content.Children.Clear(); Content.Children.Add(iessie); };
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
        private async Task LoadDataRecomend()
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
                    string nameas = reader.GetValue(0).ToString();
                    string name_kategory = reader.GetString(1);
                    string data = reader.GetString(2);
                    string data2 = reader.GetString(3);
                    string name_Rest = reader.GetString(4);
                    ComicsPreview comicsPreview = new ComicsPreview();
                    Iessie iessie = new Iessie();
                    iessie.Margin = new Thickness(10, 10, 10, 10);
                    iessie.Content.HorizontalAlignment = HorizontalAlignment.Center;
                    comicsPreview.ComicsWalpeper.Source = new BitmapImage(new Uri(catalog+data));
                    comicsPreview.NameComix.Text = name_Rest;
                    comicsPreview.NameGlavaComix.Text = name_kategory;
                    #region IESSIE_ADD_BUTTON
                    int buttonMarginLeft = 0,
                        buttonMarginTop = 1;
                    sqlConect1 = new SqlConnection(connectionString);
                    await sqlConect1.OpenAsync();
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
                                button.Style = (Style)button.FindResource("ButtonSignStyle");
                                button.Width = 150;
                                button.Height = 40;
                                button.HorizontalAlignment = HorizontalAlignment.Left;
                                button.Content = name_Rest1;
                                if (WidthGridForbutton < 800)
                                {
                                    button.Margin = new Thickness(buttonMarginLeft * 160, 150 * buttonMarginTop, 0, 0);
                                    buttonMarginLeft++;
                                    WidthGridForbutton = buttonMarginLeft * 160;
                                }
                                if (WidthGridForbutton >= 800)
                                {
                                    buttonMarginLeft = 0; buttonMarginTop++;
                                    button.Margin = new Thickness(buttonMarginLeft * 160, 150 * buttonMarginTop, 0, 0);
                                    buttonMarginLeft++;
                                    WidthGridForbutton = 0;
                                }
                                button.Click += (sender, e) =>
                                {
                                    Content.Children.Clear();
                                    Loader loaderItems = new Loader();
                                    Canvas.SetZIndex(loaderItems, (int)98);
                                    Content.Children.Add(loaderItems);
                                    Content.Children.Add(GridAddChild(name_Rest1));
                                    Content.Children.Remove(loaderItems);
                                    ImagesMarginTop = 0;
                                    #region READER_IMAGESITEMS
                                    //sqlConect2 = new SqlConnection(connectionString);
                                    //await sqlConect2.OpenAsync();
                                    //using (SqlConnection connection2 = new SqlConnection(connectionString))
                                    //{
                                    //    connection2.Open();
                                    //    string sql2 = "SELECT * FROM IMAGESITEMS";
                                    //    SqlCommand command2 = new SqlCommand(sql2, connection2);
                                    //    SqlDataReader reader2 = await command2.ExecuteReaderAsync();
                                    //    while (reader2.Read())
                                    //    {
                                    //        try
                                    //        {
                                    //            string name_Rest2 = reader2.GetString(1);
                                    //            if (name_Rest2 == name_Rest1)
                                    //            {
                                    //                byte[] data2 = (byte[])reader2.GetValue(2);
                                    //                MemoryStream ms_2 = new MemoryStream(data2);
                                    //                System.Drawing.Image newImage_2 = System.Drawing.Image.FromStream(ms_2);
                                    //                Image imageS = new Image();
                                    //                BitmapImage bi_2 = new BitmapImage();
                                    //                bi_2.BeginInit();
                                    //                MemoryStream ms2_2 = new MemoryStream();
                                    //                newImage_2.Save(ms2_2, System.Drawing.Imaging.ImageFormat.Jpeg);
                                    //                ms2_2.Seek(0, SeekOrigin.Begin);
                                    //                bi_2.StreamSource = ms2_2;
                                    //                bi_2.EndInit();
                                    //                imageS.Source = bi_2;
                                    //                imageS.Width = 812.5;
                                    //                imageS.VerticalAlignment = VerticalAlignment.Top;
                                    //                imageS.Margin = new Thickness(0, 1260 * ImagesMarginTop, 0, 0);
                                    //                ImagesMarginTop++;
                                    //                Content.Children.Add(imageS);
                                    //            }
                                    //        }
                                    //        catch
                                    //        {
                                    //            MessageBox.Show("Erorr");
                                    //        }
                                    //    };
                                    //    connection.Close();
                                    //}
                                    //ImagesMarginTop = 0;
                                    #endregion
                                };
                                iessie.Content.Children.Add(button);
                            }
                        };
                        connection1.Close();
                    }
                    buttonMarginTop = 1; buttonMarginLeft = 0;
                    #endregion
                    iessie.TitleImages.Source = new BitmapImage(new Uri(catalog+data2));
                    comicsPreview.ButtonGrid.MouseDown += (source, e) => { Content.Children.Clear(); Content.Children.Add(iessie); };
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
            LoadDataRecomend().GetAwaiter();
            var str = NameSignIN.flag;
            if (str == "ADMIN")
            {
                addButton.Height = 40;
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
        private void Grid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)=>this.DragMove();
        private void ChangeUserImage_Click(object sender, RoutedEventArgs e)
        {

        }
        #region ChooseButtonMenu
        private void RecommendationButton2_Click(object sender, RoutedEventArgs e)
        {
            #region recommendationBackground
            DoubleAnimation recollor1 = new DoubleAnimation();
            recollor1.From = RecommendationBackground.Opacity;
            recollor1.To = 1;
            recollor1.Duration = TimeSpan.FromSeconds(timeOpacity);
            RecommendationBackground.BeginAnimation(Border.OpacityProperty, recollor1);

            DoubleAnimation recollor = new DoubleAnimation();
            recollor.From = LatestUpdateBackground.Opacity;
            recollor.To = 0;
            recollor.Duration = TimeSpan.FromSeconds(timeOpacity);
            LatestUpdateBackground.BeginAnimation(Border.OpacityProperty, recollor);

            DoubleAnimation recollor2 = new DoubleAnimation();
            recollor2.From = CategoryBackground.Opacity;
            recollor2.To = 0;
            recollor2.Duration = TimeSpan.FromSeconds(timeOpacity);
            CategoryBackground.BeginAnimation(Border.OpacityProperty, recollor2);

            DoubleAnimation recollor3 = new DoubleAnimation();
            recollor3.From = HistoryBackground.Opacity;
            recollor3.To = 0;
            recollor3.Duration = TimeSpan.FromSeconds(timeOpacity);
            HistoryBackground.BeginAnimation(Border.OpacityProperty, recollor3);

            DoubleAnimation recollor4 = new DoubleAnimation();
            recollor4.From = FavouritesBackground.Opacity;
            recollor4.To = 0;
            recollor4.Duration = TimeSpan.FromSeconds(timeOpacity);
            FavouritesBackground.BeginAnimation(Border.OpacityProperty, recollor4);

            DoubleAnimation recollor5 = new DoubleAnimation();
            recollor5.From = SettingsBackground.Opacity;
            recollor5.To = 0;
            recollor5.Duration = TimeSpan.FromSeconds(timeOpacity);
            SettingsBackground.BeginAnimation(Border.OpacityProperty, recollor5);

            DoubleAnimation recollor6 = new DoubleAnimation();
            recollor6.From = BrowserBackground.Opacity;
            recollor6.To = 0;
            recollor6.Duration = TimeSpan.FromSeconds(timeOpacity);
            BrowserBackground.BeginAnimation(Border.OpacityProperty, recollor6);
            #endregion
            Content.Children.Clear();
            LoadDataRecomend().GetAwaiter();
        }
        private void LatestUpdateButton2_Click(object sender, RoutedEventArgs e)
        {
            #region latestUpdateBackground
            DoubleAnimation recollor1 = new DoubleAnimation();
            recollor1.From = RecommendationBackground.Opacity;
            recollor1.To = 0;
            recollor1.Duration = TimeSpan.FromSeconds(timeOpacity);
            RecommendationBackground.BeginAnimation(Border.OpacityProperty, recollor1);

            DoubleAnimation recollor = new DoubleAnimation();
            recollor.From = LatestUpdateBackground.Opacity;
            recollor.To = 1;
            recollor.Duration = TimeSpan.FromSeconds(timeOpacity);
            LatestUpdateBackground.BeginAnimation(Border.OpacityProperty, recollor);

            DoubleAnimation recollor2 = new DoubleAnimation();
            recollor2.From = CategoryBackground.Opacity;
            recollor2.To = 0;
            recollor2.Duration = TimeSpan.FromSeconds(timeOpacity);
            CategoryBackground.BeginAnimation(Border.OpacityProperty, recollor2);

            DoubleAnimation recollor3 = new DoubleAnimation();
            recollor3.From = HistoryBackground.Opacity;
            recollor3.To = 0;
            recollor3.Duration = TimeSpan.FromSeconds(timeOpacity);
            HistoryBackground.BeginAnimation(Border.OpacityProperty, recollor3);

            DoubleAnimation recollor4 = new DoubleAnimation();
            recollor4.From = FavouritesBackground.Opacity;
            recollor4.To = 0;
            recollor4.Duration = TimeSpan.FromSeconds(timeOpacity);
            FavouritesBackground.BeginAnimation(Border.OpacityProperty, recollor4);

            DoubleAnimation recollor5 = new DoubleAnimation();
            recollor5.From = SettingsBackground.Opacity;
            recollor5.To = 0;
            recollor5.Duration = TimeSpan.FromSeconds(timeOpacity);
            SettingsBackground.BeginAnimation(Border.OpacityProperty, recollor5);

            DoubleAnimation recollor6 = new DoubleAnimation();
            recollor6.From = BrowserBackground.Opacity;
            recollor6.To = 0;
            recollor6.Duration = TimeSpan.FromSeconds(timeOpacity);
            BrowserBackground.BeginAnimation(Border.OpacityProperty, recollor6);
            #endregion
            Content.Children.Clear();
            LoadDataLatestUpdate().GetAwaiter();
        }
        private void CategoryButton2_Click(object sender, RoutedEventArgs e)
        {
            Content.Children.Clear();
            #region categoryBackground
            DoubleAnimation recollor1 = new DoubleAnimation();
            recollor1.From = RecommendationBackground.Opacity;
            recollor1.To = 0;
            recollor1.Duration = TimeSpan.FromSeconds(timeOpacity);
            RecommendationBackground.BeginAnimation(Border.OpacityProperty, recollor1);

            DoubleAnimation recollor = new DoubleAnimation();
            recollor.From = LatestUpdateBackground.Opacity;
            recollor.To = 0;
            recollor.Duration = TimeSpan.FromSeconds(timeOpacity);
            LatestUpdateBackground.BeginAnimation(Border.OpacityProperty, recollor);

            DoubleAnimation recollor2 = new DoubleAnimation();
            recollor2.From = CategoryBackground.Opacity;
            recollor2.To = 1;
            recollor2.Duration = TimeSpan.FromSeconds(timeOpacity);
            CategoryBackground.BeginAnimation(Border.OpacityProperty, recollor2);

            DoubleAnimation recollor3 = new DoubleAnimation();
            recollor3.From = HistoryBackground.Opacity;
            recollor3.To = 0;
            recollor3.Duration = TimeSpan.FromSeconds(timeOpacity);
            HistoryBackground.BeginAnimation(Border.OpacityProperty, recollor3);

            DoubleAnimation recollor4 = new DoubleAnimation();
            recollor4.From = FavouritesBackground.Opacity;
            recollor4.To = 0;
            recollor4.Duration = TimeSpan.FromSeconds(timeOpacity);
            FavouritesBackground.BeginAnimation(Border.OpacityProperty, recollor4);

            DoubleAnimation recollor5 = new DoubleAnimation();
            recollor5.From = SettingsBackground.Opacity;
            recollor5.To = 0;
            recollor5.Duration = TimeSpan.FromSeconds(timeOpacity);
            SettingsBackground.BeginAnimation(Border.OpacityProperty, recollor5);

            DoubleAnimation recollor6 = new DoubleAnimation();
            recollor6.From = BrowserBackground.Opacity;
            recollor6.To = 0;
            recollor6.Duration = TimeSpan.FromSeconds(timeOpacity);
            BrowserBackground.BeginAnimation(Border.OpacityProperty, recollor6);
            #endregion
            #region ACTIONBUTTON
            action.Width = 100;
            action.Height = 30;
            action.Content = "ACTION";
            action.Margin = new Thickness(250, 100,0,0);
            action.Click += (source, t) => { act = true;
                                             adv = false;
                                             hor = false;
                                             com = false;
                                             dem = false;
                                             mis = false;
                                             his = false;
                                             tra = false;
                                             Content.Children.Clear();
                                           };
            Content.Children.Add(action);
            #endregion
            #region ADVENTUREBUTTON
            adventure.Width = 100;
            adventure.Height = 30;
            adventure.Content = "ADVENTURE";
            adventure.Margin = new Thickness(500, 100, 0, 0);
            adventure.Click += (source, t) => { act = false;
                                                adv = true;
                                                hor = false;
                                                com = false;
                                                dem = false;
                                                mis = false;
                                                his = false;
                                                tra = false;
                                                Content.Children.Clear();
                                              };
            Content.Children.Add(adventure);
            #endregion
            #region MISTERYBUTTON
            comedy.Width = 100;
            comedy.Height = 30;
            comedy.Content = "COMEDY";
            comedy.Margin = new Thickness(500, 400, 0, 0);
            comedy.Click += (source, t) => { act = false;
                                             adv = false;
                                             hor = false;
                                             com = true;
                                             dem = false;
                                             mis = false;
                                             his = false;
                                             tra = false;
                                             Content.Children.Clear();
                                           };
            Content.Children.Add(comedy);
            #endregion
        }
        private void HistoryButton2_Click(object sender, RoutedEventArgs e)
        {
            #region historyBackground
            DoubleAnimation recollor1 = new DoubleAnimation();
            recollor1.From = RecommendationBackground.Opacity;
            recollor1.To = 0;
            recollor1.Duration = TimeSpan.FromSeconds(timeOpacity);
            RecommendationBackground.BeginAnimation(Border.OpacityProperty, recollor1);

            DoubleAnimation recollor = new DoubleAnimation();
            recollor.From = LatestUpdateBackground.Opacity;
            recollor.To = 0;
            recollor.Duration = TimeSpan.FromSeconds(timeOpacity);
            LatestUpdateBackground.BeginAnimation(Border.OpacityProperty, recollor);

            DoubleAnimation recollor2 = new DoubleAnimation();
            recollor2.From = CategoryBackground.Opacity;
            recollor2.To = 0;
            recollor2.Duration = TimeSpan.FromSeconds(timeOpacity);
            CategoryBackground.BeginAnimation(Border.OpacityProperty, recollor2);

            DoubleAnimation recollor3 = new DoubleAnimation();
            recollor3.From = HistoryBackground.Opacity;
            recollor3.To = 1;
            recollor3.Duration = TimeSpan.FromSeconds(timeOpacity);
            HistoryBackground.BeginAnimation(Border.OpacityProperty, recollor3);

            DoubleAnimation recollor4 = new DoubleAnimation();
            recollor4.From = FavouritesBackground.Opacity;
            recollor4.To = 0;
            recollor4.Duration = TimeSpan.FromSeconds(timeOpacity);
            FavouritesBackground.BeginAnimation(Border.OpacityProperty, recollor4);

            DoubleAnimation recollor5 = new DoubleAnimation();
            recollor5.From = SettingsBackground.Opacity;
            recollor5.To = 0;
            recollor5.Duration = TimeSpan.FromSeconds(timeOpacity);
            SettingsBackground.BeginAnimation(Border.OpacityProperty, recollor5);

            DoubleAnimation recollor6 = new DoubleAnimation();
            recollor6.From = BrowserBackground.Opacity;
            recollor6.To = 0;
            recollor6.Duration = TimeSpan.FromSeconds(timeOpacity);
            BrowserBackground.BeginAnimation(Border.OpacityProperty, recollor6);
            #endregion
            Content.Children.Clear();
            Content.Children.Add(History);
        }
        private void FavouritesButton2_Click(object sender, RoutedEventArgs e)
        {
            #region favouritesBackground
            DoubleAnimation recollor1 = new DoubleAnimation();
            recollor1.From = RecommendationBackground.Opacity;
            recollor1.To = 0;
            recollor1.Duration = TimeSpan.FromSeconds(timeOpacity);
            RecommendationBackground.BeginAnimation(Border.OpacityProperty, recollor1);

            DoubleAnimation recollor = new DoubleAnimation();
            recollor.From = LatestUpdateBackground.Opacity;
            recollor.To = 0;
            recollor.Duration = TimeSpan.FromSeconds(timeOpacity);
            LatestUpdateBackground.BeginAnimation(Border.OpacityProperty, recollor);

            DoubleAnimation recollor2 = new DoubleAnimation();
            recollor2.From = CategoryBackground.Opacity;
            recollor2.To = 0;
            recollor2.Duration = TimeSpan.FromSeconds(timeOpacity);
            CategoryBackground.BeginAnimation(Border.OpacityProperty, recollor2);

            DoubleAnimation recollor3 = new DoubleAnimation();
            recollor3.From = HistoryBackground.Opacity;
            recollor3.To = 0;
            recollor3.Duration = TimeSpan.FromSeconds(timeOpacity);
            HistoryBackground.BeginAnimation(Border.OpacityProperty, recollor3);

            DoubleAnimation recollor4 = new DoubleAnimation();
            recollor4.From = FavouritesBackground.Opacity;
            recollor4.To = 1;
            recollor4.Duration = TimeSpan.FromSeconds(timeOpacity);
            FavouritesBackground.BeginAnimation(Border.OpacityProperty, recollor4);

            DoubleAnimation recollor5 = new DoubleAnimation();
            recollor5.From = SettingsBackground.Opacity;
            recollor5.To = 0;
            recollor5.Duration = TimeSpan.FromSeconds(timeOpacity);
            SettingsBackground.BeginAnimation(Border.OpacityProperty, recollor5);

            DoubleAnimation recollor6 = new DoubleAnimation();
            recollor6.From = BrowserBackground.Opacity;
            recollor6.To = 0;
            recollor6.Duration = TimeSpan.FromSeconds(timeOpacity);
            BrowserBackground.BeginAnimation(Border.OpacityProperty, recollor6);
            #endregion
        }
        private void SettingsButton2_Click(object sender, RoutedEventArgs e)
        {
            #region settingsBackground
            DoubleAnimation recollor1 = new DoubleAnimation();
            recollor1.From = RecommendationBackground.Opacity;
            recollor1.To = 0;
            recollor1.Duration = TimeSpan.FromSeconds(timeOpacity);
            RecommendationBackground.BeginAnimation(Border.OpacityProperty, recollor1);

            DoubleAnimation recollor = new DoubleAnimation();
            recollor.From = LatestUpdateBackground.Opacity;
            recollor.To = 0;
            recollor.Duration = TimeSpan.FromSeconds(timeOpacity);
            LatestUpdateBackground.BeginAnimation(Border.OpacityProperty, recollor);

            DoubleAnimation recollor2 = new DoubleAnimation();
            recollor2.From = CategoryBackground.Opacity;
            recollor2.To = 0;
            recollor2.Duration = TimeSpan.FromSeconds(timeOpacity);
            CategoryBackground.BeginAnimation(Border.OpacityProperty, recollor2);

            DoubleAnimation recollor3 = new DoubleAnimation();
            recollor3.From = HistoryBackground.Opacity;
            recollor3.To = 0;
            recollor3.Duration = TimeSpan.FromSeconds(timeOpacity);
            HistoryBackground.BeginAnimation(Border.OpacityProperty, recollor3);

            DoubleAnimation recollor4 = new DoubleAnimation();
            recollor4.From = FavouritesBackground.Opacity;
            recollor4.To = 0;
            recollor4.Duration = TimeSpan.FromSeconds(timeOpacity);
            FavouritesBackground.BeginAnimation(Border.OpacityProperty, recollor4);

            DoubleAnimation recollor5 = new DoubleAnimation();
            recollor5.From = SettingsBackground.Opacity;
            recollor5.To = 1;
            recollor5.Duration = TimeSpan.FromSeconds(timeOpacity);
            SettingsBackground.BeginAnimation(Border.OpacityProperty, recollor5);

            DoubleAnimation recollor6 = new DoubleAnimation();
            recollor6.From = BrowserBackground.Opacity;
            recollor6.To = 0;
            recollor6.Duration = TimeSpan.FromSeconds(timeOpacity);
            BrowserBackground.BeginAnimation(Border.OpacityProperty, recollor6);
            #endregion
        }
        private void BrowserButton2_Click(object sender, RoutedEventArgs e)
        {
            #region browserBackground
            DoubleAnimation recollor1 = new DoubleAnimation();
            recollor1.From = RecommendationBackground.Opacity;
            recollor1.To = 0;
            recollor1.Duration = TimeSpan.FromSeconds(timeOpacity);
            RecommendationBackground.BeginAnimation(Border.OpacityProperty, recollor1);

            DoubleAnimation recollor = new DoubleAnimation();
            recollor.From = LatestUpdateBackground.Opacity;
            recollor.To = 0;
            recollor.Duration = TimeSpan.FromSeconds(timeOpacity);
            LatestUpdateBackground.BeginAnimation(Border.OpacityProperty, recollor);

            DoubleAnimation recollor2 = new DoubleAnimation();
            recollor2.From = CategoryBackground.Opacity;
            recollor2.To = 0;
            recollor2.Duration = TimeSpan.FromSeconds(timeOpacity);
            CategoryBackground.BeginAnimation(Border.OpacityProperty, recollor2);

            DoubleAnimation recollor3 = new DoubleAnimation();
            recollor3.From = HistoryBackground.Opacity;
            recollor3.To = 0;
            recollor3.Duration = TimeSpan.FromSeconds(timeOpacity);
            HistoryBackground.BeginAnimation(Border.OpacityProperty, recollor3);

            DoubleAnimation recollor4 = new DoubleAnimation();
            recollor4.From = FavouritesBackground.Opacity;
            recollor4.To = 0;
            recollor4.Duration = TimeSpan.FromSeconds(timeOpacity);
            FavouritesBackground.BeginAnimation(Border.OpacityProperty, recollor4);

            DoubleAnimation recollor5 = new DoubleAnimation();
            recollor5.From = SettingsBackground.Opacity;
            recollor5.To = 0;
            recollor5.Duration = TimeSpan.FromSeconds(timeOpacity);
            SettingsBackground.BeginAnimation(Border.OpacityProperty, recollor5);

            DoubleAnimation recollor6 = new DoubleAnimation();
            recollor6.From = BrowserBackground.Opacity;
            recollor6.To = 1;
            recollor6.Duration = TimeSpan.FromSeconds(timeOpacity);
            BrowserBackground.BeginAnimation(Border.OpacityProperty, recollor6);
            #endregion
        }
        #endregion
        private void addButton2_Click(object sender, RoutedEventArgs e)
        {
            ADMINPAGE admin = new ADMINPAGE();
            Content.Children.Clear();
            admin.Width = 800;
            admin.Height = 450;
            Content.Children.Add(admin);
        }
    }
}
