using MediatR;
using PTS.Application.Dto;
using PTS.Domain.Entities;
using PTS.Application.Interfaces.Repositories;
using BCrypt.Net;
using Microsoft.AspNetCore.Identity;

namespace PTS.Application.Features.Account.Commands
{
    public class CreateOrUpdateAccountQuery : IRequest<ServiceResponse>
    {
        public UserDto? UserDto { get; set; }
    }
    public class CreateOrUpdateAccountHandler : IRequestHandler<CreateOrUpdateAccountQuery, ServiceResponse>
    {
        private readonly IUserRepository _repository;
        public CreateOrUpdateAccountHandler(IUserRepository repository)
        {
            _repository = repository;
        }
        public async Task<ServiceResponse> Handle(CreateOrUpdateAccountQuery request, CancellationToken cancellationToken)
        {
            UserEntity user = new UserEntity();
            user.Id = request.UserDto.Id;
            user.UserName = request.UserDto.UserName;
            user.PhoneNumber = request.UserDto.PhoneNumber;
            user.FullName = request.UserDto.FullName;
            user.Email = request.UserDto.Email;
            PasswordHasher<UserEntity> passwordHasher = new PasswordHasher<UserEntity>();
            user.PasswordHash = passwordHasher.HashPassword(user, request.UserDto.Password);
           // user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.UserDto.Password);
            user.CrDateTime = DateTime.Now;
            //user.Status = 1;
            if (request.UserDto.Id > 0)
            {
                if (await _repository.Update(user))
                {
                    return new ServiceResponse(true, "Cập nhật thành công");

                }
                return new ServiceResponse(false, "Cập nhật hất bại");
            }
            else
            {
               // user.RoleEntityId = 2;
                if (await _repository.Create(user))
                    return new ServiceResponse(true, "Thêm thành công");
                return new ServiceResponse(false, "Thêm thất bại");
            }


        }
    }
}
