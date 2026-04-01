namespace PasswordValidatorApi.Services
{
    public interface IPasswordService
    {   
        bool Validate(string password);
    }
}
