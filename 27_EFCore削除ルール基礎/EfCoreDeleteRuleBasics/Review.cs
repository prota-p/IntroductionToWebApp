namespace EfCoreDeleteRuleBasics;

/// <summary>
/// レビューエンティティ（1対多の「多」側＝子）
/// </summary>
public class Review
{
    public int Id { get; set; }

    // 外部キープロパティ: 規約により Books テーブルへのFK列になる
    public int BookId { get; set; }

    public string Text { get; set; } = "";
}
