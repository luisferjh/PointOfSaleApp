using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ResultService
    {
        public bool IsSuccess { get; set; }
        public object Data { get; set; }
        public string ErrorMessage { get; set; }
        
    }
}
