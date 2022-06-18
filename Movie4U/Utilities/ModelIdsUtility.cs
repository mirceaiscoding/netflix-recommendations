using Movie4U.EntitiesModels;

namespace Movie4U.Utilities
{
    public class ModelIdsUtility<TModel>
        where TModel : IModel<TModel>
    {
        /**<summary>
         * Gets an object containing the ids that form the primary key of the model.
         * </summary>*/
        public static IdModel Get(TModel model)
        {
            var idSelectors = model.GetIdSelectors();
            object[] ids = new object[idSelectors.Length];
            for (int i = 0; i < ids.Length; i++)
                ids[i] = idSelectors[i].Compile()(model);

            return new IdModel(ids);
        }

    }
}
