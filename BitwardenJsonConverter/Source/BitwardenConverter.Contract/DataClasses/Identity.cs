namespace KaWoDev.BitwardenJsonConverter.BitwardenConverter.Contract.DataClasses;

public class Identity
{
	public string? Title { get; set; }
	public string? FirstName { get; set; }
	public string? MiddleName { get; set; }
	public string? LastName { get; set; }

	public string? Address1 { get; set; }
	public string? Address2 { get; set; }
	public string? Address3 { get; set; }
	public string? City { get; set; }
	public string? State { get; set; }
	public string? PostalCode { get; set; }
	public string? Country { get; set; }

	public string? Company { get; set; }
	public string? Email { get; set; }
	public string? Phone { get; set; }

	/// <summary>
	/// Sozialversicherungsnummer
	/// </summary>
	public string? SSV { get; set; }
	/// <summary>
	/// Führerscheinnummer
	/// </summary>
	public string? LicenseNumber { get; set; }
	public string? PassportNumber { get; set; }


	public string? Username { get; set; }
}