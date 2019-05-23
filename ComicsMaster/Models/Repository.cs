using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Data.SqlClient;

namespace ComicsMaster.Models
{
    class Repository : IRepository
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public int FindUser(string login, string password)
        {
            var sqlConect = new SqlConnection(connectionString);
            int sqlrezult;
            sqlConect.Open();
            string sq1 = string.Format("SELECT COUNT(*) from LOGINANDPASSWORD WHERE" + "( LOGINS=" + "@LOG" + " AND PASSWORDS= " + "@PAS)");
            using (var cmd = new SqlCommand(sq1, sqlConect))
            {
                cmd.Parameters.AddWithValue("@LOG", login);
                cmd.Parameters.AddWithValue("@PAS", password.GetHashCode());
                sqlrezult = (int)cmd.ExecuteScalar();
            }
            sqlConect.Close();

            return sqlrezult;
        }
    }
}
