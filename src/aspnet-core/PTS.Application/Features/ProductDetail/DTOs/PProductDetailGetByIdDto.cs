using PTS.Application.Common.Mappings;
using PTS.Application.Dto;
using PTS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTS.Application.Features.ProductDetail.DTOs
{
	public class PProductDetailGetByIdDto 
	{
		public int Id { get; set; }
		public string Code { get; set; }
		public decimal Price { get; set; }
        public decimal NewPrice { get; set; }
        public decimal OldPrice { get; set; }
		public string ImageMain { get; set; }
		public int IdImage { get; set; }
		public string? Upgrade { get; set; }
		public string? Description { get; set; }
		public int AvailableQuantity { get; set; }
		public string? MaRam { get; set; }
		public string? ThongSoRam { get; set; }
		public string? ThongSoHardDrive { get; set; }
		public string? MaHardDrive { get; set; }
		public string? MaCpu { get; set; }
		public string? TenCpu { get; set; }
		public string? NameColor { get; set; }
		public string? MaColor { get; set; }
		public string? NameProduct { get; set; }
		public string? NameManufacturer { get; set; }
		public string? NameProductType { get; set; }
		public string? MaManHinh { get; set; }
		public string? KichCoManHinh { get; set; }
		public string? TanSoManHinh { get; set; }
		public string? ChatLieuManHinh { get; set; }
		public string? MaCardVGA { get; set; }
		public string? TenCardVGA { get; set; }
		public string? ThongSoCardVGA { get; set; }
		public List<string>? ListImage { get; set; }
		public int PhanTramGiamGia { get; set; }
		public int Status { get; set; }
	}
}
