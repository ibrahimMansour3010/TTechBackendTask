using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTechTask.Domain.DTOs
{
    public class ApiResponse
    {
        public Boolean Status { get; set; } = false;
        public string Message { get; set; }
    }
    public class ApiResponse<T>: ApiResponse where T : class
    {
        public T Response { get; set; }
    }
}
