using System.Text.Json.Serialization;

namespace KaWoDev.BitwardenJsonConverter.BitwardenConverter.Contract.DataClasses;

public class Item
{
	public string Id { get; set; }
	public string? OrganizationId { get; set; }
	public string? FolderId { get; set; }
	public Folder? Folder { get; set; }

	public int Reprompt { get; set; }
	public string? Name { get; set; }
	public string? Notes { get; set; }
	public bool Favorite { get; set; }
	
	[JsonPropertyName("type")]
	public ItemType ItemType { get; set; }
	public Login? Login { get; set; }
	public Card? Card { get; set; }
	public Identity? Identity { get; set; }
	
	public List<Field> Fields { get; set; }

	public string? CollectionIds { get; set; }
}