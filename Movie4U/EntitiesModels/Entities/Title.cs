using Movie4U.EntitiesModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Movie4U.EntitiesModels.Entities
{
    public class Title: EntitiesModelsBase<Title,TitleModel>, IEntity<Title>
    {
        public static Expression<Func<Title, object>>[] idSelectors;

        static Title()
        {
            idSelectors = new Expression<Func<Title, object>>[1];
            idSelectors[0] = entity => entity.netflix_id;
        }


        public string title { get; set; }

        public string img { get; set; }

        public string title_type { get; set; }

        [Required, Key]
        public int netflix_id { get; set; }

        public string synopsis { get; set; }

        public string rating { get; set; }

        public string year { get; set; }

        public string runtime { get; set; }

        public string poster { get; set; }

        public int top250 { get; set; }

        public int top250tv { get; set; }

        public string title_date { get; set; }


        virtual public List<TitleCountry> titleCountries { get; set; }
        virtual public List<TitleGenre> titleGenres { get; set; }
        virtual public List<TitleImage> titleImages { get; set; }
        public virtual List<WatcherTitle> watcherTitles { get; set; }


        /**<summary>
         * Constructor.
         * </summary>*/
        public Title(Title source)
        {
            Copy(source);
        }

        /**<summary>
         * Constructor.
         * </summary>*/
        public Title(TitleModel source)
        {
            Copy(source);
        }

        /**<summary>
         * Constructor.
         * </summary>*/
        public Title(TitleModelParameter source)
        {
            Copy(source);
        }

        /**<summary>
         * Constructor.
         * </summary>*/
        public Title() { }


        override public void Copy(Title source)
        {
            this.title = source.title;
            this.img = source.img;
            this.title_type = source.title_type;
            this.netflix_id = source.netflix_id;
            this.synopsis = source.synopsis;
            this.rating = source.rating;
            this.runtime = source.runtime;
            this.poster = source.poster;
            this.top250 = source.top250;
            this.top250tv = source.top250tv;
            this.title_date = source.title_date;
            this.year = source.year;

        }

        override public void Copy(TitleModel source)
        {
            this.title = source.title;
            this.img = source.img;
            this.title_type = source.title_type;
            this.netflix_id = source.netflix_id;
            this.synopsis = source.synopsis;
            this.rating = source.rating;
            this.runtime = source.runtime;
            this.poster = source.poster;
            this.top250 = source.top250;
            this.top250tv = source.top250tv;
            this.title_date = source.title_date;
            this.year = source.year;
        }

        public void Copy(TitleModelParameter source)
        {
            this.title = source.title;
            this.img = source.img;
            this.title_type = source.title_type;
            this.netflix_id = source.netflix_id;
            this.synopsis = source.synopsis;
            this.rating = source.rating;
            this.runtime = source.runtime;
            this.poster = source.poster;
            this.top250 = source.top250;
            this.top250tv = source.top250tv;
            this.title_date = source.title_date;
            this.year = source.year;
        }

        public Expression<Func<Title, object>>[] GetIdSelectors()
        {
            return idSelectors;
        }

    }
}
