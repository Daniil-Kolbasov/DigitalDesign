using System.Linq;
using System.Reflection;
using System;

namespace DigDes.UWP.Lib
{
	public static class Reflection
	{
		public static object InvokeMethod(this object obj, string methodName, params object[] methodParams)
		{
			Type type = obj.GetType();
			MethodInfo myMethod = type.GetMethod("GetDictionary", BindingFlags.NonPublic | BindingFlags.Instance);
			object insObj = Activator.CreateInstance(type);
			return myMethod.Invoke(insObj, methodParams);
		}

		//public static object InvokeMethod(this object obj, string methodName, params object[] methodParams)
		//{
		//	var methodParamTypes = methodParams?.Select(p => p.GetType()).ToArray() ?? Array.Empty<Type>();
		//	var bindingFlags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static;
		//	MethodInfo method = null;
		//	var type = obj.GetType();
		//	while (method == null && type != null)
		//	{
		//		method = type.GetMethod(methodName, bindingFlags, Type.DefaultBinder, methodParamTypes, null);
		//		type = type.BaseType;
		//	}

		//	return method?.Invoke(obj, methodParams);
		//}
	}
}