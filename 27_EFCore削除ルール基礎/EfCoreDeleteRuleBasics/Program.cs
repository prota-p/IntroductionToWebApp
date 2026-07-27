using EfCoreDeleteRuleBasics;
using Microsoft.EntityFrameworkCore;

// 何度でも実行できるよう、毎回データベースを作り直す
await using (var db1 = new AppDbContext())
{
    await db1.Database.EnsureDeletedAsync();
    await db1.Database.EnsureCreatedAsync();

    Console.WriteLine("=== 1. Reviewを2件持つBookを保存する ===");
    Book book = new Book { Title = "C#プログラミング入門" };
    book.Reviews.Add(new Review { Text = "とても分かりやすかった" });
    book.Reviews.Add(new Review { Text = "サンプルコードが豊富" });
    db1.Books.Add(book);
    await db1.SaveChangesAsync();
    Console.WriteLine("保存しました");
}

Console.WriteLine();
Console.WriteLine("=== 2. Bookを削除してみる ===");
await using (var db2 = new AppDbContext())
{
    // 子Reviewは読み込まず、親Bookだけを削除する
    Book book = await db2.Books.SingleAsync();
    db2.Books.Remove(book);
    try
    {
        await db2.SaveChangesAsync();
        Console.WriteLine("削除できました");
    }
    catch (DbUpdateException ex)
    {
        // 削除ルールによっては失敗するので、結果を確認できるようにしておく
        Console.WriteLine($"削除に失敗しました: {ex.InnerException?.Message}");
    }
}

Console.WriteLine();
Console.WriteLine("=== 3. 残っている行数を確認する ===");
await using (var db3 = new AppDbContext())
{
    Console.WriteLine($"Books: {await db3.Books.CountAsync()} 件 / Reviews: {await db3.Reviews.CountAsync()} 件");
}
