using Dapper.Desafio.Models;
using Dapper.Desafio.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Desafio.Screens.TagScreens
{
    public static class CreateTagScreen
    {
        public static void Load()
        {
            Console.Clear();
            Console.WriteLine("Nova de tag");
            Console.WriteLine("-------------");
            Console.WriteLine("Name: ");
            var name = Console.ReadLine();
            Console.WriteLine("Slug: ");
            var slug= Console.ReadLine();
            Create(new Tag { Name=name,Slug=slug});
            Console.ReadKey();
            MenuTagScreen.Load();
        }

        public static void Create(Tag tag)
        {
            try
            {
                var repository = new Repository<Tag>(DataBase.Connection);
                repository.Create(tag);
                Console.WriteLine("Tag Cadastrada Com Sucesso");
            }
            catch (Exception e)
            {
                Console.WriteLine("Nao Foi Possivel Salvar a Tag");
                Console.WriteLine(e.Message);
            }
        }
    }
}
