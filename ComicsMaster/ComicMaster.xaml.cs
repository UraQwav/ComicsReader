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
namespace ComicsMaster
{
    /// <summary>
    /// Логика взаимодействия для ComicMaster.xaml
    /// </summary>
    public partial class ComicMaster : Window
    {
        #region globalParameters
        int  lll = 0;
        int j = 0, l = 0;
        public SqlConnection sqlConect = null;
        public SqlConnection sqlConect1 = null;
        public SqlConnection sqlConect2 = null;

        public SqlConnection sqlConect4 = null;
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        #endregion
        private async Task LoadData()
        {

            Loader loader = new Loader();
            loader.VerticalAlignment = VerticalAlignment.Center;
            loader.HorizontalAlignment = HorizontalAlignment.Center;
            loader.Width = 50;
            loader.Height = 50;
            Content.Children.Add(loader);
            sqlConect = new SqlConnection(connectionString);
                    await sqlConect.OpenAsync();

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string sql = "SELECT * FROM COMICSCOVER";
                        SqlCommand command = new SqlCommand(sql, connection);
                        SqlDataReader reader = await command.ExecuteReaderAsync();

                        //string sqls = "SELECT * FROM COMICSCOVERITEM";
                        //SqlCommand commands = new SqlCommand(sqls, connection);


                        while (reader.Read())
                        {
                            string nameas = reader.GetValue(0).ToString();
                            string name_Rest = reader.GetString(3);
                            byte[] data = (byte[])reader.GetValue(2);
                            MemoryStream ms = new MemoryStream(data);
                            System.Drawing.Image newImage = System.Drawing.Image.FromStream(ms);
                            ComicsPreview comicsPreview = new ComicsPreview();
                            Iessie iessie = new Iessie();
                            iessie.Margin = new Thickness(10, 10, 10, 10);

                            BitmapImage bi = new BitmapImage();
                            bi.BeginInit();
                            MemoryStream ms2 = new MemoryStream();
                            newImage.Save(ms2, System.Drawing.Imaging.ImageFormat.Bmp);
                            ms2.Seek(0, SeekOrigin.Begin);
                            bi.StreamSource = ms2;
                            bi.EndInit();
                            comicsPreview.ComicsWalpeper.Source = bi;
                            comicsPreview.NameComix.Text = name_Rest;
                            #region IESSIE
                            int jj = 0, ll = 1;
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
                                    if (nameqw == nameas)
                                    {
                                        Button button = new Button();
                                        button.Style = (Style)button.FindResource("ButtonSignStyle");

                                        button.Width = 80;
                                        button.Height = 30;
                                        button.HorizontalAlignment = HorizontalAlignment.Left;
                                        button.Content = name_Rest;
                                        if (jj < 5)
                                        {
                                            button.Margin = new Thickness(jj * 90, 150 * ll, 0, 0);
                                            jj++;
                                        }
                                        if (jj == 5)
                                        {

                                            jj = 0; ll++;
                                            button.Margin = new Thickness(jj * 90, 150 * ll, 0, 0);
                                            jj++;
                                        }

                                        button.Click += async (source, e) =>
                                        {
                                            Content.Children.Clear();
                                            Content.Children.Clear();

                                            Loader loader1 = new Loader();
                                            loader1.VerticalAlignment = VerticalAlignment.Center;
                                            loader1.HorizontalAlignment = HorizontalAlignment.Center;
                                            loader1.Width = 50;
                                            loader1.Height = 50;
                                            Content.Children.Add(loader1);

                                            #region READERCLIK
                                            sqlConect2 = new SqlConnection(connectionString);
                                            await sqlConect2.OpenAsync();
                                            using (SqlConnection connection2 = new SqlConnection(connectionString))
                                            {
                                                connection2.Open();
                                                string sql2 = "SELECT * FROM IMAGESITEMS";
                                                SqlCommand command2 = new SqlCommand(sql2, connection2);
                                                SqlDataReader reader2 = await command2.ExecuteReaderAsync();
                                                

                                                while (reader2.Read())
                                                {
                                                    try
                                                    {
                                                        string name_Rest2 = reader2.GetString(1);
                                                        if (name_Rest2 == name_Rest1)
                                                        {

                                                            byte[] data2 = (byte[])reader2.GetValue(2);
                                                            MemoryStream ms_2 = new MemoryStream(data2);
                                                            System.Drawing.Image newImage_2 = System.Drawing.Image.FromStream(ms_2);
                                                            Image imageS = new Image();

                                                            BitmapImage bi_2 = new BitmapImage();
                                                            bi_2.BeginInit();
                                                            MemoryStream ms2_2 = new MemoryStream();
                                                            newImage_2.Save(ms2_2, System.Drawing.Imaging.ImageFormat.Jpeg);
                                                            ms2_2.Seek(0, SeekOrigin.Begin);
                                                            bi_2.StreamSource = ms2_2;
                                                            bi_2.EndInit();
                                                            imageS.Source = bi_2;
                                                            imageS.Width = 812.5;
                                                            imageS.VerticalAlignment = VerticalAlignment.Top;
                                                            imageS.Margin = new Thickness(0, 1260 * lll, 0, 0);
                                                            lll++;

                                                            Content.Children.Add(imageS);
                                                        }
                                                    }
                                                    catch
                                                    {
                                                        MessageBox.Show("Erorr");
                                                    }

                                                };
                                                connection.Close();
                                            }
                                            lll = 0;
                                            #endregion
                                        };

                                        iessie.Content.Children.Add(button);
                                    }

                                };
                                connection1.Close();
                            }
                            ll = 1; jj = 0;
                            #endregion
                            iessie.TitleImages.Source = bi;
                            comicsPreview.ButtonGrid.MouseDown += (source, e) => { Content.Children.Clear(); Content.Children.Add(iessie); };
                            if (j < 5)
                            {
                                comicsPreview.Margin = new Thickness(j * 184, 320 * l, 0, 0);
                                j++;
                            }
                            if (j >= 5)
                            {

                                j = 0; l++;
                                comicsPreview.Margin = new Thickness(j * 184, 320 * l, 0, 0);
                                j++;
                            }

