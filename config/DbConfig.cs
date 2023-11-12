using YorozuyaServer.entity;
using Microsoft.EntityFrameworkCore;

namespace YorozuyaServer.config;

public class DbConfig : DbContext
{
    public DbSet<UserInfo> UserInfos { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Reply> Replies { get; set; }
    public DbSet<Like> Likes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL("server=mysql.sqlpub.com;port=3306;database=yorozuya;uid=yorozuya;pwd=faf3a1b7cd40cd41;CharSet=utf8mb4;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserInfo>(entity =>
        {
            entity.ToTable("user_info");
        });
        
        modelBuilder.Entity<Post>(entity =>
        {
            entity.ToTable("post");
        });
        
        modelBuilder.Entity<Reply>(entity =>
        {
            entity.ToTable("reply");
        });
        
        modelBuilder.Entity<Like>(entity =>
        {
            entity.ToTable("like");
        });
    }

    // 测试连接
    public static void TestConnection()
    {
        using var db = new DbConfig();
        db.Database.EnsureCreated();
        db.Database.EnsureDeleted();
    }
}