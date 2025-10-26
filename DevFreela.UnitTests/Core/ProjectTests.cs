using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using DevFreela.UnitTests.Fakes;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.UnitTests.Core;

public class ProjectTests
{
    [Fact]
    public void ProjectIsCreated_Start_success()
    {
        // Arrange
        //var project = new Project(
        //    "Projeto A",
        //    "Description project",
        //    1,
        //    2,
        //    1000
        //);

        var project = FakeDataHelper.CreateFakeProject();

        // Act
        project.Start();

        // Assert
        Assert.Equal(ProjectStatusEnum.InProgress, project.Status);

        project.Status.Should().Be(ProjectStatusEnum.InProgress);

        Assert.NotNull(project.StartedAt);

        //com fluent assertions, olhe como melhora a legibilidade
        project.StartedAt.Should().NotBeNull();

        Assert.True(project.Status == ProjectStatusEnum.InProgress);


        Assert.False(project.StartedAt is null);
    }

    [Fact]
    public void ProjectIsInInvalidState_Start_ThrowsInvalidOperationException()
    {
        // Arrange
        //var project = new Project(
        //    "Projeto A",
        //    "Description project",
        //    1,
        //    2,
        //    1000
        //);
        var project = FakeDataHelper.CreateFakeProject();
        project.Start();

        // Act & Assert
        Action? start = project.Start;
        var exception = Assert.Throws<InvalidOperationException>(start);

        Assert.Equal(Project.INVALID_STATE_MESSAGE, exception.Message);

        exception.Message.Should().Be(Project.INVALID_STATE_MESSAGE);

        start.Should().Throw<InvalidOperationException>().WithMessage(Project.INVALID_STATE_MESSAGE);

    }
}
