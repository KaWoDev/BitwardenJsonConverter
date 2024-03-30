namespace KaWoDev.BitwardenJsonConverter.BitwardenConverter.Contract.DataClasses;

public class Field
{
	public string? Name { get; set; }
	public string? Value { get; set; }
	public FieldType Type { get; set; }
	public string? LinkedId { get; set; }
}