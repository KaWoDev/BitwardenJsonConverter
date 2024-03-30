using KaWoDev.BitwardenJsonConverter.BitwardenConverter.Contract.DataClasses;

namespace KaWoDev.BitwardenJsonConverter.BitwardenConverter.Contract;

public interface IBitwardenJsonConverter
{
	Bitwarden Deserialize(string json);
}