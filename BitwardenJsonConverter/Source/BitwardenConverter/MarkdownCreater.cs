using System.Text;

using KaWoDev.BitwardenJsonConverter.Base.Contract.Exceptions;
using KaWoDev.BitwardenJsonConverter.BitwardenConverter.Contract;
using KaWoDev.BitwardenJsonConverter.BitwardenConverter.Contract.DataClasses;
using KaWoDev.BitwardenJsonConverter.BitwardenConverter.Contract.Exceptions;

namespace KaWoDev.BitwardenJsonConverter.BitwardenConverter;

public class MarkdownCreater : IMarkdownCreater
{
	public string CreateMarkdown(Bitwarden bitwarden, DateTime sourceDate)
	{
		try
		{
			var result = new StringBuilder();

			result.Append($"""
							# Bitwarden - Passwörter

							Passwörter von {sourceDate}
							Secrets insgesamt {bitwarden.Items?.Count ?? 0} 

							""");

			AppendFolderItems(bitwarden, result);

			return result.ToString();
		}
		catch (Exception e)
		when(e is not BaseException)
		{
			var myEx = new BitwardenJsonConverterCreateMarkdownException("The Markdown can not be created.", e);
			throw myEx;
		}
	}

	private void AppendFolderItems(Bitwarden bitwarden, StringBuilder stringBuilder)
	{
		if (bitwarden.Items is null || bitwarden.Items.Count == 0)
		{
			return;
		}
		
		SetItemsWithNoFolderInSpecialFolder(bitwarden);
		
		foreach (var folder in bitwarden.Folders!)
		{
			var folderItems = bitwarden.Items.Where(i => i.FolderId == folder.Id).ToList();
			
			stringBuilder.Append($"""
			
				## {folder.Name}
				{folderItems.Count} Secrets
				
				""");
			
			foreach (var folderItem in folderItems)
			{
				AppendFolderItem(folderItem, stringBuilder);
			}
		}
	}
	
	private void SetItemsWithNoFolderInSpecialFolder(Bitwarden bitwarden)
	{
		if (bitwarden.Folders is null || bitwarden.Items?.Where(i => i.Folder is null).ToList().Count == 0)
		{
			bitwarden.Folders = new List<Folder>();
		}

		var folderWithoutName = new Folder() { Id = "ohneOrdner", Name = "[Ohne Ordner]" };
		bitwarden.Folders.Add(folderWithoutName);
		
		var itemsWithoutFolder = bitwarden.Items!.Where(i => i.Folder is null);
		foreach (Item item in itemsWithoutFolder)
		{
			item.Folder = folderWithoutName;
			item.FolderId = folderWithoutName.Id;
		}
	}

	private void AppendFolderItem(Item folderItem, StringBuilder stringBuilder)
	{
		stringBuilder.Append($"{Environment.NewLine}");
		AppendTextLineByAvailableValue($"### " ,$"{folderItem.Name}",stringBuilder);
		
		AppendLogin(folderItem.Login,stringBuilder);
		AppendCard(folderItem.Card,stringBuilder);
		AppendIdentity(folderItem.Identity, stringBuilder);
		
		AppendFields(folderItem.Fields, stringBuilder);
		AppendTextLineByAvailableValue($"Notizen:{Environment.NewLine}",$"{folderItem.Notes?? string.Empty}", stringBuilder);
	}

	private void AppendLogin(Login? login, StringBuilder stringBuilder)
	{
		if (login is null)
		{
			return;
		}
		
		AppendTextLineByAvailableValue($"Benutzername: " ,$"{login.Username ?? String.Empty}",stringBuilder);
		AppendTextLineByAvailableValue($"Passwort: " ,$"{login.Password ?? string.Empty}",stringBuilder);
		AppendTextLineByAvailableValue($"TOTP: " ,$"{login.Totp?? string.Empty}",stringBuilder);
		
		AppendUriList(login, stringBuilder);
	}

