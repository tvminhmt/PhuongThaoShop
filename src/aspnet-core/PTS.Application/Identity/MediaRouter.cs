using MTS.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTS.Domain.Entities.Identity
{
    public class MediaRouter : BaseAuditableEntity
    {
        public int Id { get; set; }
        public string RouteName { get; set; }
        public string RouteDesc { get; set; }
        public string MediaDomain { get; set; }
        public string RoutePrefix { get; set; }
        public string RouteType { get; set; }
        public string ProxyDomain { get; set; }
        public string AwsEndPoint { get; set; }
        public string AwsKey { get; set; }
        public string AwsSecret { get; set; }
        public string AwsRegion { get; set; }
        public string AwsBucket { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CrDateTime { get; set; }

    }
}
