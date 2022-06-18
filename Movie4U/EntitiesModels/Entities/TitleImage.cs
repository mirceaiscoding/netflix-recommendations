using Movie4U.EntitiesModels.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Movie4U.EntitiesModels.Entities
{
    public class TitleImage: EntitiesModelsBase<TitleImage, TitleImageModel>, IEntity<TitleImage>
    {
        public static Expression<Func<TitleImage, object>>[] idSelectors;

        static TitleImage()
        {
            idSelectors = new Expression<Func<TitleImage, object>>[1];
            idSelectors[0] = entity => entity.url;
        }


        public string image_type { get; set; }

        [Required]
        public int netflix_id { get; set; }

        [Required, Key]
        public string url { get; set; }        // "filmid as netflixid,url,itype"

        virtual public Title title { get; set; }


        /**<summary>
         * Constructor.
         * </summary>*/
        public TitleImage(TitleImage source)
        {
            Copy(source);
        }

        /**<summary>
        * Constructor.
        * </summary>*/
        public TitleImage(TitleImageModel source)
        {
            Copy(source);
        }

        /**<summary>
         * Constructor.
         * </summary>*/
        public TitleImage() { }

        override public void Copy(TitleImage source)
        {
            image_type = source.image_type;
            netflix_id = source.netflix_id;
            url = source.url;
        }

        override public void Copy(TitleImageModel source)
        {
            image_type = source.image_type;
            netflix_id = source.netflix_id;
            url = source.url;
        }

        public Expression<Func<TitleImage, object>>[] GetIdSelectors()
        {
            return idSelectors;
        }
    }
}
