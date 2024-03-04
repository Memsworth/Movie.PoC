namespace Movie.PoC.Api.Settings;

public class JwtConfig
{
    public string Key { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int? ExpiryMins { get; set; }
}