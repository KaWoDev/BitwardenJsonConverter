using System.Text.Json;

using KaWoDev.BitwardenJsonConverter.BitwardenConverter.Contract;
using KaWoDev.BitwardenJsonConverter.BitwardenConverter.Contract.DataClasses;
using KaWoDev.BitwardenJsonConverter.BitwardenConverter.Contract.Exceptions;

namespace KaWoDev.BitwardenJsonConverter.BitwardenConverter;

public class BitwardenJsonConverter : IBitwardenJsonConverter
{
	public Bitwarden Deserialize(string json)
	{
		ArgumentException.ThrowIfNullOrWhiteSpace(json);

		try
		{
			var serialOptions = new JsonSerializerOptions()
			{
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
				WriteIndented = true
			};

			var result = JsonSerializer.Deserialize<Bitwarden>(json, serialOptions);

			SetFolderInItems(result);
			CorrectionNotesLineBreak(result);

			return result;
		}
		catch (Exception e)
		when(e is not BaseException)
		{
			var myEx = new JsonDeserializeException("The BitwardenJson can not Deserialze",e);
			throw myEx;
		}
	}

	private void SetFolderInItems(Bitwarden bitwarden)
	{
		if (bitwarden.Folders is null || bitwarden.Items is null)
		{
			return;
		}
		
		foreach (Folder folder in bitwarden.Folders)
		{
			foreach (Item item in bitwarden.Items.Where(i => i.FolderId == folder.Id))
			{
				item.Folder = folder;
			}
		}
	}

	private void CorrectionNotesLineBreak(Bitwarden bitwarden)
	{
		foreach (Item item in bitwarden.Items)
		{
			item.Notes?.Replace("/n", Environment.NewLine);
		}
	}
}
