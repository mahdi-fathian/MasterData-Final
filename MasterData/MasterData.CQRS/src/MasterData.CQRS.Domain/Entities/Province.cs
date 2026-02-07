namespace MasterData.CQRS.Domain.Entities;

/// <summary>
/// موجودیت استان - Province Entity
/// </summary>
public class Province
{
    /// <summary>
    /// شناسه یکتا - Unique Identifier
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    /// نام استان - Province Name
    /// </summary>
    public string Name { get; private set; } = string.Empty;

    /// <summary>
    /// تاریخ ایجاد - Created Date
    /// </summary>
    public DateTime CreatedAt { get; private set; }

    /// <summary>
    /// تاریخ آخرین بروزرسانی - Last Updated Date
    /// </summary>
    public DateTime? UpdatedAt { get; private set; }

    // Private constructor for EF Core
    private Province() { }

    /// <summary>
    /// سازنده برای ایجاد استان جدید - Constructor for creating new province
    /// </summary>
    public Province(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("نام استان نمی‌تواند خالی باشد", nameof(name));

        if (name.Length > 50)
            throw new ArgumentException("نام استان نمی‌تواند بیشتر از 50 کاراکتر باشد", nameof(name));

        Name = name.Trim();
        CreatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// بروزرسانی نام استان - Update province name
    /// </summary>
    public void UpdateName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new ArgumentException("نام استان نمی‌تواند خالی باشد", nameof(newName));

        if (newName.Length > 50)
            throw new ArgumentException("نام استان نمی‌تواند بیشتر از 50 کاراکتر باشد", nameof(newName));

        Name = newName.Trim();
        UpdatedAt = DateTime.UtcNow;
    }
}
