using Movie4U.EntitiesModels.Entities;
using System;
using System.Linq.Expressions;

namespace Movie4U.EntitiesModels.Models
{
    public class WatcherModel: EntitiesModelsBase<Watcher,WatcherModel>, IModel<WatcherModel>
    {
        public static Expression<Func<WatcherModel, object>>[] idSelectors;

        static WatcherModel()
        {
            idSelectors = new Expression<Func<WatcherModel, object>>[1];
            idSelectors[0] = model => model.watcher_name;
        }


        public string watcher_name { get; set; }

        public DateTime register_date { get; set; }

        public string refreshToken { get; set; }

        public DateTime refreshTokenExpiryTime { get; set; }

        public string userId { get; set; }

        public int? countryId { get; set; }

        public int nextPageIndex { get; set; }


        /**<summary>
        * Constructor.
        * </summary>*/
        public WatcherModel(WatcherModel source)
        {
            Copy(source);
        }

        /**<summary>
         * Constructor.
         * </summary>*/
        public WatcherModel(Watcher source)
        {
            Copy(source);
        }
        
        /**<summary>
        * Constructor.
        * </summary>*/
        public WatcherModel() { }

        public override void Copy(Watcher source)
        {
            this.watcher_name = source.watcher_name;
            this.register_date = source.register_date;
            this.refreshToken = source.refreshToken;
            this.refreshTokenExpiryTime = source.refreshTokenExpiryTime;
            this.userId = source.userId;
            this.countryId = source.coutryId;
            this.nextPageIndex = source.nextPageIndex;
        }

        public override void Copy(WatcherModel source)
        {
            this.watcher_name = source.watcher_name;
            this.register_date = source.register_date;
            this.refreshToken = source.refreshToken;
            this.refreshTokenExpiryTime = source.refreshTokenExpiryTime;
            this.userId = source.userId;
            this.countryId = source.countryId;
            this.nextPageIndex = source.nextPageIndex;
        }

        public Expression<Func<WatcherModel, object>>[] GetIdSelectors()
        {
            return idSelectors;
        }
    }
}
