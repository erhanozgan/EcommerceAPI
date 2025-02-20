using System.Text.Json.Serialization;

namespace ECommerce.Core.Dtos;

public class LoginDto
{
    [JsonPropertyName("username")]
    public string UserName { get; set; }
    
    public string? Password { get; set; }
}
