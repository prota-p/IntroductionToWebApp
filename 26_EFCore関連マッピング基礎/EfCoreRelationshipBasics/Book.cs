namespace EfCoreRelationshipBasics;

/// <summary>
/// 書籍エンティティ（1対多の「1」側＝親）
/// </summary>
public class Book
{
    public int Id { get; set; }

    public string Title { get; set; } = "";

    // ナビゲーションプロパティ
    public List<Review> Reviews { get; set; } = [];
}
