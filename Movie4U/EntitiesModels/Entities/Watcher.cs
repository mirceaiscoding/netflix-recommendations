using Movie4U.EntitiesModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Movie4U.EntitiesModels.Entities
{
    public class Watcher: EntitiesModelsBase<Watcher,WatcherModel>, IEntity<Watcher>
    {
        public static Expression<Func<Watcher, object>>[] idSelectors;

        static Watcher()
        {
            idSelectors = new Expression<Func<Watcher, object>>[1];
            idSelectors[0] = entity => entity.watcher_name;
        }


        [Required, Key]
        public string watcher_name { get; set; }

        public DateTime register_date { get; set; }

        public string refreshToken { get; set; }

        public DateTime refreshTokenExpiryTime { get; set; }

        [Required]
        public string userId { get; set; }

        public int? coutryId { get; set; } 

        public int nextPageIndex { get; set; }

        public virtual User user { get; set; }
        public virtual List<WatcherTitle> watcherTitles { get; set; }
        public virtual List<WatcherGenre> watcherGenres { get; set; }

        /**<summary>
         * Constructor.
         * </summary>*/
        public Watcher(Watcher source)
        {
            Copy(source);
        }

        /**<summary>
         * Constructor.
         * </summary>*/
        public Watcher(WatcherModel source)
        {
            Copy(source);
        }

        /**<summary>
         * Constructor.
         * </summary>*/
        public Watcher() { }

        public override void Copy(Watcher source)
        {
            this.watcher_name = source.watcher_name;
            this.register_date = source.register_date;
            this.refreshToken = source.refreshToken;
            this.refreshTokenExpiryTime = source.refreshTokenExpiryTime;
            this.userId = source.userId;
            this.coutryId = source.coutryId;
            this.nextPageIndex = source.nextPageIndex;
        }

        public override void Copy(WatcherModel source)
        {
            this.watcher_name = source.watcher_name;
            this.register_date = source.register_date;
            this.refreshToken = source.refreshToken;
            this.refreshTokenExpiryTime = source.refreshTokenExpiryTime;
            this.userId = source.userId;
            this.coutryId = source.countryId;
            this.nextPageIndex = source.nextPageIndex;
        }

        public Expression<Func<Watcher, object>>[] GetIdSelectors()
        {
            return idSelectors;
        }
    }
}
