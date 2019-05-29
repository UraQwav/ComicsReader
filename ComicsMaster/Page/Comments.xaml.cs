using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace ComicsMaster.Page
{

    public partial class Comments : UserControl
    {
        int Row = 0;
        public string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString,
                      catalog = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
            IDCOMICSCOVER, IDUSERNAME;
        private async Task LoadDataComments()
        {
            Content.Children.Clear();
            var sqlConect = new SqlConnection(connectionString);
            await sqlConect.OpenAsync();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = $"SELECT * FROM COMICSCOMMENT WHERE IDCOMICSCOVER='{IDCOMICSCOVER}'";
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader reader = await command.ExecuteReaderAsync();
                while (reader.Read())
                {
                    string name = reader.GetString(2);
                    string message = reader.GetString(3);
                    SendComics sendComics = new SendComics();
                    Grid.SetRow(sendComics, Row);
                    sendComics.CommentMessage.Text = message;
                    sendComics.NameUserSender.Text = name;
                    Content.Children.Add(sendComics);
                    Content.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                    Row++;

                };
                Row = 0;
            }
        }
        
        public Comments(string IDCOMICSCOVER1,string IDUSERNAME1)
        {
            InitializeComponent();
            IDCOMICSCOVER = IDCOMICSCOVER1;
            IDUSERNAME=IDUSERNAME1;
            LoadDataComments().GetAwaiter();
        }
       private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var connect = new SqlConnection(connectionString);
            connect.Open();
            string sql = string.Format("Insert Into COMICSCOMMENT" +
            "(IDCOMICSCOVER,USERNAME,COMMENTMESSAGE) Values(@IDCOMICSCOVER,@IDUSERNAME,@COMMENTMESSAGE)");
            using (SqlCommand cmd = new SqlCommand(sql, connect))
            {
                // Добавить параметры 
                cmd.Parameters.AddWithValue("@IDCOMICSCOVER", IDCOMICSCOVER);
                cmd.Parameters.AddWithValue("@IDUSERNAME", IDUSERNAME);
                cmd.Parameters.AddWithValue("@COMMENTMESSAGE", Message.Text);
                cmd.ExecuteNonQuery();
            }
            connect.Close();
            LoadDataComments().GetAwaiter();
        }
    }
}
