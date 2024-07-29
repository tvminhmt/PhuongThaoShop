using AutoMapper;
using AutoMapper.Internal;

namespace PTS.Application.Common.Mappings
{
	public interface IMapFrom<T>
	{
		void Mapping(Profile profile)
		{
			profile.Internal().MethodMappingEnabled = false;
			profile.CreateMap(typeof(T), GetType(), MemberList.None);
			profile.CreateMap(GetType(), typeof(T), MemberList.None);
		}
	}
}
