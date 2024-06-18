using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTS.Application.Dto
{
    public class UserSessionDto
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? AccessToken { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? Name { get;set; }
        public bool? IsAdmin {  get; set; }
    }
}
