using System.Text.Json.Serialization;

namespace NightTech.Application.PublicDtos;

public class CategoryDto
{
    public int Id { get; set; }
    public string CategoryName { get; set; } = default!;
    [JsonIgnore]
    public virtual ICollection<ProductDto>? Products { get; set; }
}
