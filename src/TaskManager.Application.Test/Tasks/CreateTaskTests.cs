using FluentAssertions;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Application.Tasks.Command;
using TaskManager.Domain.Enums;

namespace TaskManager.Application.Test.Tasks
{
    public class CreateTaskTests : CommandTestBase
    {
        [Test]
        public async Task Should_Handle_AddTask()
        {
            // Arrange
            var command = new CreateTaskCommand{
                Title = "New test task",
                Description = "The task description",
                Status = TaskStatus_Enum.Unassigned
            };

            // Act
            var handler = new CreateTaskCommand.CreateTaskCommandHandler(Context);
            var result = await handler.Handle(command, CancellationToken.None).ConfigureAwait(false);

            // Assert
            result.Should().BeTrue();
        }


        [TearDown]
        public void TearDown()
        {
            Dispose();
        }
    }
}
