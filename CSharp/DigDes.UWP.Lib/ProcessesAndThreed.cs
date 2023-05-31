using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace DigDes.UWP.Lib
{
	public class ProcessesAndThreed
	{
		private const string pattern = @"\W+";

		public static async Task<Dictionary<string, long>> GetTimeOfProcessingMethod(string text)
		{
			var enumer = new Dictionary<string, long>
			{
				{ $"Regular", GetDictionary(text) },
				{ $"Task", await GetDictionaryAsync(text) },
				{ $"Thread", GetDictionaryThread(text) },
				{ $"Parallel", GetDictionaryParallel(text) }
			};

			return enumer;
		}

		private static long GetDictionary(string test)
		{
			var stopwatch = new Stopwatch();
			stopwatch.Start();

			var result = new Dictionary<string, int>();
			var arrayWords = Regex.Split(test.ToLower(), pattern);

			foreach (string word in arrayWords)
			{
				Algorithm(result, word);
			}

			stopwatch.Stop();

			return stopwatch.ElapsedMilliseconds;
		}

		private static async Task<long> GetDictionaryAsync(string test)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();

			var result = new Dictionary<string, int>();
			string[] arrayWords = Regex.Split(test.ToLower(), pattern);

			foreach (string word in arrayWords)
			{
				await Task.Run(() => Algorithm(result, word));
			}

			stopwatch.Stop();

			return stopwatch.ElapsedMilliseconds;
		}

		private static long GetDictionaryThread(string test)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();

			var result = new Dictionary<string, int>();
			string[] arrayWords = Regex.Split(test.ToLower(), pattern);

			foreach (string word in arrayWords)
			{
				if (string.IsNullOrEmpty(word))
					continue;

				var thread = new Thread(() => Algorithm(result, word));
				thread.Start();
				thread.Join();
			}

			stopwatch.Stop();

			return stopwatch.ElapsedMilliseconds;
		}

		private static long GetDictionaryParallel(string test)
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();

			var result = new Dictionary<string, int>();
			string[] arrayWords = Regex.Split(test.ToLower(), pattern);

			Parallel.ForEach(arrayWords, (word) => Algorithm(result, word));

			stopwatch.Stop();

			return stopwatch.ElapsedMilliseconds;
		}

		private static void Algorithm(Dictionary<string, int> result, string word)
		{
			if (string.IsNullOrEmpty(word)) return;
			if (result.ContainsKey(word) == false)
				result.Add(word, 1);
			else
				result[word]++;
		}
	}
}
