using System.Text.Json.Serialization;

namespace KaWoDev.BitwardenJsonConverter.BitwardenConverter.Contract.DataClasses;

public class Uri
{
	public string? Match { get; set; }
	[JsonPropertyName("uri")]
	public string Url { get; set; }
}