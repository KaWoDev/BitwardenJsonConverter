namespace KaWoDev.BitwardenJsonConverter.BitwardenConverter.Contract.DataClasses;

public class Login
{
	public List<Uri>? Uris { get; set; }
	public string? Username { get; set; }
	public string? Password { get; set; }
	public string? Totp { get; set; }
}