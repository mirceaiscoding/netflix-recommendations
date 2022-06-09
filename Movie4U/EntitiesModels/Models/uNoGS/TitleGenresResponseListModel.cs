using System;
namespace Movie4U.EntitiesModels.Models.uNoGS
{
    public class TitleGenresResponseListModel
    {
        public TitleResponseInfoModel Object { get; set; }

        public GenreModel[] results { get; set; }
    }
}
