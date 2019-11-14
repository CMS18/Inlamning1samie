﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace samiesbank.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public List<Account> Accounts { get; set; }
    }
}
