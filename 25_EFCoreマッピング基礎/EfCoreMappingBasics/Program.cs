using EfCoreMappingBasics;
using Microsoft.EntityFrameworkCore;

// 何度でも実行できるよう、毎回データベースを作り直す
await using (var db1 = new AppDbContext())
{
    await db1.Database.EnsureDeletedAsync();
    await db1.Database.EnsureCreatedAsync();

    Console.WriteLine("=== 1. 生成されたテーブル定義を確認する ===");
    Console.WriteLine(db1.Database.GenerateCreateScript());

    Console.WriteLine();
    Console.WriteLine("=== 2. Bookを保存する ===");
    Book book = new Book
    {
        Title = "C#プログラミング入門",
        Price = 2800,
        Isbn = new Isbn("978-4-1234-5678-9"),
    };
    db1.Books.Add(book);
    await db1.SaveChangesAsync();
    Console.WriteLine($"保存しました: Id={book.Id}（DBが自動採番した値）");

    // DBに入っている生の値を確認するため、ここだけあえてSQLで直接取り出す
    string rawIsbn = (await db1.Database
        .SqlQuery<string>($"SELECT Isbn FROM Books")
        .ToListAsync()).Single();
    Console.WriteLine($"DB上の生の値: \"{rawIsbn}\"（ただの文字列として保存されている）");
}

Console.WriteLine();
Console.WriteLine("=== 3. 読み出すとIsbnは独自型に戻っている ===");
// 別のDbContextで読み直し、DBからの往復を確認する
await using (var db2 = new AppDbContext())
{
    Book loaded = await db2.Books.SingleAsync();
    Console.WriteLine($"{loaded.Title} / {loaded.Price}円");
    Console.WriteLine($"Isbn: {loaded.Isbn}（C#上の型: {loaded.Isbn.GetType().Name}）");
}
