using System.Reflection;

namespace DigitalDesign.UWPLib.Test
{
	public class ConvertStringTest
	{
		[Fact]
		public void ToDictionary_Get_dictionary_number_of_words_from_plan_text()
		{
			Dictionary<string, int> expected = new()
			{
				{"three", 3},
				{"two", 2},
				{"one", 1}
			};

			string text = "one two two three three three";
			ConvertString convertString = new();

			MethodInfo method = typeof(ConvertString).GetMethod("ToDictionary", BindingFlags.NonPublic | BindingFlags.Instance)!;

			Assert.Equal(expected, method.Invoke(convertString, new object[] { text }));
		}
	}
}