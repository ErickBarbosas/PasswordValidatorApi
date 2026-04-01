using PasswordValidatorApi.Validators;

namespace PasswordValidatorApi.Tests.Unit.Validators
{
    public class PasswordValidatorTests
    {
        private readonly PasswordValidator _validator;

        public PasswordValidatorTests()
        {
            _validator = new PasswordValidator();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData("aa")]
        [InlineData("ab")]
        [InlineData("AAAbbbCc")]
        [InlineData("AbTp9!foo")]
        [InlineData("AbTp9!foA")]
        [InlineData("AbTp9!fok?")]
        [InlineData("AbTp9!fok?.")]
        [InlineData("AbTp9 fok")]
        public void Should_Return_False_For_Invalid_Passwords(string password)
        {
            var result = _validator.IsValid(password);
            Assert.False(result);
        }

        [Theory]
        [InlineData("AbTp9!fok")]
        [InlineData("A1b2C3d!e")]
        [InlineData("9aA!bcdefg")]
        [InlineData("9aA!bcdefgzxv")]
        public void Should_Return_True_For_Valid_Passwords(string password)
        {
            var result = _validator.IsValid(password);
            Assert.True(result);
        }
    }
}
