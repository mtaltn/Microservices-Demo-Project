namespace RedLine.Services.Catalog.Models;

public class GunDto : BaseDto
{
    public string Picture { get; set; }
    public string UserId { get; set; }
    public string Description { get; set; }
    public FeatureDto FeatureModels { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime UpdatedTime { get; set; }
    public string CategotyId { get; set; }
    public CategoryDto CategoryModels { get; set; }
    public bool isDeleted { get; set; }
}
