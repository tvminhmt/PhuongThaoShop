using PTS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTS.Core.Dto
{
    public class ImageDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string? Ma { get; set; }
        [MaxLength(150)]
        public string? LinkImage { get; set; }
        public int Status { get; set; }
        public int ProductDetailId { get; set; }
        public virtual ProductDetailEntity? ProductDetail { get; set; }

    }
}
