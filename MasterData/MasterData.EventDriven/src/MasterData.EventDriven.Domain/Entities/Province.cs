using MasterData.EventDriven.Domain.Events;

namespace MasterData.EventDriven.Domain.Entities;

public class Province
{
    private readonly List<DomainEvent> _events = new();

    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public bool IsDeleted { get; private set; }

    public IReadOnlyCollection<DomainEvent> Events => _events.AsReadOnly();

    // Private constructor for EF Core
    private Province() { }

    private Province(string name)
    {
        Name = name;
        CreatedAt = DateTime.UtcNow;
        IsDeleted = false;
    }

    public static Province Register(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("نام استان نمی‌تواند خالی باشد", nameof(name));

        if (name.Length > 50)
            throw new ArgumentException("نام استان نمی‌تواند بیشتر از 50 کاراکتر باشد", nameof(name));

        var province = new Province(name.Trim());
        return province;
    }

    public void SetId(int id)
    {
        Id = id;
        AddEvent(new ProvinceRegisteredEvent(Id, Name, CreatedAt));
    }

    public void UpdateName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new ArgumentException("نام استان نمی‌تواند خالی باشد", nameof(newName));

        if (newName.Length > 50)
            throw new ArgumentException("نام استان نمی‌تواند بیشتر از 50 کاراکتر باشد", nameof(newName));

        var oldName = Name;
        Name = newName.Trim();
        UpdatedAt = DateTime.UtcNow;

        AddEvent(new ProvinceNameUpdatedEvent(Id, oldName, Name, UpdatedAt.Value));
    }

    public void Delete()
    {
        IsDeleted = true;
        UpdatedAt = DateTime.UtcNow;

        AddEvent(new ProvinceDeletedEvent(Id, Name, UpdatedAt.Value));
    }

    private void AddEvent(DomainEvent @event)
    {
        _events.Add(@event);
    }

    public void ClearEvents()
    {
        _events.Clear();
    }
}
