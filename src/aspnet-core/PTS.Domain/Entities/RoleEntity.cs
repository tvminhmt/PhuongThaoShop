
using Microsoft.AspNetCore.Identity;
using PTS.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTS.Domain.Entities
{
    [Table("Role")]
    public class RoleEntity : IdentityRole<int>
	{
		public string Description { get; set; }
	}
}
