namespace KaWoDev.BitwardenJsonConverter.BitwardenConverter.Contract.DataClasses;

public class Bitwarden
{
	public bool Encrypted { get; set; }
	public List<Folder>? Folders { get; set; }
	public List<Item>? Items { get; set; }
}