using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using NSubstitute;

namespace DevFreela.UnitTests.Application;
public class DeleteProjectHandlerTests
{
    [Fact]
    public async Task ProjectExists_Delete_Success_NSubstitute()
    {
        // Arrange
        var project = new Project("Test Title", "Test Description", 1, 2, 1000);

        var repositoy = Substitute.For<IProjectRepository>();
        repositoy.GetById(Arg.Any<int>()).Returns(Task.FromResult((Project?)project));
        repositoy.Update(Arg.Any<Project>()).Returns(Task.CompletedTask);

        var handler = new DeleteProjectHandler(repositoy);

        var command = new DeleteProjectCommand(1);  

        // Act
        var result = await handler.Handle(command, new CancellationToken());

        // Assert
        Assert.True(result.IsSuccess);

        await repositoy.Received(1).GetById(1);
        await repositoy.Received(1).Update(Arg.Any<Project>());

    }

    [Fact]
    public async Task ProjectDoesNotExists_Delete_Error_NSubstitute()
    {
        // Arrange
        var repositoy = Substitute.For<IProjectRepository>();
        repositoy.GetById(Arg.Any<int>()).Returns(Task.FromResult((Project?)null));

        var handler = new DeleteProjectHandler(repositoy);

        var command = new DeleteProjectCommand(1);

        // Act
        var result = await handler.Handle(command, new CancellationToken());

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(DeleteProjectHandler.PROJECT_NOT_FOUND_MESSAGE, result.Message);

        await repositoy.Received(1).GetById(Arg.Any<int>());
        await repositoy.DidNotReceive().Update(Arg.Any<Project>());
    }
}
