using AutoMapper;
using MTS.Application.Common.Mappings;
using MTS.Application.Interfaces.Repositories;
using MTS.Application.Interfaces.Repositories.Identity;
using MTS.Domain.Entities.Identity;
using IC.Shared;
using MediatR;

namespace MTS.Application.Features.IdentityFeatures.UserLogs.Commands
{
    public record UserLogCreateCommand : IRequest<Result<int>>, IMapFrom<UserLog>
	{
		public string UserName { get; set; }
		public string FromIP { get; set; }
		public string UserAction { get; set; }
		public string ActionStatus { get; set; }
	}

	internal class UserLogCreateCommandHandler : IRequestHandler<UserLogCreateCommand, Result<int>>
	{
		private readonly IIdentityUnitOfWork _unitOfWork;
		private readonly IUserRepo _userRepository;
		private readonly IMapper _mapper;

		public UserLogCreateCommandHandler(IIdentityUnitOfWork unitOfWork, IUserRepo userRepository, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_userRepository = userRepository;
			_mapper = mapper;
		}

		public async Task<Result<int>> Handle(UserLogCreateCommand command, CancellationToken cancellationToken)
		{
			var userLog = _mapper.Map<UserLog>(command);

			await _unitOfWork.Repository<UserLog>().AddAsync(userLog);

			await _unitOfWork.Save(cancellationToken);

			//Cập nhật thời gian bảng Users
			if (command.UserAction.Equals("Đăng nhập") && command.ActionStatus.Equals("Thành công"))
			{
				await _userRepository.UpdateLastTimeLogin(command.UserName);
			}
			else if (command.UserAction.Equals("Đổi mật khẩu") && command.ActionStatus.Equals("Thành công"))
			{
				await _userRepository.UpdateLastTimeLogin(command.UserName);
			}

			return await Result<int>.SuccessAsync(userLog.Id, "Created.");
		}
	}
}
