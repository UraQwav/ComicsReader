using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicsMaster.Models
{
    interface IRepository
    {
        int FindUser(string login, string password);
    }
}
