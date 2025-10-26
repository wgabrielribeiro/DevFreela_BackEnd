using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
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
        var project = new Project(
            "Projeto A",
            "Description project",
            1,
            2,
            1000
        );

        // Act
        project.Start();

        // Assert
        Assert.Equal(ProjectStatusEnum.InProgress, project.Status);
        Assert.NotNull(project.StartedAt);

        Assert.True(project.Status == ProjectStatusEnum.InProgress);
        Assert.False(project.StartedAt is null);
    }

    [Fact]
    public void ProjectIsInInvalidState_Start_ThrowsInvalidOperationException()
    {
        // Arrange
        var project = new Project(
            "Projeto A",
            "Description project",
            1,
            2,
            1000
        );
        project.Start();

        // Act & Assert
        Action? start = project.Start;
        var exception = Assert.Throws<ArgumentException>(start);

        Assert.Equal(Project.INVALID_STATE_MESSAGE, exception.Message);
    }
}
