namespace PYPJ.Models.CustomModel
{
    /// <summary>
    ///  Base response in case there is no data 
    /// </summary>
    public class APIResponseModel
    {
        /// <summary>
        /// If the call was successfull
        /// </summary>
        public bool Success => Errors == null || Errors.Count == 0;

        /// <summary>
        /// The errors if not successfull
        /// </summary>
        public List<string>? Errors { get; set; }

        /// <summary>
        /// ADding an error to the error list
        /// </summary>
        /// <param name="error"></param>
        public void AddError(string error)
        {
            if (Errors == null)
                Errors = new List<string>();

            Errors.Add(error);
        }
    }

    /// <summary>
    /// Base response with data
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class APIResponseModel<T> : APIResponseModel
        where T : new()
    {
        /// <summary>
        /// The response data
        /// </summary>
        public T? Response { get; set; }

    }
    public class APIResponseJsonString
    {
        public string ResponseJsonString { get; set; }
    }
    public class APIRequestModel
    {
        public string RequestJsonString { get; set; }
        public string HandShakeToken { get; set; }
    }

  
}
