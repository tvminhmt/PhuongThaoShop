using AutoMapper;
using MTS.Application.Common.Mappings;
using MTS.Domain.Entities.Identity;
using IC.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using MTS.Application.Interfaces;
using MTS.Application.DTOs;
using IC.Shared.Helpers;
using MTS.Application.Interfaces.Repositories.Identity;

namespace MTS.Application.Features.IdentityFeatures.Users.Commands
{
    public record UserCreateCommand : BaseCreateCommand, IRequest<Result<int>>, IMapFrom<User>
    {
        [DisplayName("Tên truy cập")]
        public string UserName { get; set; }

        public string Email { get; set; }

        [DisplayName("Mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Mật khẩu xác nhận")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [DisplayName("Số điện thoại")]
        public string PhoneNumber { get; set; }

        [DisplayName("Xác thực 2 bước")]
        public bool TwoFactorEnabled { get; set; }

        [DisplayName("Họ và tên")]
        public string FullName { get; set; }

        [DisplayName("Địa chỉ")]
        public string Address { get; set; }

        [DisplayName("Ngày sinh")]
        public string BirthDay { get; set; }
        public int? OrganId { get; set; }
        public int? DefaultActionId { get; set; }

        [DisplayName("Ghi chú")]
        public string Notes { get; set; }
        public string AvatarPath { get; set; }

        [DisplayName("Kích hoạt")]
        public bool IsEnabled { get; set; }

        [DisplayName("Gắn với tài khoản News")]
        public int? MapNewsUserId { get; set; }

        [DisplayName("Gắn với tài khoản PodCast")]
        public string MapPodCastUserId { get; set; }

        [DisplayName("Gắn với tài khoản VohData")]
        public string MapVohDataUserId { get; set; }

        public List<int> SelectedRoles { get; set; }
    }

    internal class UserCreateCommandHandler : IRequestHandler<UserCreateCommand, Result<int>>
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IRoleRepo _roleRepo;
		private readonly IMediator _mediator;
		private readonly ICurrentUserService _currentUserService;

		public UserCreateCommandHandler(IMapper mapper, IRoleRepo roleRepo, UserManager<User> userManager, 
            IMediator mediator, ICurrentUserService currentUserService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleRepo = roleRepo;
            _mediator = mediator;
            _currentUserService = currentUserService;
        }

        public async Task<Result<int>> Handle(UserCreateCommand command, CancellationToken cancellationToken)
        {
            User entity = _mapper.Map<User>(command);

            entity.NormalizedEmail = entity.Email;
            entity.NormalizedUserName = entity.UserName;
            entity.SecurityStamp = entity.UserName;
            entity.ConcurrencyStamp = entity.UserName;

            PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
            entity.PasswordHash = passwordHasher.HashPassword(entity, command.Password);

			if (!string.IsNullOrWhiteSpace(command.BirthDay))
			{
				DateTime birthDayDb = command.BirthDay.ToDateTime();

				if (birthDayDb != DateTime.MinValue)
				{
					entity.BirthDay = birthDayDb;
				}
			}

			IdentityResult identityResult = await _userManager.CreateAsync(entity);

            if (!identityResult.Succeeded)
            {
                return await Result<int>.FailureAsync(string.Join(", ", identityResult.Errors.Select(x => x.Description)));
            }

            //Add role
            var roles = await _roleRepo.GetAllAsync();

            if (command.SelectedRoles != null)
            {
                foreach (var roleId in command.SelectedRoles)
                {
                    await _userManager.AddToRoleAsync(entity, roles.Find(x => x.Id == roleId).Name);
                }
            }

            //publish event

			return await Result<int>.SuccessAsync(entity.Id, "Thêm dữ liệu thành công.");
        }
    }
}
