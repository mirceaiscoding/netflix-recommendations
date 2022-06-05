using System;
namespace Movie4U.EntitiesModels.Models.uNoGS
{
    public class TitleResponseListModel
    {
        public TitleResponseInfoModel Object { get; set; }

        public TitleModel[] results { get; set; }

    }
}
