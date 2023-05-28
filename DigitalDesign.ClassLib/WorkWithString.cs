using System.Diagnostics;
using System.Text.RegularExpressions;

namespace DigitalDesign.ClassLib
{
	public class WorkWithString
	{
		private const string pattern = @"\W+";

		public static long RegularTime { get; private set; }
		public static long TaskAsyncTime { get; private set; }
		public static long ThreadTime { get; private set; }
		public static long ParallelTime { get; private set; }


		private static Dictionary<string, int> GetDictionary(string test)
		{
			var result = new Dictionary<string, int>();
			string[] arrayWords = Regex.Split(test.ToLower(), pattern);

			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();

			foreach (string word in arrayWords)
			{
				AddItem(result, word);
			}

			stopwatch.Stop();
			RegularTime = stopwatch.ElapsedMilliseconds;

			return result;
		}

		public static async Task<Dictionary<string, int>> GetDictionaryAsync(string test)
		{
			var result = new Dictionary<string, int>();
			string[] arrayWords = Regex.Split(test.ToLower(), pattern);

			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();

			foreach (string word in arrayWords)
			{
				await Task.Run(() => AddItem(result, word));
			}

			stopwatch.Stop();
			TaskAsyncTime = stopwatch.ElapsedMilliseconds;

			return result;
		}

		public static Dictionary<string, int> GetDictionaryThread(string test)
		{
			var result = new Dictionary<string, int>();
			string[] arrayWords = Regex.Split(test.ToLower(), pattern);

			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();

			foreach (string word in arrayWords)
			{
				if (string.IsNullOrEmpty(word))
					continue;

				var thread = new Thread(() => AddItem(result, word));
				thread.Start();
				thread.Join();
			}

			stopwatch.Stop();
			ThreadTime = stopwatch.ElapsedMilliseconds;

			return result;
		}

		public static Dictionary<string, int> GetDictionaryParallel(string test)
		{
			var result = new Dictionary<string, int>();
			string[] arrayWords = Regex.Split(test.ToLower(), pattern);

			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();

			Parallel.ForEach(arrayWords, (word) => AddItem(result, word));

			stopwatch.Stop();
			ParallelTime = stopwatch.ElapsedMilliseconds;

			return result;
		}

		private static void AddItem(Dictionary<string, int> result, string word)
		{
			if (string.IsNullOrEmpty(word)) return;
			if (result.ContainsKey(word) == false)
				result.Add(word, 1);
			else
				result[word]++;
		}
	}
}
