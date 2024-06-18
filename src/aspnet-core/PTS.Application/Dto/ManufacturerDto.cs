using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTS.Application.Dto
{
   public class ManufacturerDto
    {
        public int Id { get; set; }
        [Required] 
        public string Name { get; set; }
    }
}
