using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTechTask.Domain.DTOs
{
    public class ProductDTO
    {
        public class Add
        {
            public string Name { get; set; }
            public int Quantity { get; set; }
            public float Price { get; set; }
            public DateTime ExpirationDate { get; set; }
            public IFormFile ImageFile { get; set; }
        }
        public class Edit:Add
        {
            public int Id { get; set; }
            public string Image { get; set; }
        }
        public class Get
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Quantity { get; set; }
            public float Price { get; set; }
            public DateTime ExpirationDate { get; set; }
            public string Image { get; set; }
        }
    }
}
