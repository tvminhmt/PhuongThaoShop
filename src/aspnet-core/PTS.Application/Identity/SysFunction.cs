using MTS.Domain.Common;

namespace MTS.Domain.Entities.Identity
{
    public class SysFunction : BaseAuditableEntity
    {
        public int Id { get; set; }
        public string FunctionName { get; set; }
        public string FunctionDesc { get; set; }
        public int ParentItemId { get; set; }
        public string Url { get; set; }
        public string IconPath { get; set; }
        public int DisplayOrder { get; set; }
        public bool? IsShow { get; set; }
        public bool? IsEnable { get; set; }
        public bool? HasChild { get; set; }
		public string CssMenuActive { get; set; }
        public string CssMenuOpen { get; set; }
        public int TreeOrder { get; set; }
        public byte TreeLevel { get; set; }
    }
}
