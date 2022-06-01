using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movie4U.Utilities
{
    public static class FlagsUtility
    {
        public static List<int> GetFlagsUnpacked(int flagsPacked)
        {
            List<int> flagsUnpacked = new List<int>();
            int power2 = 1;
            while(power2 <= flagsPacked)
            {
                if((flagsPacked & power2) != 0)
                    flagsUnpacked.Add(power2);
                power2 <<= 1;
            }
            return flagsUnpacked;
        }
    }
}
