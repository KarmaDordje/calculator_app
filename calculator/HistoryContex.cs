using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Entity;

namespace calculator
{
    class HistoryContex : DbContext
    {
        public HistoryContex()
            : base("DBConnection")
        { }

        public DbSet<History> HistoryItems { get; set; }
    }
}
