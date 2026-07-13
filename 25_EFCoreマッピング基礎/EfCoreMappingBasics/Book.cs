namespace EfCoreMappingBasics;

/// <summary>
/// 書籍エンティティ
/// </summary>
/// <remarks>
/// Id・Title・Price は EF Core の規約（Convention）だけでテーブルにマッピングできる。
/// 独自型の Isbn だけは規約では変換できないため、AppDbContext 側で Fluent API の設定が必要になる。
/// </remarks>
public class Book
{
    // 規約: 「Id」という名前のプロパティが主キーになる（int なら自動採番）
    public int Id { get; set; }

    // 規約: プロパティ名がそのまま列名になり、C#の型からDBの型が推論される
    public string Title { get; set; } = "";

    public int Price { get; set; }

    // 独自型: 規約では変換できない → AppDbContext で HasConversion を設定
    public Isbn Isbn { get; set; } = null!;
}
