namespace PasswordValidatorApi.Validators
{
    public class PasswordValidator : IPasswordValidator
    {
        public bool IsValid(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            const string specialChars = "!@#$%^&*()-+";

            return password.Length >= 9
                && password.All(c => !char.IsWhiteSpace(c))
                && password.Any(char.IsDigit)
                && password.Any(char.IsLower)
                && password.Any(char.IsUpper)
                && password.Any(c => specialChars.Contains(c))
                && password.Distinct().Count() == password.Length
                && password.All(c => char.IsLetterOrDigit(c) || specialChars.Contains(c));
        }
    }
}
