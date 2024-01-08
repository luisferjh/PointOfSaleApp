﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; }        
        public string Name { get; set; }      
        public string LastName { get; set; }        
        public string Identification { get; set; }       
        public string Email { get; set; }        
        public string Phone { get; set; }
        public DateTime Date { get; set; }
        public bool State { get; set; }
    }
}
