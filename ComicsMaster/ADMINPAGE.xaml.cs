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
using Ionic.Zip;
using Path = System.IO.Path;

namespace ComicsMaster
{
    /// <summary>
    /// Логика взаимодействия для ADMINPAGE.xaml
    /// </summary>
    /// 
    public static class IEnumerableExtension
    {
        public static IEnumerable<T> Safe<T>(this IEnumerable<T> source)
        {
            if (source == null)
            {
                yield break;
            }
            foreach (var item in source)
            {
                yield return item;
            }
        }
    }
    public partial class ADMINPAGE : UserControl
    {
        #region globalParameters
        public SqlConnection sqlConect = null;
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public bool flag = false;
        public string File_image;
        public string File_rarimage;
        public SqlConnection connect = null;
        public string category;
        string IDPARENTCATEGORY;
        #endregion

        public ADMINPAGE()
        {
            InitializeComponent();
        }
        

        private void Get_image_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            Nullable<bool> result = openFileDialog.ShowDialog();
            File_image = openFileDialog.FileName;
            if (File_image.Length > 5)
                flag = true;
        }
        public string ExtractZipArchive()
        {
            using (ZipFile zip = new ZipFile(File_rarimage))
            {
                zip.Encryption = EncryptionAlgorithm.WinZipAes256;
                zip.ExtractAll(@".\Temp", ExtractExistingFileAction.OverwriteSilently);
            }
            return File_rarimage + ".rar";
        }
        private void Get_image1_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            Nullable<bool> result = openFileDialog.ShowDialog();
            File_rarimage = openFileDialog.FileName;
            if (File_rarimage.Length > 5)
                flag = true;
            ExtractZipArchive();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (flag)
                SaveFileToDatabase();
        }
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            if (flag)
                SaveFileToDatabase1();
        }
        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            if (flag)
                SaveFileToDatabase2();
        }
        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            if (flag)
            {
                SaveFileToDatabase2();
                SaveFileToDatabase();
                SaveFileToDatabase1(); 
                SaveFileToDatabase3();
            }
        }
        
        private void SaveFileToDatabase()
        {
            try
            {

                connect = new SqlConnection(connectionString);
                connect.Open();
                // путь к файлу для загрузки 
                string filename = File_image;
                // получаем короткое имя файла для сохранения в бд 
                string shortFileName = filename.Substring(filename.LastIndexOf('\\') + 1); // массив для хранения бинарных данных файла 

                byte[] imageData;
                using (System.IO.FileStream fs = new System.IO.FileStream(filename, FileMode.Open))
                {
                    imageData = new byte[fs.Length];
                    fs.Read(imageData, 0, imageData.Length);
                }
                string sql = string.Format("Insert Into COMICSCOVER" +
                "(IDPARENTCATEGORY,COMICSCOVERWALPAPER,COMICSNAME) Values(@IDPARENTCATEGORY,@ImageData,@Name)");


                using (SqlCommand cmd = new SqlCommand(sql, this.connect))
                {
                    // Добавить параметры 
                    cmd.Parameters.AddWithValue("@IDPARENTCATEGORY", IDPARENTCATEGORY);
                    cmd.Parameters.AddWithValue("@ImageData", imageData);
                    cmd.Parameters.AddWithValue("@Name", title.Text);
                    cmd.ExecuteNonQuery();
                }
                connect.Close();
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }
        private void SaveFileToDatabase1()
        {
            try
            {

                connect = new SqlConnection(connectionString);
                connect.Open();
                // путь к файлу для загрузки 
               
                string sql = string.Format("Insert Into COMICSCOVERITEM" +
                "(IDPARENTCOMICS,ISSIE) Values(@ImageData,@Name)");


                using (SqlCommand cmd = new SqlCommand(sql, this.connect))
                {
                    // Добавить параметры 

                    cmd.Parameters.AddWithValue("@ImageData",Group.Text  );
                    cmd.Parameters.AddWithValue("@Name", Group_Copy.Text);
                    cmd.ExecuteNonQuery();
                }
                connect.Close();
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }
        private void SaveFileToDatabase2()
        {
            try
            {
                connect = new SqlConnection(connectionString);
                connect.Open();
                string sql = string.Format("Insert Into CATEGORY" +
                "(CATEGORY) Values(@Name)");
                using (SqlCommand cmd = new SqlCommand(sql, this.connect))
                {    
                    cmd.Parameters.AddWithValue("@Name", category);
                    cmd.ExecuteNonQuery();
                }
                connect.Close();
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }
        private void SaveFileToDatabase3()
        {  
            connect = new SqlConnection(connectionString);
            connect.Open();
            List<string> lst = new List<string>();
            using (ZipFile zipFile = new ZipFile(File_rarimage))
            {
                ICollection<ZipEntry> files = zipFile.Entries;
            
                foreach (ZipEntry entry in files)
                    if (!entry.IsDirectory) {
                        lst.Add(@".\Temp\" + entry.FileName);
                    }
            }
            MessageBox.Show(lst[0]);
            string filename = File_image;
            byte[] imageData; int j = 0;
            while( lst!= null)
            {
                try
                {
                    if (lst.Safe().Any())
                    {
                        using (FileStream fs = new FileStream(lst[j], FileMode.Open))
                        {
                         imageData = new byte[fs.Length];
                            fs.Read(imageData, 0, imageData.Length);
                        }
                        string sql = string.Format("Insert Into IMAGESITEMS" +
                        "(IDPARENTCOMICSITEM,COMICSWALPAPER) Values(@Name,@ImageData)");
                        using (SqlCommand cmd = new SqlCommand(sql, this.connect))
                        {
                            cmd.Parameters.AddWithValue("@ImageData", imageData);
                            cmd.Parameters.AddWithValue("@Name", Group_Copy1.Text);
                            cmd.ExecuteNonQuery();
                        }
                        j++;
                    }
                    else break;
                }
                catch
                {
                    break;
                }
            }
               
            connect.Close();
            DirectoryInfo directoryInfo = new DirectoryInfo(@".\Temp\");
            foreach (FileInfo file in directoryInfo.GetFiles())
                file.Delete();
        }

        private void Action_Checked(object sender, RoutedEventArgs e)
        {
            category = Action.Content.ToString();
            IDPARENTCATEGORY = category;
        }
        private void Adventure_Checked(object sender, RoutedEventArgs e)
        {
            IDPARENTCATEGORY = category;
            category = Adventure.Content.ToString();
        }
        private void Comedy_Checked(object sender, RoutedEventArgs e)
        {
            category = Comedy.Content.ToString();
            IDPARENTCATEGORY = category;
        }
        private void Historical_Checked(object sender, RoutedEventArgs e)
        {
            category = Historical.Content.ToString();
            IDPARENTCATEGORY = category;
        }
        private void Horor_Checked(object sender, RoutedEventArgs e)
        {
            category = Horor.Content.ToString();
            IDPARENTCATEGORY = category;
        }
        private void Mistery_Checked(object sender, RoutedEventArgs e)
        {
            category = Mistery.Content.ToString();
            IDPARENTCATEGORY = category;
        }
        private void Demons_Checked(object sender, RoutedEventArgs e)
        {
            category = Demons.Content.ToString();
            IDPARENTCATEGORY = category;
        }
        private void Tragedy_Checked(object sender, RoutedEventArgs e)
        {
            category = Tragedy.Content.ToString();
            IDPARENTCATEGORY = category;
        }
    }
}
