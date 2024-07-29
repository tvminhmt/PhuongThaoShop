using PTS.Application.Dto;

namespace PTS.Core.Services
{
    public interface ISendMailService
    {
        Task SendMail(EmailDto mailContent);
    }
}
