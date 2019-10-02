using System;
using Microsoft.EntityFrameworkCore;

namespace DB
{
    public class ConnectionContext : DbContext
    {
        #region Public Properties

        public DbSet<Commands> Commands { get; set; }

        #endregion
    }

    public ApplicationDbContext() {

    }

    protected override void OnConfigure(DbContextOptionsBuilder OptionsBuilder) {
        base.OnConfigure(OptionsBuilder);

        OptionsBuilder.UseSqlServer("Server=192.168.1.109;Database=Botty;User Id=Botty; Password=Password123;MultipleActiveResultSets=true");
    }

    protected override void OnModelCreatin(ModelBuilder modelBuilder) {
        base.OnModelCreatin(modelBuilder);
    }

    public class Commands {
        public int id { get; set; }
        public string CommandName { get; set; }
        public string ComamndDescription { get; set; }
    }
}
