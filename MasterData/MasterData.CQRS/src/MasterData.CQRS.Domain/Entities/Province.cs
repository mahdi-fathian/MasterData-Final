namespace MasterData.CQRS.Domain.Entities;


public class Province
{
    
    public int Id { get; private set; }

    
    public string Name { get; private set; } = string.Empty;

    
    public DateTime CreatedAt { get; private set; }

   
    public DateTime? UpdatedAt { get; private set; }

   
    private Province() { }

    
    public Province(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("نام استان نمی‌تواند خالی باشد", nameof(name));

        if (name.Length > 50)
            throw new ArgumentException("نام استان نمی‌تواند بیشتر از 50 کاراکتر باشد", nameof(name));

        Name = name.Trim();
        CreatedAt = DateTime.UtcNow;
    }

 
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
