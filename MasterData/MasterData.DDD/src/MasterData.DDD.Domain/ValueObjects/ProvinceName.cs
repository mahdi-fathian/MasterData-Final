using MasterData.DDD.Domain.SeedWork;

namespace MasterData.DDD.Domain.ValueObjects;

/// <summary>
/// شیء ارزشی نام استان - Province Name Value Object
/// </summary>
public class ProvinceName : ValueObject
{
    public string Value { get; private set; }

    private ProvinceName() { Value = string.Empty; }

    private ProvinceName(string value)
    {
        Value = value;
    }

    public static ProvinceName Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("نام استان نمی‌تواند خالی باشد", nameof(name));

        var trimmedName = name.Trim();

        if (trimmedName.Length < 2)
            throw new ArgumentException("نام استان باید حداقل 2 کاراکتر باشد", nameof(name));

        if (trimmedName.Length > 50)
            throw new ArgumentException("نام استان نمی‌تواند بیشتر از 50 کاراکتر باشد", nameof(name));

        return new ProvinceName(trimmedName);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;

    public static implicit operator string(ProvinceName provinceName) => provinceName.Value;
}
