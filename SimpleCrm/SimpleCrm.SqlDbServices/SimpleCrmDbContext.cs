﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCrm.SqlDbServices
{
    public class SimpleCrmDbContext : DbContext
    {
        public SimpleCrmDbContext(DbContextOptions<SimpleCrmDbContext> options)
            : base(options) { }

        public DbSet<Customer> Customer { get; set; }
        
    }
}
