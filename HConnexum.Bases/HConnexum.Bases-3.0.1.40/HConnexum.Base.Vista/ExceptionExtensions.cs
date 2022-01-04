using System;
using System.Linq;
using System.Reflection;

namespace HConnexum.Base.Vista
{
	public static class ExceptionExtensions
	{
		public static void Rethrow(this Exception ex)
		{
			typeof(Exception).GetMethod(@"PrepForRemoting", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(ex, new object[0]);
			throw ex;
		}
	}
}