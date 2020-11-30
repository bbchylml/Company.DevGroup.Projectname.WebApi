using Company.DevGroup.Projectname.Models;
using Microsoft.EntityFrameworkCore;
/**
 * 1、Microsoft.EntityFrameworkCore              ef core的核心包
 * 2、Microsoft.EntityFrameworkCore.SqlServer    数据库驱动包
 * 3、Microsoft.EntityFrameworkCore.Tools        工具扩展包
 * 4、Microsoft.EntityFrameworkCore.Proxies      延迟加载实现包
 * 
 * 初始化数据库：Add-Migration Init
 * 更新到数据库：update-database Init
 * */
namespace Company.DevGroup.Projectname.Data
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
        }

        public DbSet<Todo> Todo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies(false);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Todo>(entity => {
                entity.Property(t => t.Name).HasDefaultValue("bbchylml");
            });
        }
    }
}
