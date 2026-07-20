using Microsoft.EntityFrameworkCore;

namespace EfCoreRelationshipBasics;

/// <summary>
/// アプリケーションのDbContext
/// </summary>
/// <remarks>
/// Book と Review の1対多関連は、規約（Book.Reviews コレクション＋Review.BookId）だけで
/// 推論されるため、関連に関する Fluent API の設定はゼロでよい。
/// </remarks>
public class AppDbContext : DbContext
{
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Review> Reviews => Set<Review>();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer(
            "Server=(localdb)\\MSSQLLocalDB;Database=EfCoreRelationshipBasics;Trusted_Connection=True;TrustServerCertificate=True");
}
