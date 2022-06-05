
namespace Movie4U.EntitiesModels.Models
{
    /**<summary>
     * A TitleModel without models lists. 
     * Used as parameter for POST and PUT requests.
     * </summary> */
    public class TitleModelParameter
    {

        public string title { get; set; }

        public string img { get; set; }

        public string title_type { get; set; }

        public int netflix_id { get; set; }

        public string synopsis { get; set; }

        public string rating { get; set; }

        public string year { get; set; }

        public string runtime { get; set; }

        public string poster { get; set; }

        public int top250 { get; set; }

        public int top250tv { get; set; }

        public string title_date { get; set; }
    }
}
