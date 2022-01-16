using System.Data.Entity;
using TestTele2._2.DatBase;

namespace TestTele2._2.Data
{
    public class DbTestTele2Context : DbContext
    {

        public DbTestTele2Context()
            : base(@"Server=.\SQLEXPRESS;Database=testtele2;Trusted_Connection=true")
        {
            Database.SetInitializer(
                new DropCreateDatabaseIfModelChanges<DbTestTele2Context>());

        }

        public DbSet<Persone> Persones { get; set; }
    }
}
