using System;

namespace Movie4U.EntitiesModels
{
    public class IdModel
    {
        static Type typeInt;
        static Type typeString;

        IdModel()
        {
            typeInt = typeof(int);
            typeString = typeof(string);
        }


        public object[] ids { get; set; }

        public int Length => ids.Length;

        public object this[int key]
        {
            get => Length <= key ? ids[key] : null;
            set
            {
                if (Length <= key)
                    ids[key] = value;
            }
        }


        /**<summary>
         * Constructor.
         * </summary>*/
        public IdModel(params object[] ids)
        {
            this.ids = ids;
        }

        /// <summary>
        /// Compares the Ids of the given EntityModels.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(IdModel other)
        {
            if (other == null)
                return 1;

            int i;
            for(i = 0; i < Length; i++)
            {
                var thisId = ids[i];
                var otherId = other[i];

                if (thisId == null && otherId == null)
                    continue;

                if (thisId == null)
                    return -1;
                if (otherId == null)
                    return 1;

                Type crtType = thisId.GetType();
                if (crtType != otherId.GetType())   // Can not be compared.
                    return 0;

                try
                {
                    if (crtType == typeInt)
                    {
                        int result = ((int)thisId).CompareTo((int)otherId);
                        if (result != 0)
                            return result;
                    }

                    else if (crtType == typeString)
                    {
                        int result = ((string)thisId).CompareTo((string)otherId);
                        if (result != 0)
                            return result;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return 0;
                }
            }

            for(; i < other.Length; i++)
            {
                if (other[i] != null)
                    return -1;
            }

            return 0;
        }

    }
}
