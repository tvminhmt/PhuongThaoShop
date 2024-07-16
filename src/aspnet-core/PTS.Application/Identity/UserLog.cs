using MTS.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MTS.Domain.Entities.Identity
{
    public class UserLog : BaseAuditableEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FromIP { get; set; }
        public string UserAction { get; set; }
        public string ActionStatus { get; set; }
        public DateTime CrDateTime { get; set; }
    }
}
