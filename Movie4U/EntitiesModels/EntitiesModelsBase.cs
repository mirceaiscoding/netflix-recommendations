
namespace Movie4U.EntitiesModels
{
    /**<summary>
     * Abstract base class for pairs of Entities and Models.
     * </summary>>*/
    abstract public class EntitiesModelsBase<TEntity, TModel>
        where TEntity : IEntity<TEntity>
        where TModel : IModel<TModel>
    {        
        /**<summary>
        * Copies data from source Entity. 
        * Does not include reference values.
        * </summary> */
        abstract public void Copy(TEntity source);

        /**<summary>
        * Copies data from source Model. 
        * Does not include reference values.
        * </summary> */
        abstract public void Copy(TModel source);


        /**<summary>
        * Copies data from source Model. 
        * It should include reference values too.
        * However, it calls its Copy couterpart by default, as overriding it is not mandatory (objects might not have reference types).
        * </summary> */
        virtual public void ShallowCopy(TModel source) 
        {
            Copy(source);
        }

    }
}
