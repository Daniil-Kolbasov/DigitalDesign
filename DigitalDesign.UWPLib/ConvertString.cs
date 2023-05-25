using DigitalDesign.UWPLib.Services;
using System.Collections.Generic;

namespace DigitalDesign.UWPLib
{
	public class ConvertString
    {
		private readonly IFormat _service;

		public ConvertString()
		{
			_service = new PlanTextFormat();
		}

		private Dictionary<string, int> ToDictionary(string text)
		{
			return _service.GetDictionary(text);
		}
	}
}
