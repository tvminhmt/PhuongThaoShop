using MTS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTS.Domain.Entities.Identity
{
    public class MediaType : BaseAuditableEntity
    {
        public byte Id { get; set; }
        public string MediaTypeName { get; set; }
        public string MediaTypeDesc { get; set; }
        public string PrefixPath { get; set; }
        public string FileType { get; set; }
        public string FileExtension { get; set; }
        public byte DisplayOrder { get; set; }

    }
}
