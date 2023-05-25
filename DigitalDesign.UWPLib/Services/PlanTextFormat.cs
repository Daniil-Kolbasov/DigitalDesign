using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DigitalDesign.UWPLib.Services
{
	/// <summary>
	/// Iutput number of words in text
	/// </summary>
	internal class PlanTextFormat : IFormat
	{
		public Dictionary<string, int> GetDictionary(string text)
		{
			var words = Regex.Split(text.ToLower(), @"\W+")
				.Where(x => !string.IsNullOrEmpty(x))
				.GroupBy(g => g)
				.Select(s => new { Word = s.Key, Count = s.Count() })
				.OrderBy(o => o.Count)
				.ThenBy(t => t.Word)
				.ToDictionary(k => k.Word, v => v.Count);

			return words;
		}
	}
}
