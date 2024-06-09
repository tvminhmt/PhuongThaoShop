using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTS.Core.Dto
{
    public record LoginResponse(bool IsSuccess, string? Username, string? FullName, string? PhoneNumber, string? Address, string? Email, string? RoleName, bool IsAdmin, string? AccessToken)
    {
    }
}
