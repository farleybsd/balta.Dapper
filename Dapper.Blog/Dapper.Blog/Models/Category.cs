using Dapper.Contrib.Extensions;
using System.Collections.Generic;

namespace Dapper.Blog.Models
{
    [Table("[Category]")]
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        //public List<Post> Posts { get; set; } // Relacionamento Uma Categoria tem  N Post
    }
}
