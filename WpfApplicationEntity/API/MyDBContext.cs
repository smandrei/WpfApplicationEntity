using System.Data.Entity;

namespace WFAEntity.API
{
    class MyDBContext : DbContext
    {
        public MyDBContext() : base("DbConnectString")
        {
        }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
