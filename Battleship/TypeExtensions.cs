using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Battleship
{
	public static class TypeExtensions
	{
		public static bool Implements<TInterface>(this Type type) where TInterface : class
		{
			return type.FindInterfaces((interfaceType, sought) => interfaceType == sought, typeof(TInterface)).Any();
		}
	}
}
