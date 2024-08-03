
namespace PTS.Domain.Model.Base
{
    public class ApiResult<T>
    {
        public bool IsSuccessed { set; get; }
        public string Message { set; get; }
        public T ResultObj { set; get; }
    }
}
