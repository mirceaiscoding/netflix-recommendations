using System;
using System.Collections.Generic;

namespace Movie4U.Utilities
{
    public class ModelChainComparer<TModel>: IComparer<TModel>
    {
        private List<Func<TModel, TModel, int>> comparerList;


        public ModelChainComparer(List<Func<TModel, TModel, int>> comparerList)
        {
            this.comparerList = comparerList;
        }

        public int Compare(TModel m1, TModel m2)
        {
            if (comparerList == null || comparerList.Count == 0)
                return -1;

            foreach (var comparer in comparerList)
            {
                int result = comparer(m1, m2);
                if (result != 0)
                    return result;
            }

            return 0;
        }

    }
}
