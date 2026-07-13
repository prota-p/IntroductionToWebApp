namespace EfCoreMappingBasics;

/// <summary>
/// ISBN値オブジェクト（ハイフン付き13桁）
/// </summary>
/// <remarks>
/// 「不正な値のインスタンスは作れない」というルールを型に閉じ込める。
/// MentorApp の Email 型と同じ作りのミニ版。
/// </remarks>
public sealed record Isbn
{
    public string Value { get; }

    public Isbn(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("ISBNは必須です。");

        var digits = value.Replace("-", "");
        if (digits.Length != 13 || !digits.All(char.IsAsciiDigit))
            throw new ArgumentException($"ISBNの形式が不正です: {value}");

        Value = value.Trim();
    }

    public override string ToString() => Value;
}
