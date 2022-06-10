using System.Collections.Generic;

namespace Movie4U.Utilities
{
    public static class FlagsUtility
    {
        public static List<int> GetFlagsUnpacked(int flagsPacked)
        {
            List<int> flagsUnpacked = new List<int>();

            for(int power2 = 1; power2 <= flagsPacked; power2 <<= 1)
                if ((flagsPacked & power2) != 0)
                    flagsUnpacked.Add(power2);

            return flagsUnpacked;
        }
    }
}
