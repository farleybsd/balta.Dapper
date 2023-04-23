using Dapper.Desafio.Models;
using Dapper.Desafio.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Desafio.Screens.TagScreens
{
    public static class ListTagScreen
    {
        public static void Load() { List(); }
        private static void List()
        {
            var repository = new Repository<Tag>(DataBase.Connection);
        }
    }
}
