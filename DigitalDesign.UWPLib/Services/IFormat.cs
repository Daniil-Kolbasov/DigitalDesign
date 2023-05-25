using System.Collections.Generic;

namespace DigitalDesign.UWPLib.Services
{
	internal interface IFormat
	{
		Dictionary<string, int> GetDictionary(string text);
	}
}
