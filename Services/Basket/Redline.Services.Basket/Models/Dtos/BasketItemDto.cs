namespace Redline.Services.Basket.Models;

public class BasketItemDto
{
    public int Quantity { get; set; }
    public string GunId { get; set; }
    public string GunName { get; set; }
    public decimal Price { get; set; }
}
