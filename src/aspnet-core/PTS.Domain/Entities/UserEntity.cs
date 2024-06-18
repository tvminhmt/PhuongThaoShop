using Microsoft.AspNetCore.Identity;
using PTS.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace PTS.Domain.Entities
{
    [Table("User")]
    public class UserEntity : IdentityUser<int>
	{
		public string? FullName { get; set; }
		public string? Address { get; set; }
		public DateTime? BirthDay { get; set; }
		public int? DefaultActionId { get; set; }
		public string? Notes { get; set; }
		public string? AvatarPath { get; set; }
		public bool? IsEnabled { get; set; }
		public DateTime? LastTimeChangePass { get; set; }
		public DateTime? LastTimeLogin { get; set; }
		public DateTime? CrDateTime { get; set; }
		public int? Status {  get; set; }
		public virtual CartEntity? Cart { get; set; }
        public virtual RoleEntity? RoleEntities { get; set; }
    }
}
