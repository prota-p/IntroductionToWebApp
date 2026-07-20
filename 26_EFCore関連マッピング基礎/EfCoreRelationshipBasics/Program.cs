using EfCoreRelationshipBasics;
using Microsoft.EntityFrameworkCore;

// 何度でも実行できるよう、毎回データベースを作り直す
await using (var db1 = new AppDbContext())
{
    await db1.Database.EnsureDeletedAsync();
    await db1.Database.EnsureCreatedAsync();

    Console.WriteLine("=== 1. 生成されたテーブル定義を確認する ===");
    // 設定ゼロ（規約だけ）で、ReviewsテーブルにFK列と外部キー制約ができている
    Console.WriteLine(db1.Database.GenerateCreateScript());

    Console.WriteLine();
    Console.WriteLine("=== 2. Reviewを2件持つBookを保存する ===");
    Book book = new Book { Title = "C#プログラミング入門" };
    book.Reviews.Add(new Review { Text = "とても分かりやすかった" });
    book.Reviews.Add(new Review { Text = "サンプルコードが豊富" });
    db1.Books.Add(book); // 親をAddすると、コレクション内の子も一緒に保存される
    await db1.SaveChangesAsync();
    Console.WriteLine("保存しました");
    // 保存結果（ReviewsテーブルのBookId列にFKが入っている様子）は、
    // SQL Serverオブジェクトエクスプローラーなどでテーブルの中身を直接確認する
}

Console.WriteLine();
Console.WriteLine("=== 3. Includeなしで読むと、関連は読み込まれない ===");
// 保存に使ったDbContextは保存したオブジェクトを覚えているため、
// 「DBから読み戻す」動きを確認するには別のDbContextを使う
await using (var db2 = new AppDbContext())
{
    Book withoutInclude = await db2.Books.SingleAsync();
    Console.WriteLine($"{withoutInclude.Title} / Reviews.Count = {withoutInclude.Reviews.Count}（空のまま）");
}

Console.WriteLine();
Console.WriteLine("=== 4. Includeで、Reviewごと一気に読み戻す ===");
await using (var db3 = new AppDbContext())
{
    Book loaded = await db3.Books
        .Include(b => b.Reviews) // 親子をまとめて（＝集約を単位に）読み出す
        .SingleAsync();
    Console.WriteLine($"{loaded.Title} / Reviews.Count = {loaded.Reviews.Count}");
    foreach (Review review in loaded.Reviews)
    {
        Console.WriteLine($"  - {review.Text}");
    }
}
