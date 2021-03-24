using System.Data.Entity;
//using SQLite.CodeFirst.Utility;

namespace WFAEntity.API
{
    class MyDBContext : DbContext
    {
        public MyDBContext() : base("DbConnectStringSQLite")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<MyDBContext>(modelBuilder);
            //Database.SetInitializer(sqliteConnectionInitializer);
            System.Windows.MessageBox.Show("--");
        }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
