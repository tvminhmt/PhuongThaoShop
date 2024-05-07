namespace PTS.Base.Application.Dto
{
    public class ComboBoxDto
    {
        public object Value { get; set; }
        public string DisplayText { get; set; }
        public string HideText { get; set; }
        public bool IsActive { get; set; } = true;
        public object Data { get; set; }
    }
    public class ComboBoxTDto<T> where T : new()
    {
        public object Value { get; set; }
        public string DisplayText { get; set; }
        public string HideText { get; set; }
        public T Data { get; set; }
        public bool IsActive { get; set; } = true;
        protected T GetObject()
        {
            return new T();
        }
    }
}
