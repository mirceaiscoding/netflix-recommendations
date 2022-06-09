using System;
using System.Collections.Generic;

namespace Movie4U.Utilities
{
    public class EntityChainComparer<TEntity> : IComparer<TEntity>
    {
        private List<Func<TEntity, TEntity, int>> comparerList;

        public EntityChainComparer(List<Func<TEntity, TEntity, int>> comparerList)
        {
            this.comparerList = comparerList;
        }

        public int Compare(TEntity m1, TEntity m2)
        {
            if (comparerList == null | comparerList.Count == 0)
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
