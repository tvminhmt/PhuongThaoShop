namespace PTS.Shared.Helper
{
    public static class ResponseHelper
    {
        public static ResponseResult GetResult(
            bool isSuccessful = true,
            string message = "",
            string errorMessage = "",
            object entity = null,
            dynamic result = null)
        {
            return new ResponseResult
            {
                IsSuccessful = isSuccessful,
                Entity = entity,
                Result = result,                
                Message = message,
                ErrorMessage = errorMessage
            };
        }
    }

    public class ResponseResult
    {
        public bool IsSuccessful { get; set; }
        public object Entity { get; set; }
        public dynamic Result { get; set; }
        public string Message { get; set; }
        public string ErrorMessage { get; set; }
    }
}
