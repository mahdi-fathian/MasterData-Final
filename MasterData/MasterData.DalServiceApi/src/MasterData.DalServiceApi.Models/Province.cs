namespace MasterData.DalServiceApi.Models;

/// <summary>
/// مدل استان - Province Model
/// </summary>
public class Province
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
