using Moq;
using PasswordValidatorApi.Services;
using PasswordValidatorApi.Validators;

namespace PasswordValidatorApi.Tests.Unit.Services
{
    public class PasswordServiceTests
    {
        [Fact]
        public void Should_Return_True_When_Validator_Returns_True()
        {
            var mockValidator = new Mock<IPasswordValidator>();
            mockValidator.Setup(v => v.IsValid(It.IsAny<string>())).Returns(true);

            var service = new PasswordService(mockValidator.Object);

            var result = service.Validate("anything");

            Assert.True(result);
        }

        [Fact]
        public void Should_Return_False_When_Validator_Returns_False()
        {
            var mockValidator = new Mock<IPasswordValidator>();
            mockValidator.Setup(v => v.IsValid(It.IsAny<string>())).Returns(false);

            var service = new PasswordService(mockValidator.Object);

            var result = service.Validate("anything");

            Assert.False(result);
        }

        [Fact]
        public void Should_Call_Validator_When_Validate_Is_Called()
        {
            var mockValidator = new Mock<IPasswordValidator>();

            var service = new PasswordService(mockValidator.Object);

            service.Validate("password");

            mockValidator.Verify(v => v.IsValid("password"), Times.Once);
        }

        [Fact]
        public void Should_Pass_Correct_Password_To_Validator()
        {
            var mockValidator = new Mock<IPasswordValidator>();

            var service = new PasswordService(mockValidator.Object);

            service.Validate("MyPassword123!");

            mockValidator.Verify(v => v.IsValid("MyPassword123!"), Times.Once);
        }
    }
}
