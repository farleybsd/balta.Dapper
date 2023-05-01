using Dapper.Desafio.Models;
using Dapper.Desafio.Repositories;
using System;

namespace Dapper.Desafio.Screens.TagScreens
{
    public static class DeleteTagScreen
    {
        public static void Load() {
            Console.Clear();
            Console.WriteLine("Deletar uma tag");
            Console.WriteLine("-------------");
            Console.WriteLine("Qual o Id da Tag que deseja remover? ");
            var id = Console.ReadLine();
            Delete(int.Parse(id));
            Console.ReadKey();
            MenuTagScreen.Load();
        }

        public static void Delete(int id)
        {
            try
            {
                var repository = new Repository<Tag>(DataBase.Connection);
                repository.Delete(id);
                Console.WriteLine("Tag Deletada Com Sucesso");
            }
            catch (Exception e)
            {
                Console.WriteLine("Nao Foi Possivel Deletar a Tag");
                Console.WriteLine(e.Message);
            }
        }
    }
}
