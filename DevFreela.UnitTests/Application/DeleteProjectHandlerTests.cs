using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Moq;
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

    [Fact]
    public async Task ProjectExists_Delete_Success_Moq()
    {
        // Arrange
        var project = new Project("Test Title", "Test Description", 1, 2, 1000);

        var repositoy = Mock.Of<IProjectRepository>(p => p.GetById(It.IsAny<int>()) == Task.FromResult(project) &&
        p.Update(It.IsAny<Project>()) == Task.CompletedTask);


        var handler = new DeleteProjectHandler(repositoy);

        var command = new DeleteProjectCommand(1);

        // Act
        var result = await handler.Handle(command, new CancellationToken());

        // Assert
        Assert.True(result.IsSuccess);

        Mock.Get(repositoy).Verify(r => r.GetById(1), Times.Once);
        Mock.Get(repositoy).Verify(r => r.Update(It.IsAny<Project>()), Times.Once);
    }

    [Fact]
    public async Task ProjectDoesNotExists_Delete_Error_Moq()
    {
        // Arrange
        var repositoy = Mock.Of<IProjectRepository>(r =>
                r.GetById(It.IsAny<int>()) == Task.FromResult((Project?)null));

        var handler = new DeleteProjectHandler(repositoy);

        var command = new DeleteProjectCommand(1);

        // Act
        var result = await handler.Handle(command, new CancellationToken());

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(DeleteProjectHandler.PROJECT_NOT_FOUND_MESSAGE, result.Message);

        Mock.Get(repositoy).Verify(r => r.GetById(1), Times.Once);
        Mock.Get(repositoy).Verify(r => r.Update(It.IsAny<Project>()), Times.Never);
    }

}
