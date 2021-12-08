using Microsoft.EntityFrameworkCore;

namespace VerifyEfExample.Api.Data;

public class DataContext : DbContext
{
    public DbSet<MyTable> MyTables { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
}