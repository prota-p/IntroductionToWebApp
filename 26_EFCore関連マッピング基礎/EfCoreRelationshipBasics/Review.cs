namespace EfCoreRelationshipBasics;

/// <summary>
/// レビューエンティティ（1対多の「多」側＝子）
/// </summary>
public class Review
{
    public int Id { get; set; }

    // 外部キープロパティ: 規約により Books テーブルへのFK列になる
    // (ただし、別途ナビゲーションプロパティがないとただの列になってしまう)
    public int BookId { get; set; }

    public string Text { get; set; } = "";
}