                            Content.Children.Add(comicsPreview);
                        };
                        connection.Close();
                        j = 0;l = 0;
                    }
        



           
        }
       

        private async Task LoadDatatEST()
        {
            sqlConect = new SqlConnection(connectionString);
            await sqlConect.OpenAsync();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM IMAGESITEMS";
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader = await command.ExecuteReaderAsync();
                Content.Children.Clear();

                while (reader.Read())
                {

                    string name_Rest = reader.GetString(1);
                    byte[] data = (byte[])reader.GetValue(2);
                    MemoryStream ms = new MemoryStream(data);
                    System.Drawing.Image newImage = System.Drawing.Image.FromStream(ms);
                    
                    BitmapImage bi = new BitmapImage();
                    bi.BeginInit();
                    MemoryStream ms2 = new MemoryStream();
                    newImage.Save(ms2, System.Drawing.Imaging.ImageFormat.Bmp);
                    ms2.Seek(0, SeekOrigin.Begin);
                    bi.StreamSource = ms2;
                    bi.EndInit();
                    Image image = new Image();

                    image.Source = bi;
                    image.Width = 750;
                    
                    if (j == 0)
                    {

                        j = 0; l++;
                        image.Margin = new Thickness(0, 750 * l, 0, 0);
                        
                    }

                    Content.Children.Add(image);
                };
                connection.Close();
            }
            l = 0; j = 0;
        }
        private async Task LoadDatatEST1()
        {
            sqlConect = new SqlConnection(connectionString);
            await sqlConect.OpenAsync();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM COMICSCOVERITEM";
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader = await command.ExecuteReaderAsync();
                Content.Children.Clear();

                while (reader.Read())
                {

                    string name_Rest = reader.GetString(2);
                    
                    ComicsPreview comicsPreview = new ComicsPreview();
                  
                    comicsPreview.NameComix.Text = name_Rest;
                    if (j < 5)
                    {
                        comicsPreview.Margin = new Thickness(j * 184, 320 * l, 0, 0);
                        j++;
                    }
                    if (j == 5)
                    {

                        j = 0; l++;
                        comicsPreview.Margin = new Thickness(j * 184, 320 * l, 0, 0);
                        j++;
                    }

                    Content.Children.Add(comicsPreview);
                };
                connection.Close();
            }
            l = 0; j = 0;
        }
        SqlConnection sqlConect5 = null;
        
        public ComicMaster()
        {

            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            MainWindow main = new MainWindow();
            if (main.LoginBox.Text == "ADMIN")
            {
                addButton.Height = 40;
            }
            var str = NameSignIN.flag;
            if (str == "ADMIN")
            {
                addButton.Height = 40;
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
            else if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
                Shadow.Margin = new Thickness(10, 10, 10, 10);
            }

        }
        private void Grid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void ChangeUserImage_Click(object sender, RoutedEventArgs e)
        {

        }
        #region chooseMenu
        double timeOpacity = 0.1;
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
            LoadData().GetAwaiter();
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

            LoadDatatEST().GetAwaiter();
            
        }

        private void CategoryButton2_Click(object sender, RoutedEventArgs e)
        {
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
