using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.Store.Extensions
{
    public static class ObjectExtensions
    {
        

        /// <summary>
        /// builds a string by itterating through the objects properties, selecting prop name and prop values.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static string HumanReadable(this object target)
        {
            if(target == null)
            {
                return string.Empty;
            }

            var text = target.GetType().Name + "\n";

            foreach (var prop in target.GetType().GetProperties())
            {
                if (prop.PropertyType.IsSimple())
                {
                    text += prop.Name + ": " + prop.GetValue(target) + "\n";
                }
                else
                {
                    text += $"{{\n{prop.PropertyType.Name}\n{prop.GetValue(target).HumanReadable()}}}";
                }
            }

            return text;
        }
    }
}
