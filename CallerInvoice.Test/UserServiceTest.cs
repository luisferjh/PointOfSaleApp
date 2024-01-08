using Application.DTOs;
using Application.Interfaces;
using Moq;

namespace CallerInvoice.Test
{
    public class UserServiceTest
    {                

        [Fact]
        public void Test_GetUserByIdentification_ExistingId_ShouldReturnValidUser()
        {
            // Arrange
            string identification = "ID123";
            var mock = new Mock<IUserService>();
            mock.Setup(m => m.GetUserByIdentification(identification).Result).Returns(new UserDTO
            {
                Phone = "123",
                Identification = identification,
                Date = DateTime.Now,
                Email = "alice.smith@example.com",
                Name = "jimenez",
                LastName = "luis",
            });

            //Act
            var user =  mock.Object.GetUserByIdentification("ID123").Result;

            //Assert
            Assert.NotNull(user);
        }

        [Fact]
        public void Test_GetUserByIdentification_NotExistingId_ShouldReturnNull()
        {
            // Arrange
            string identification = "ID123";
            var mock = new Mock<IUserService>();
            mock.Setup(m => m.GetUserByIdentification(identification)).ReturnsAsync(() => null);

            //Act
            var user = mock.Object.GetUserByIdentification("ID123").Result;

            //Assert
            Assert.Null(user);
        }
    }
}