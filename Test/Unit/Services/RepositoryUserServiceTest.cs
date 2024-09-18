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

    // private readonly User _user = new(1, "peter.pan@test.cz", "hash", "Peter", "Pan", new List<Recipe>());

    public RepositoryUserServiceTest()
    {
        _cut = new RepositoryUserService(_mock.Object);
    }

    [Fact]
    public void Test_GetAllUsers()
    {
        // _mock.Setup(mock => mock.GetAll())
        //     .Returns([_user]);
        //
        // // _mock.Verify(mock => mock.GetAll(), Times.Exactly(1));
        //
        // var users = _cut.GetAll();
        //
        // Assert.Single(users);
    }
}