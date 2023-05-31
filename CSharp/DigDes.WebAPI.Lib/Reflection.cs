using DigDes.WebAPI.Lib;
using System.Reflection;

namespace DigDes.UWP.Lib
{
	public static class Reflection
	{
		//public static object? InvokeMethod<T>(this T obj, string methodName, params object[] methodParams)
		//{
		//	var type = typeof(T);
		//	var method = type.GetTypeInfo().GetDeclaredMethod(methodName);
		//	return method.Invoke(obj, methodParams);
		//}

		public static object? InvokeMethod(this object obj, string methodName, params object[] methodParams)
		{
			var methodParamTypes = methodParams?.Select(p => p.GetType()).ToArray() ?? Array.Empty<Type>();
			var bindingFlags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static;
			MethodInfo? method = null;
			var type = obj.GetType();
			while (method == null && type != null)
			{
				method = type.GetMethod(methodName, bindingFlags, Type.DefaultBinder, methodParamTypes, null);
				type = type.BaseType;
			}

			return method?.Invoke(obj, methodParams);
		}
	}
}