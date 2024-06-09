using PTS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTS.Core.Dto
{
    public class SerialDto
    {
        public int Id { get; set; }
        public string? SerialNumber { get; set; }
        public int? ProductDetailEntityId { get; set; }
        public int? BillDetailEntityId { get; set; }
        public int? Status {  get; set; }
    }
}
