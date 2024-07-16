namespace MTS.Application.Features.IdentityFeatures.Users.Queries
{
	public class UserGetAllDto : UserDto
    {
        public string DisplayName => !string.IsNullOrWhiteSpace(FullName) ? FullName : UserName;
    }
}
