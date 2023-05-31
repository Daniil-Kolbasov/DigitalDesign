using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DigDes.WebAPI.Lib
{
	public class ClassWithPtivateMethod
	{
		private Dictionary<string, int> GetDictionary(string text)
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
