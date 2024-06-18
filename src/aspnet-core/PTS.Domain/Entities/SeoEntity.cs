using PTS.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace PTS.Domain.Entities
{
    public class SeoEntity : BaseAuditableEntity
	{
        [Key]
        public int SeoId { get; set; }
        public string SeoName { get; set; }
        public string Url { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDesc { get; set; }
        public string MetaKeyword { get; set; }
        public string CanonicalTag { get; set; }
        public string H1Tag { get; set; }
        public string SeoFooter { get; set; }
        public int? CrUserId { get; set; }
        public DateTime CrDateTime { get; set; }
    }
}
