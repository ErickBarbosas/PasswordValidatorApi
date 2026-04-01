using PasswordValidatorApi.Validators;

namespace PasswordValidatorApi.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly IPasswordValidator _validator;

        public PasswordService(IPasswordValidator validator)
        {
            _validator = validator;
        }

        public bool Validate(string password)
        {
            return _validator.IsValid(password);
        }
    }
}
    