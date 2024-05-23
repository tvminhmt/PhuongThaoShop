using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTS.Core.Dto
{
    public class ProductDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(150)]
        public string? Name { get; set; }
        public int? ManufacturerEntityId { get; set; }
        public int? ProductTypeEntityId { get; set; }
    }
}
