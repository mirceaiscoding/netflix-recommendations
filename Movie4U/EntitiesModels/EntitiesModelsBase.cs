
namespace Movie4U.EntitiesModels
{
    /**<summary>
     * Abstract base class for pairs of Entities and Models.
     * </summary>>*/
    abstract public class EntitiesModelsBase<EntityType, ModelType>
    {
        /**<summary>
         * Copies data from source Entity.
         * </summary> */
        abstract public void Copy(EntityType source);

        /**<summary>
        * Copies data from source Model.
        * </summary> */
        abstract public void Copy(ModelType source);

    }
}
