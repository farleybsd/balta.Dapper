using Dapper.Imersao.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dapper.Imersao
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string connectionString = "Server=localhost,1433;Database=balta;User ID=sa;Password=1q2w3e4r@#$";

            using (var connection = new SqlConnection(connectionString))
            {
                //UpdateCategory(connection);
                //CreateManyCategory(connection);
                //ListarCategorias(connection);
                //CreateCategory(connection);
                //ExecuteProcedure(connection);
                // ExecuteReadProcedure(connection);
                //ExeculteEscalar(connection);
                //ReadView(connection);
                //RelacionamenoUmParaUm(connection);
                //RelacionamenoUmParaMuitos(connection);
                //MuitosParaMuitos(connection);
                //SelectIn(connection);
                // Like(connection, "backend");
                // Transaction(connection);
            }

            Console.ReadKey();
        }
        static void ListarCategorias(SqlConnection connection)
        {
            var categorias = connection.Query<Category>("SELECT [Id], [Title] FROM [Category]");

            foreach (var item in categorias)
            {
                Console.WriteLine($"{item.Id} - {item.Title}");
            }
        }
        static void CreateCategory(SqlConnection connection)
        {
            var category = new Category();
            category.Id = Guid.NewGuid();
            category.Title = "Amazon AWS";
            category.Url = "amazon";
            category.Description = "Categoria destinada a serviços do AWS";
            category.Order = 8;
            category.Summary = "AWS Cloud";
            category.Featured = false;

            var insertSql = @"INSERT INTO 
                    [Category] 
                VALUES(
                    @Id, 
                    @Title, 
                    @Url, 
                    @Summary, 
                    @Order, 
                    @Description, 
                    @Featured)";
            var rows = connection.ExecuteScalar(insertSql, new
            {
                Id = category.Id,
                Title = category.Title,
                Url = category.Url,
                Summary = category.Summary,
                Order = category.Order,
                Description = category.Description,
                Featured = category.Featured
            });
            Console.WriteLine($"{rows} linhas inseridas");
        }
        static void UpdateCategory(SqlConnection connection)
        {
            var updateQuery = "UPDATE [Category] SET [Title]=@title WHERE [Id]=@id";
            var rows = connection.Execute(updateQuery, new
            {
                id = new Guid("af3407aa-11ae-4621-a2ef-2028b85507c4"),
                title = "Frontend 2021"
            });

            Console.WriteLine($"{rows} registros atualizadas");
        }
        static void DeleteCategory(SqlConnection connection)
        {
            var deleteQuery = "DELETE [Category] WHERE [Id]=@id";
            var rows = connection.Execute(deleteQuery, new
            {
                id = new Guid("ea8059a2-e679-4e74-99b5-e4f0b310fe6f"),
            });

            Console.WriteLine($"{rows} registros excluídos");
        }
        static void CreateManyCategory(SqlConnection connection)
        {
            var category = new Category();
            category.Id = Guid.NewGuid();
            category.Title = "Amazon AWS";
            category.Url = "amazon";
            category.Description = "Categoria destinada a serviços do AWS";
            category.Order = 8;
            category.Summary = "AWS Cloud";
            category.Featured = false;

            var category2 = new Category();
            category2.Id = Guid.NewGuid();
            category2.Title = "Categoria Nova";
            category2.Url = "categoria-nova";
            category2.Description = "Categoria nova";
            category2.Order = 9;
            category2.Summary = "Categoria";
            category2.Featured = true;

            var insertSql = @"INSERT INTO 
                    [Category] 
                VALUES(
                    @Id, 
                    @Title, 
                    @Url, 
                    @Summary, 
                    @Order, 
                    @Description, 
                    @Featured)";
            var rows = connection.Execute(insertSql, new[] {
            new {
                Id = category.Id,
                Title = category.Title,
                Url = category.Url,
                Summary = category.Summary,
                Order = category.Order,
                Description = category.Description,
                Featured = category.Featured
            },
             new {
                Id = category2.Id,
                Title = category2.Title,
                Url = category2.Url,
                Summary = category2.Summary,
                Order = category2.Order,
                Description = category2.Description,
                Featured = category2.Featured
            }
            });
            Console.WriteLine($"{rows} linhas inseridas");
        }
        static void ExecuteProcedure(SqlConnection connection)
        {
            var procedure = "[spDeleteStudent]";
            var parms = new { StudentId = "B0EBB704-D537-42EB-AFDD-20A2AC52D08F" };
            var affectedRows = connection.Execute(procedure,
                                                  parms,
                                                  commandType: System.Data.CommandType.StoredProcedure);
            Console.WriteLine($"{affectedRows} linhas afetadas");
        }
        static void ExecuteReadProcedure(SqlConnection connection)
        {
            var procedure = "[spGetCoursesByCategory]";
            var parms = new { CategoryId = "09ce0b7b-cfca-497b-92c0-3290ad9d5142" };
            var courses = connection.Query(procedure,
                                                  parms,
                                                  commandType: System.Data.CommandType.StoredProcedure);
            foreach (var item in courses)
            {
                Console.WriteLine(item.Id);
            }
        }
        static void ExeculteEscalar(SqlConnection connection)
        {
            var category = new Category();
            category.Title = "Amazon AWS";
            category.Url = "amazon";
            category.Description = "Categoria destinada a serviços do AWS";
            category.Order = 8;
            category.Summary = "AWS Cloud";
            category.Featured = false;

            var insertSql = @"INSERT INTO 
                    [Category] 
                OUTPUT inserted.[Id]
                VALUES(
                    NEWID(), 
                    @Title, 
                    @Url, 
                    @Summary, 
                    @Order, 
                    @Description, 
                    @Featured)";
            var id = connection.ExecuteScalar<Guid>(insertSql, new
            {
                Title = category.Title,
                Url = category.Url,
                Summary = category.Summary,
                Order = category.Order,
                Description = category.Description,
                Featured = category.Featured
            });
            Console.WriteLine($"Categoria Inserida foi:{id} ... ");
        }
        static void ReadView(SqlConnection connection)
        {
            var sql = "SELECT * FROM [vwCourses ]";
            var coruses = connection.Query(sql);

            foreach (var item in coruses)
            {
                Console.WriteLine($"{item.Id} - {item.Title}");
            }
        }
        static void RelacionamenoUmParaUm(SqlConnection connection)
        {
            var sql = @"Select 
                        *
                        from CareerItem 
                             INNER join Course 
                                on CareerItem.CourseId=Course.Id";

            //Primeiro Item para o join e CarrerItem depois o segundo item e Course
            // e o Terceiro Item e o retorno CarrerItem
            var items = connection.Query<CarrerItem, Course, CarrerItem>(
                sql,
                (carrerItem, course) =>
                {
                    carrerItem.Course = course;
                    return carrerItem;
                }, splitOn: "Id");

            foreach (var item in items)
            {
                Console.WriteLine($"{item.Title} - Curso: {item.Course.Title}");
            }
        }
        static void RelacionamenoUmParaMuitos(SqlConnection connection)
        {
            var sql = @"    SELECT 
                            [Career].[Id],
                            [Career].[Title],
                            [CareerItem].[CareerId],
                            [CareerItem].[Title]
                            FROM 
                                [Career] 
                                    INNER JOIN 
                                    [CareerItem] ON [CareerItem].[CareerId] = [Career].[Id]
                            ORDER BY
                                [Career].[Title]";

            //Primeiro Item para o join e CarrerItem depois o segundo item e Course
            // e o Terceiro Item e o retorno CarrerItem
            var careers = new List<Carrer>();
            var items = connection.Query<Carrer, CarrerItem, Carrer>(
                sql,
                (carrer, item) =>
                {
                    var car = careers.Where(x => x.Id == carrer.Id).FirstOrDefault();
                    if(car == null)
                    {
                        car = carrer;
                        car.Items.Add(item);
                        careers.Add(car);
                    } else
                    {
                        careers.Add(car);
                    }
                    return carrer;
                }, splitOn: "CareerId");

            foreach (var carrer in items)
            {
                Console.WriteLine($"{carrer.Title}");

                foreach (var item in carrer.Items)
                {
                    Console.WriteLine($" -  {item.Title}");
                }

            }
        }
        static void MuitosParaMuitos(SqlConnection connection) // QueryMutiple
        {
            var query = "SELECT * FROM [Category]; SELECT * FROM [Course]";

            using (var multi = connection.QueryMultiple(query))
            {
                var categorias = multi.Read<Category>();
                var courses = multi.Read<Course>();

                foreach (var item in categorias)
                {
                    Console.WriteLine(item.Title);
                }
                foreach (var item in courses)
                {
                    Console.WriteLine(item.Title);
                }
            }
        }
        static void SelectIn(SqlConnection connection)
        {
            var query = @"select * from Career where [Id] IN @Id";

            var items = connection.Query<Carrer>(query, new
            {
                Id = new[]{
                    "4327ac7e-963b-4893-9f31-9a3b28a4e72b",
                    "e6730d1c-6870-4df3-ae68-438624e04c72"
                }
            });

            foreach (var item in items)
            {
                Console.WriteLine(item.Title);
            }

        }
        static void Like(SqlConnection connection, string term)
        {
            var query = @"SELECT * FROM [Course] WHERE [Title] LIKE @exp";

            var items = connection.Query<Course>(query, new
            {
                exp = $"%{term}%"
            });

            foreach (var item in items)
            {
                Console.WriteLine(item.Title);
            }
        }

        static void Transaction(SqlConnection connection)
        {
            var category = new Category();
            category.Id = Guid.NewGuid();
            category.Title = "Minha categoria que não";
            category.Url = "amazon";
            category.Description = "Categoria destinada a serviços do AWS";
            category.Order = 8;
            category.Summary = "AWS Cloud";
            category.Featured = false;

            var insertSql = @"INSERT INTO 
                    [Category] 
                VALUES(
                    @Id, 
                    @Title, 
                    @Url, 
                    @Summary, 
                    @Order, 
                    @Description, 
                    @Featured)";

            connection.Open();
            using (var transaction = connection.BeginTransaction())
            {
                var rows = connection.Execute(insertSql, new
                {
                    category.Id,
                    category.Title,
                    category.Url,
                    category.Summary,
                    category.Order,
                    category.Description,
                    category.Featured
                }, transaction);

                transaction.Commit();
                // transaction.Rollback();

                Console.WriteLine($"{rows} linhas inseridas");
            }
        }
    }
}

