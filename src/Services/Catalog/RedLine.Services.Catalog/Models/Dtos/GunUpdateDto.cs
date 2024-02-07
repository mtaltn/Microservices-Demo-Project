namespace RedLine.Services.Catalog.Models;

public class GunUpdateDto : BaseDto
{
    public string Picture { get; set; }
    public string UserId { get; set; }
    public string Description { get; set; }
    public FeatureDto FeatureModels { get; set; }
    public decimal Price { get; set; }
    public string CategotyId { get; set; }
}
