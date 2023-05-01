using Dapper.Contrib.Extensions;
using System.Collections.Generic;

namespace Dapper.Blog.Models
{
    [Table("[Post]")]
    public class Post
    {
        public int Id { get; set; }
        public string Name { get; set; }
       // public int CategoryId { get; set; } // Relacionamento Com Categoria Um Post uma categoria
    }
}
