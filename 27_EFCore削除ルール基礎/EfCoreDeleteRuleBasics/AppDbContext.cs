using Microsoft.EntityFrameworkCore;

namespace EfCoreDeleteRuleBasics;

/// <summary>
/// アプリケーションのDbContext
/// </summary>
/// <remarks>
/// 必須（non-nullable FK）の1対多関連では、削除ルールの既定は Cascade。
/// このままの状態では既定（Cascade）で動作する（記事前半の手順）。
/// 記事後半の「Restrictに変えると削除できなくなる」を試す場合は、
/// 下の OnModelCreating のコメントを解除して実行する。
/// </remarks>
public class AppDbContext : DbContext
{
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Review> Reviews => Set<Review>();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer(
            "Server=(localdb)\\MSSQLLocalDB;Database=EfCoreDeleteRuleBasics;Trusted_Connection=True;TrustServerCertificate=True");

    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{
    //    // 削除ルールを既定（Cascade）からRestrictに変更する
    //    modelBuilder.Entity<Book>()
    //        .HasMany(b => b.Reviews)
    //        .WithOne()
    //        .OnDelete(DeleteBehavior.Restrict);
    //}
}
