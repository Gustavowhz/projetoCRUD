using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetoCRUD
{
    public static class DataSimulator
    {
        public static List<User> Users { get; } = new List<User>();

        public class User
        {
            public string Nome { get; set; }
            public string Senha { get; set; }
        }
    }
}
