using MTS.Domain.Common;

namespace MTS.Domain.Entities.Identity
{
	public class SysLog : BaseAuditableEntity
	{
		public int Id { get; set; }
		public string Message { get; set; }
		public string Level { get; set; }
		public DateTime? TimeStamp { get; set; }
		public string Exception { get; set; }
		public string SourceContext { get; set; }
		public string Application { get; set; }
	}
}
