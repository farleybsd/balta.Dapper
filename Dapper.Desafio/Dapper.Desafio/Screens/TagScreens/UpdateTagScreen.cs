using Dapper.Desafio.Models;
using Dapper.Desafio.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Desafio.Screens.TagScreens
{
    public static class UpdateTagScreen
    {
        public static void Load() {
            Console.Clear();
            Console.WriteLine("Atualizando uma tag");
            Console.WriteLine("-------------");
            Console.WriteLine("Id: ");
            var id = Console.ReadLine();
            Console.WriteLine("Name: ");
            var name = Console.ReadLine();
            Console.WriteLine("Slug: ");
            var slug = Console.ReadLine();
            Update(new Tag {
                Id = int.Parse(id),
                Name = name,
                Slug = slug });
            Console.ReadKey();
            MenuTagScreen.Load();
        }

        public static void Update(Tag tag)
        {
            try
            {
                var repository = new Repository<Tag>(DataBase.Connection);
                repository.Update(tag);
                Console.WriteLine("Tag Atualizada Com Sucesso");
            }
            catch (Exception e)
            {
                Console.WriteLine("Nao Foi Possivel Atualizar a Tag");
                Console.WriteLine(e.Message);
            }
        }
    }
}
