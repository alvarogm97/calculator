using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVCalculator.Models;

namespace MVCalculator.Models
{
    public class MVCalculatorContext : DbContext
    {
        public MVCalculatorContext (DbContextOptions<MVCalculatorContext> options)
            : base(options)
        {
        }

        public DbSet<MVCalculator.Models.Calculator> Calculator { get; set; }

        public DbSet<MVCalculator.Models.Journal> Journal { get; set; }
    }
}