	private void AppendUriList(Login login, StringBuilder stringBuilder)
	{
		var urisNotAvailable = login.Uris is null || login.Uris.Count == 0;
		if (urisNotAvailable)
		{
			return;
		}
		
		stringBuilder.Append($"Urls:{Environment.NewLine}");
		foreach (var uri in login.Uris)
		{
			stringBuilder.Append($"* <{uri.Url ?? string.Empty}>{Environment.NewLine}");
		}
	}
	
	private void AppendCard(Card? card, StringBuilder stringBuilder)
	{
		if (card is null)
		{
			return;
		}
		
		AppendTextLineByAvailableValue("Brand: ",$"{card.Brand}",stringBuilder);
		AppendTextLineByAvailableValue("Karteninhaber: ",$"{card.CardholderName}",stringBuilder);
		AppendTextLineByAvailableValue("Nummer: ",$"{card.Number}",stringBuilder);
		AppendTextLineByAvailableValue("AblaufMonat: ",$"{card.ExpMonth}",stringBuilder);
		AppendTextLineByAvailableValue("AplaufJahr: ",$"{card.ExpYear}",stringBuilder);
		AppendTextLineByAvailableValue("Code: ",$"{card.Code}",stringBuilder);
	}
	
	private void AppendIdentity(Identity? identity, StringBuilder stringBuilder)
	{
		if (identity is null)
		{
			return;
		}
		
		AppendTextLineByAvailableValue("Benutzername: ",$"{identity.Username}",stringBuilder);
		
		AppendTextLineByAvailableValue("Anrede: ",$"{identity.Title}",stringBuilder);
		AppendTextLineByAvailableValue("Vorname: ",$"{identity.FirstName}",stringBuilder);
		AppendTextLineByAvailableValue("Zweitname",$"{identity.MiddleName}",stringBuilder);
		AppendTextLineByAvailableValue("Nachname",$"{identity.LastName}",stringBuilder);
				
		AppendTextLineByAvailableValue("Firma: ",$"{identity.Company}",stringBuilder);
        		
		AppendTextLineByAvailableValue("Email: ",$"{identity.Email}",stringBuilder);
        AppendTextLineByAvailableValue("Telefon: ",$"{identity.Phone}",stringBuilder);
		
		AppendTextLineByAvailableValue("Adresse1 :",$"{identity.Address1}",stringBuilder);
		AppendTextLineByAvailableValue("Adresse2: ",$"{identity.Address2}",stringBuilder);
		AppendTextLineByAvailableValue("Adresse3: ",$"{identity.Address3}", stringBuilder);
		AppendTextLineByAvailableValue("PLZ: ",$"{identity.PostalCode}",stringBuilder);
		AppendTextLineByAvailableValue("Stadt: ",$"{identity.City}",stringBuilder);
		AppendTextLineByAvailableValue("Bundesland: ",$"{identity.State}",stringBuilder);
		AppendTextLineByAvailableValue("Land: ",$"{identity.Country}",stringBuilder);
		
		AppendTextLineByAvailableValue("Führerscheinnr: ",$"{identity.LicenseNumber}",stringBuilder);
		AppendTextLineByAvailableValue("Sozialversicherungsnr.: ",$"{identity.SSV}",stringBuilder);
		AppendTextLineByAvailableValue("Personalausweisnr.: ",$"{identity.PassportNumber}",stringBuilder);
	}
	
	private void AppendFields(List<Field>? fields, StringBuilder stringBuilder)
	{
		if (fields is null)
		{
			return;
		}

		foreach (Field field in fields)
		{
			switch (field.Type)
			{
				case FieldType.Hidden:
				case FieldType.Text:
					stringBuilder.Append($"""
					{field.Value}
					
					""");
					break;
				case FieldType.Bool:
					var value = field.Value == "true" ?"[x]":"[]";
					stringBuilder.Append($"""
					{field.Name}: {value}
					
					""");
					break;
				default:
					throw new Exception($"Field Type '{field.Type}' is not implemented.");
			}
		}
	}
	private void AppendTextLineByAvailableValue(string? text, string? value, StringBuilder stringBuilder)
	{
		if (string.IsNullOrWhiteSpace(value))
		{
			return;
		}

		stringBuilder.Append($"""
			{text?? string.Empty}{value}

			""");
	}
}