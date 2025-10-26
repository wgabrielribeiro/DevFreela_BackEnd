using DevFreela.Application.Commands.InsertProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.UnitTests.Fakes;
using FluentAssertions;
using Moq;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.UnitTests.Application;

public class InsertProjectHandlerTests
{
    public const int ID = 1;

    [Fact]
    public async Task InputDataAreOk_Insert_Success_NSubstitute()
    {
        // Arrange
        var repository = Substitute.For<IProjectRepository>();
        repository.Add(Arg.Any<Project>()).Returns(Task.FromResult(ID));

        //var command = new InsertProjectCommand
        //{
        //    Title = "Title Test",
        //    Description = "Description Test",
        //    IdCliente = 1,
        //    IdFreelancer = 2,
        //    TotalCost = 1000
        //};

        var command = FakeDataHelper.CreateFakeInsertProjectCommand();

        var handler = new InsertProjectHandler(repository);

        // Act
        var result = await handler.Handle(command, new CancellationToken());

        //Assert
        Assert.True(result.IsSuccess);
        //com fluent assertions, olhe como melhora a legibilidade
        result.IsSuccess.Should().BeTrue();

        Assert.Equal(ID, result.Data);
        //com fluent assertions, olhe como melhora a legibilidade
        result.Data.Should().Be(ID);

        await repository.Received(1).Add(Arg.Any<Project>());
    }

    [Fact]
    public async Task InputDataAreOk_Insert_Success_Moq()
    {
        // Arrange

        //Abordagem numero 1:
        var mock = new Mock<IProjectRepository>();

        mock.Setup(mock => mock.Add(It.IsAny<Project>()))
            .ReturnsAsync(ID);

        //var handler = new InsertProjectHandler(mock.Object);

        //Abordagem numero 2:
        var repository = Mock.Of<IProjectRepository>(r =>
            r.Add(It.IsAny<Project>()) == Task.FromResult(ID)
        );

        //var command = new InsertProjectCommand
        //{
        //    Title = "Title Test",
        //    Description = "Description Test",
        //    IdCliente = 1,
        //    IdFreelancer = 2,
        //    TotalCost = 1000
        //};

        var command = FakeDataHelper.CreateFakeInsertProjectCommand();

        var handler = new InsertProjectHandler(repository);

        // Act
        var result = await handler.Handle(command, new CancellationToken());

        //Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(ID, result.Data);

        //Abordagem de checagem numero 1:
        //mock.Verify(mock => mock.Add(It.IsAny<Project>()), Times.Once);

        //Abordagem de checagem numero 2:
        Mock.Get(repository).Verify(r => r.Add(It.IsAny<Project>()), Times.Once);
    }

}
