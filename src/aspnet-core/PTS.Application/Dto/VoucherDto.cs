
using PTS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTS.Application.Dto
{
    public class VoucherDto : BaseEntity
    {
        [MaxLength(50)]
        public string? MaVoucher { get; set; }
        [MaxLength(150)]
        public string? TenVoucher { get; set; }
        public DateTime? StarDay { get; set; }
        public DateTime? EndDay { get; set; }
        public decimal GiaTri { get; set; }
        public int SoLuong { get; set; }
    }
}
