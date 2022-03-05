using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Movie4U.Utilities
{
    public static class NullCheckerUtility
    {
        //counts null or empty (for strings) fields of given object
        public static int nullCount(object obj)
        {
            int counter = 0;
            foreach (PropertyInfo pi in obj.GetType().GetProperties())
            {
                var value = pi.GetValue(obj);
                if (pi.PropertyType == typeof(string)  &&  string.IsNullOrEmpty((string)value))
                    counter++;
                else if (value == null)
                    counter++;
            }
            return counter;
        }
        
        // checks wether the given object has null or empty (for strings) fields
        public static bool hasNulls(object obj)
        {
            foreach (PropertyInfo pi in obj.GetType().GetProperties())
            {
                var value = pi.GetValue(obj);
                if (pi.PropertyType == typeof(string) && string.IsNullOrEmpty((string)value))
                    return true;
                else if (value == null)
                    return true;
            }
            return false;
        }

    }
}
