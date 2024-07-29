using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTS.Application.Dto
{
   public class ScreenDto
    {
        public int Id { get; set; }
        public string? Ma { get; set; }
        public string? KichCo { get; set; }
        public string? TanSo { get; set; }
        public string? ChatLieu { get; set; }
    }
}
