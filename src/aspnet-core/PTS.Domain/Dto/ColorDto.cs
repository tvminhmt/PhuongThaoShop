using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTS.Core.Dto
{
    public class ColorDto
    {
        public int Id { get; set; }
        public string? Ma { get; set; }
        [MaxLength(100)]
        public string? Name { get; set; }
    }
}
