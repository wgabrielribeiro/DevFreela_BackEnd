using DevFreela.Application.Commands.InsertProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
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

        var command = new InsertProjectCommand
        {
            Title = "Title Test",
            Description = "Description Test",
            IdCliente = 1,
            IdFreelancer = 2,
            TotalCost = 1000
        };

        var handler = new InsertProjectHandler(repository);

        // Act
        var result = await handler.Handle(command, new CancellationToken());

        //Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(ID, result.Data);
        await repository.Received(1).Add(Arg.Any<Project>());


    }
}
