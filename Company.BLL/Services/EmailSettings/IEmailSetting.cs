using Company.DAL.Models.Shared;

namespace Company.BLL.Services.EmailSettings
{
    public interface IEmailSetting
    {
        void sendEmail(Email email);
    }
}
