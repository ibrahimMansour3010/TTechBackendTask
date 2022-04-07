using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTechTash.Domain.Models
{
    public class Product:BaseModel
    {
        [MaxLength]
        public string Name { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Image { get; set; }
    }
}
