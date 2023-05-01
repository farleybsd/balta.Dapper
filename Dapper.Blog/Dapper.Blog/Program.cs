using Dapper.Blog.Models;
using Dapper.Blog.Repositories;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;
using System;

namespace Dapper.Blog
{
    internal class Program
    {
        private const string CONNECTION_STRING = @"Server=localhost,1433;Database=Blog;User ID=sa;Password=1q2w3e4r@#$";
        static void Main(string[] args)
        {
            var connectionString = new SqlConnection(CONNECTION_STRING);
            connectionString.Open();
            //CreateUsers(connectionString);
            ReadWithRoles(connectionString);
           // ReadUsers(connectionString);
            //ReadRoles(connectionString);
            connectionString.Close();
        }

        public static void ReadUsers(SqlConnection connection)
        {
            var repository = new Repository<User>(connection);
            var users = repository.Get();

            foreach (var user in users)
            {
                Console.WriteLine(user.Name);
                foreach (var role in user.Roles)
                {
                    Console.WriteLine($"- {role.Name}");
                }
            }
            

        }
        public static void CreateUsers(SqlConnection connection)
        {
            var user = new User
            {
                Bio = "bio",
                Email = "email@balta.io",
                Image = "imagen",
                Name = "name",
                PasswordHash = "HASH",
                Slug = "slug"
            };

            var repository = new Repository<User>(connection);
            repository.Create(user);
        }

        private static void ReadWithRoles(SqlConnection connection)
        {
            var repository = new UserRepositoryEspeficico(connection);
            var users = repository.GetWithRoles();

            foreach (var user in users)
            {
                Console.WriteLine(user.Email);
                foreach (var role in user.Roles) Console.WriteLine($" - {role.Slug}");
            }
        }
        public static void ReadRoles(SqlConnection connection)
        {
            var repository = new RoleRepository(connection);
            var roles = repository.Get();

            foreach (var role in roles)
                Console.WriteLine(role.Name);


        }

        public static void ReadUser()
        {
            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                var user = connection.Get<User>(1);
                Console.WriteLine(user.Name);

            }
        }
        public static void CreateUser()
        {
            var user = new User
            {
                Bio = "Equipe balta.io",
                Email = "hello@balta.io",
                Image = "https://....",
                Name = "Equipe balta.io",
                PasswordHash = "HASH",
                Slug = "equipe-balta"
            };
            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Insert<User>(user);
                Console.WriteLine("Cadastro Realizado Com Sucesso");

            }
        }

        public static void UpdateUser()
        {
            var user = new User
            {
                Id = 2,
                Bio = "Equipe | balta.io",
                Email = "hello@balta.io",
                Image = "https://....",
                Name = "Equipe de suporte balta.io",
                PasswordHash = "HASH",
                Slug = "equipe-balta"
            };
            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                connection.Update<User>(user);
                Console.WriteLine("Atualizacao Realizado Com Sucesso");

            }
        }
        public static void DeleteUser()
        {

            using (var connection = new SqlConnection(CONNECTION_STRING))
            {
                var user = connection.Get<User>(2);
                connection.Delete<User>(user);
                Console.WriteLine("Remocao Realizado Com Sucesso");

            }
        }
    }
}
