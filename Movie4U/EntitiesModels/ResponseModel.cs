
namespace Movie4U.EntitiesModels
{
    public class ResponseModel
    {
        public string status { get; set; }
        public string message { get; set; }


        /**<summary>
         * Constructor.
         * </summary>*/
        public ResponseModel()
        {
        }

        /**<summary>
         * Constructor.
         * </summary>*/
        public ResponseModel(string status, string message)
        {
            this.status = status;
            this.message = message;
        }

    }
}
