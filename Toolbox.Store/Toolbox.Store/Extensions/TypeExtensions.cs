using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.Store.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsSimple(this Type type)
        {
            var simpleTypes = new List<Type>();
            simpleTypes.AddRange(new[] {
                typeof(string),
                typeof(byte),
                typeof(sbyte),
                typeof(short),
                typeof(ushort),
                typeof(int),
                typeof(uint),
                typeof(long),
                typeof(ulong),
                typeof(float),
                typeof(double),
                typeof(decimal),
                typeof(char),
                typeof(bool),
                typeof(object),
                typeof(DateTime)});

            return simpleTypes.Contains(type);
        }
    }
}
