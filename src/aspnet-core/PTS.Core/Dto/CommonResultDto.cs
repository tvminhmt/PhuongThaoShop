namespace PTS.Base.Application.Dto
{
    public class CommResultErrorDto
    {
        public bool IsSuccessful { get; set; } = false;
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
    public class CommonResultDto<T> : CommResultErrorDto
    {
        public T DataResult { get; set; }

        public CommonResultDto(T dataSuccess)
        {
            DataResult = dataSuccess;
            IsSuccessful = true;
            ErrorCode = string.Empty;
            ErrorMessage = string.Empty;
        }

        public CommonResultDto(string errorMessage)
        {
            IsSuccessful = false;
            ErrorMessage = errorMessage;
        }
        public CommonResultDto(string errorCode, string errorMessage)
        {
            IsSuccessful = false;
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        public CommonResultDto():base()
        {

        }
        public void SetDataSuccess(T data)
        {
            DataResult = data;
            IsSuccessful = true;
            ErrorCode = string.Empty;
            ErrorMessage = string.Empty;
        }
    }

    public static class CommResultHelper
    {
        public static CommResultErrorDto ErrorResult(string error)
        {
            return new CommResultErrorDto()
            {
                IsSuccessful = false,
                ErrorMessage = error
            };
        }
    }
}
