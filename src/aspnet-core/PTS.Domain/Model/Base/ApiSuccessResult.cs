
namespace PTS.Domain.Model.Base
{
    public class ApiSuccessResult<T> : ApiResult<T>
    {
        public ApiSuccessResult()
        {
            IsSuccessed = true;
            Message = "Successful";
        }

        public ApiSuccessResult(T resultObject)
        {
            IsSuccessed = true;
            Message = "Successful";
            ResultObj = resultObject;
        }
    }
}
