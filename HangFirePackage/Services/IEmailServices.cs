using HangFirePackage.DTOs;

namespace HangFirePackage.Services;

public interface IEmailServices
{
    void SendEmail(EmailDto dto);
}
