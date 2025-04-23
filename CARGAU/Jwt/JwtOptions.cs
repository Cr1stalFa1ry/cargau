namespace API.Jwt;
public class JwtOptions
{
    public string SecretKey { get; set; } = string.Empty;
    public int ExpiteHours { get; set; }
}