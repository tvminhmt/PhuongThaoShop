using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTS.Application.Dto
{
   public class HardDriveDto
    {
        public int Id { get; set; }
        public string? Ma { get; set; }
        public string? ThongSo { get; set; }
    }
}
