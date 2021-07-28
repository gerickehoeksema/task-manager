using FluentAssertions;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Application.Tasks.Command;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.Test.Tasks
{
    public class DeleteTaskTest : CommandTestBase
    {
        [Test]
        public async Task Should_Handle_DeleteTask()
        {
            // Arrange
            await SeedAsync().ConfigureAwait(false);
            var command = new DeleteTaskCommand { TaskId = 4 };

            // Act
            var handler = new DeleteTaskCommand.DeleteTaskCommandhandler(Context);
            var result = await handler.Handle(command, CancellationToken.None).ConfigureAwait(false);

            // Assert
            result.Should().BeTrue();
        }

        internal async Task SeedAsync()
        {
            await Context.Tasks
                .AddRangeAsync(
                    new Domain.Enitities.Task { CreatedBy = 1, Title = "Task 1", Description = "Task 1 description", Status = TaskStatus_Enum.Unassigned, IsActive = true },
                    new Domain.Enitities.Task { CreatedBy = 1, Title = "Task 2", Description = "Task 2 description", Status = TaskStatus_Enum.Unassigned, IsActive = true },
                    new Domain.Enitities.Task { CreatedBy = 1, Title = "Task 3", Description = "Task 3 description", Status = TaskStatus_Enum.Unassigned, IsActive = true },
                    new Domain.Enitities.Task { CreatedBy = 1, Title = "Task 4", Description = "Task 4 description", Status = TaskStatus_Enum.Unassigned, IsActive = true },
                    new Domain.Enitities.Task { CreatedBy = 1, Title = "Task 5", Description = "Task 5 description", Status = TaskStatus_Enum.Unassigned, IsActive = true }
                )
                .ConfigureAwait(false);

            await Context.SaveChangesAsync(CancellationToken.None).ConfigureAwait(false);
        }
    }
}
