using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper.Imersao.Models
{
    public class Carrer
    {
        public Carrer()
        {
            Items = new List<CarrerItem>();
        }
        public Guid Id { get; set; }
        public string Title { get; set; }
        public IList<CarrerItem> Items { get; set; }
    }
}
