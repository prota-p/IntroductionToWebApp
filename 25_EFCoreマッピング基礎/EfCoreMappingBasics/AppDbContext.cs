using Microsoft.EntityFrameworkCore;

namespace EfCoreMappingBasics;

/// <summary>
/// アプリケーションのDbContext
/// </summary>
/// <remarks>
/// 規約で決まるものは規約に任せ、規約で表せないものだけ Fluent API で設定する。
/// このアプリで規約から外れるのは「独自型 Isbn の変換」の1点だけ。
/// </remarks>
public class AppDbContext : DbContext
{
    public DbSet<Book> Books => Set<Book>();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer(
            "Server=(localdb)\\MSSQLLocalDB;Database=EfCoreMappingBasics;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            // 独自型 Isbn はそのままではテーブルに保存できないため、
            // 保存時: Isbn → string ／ 読み出し時: string → Isbn の変換を指定する
            entity.Property(e => e.Isbn)
                .HasConversion(
                    isbn => isbn.Value,
                    value => new Isbn(value));
        });
    }
}
