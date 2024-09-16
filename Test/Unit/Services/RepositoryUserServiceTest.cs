using Data.Repositories;

using LearningASP.DTO;
using LearningASP.Model;
using LearningASP.Services;

using Moq;

namespace LearningASPTest;

public class RepositoryUserServiceTest
{
    // todo: mock repository

    private readonly Mock<IUserRepository> _mock = new();
    private readonly RepositoryUserService _cut;

    private readonly User _user = new(1, "Peter", "Pan");

    public RepositoryUserServiceTest()
    {
        _cut = new RepositoryUserService(_mock.Object);
    }

    [Fact]
    public void Test_GetAllUsers()
    {
        _mock.Setup(mock => mock.GetAll())
            .Returns([_user]);

        // _mock.Verify(mock => mock.GetAll(), Times.Exactly(1));

        var users = _cut.GetAll();

        Assert.Single(users);
    }

    [Fact]
    public void Test_AddUser()
    {
        _mock.Setup(mock => mock.Create(It.IsAny<User>()))
            .Returns(_user);

        // _mock.Verify(mock => mock.Create(It.IsAny<User>()), Times.Once);
        // _mock.VerifyNoOtherCalls();

        var createUserDto = new CreateUserDto()
        {
            FirstName = "Pepik",
            LastName = "Smith"
        };

        var newUser = _cut.Add(createUserDto);

        Assert.Equivalent(newUser, _user);
    }
}