using MasterData.DDD.Domain.Events;
using MasterData.DDD.Domain.SeedWork;
using MasterData.DDD.Domain.ValueObjects;

namespace MasterData.DDD.Domain.Aggregates.ProvinceAggregate;

public class Province : Entity, IAggregateRoot
{
    public ProvinceName Name { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public bool IsActive { get; private set; }

    // Private constructor for EF Core
    private Province()
    {
        Name = ProvinceName.Create("Default");
    }

    private Province(ProvinceName name)
    {
        Name = name;
        CreatedAt = DateTime.UtcNow;
        IsActive = true;

        // رویداد دامنه: استان ایجاد شد
        AddDomainEvent(new ProvinceCreatedDomainEvent(this));
    }

    public static Province Create(string name)
    {
        var provinceName = ProvinceName.Create(name);
        return new Province(provinceName);
    }

    public void UpdateName(string newName)
    {
        var newProvinceName = ProvinceName.Create(newName);

        if (Name == newProvinceName)
            return;

        var oldName = Name.Value;
        Name = newProvinceName;
        UpdatedAt = DateTime.UtcNow;

        // رویداد دامنه: نام استان تغییر کرد
        AddDomainEvent(new ProvinceNameChangedDomainEvent(this, oldName, newName));
    }

    public void Deactivate()
    {
        if (!IsActive)
            return;

        IsActive = false;
        UpdatedAt = DateTime.UtcNow;

        // رویداد دامنه: استان غیرفعال شد
        AddDomainEvent(new ProvinceDeactivatedDomainEvent(this));
    }

    public void Activate()
    {
        if (IsActive)
            return;

        IsActive = true;
        UpdatedAt = DateTime.UtcNow;

        // رویداد دامنه: استان فعال شد
        AddDomainEvent(new ProvinceActivatedDomainEvent(this));
    }
}
