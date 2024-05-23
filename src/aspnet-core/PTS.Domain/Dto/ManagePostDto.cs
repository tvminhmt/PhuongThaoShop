using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTS.Core.Dto
{
    public class ManagePostDto
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? LinkImage { get; set; }
        public string? Description { get; set; }
        public int? Status { get; set; }
    }
}
