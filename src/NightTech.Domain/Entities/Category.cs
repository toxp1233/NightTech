namespace NightTech.Domain.Entities;

public class Category
{
    public int Id { get; set; }
    public string CategoryName { get; set; } = default!;
    public virtual ICollection<Product>? Products { get; set; }

}
