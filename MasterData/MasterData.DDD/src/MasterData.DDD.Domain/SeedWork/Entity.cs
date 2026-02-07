namespace MasterData.DDD.Domain.SeedWork;

/// <summary>
/// کلاس پایه موجودیت - Base Entity Class
/// </summary>
public abstract class Entity
{
    private int _id;
    private List<IDomainEvent>? _domainEvents;

    public virtual int Id
    {
        get => _id;
        protected set => _id = value;
    }

    public IReadOnlyCollection<IDomainEvent>? DomainEvents => _domainEvents?.AsReadOnly();

    protected void AddDomainEvent(IDomainEvent eventItem)
    {
        _domainEvents ??= new List<IDomainEvent>();
        _domainEvents.Add(eventItem);
    }

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Entity other)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (GetType() != other.GetType())
            return false;

        if (Id == 0 || other.Id == 0)
            return false;

        return Id == other.Id;
    }

    public static bool operator ==(Entity? a, Entity? b)
    {
        if (a is null && b is null)
            return true;

        if (a is null || b is null)
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(Entity? a, Entity? b)
    {
        return !(a == b);
    }

    public override int GetHashCode()
    {
        return (GetType().ToString() + Id).GetHashCode();
    }
}
