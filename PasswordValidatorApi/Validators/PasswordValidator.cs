namespace PasswordValidatorApi.Validators
{
    public class PasswordValidator : IPasswordValidator
    {
        public bool IsValid(string password)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < 9)
                return false;

            const string specialChars = "!@#$%^&*()-+";

            bool hasDigit = false;
            bool hasLower = false;
            bool hasUpper = false;
            bool hasSpecial = false;

            var seenChars = new HashSet<char>();

            foreach (var c in password)
            {
                if (char.IsWhiteSpace(c))
                    return false;

                bool isSpecial = specialChars.Contains(c);

                if (!char.IsLetterOrDigit(c) && !isSpecial)
                    return false;

                if (!seenChars.Add(c))
                    return false;

                if (char.IsDigit(c)) hasDigit = true;
                else if (char.IsLower(c)) hasLower = true;
                else if (char.IsUpper(c)) hasUpper = true;
                else if (isSpecial) hasSpecial = true;
            }

            return hasDigit && hasLower && hasUpper && hasSpecial;
        }
    }
}
